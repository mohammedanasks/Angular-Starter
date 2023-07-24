using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.Masters
{
    public class LeaveTypeDto: BaseEntityDto
    {
    public Boolean IsPaid{get;set;}
    public string? LeaveTypeName{get;set;}
    }
}