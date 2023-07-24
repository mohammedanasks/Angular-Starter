using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.LeaveManagement
{
    public class LeaveDto: BaseEntityDto
    {
        public string? EmployeeName{get;set;}

        public string? DepartmentName{get;set;}
        public int? EmployeeId{get;set;}


        public int? LeaveTypeId{get;set;}
        public string? LeaveTypeName{get;set;}

        public DateTime ? FromDate{get;set;}
        public DateTime ? ToDate{get;set;}


        public string? NameRequired{get;set;}
        public string? DateRequired{get;set;}

        public string ?LeaveTypeRequired{get;set;}
    }
}