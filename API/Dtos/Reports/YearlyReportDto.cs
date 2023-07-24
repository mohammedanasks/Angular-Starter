using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Reports
{
    public class YearlyReportDto
    {

         public int ? Year{get;set;}
         public DateTime Date{get;set;}
    
        public double? TotalSalary{get;set;}

        public double? tax {get;set;}

        public double? Deduction{get;set;}
        public int Leave{get;set;}
        
    }
}