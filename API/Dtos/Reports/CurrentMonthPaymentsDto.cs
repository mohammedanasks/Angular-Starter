using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.SalaryManagement
{
    public class CurrentMonthPaymentsDto
    
    {
        public string ? EmployeeName{get;set;}
        public string ? DepartmentName{get;set;}
        public string ? DesignationName{get;set;}

        public string ? Lastname{get;set;}
    
        public int ? EmployeeId{get;set;}
        public double? BasicSalary {get;set;}
        public double? Tax {get;set;}
        public int? LeaveCount {get;set;}=0;
        public int? PaidLeaveCount {get;set;}=0;
     
        public double? Deduction {get;set;}= 0;
        public Boolean IsPaid {get;set;}

        public string? LeaveType{get;set;}
        public double? NetAmount{get;set;}
        public DateTime Date{get;set;}

    }
}