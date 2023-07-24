using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Masters;

namespace API.Entities.LeaveManagement
{
    public class Leave : BaseEntity
    {
        public Employee? Employee { get; set; }
        public int? EmployeeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

    
        public LeaveType? LeaveType{ get; set; }

        public int LeaveTypeId{ get; set; }

    }
}