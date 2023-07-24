using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.SalaryManagement
{
    public class MonthlyReportDto
    {
        

        public string ? Month{get;set;}
         public DateTime Date{get;set;}
        public int ? EmployeeId{get;set;}
        public double? TotalSalary{get;set;}

        public double? tax {get;set;}

        public double? Deduction{get;set;}
        public int Leave{get;set;}

          
    }
}