using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.LeaveManagement;
using API.Entities.LeaveManagement;
using AutoMapper;

namespace API.Entities.LeaveManagement
{
    public class LeaveMappingProfile :Profile
    {
        public LeaveMappingProfile(){

            CreateMap<Leave,LeaveDto>().
            ForMember(x=>x.EmployeeName,option=>option.MapFrom(src=>src.Employee.LastName)).
             ForMember(x=>x.DepartmentName,option=>option.MapFrom(src=>src.Employee.Department.Name)).
             ForMember(x=>x.LeaveTypeName,options=>options.MapFrom(src=>src.LeaveType.LeaveTypeName));
            CreateMap<LeaveDto,Leave>();
        }
    }
}