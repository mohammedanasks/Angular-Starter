using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Masters;
using AutoMapper;

namespace API.Entities.Masters
{
    public class EmployeeMappingProfile:Profile
    {
        public EmployeeMappingProfile(){
            CreateMap<Employee,EmployeeDto>().
            ForMember(x=>x.DepartmentName,option=>option.MapFrom(src=>src.Department.Name)).
            ForMember(x=>x.DesignationName,option=>option.MapFrom(src=>src.Designation.Name));
            CreateMap<EmployeeDto,Employee>();
        }
    }
}