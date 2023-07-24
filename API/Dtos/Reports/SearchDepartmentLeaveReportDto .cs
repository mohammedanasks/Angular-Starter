using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Reports
{
    public class SearchDepartmentLeaveReportDto

    {
        public string? EmployName{get;set;}

        public int ? Leave {get;set;}=0;

        public string ? Designation{get;set;}

        public int ? EmployeeId{get;set;}

        public string ?  DepartmentName{get;set;}

        public int ? DepartmentId{get;set;}
    }
}