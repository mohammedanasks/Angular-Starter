using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.SalaryManagement;
using API.Entities.SalaryManagement;
using AutoMapper;

namespace API.Entities.Masters
{
    public class SalaryPaymentMappingProfile :Profile
    {
        public SalaryPaymentMappingProfile(){

            CreateMap<SalaryPayment,SalaryPaymentDto>().
            ForMember(x=>x.EmployeeName,option=>option.MapFrom(x=>x.Employee.LastName)).
            ForMember(x=>x.DepartmentName,option=>option.MapFrom(x=>x.Employee.Department.Name));
            CreateMap<SalaryPaymentDto,SalaryPayment>();
        }
    }
}