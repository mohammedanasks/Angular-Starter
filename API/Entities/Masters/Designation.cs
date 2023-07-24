using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Masters
{
    public class Designation : BaseEntity
    {
        public string? Name{get;set;}
        public string? Label{get;set;}
        public string? Description{get;set;}
    }
}