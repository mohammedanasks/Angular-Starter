using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Masters;

namespace API.Entities.SalaryManagement
{
    public class SalaryPayment:BaseEntity
    {
        public Employee ? Employee{get;set;}
        public int? EmployeeId{get;set;}

        public double? BasicSalary {get;set;}

        public double? tax {get;set;}

        public double? NetAmount{get;set;}
        
        public int ? Leave{get;set;}

         public int ? Deduction{get;set;}
        public DateTime Date{get;set;}

     
     
    }
}