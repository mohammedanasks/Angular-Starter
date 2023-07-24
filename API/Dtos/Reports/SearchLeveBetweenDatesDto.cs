using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.src.app.dtos.Report
{
    public class SearchLeveBetweenDatesDto
    {

        public DateTime FromDate{get;set;}
        public DateTime ToDate{get;set;}

        public int DepartmentId{get;set;}

        public string? EmployName{get;set;}

        public int ? Leave {get;set;}

        public string ? Designation{get;set;}

        public int ? EmployeeId{get;set;}

        public string ?  DepartmentName{get;set;}

   
        
    }
}