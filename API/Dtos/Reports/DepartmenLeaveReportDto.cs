using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.LeaveManagement
{
    public class DepartmentLeaveReportDto: BaseEntityDto
    {

         public string ? DepartmentName{get;set;}
         public string ? EmployName{get;set;}
             public int? EmployCount {get;set;}=0;

             public int ? DepartmentId{get;set;}

                public int ? EmployeeId{get;set;}
              public int? LeaveCount {get;set;}=0;

              public double? Netamount{get;set;}=0;

              public double? TotalTax{get;set;}=0;

        internal IEnumerable<object> GroupBy(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}