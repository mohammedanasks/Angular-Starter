using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.SalaryManagement
{
    public class SalaryPaymentDto: BaseEntityDto
    {
        public string ? EmployeeName{get;set;}
        
        public string? DepartmentName{get;set;}
        public int ? EmployeeId{get;set;}
        public double? BasicSalary {get;set;}

        public double? tax {get;set;}

        public double? NetAmount{get;set;}
        public DateTime Date{get;set;}
         public int ? Deduction{get;set;}
          public double? Total{get;set;}

          public int ? Leave{get;set;}

  
                   public string ? AllReadyPaid{get;set;} 

                   public string ? FiledNullMsg{get;set;} 
    }
}