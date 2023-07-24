using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.LeaveManagement;

namespace API.Entities.Masters
{
    public class LeaveType : BaseEntity
    {
        

    public Boolean IsPaid{get;set;}
    public string? LeaveTypeName{get;set;}
 

    }
}