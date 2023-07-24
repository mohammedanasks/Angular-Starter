using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.Masters
{
    public class EmployeeDto  : BaseEntityDto
    {        
        public string? FirstName{get;set;}
        public string? LastName {get;set;}
        
        public string? fileName {get;set;}
        public string? DepartmentName {get;set;}
        public int? DepartmentId {get;set;}

        public string? DesignationName {get;set;}
        public int ? DesignationId {get;set;}
        
        public int ? Contact {get;set;}

        public DateTime JoinDate {get;set;}
        public int? BasicSalary {get;set;}

        
        public int ? Tax {get;set;}
        public  int ? totalWorkingDays{get;set;}
        public int? Leave{get;set;}
        public double? receivedSalary{get;set;}
        public double ?salaryTotal{get;set;}

        public string? FirstNameRequired{get;set;}
        public string? LastNameRequired{get;set;}
        public string? DesignationRequired{get;set;}
        public string? DepartmentNameRequired{get;set;}
        public string? ContactRequired{get;set;}

           public string? SalaryRequired{get;set;}
        
    }
}