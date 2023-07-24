using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.Masters
{
    public class DepartmentDto: BaseEntityDto
    {   
        public string? Name {get;set;}
        public string? DepartmentHeadName {get;set;}
        public int? DepartmentHeadId {get;set;}
        public int Contact {get;set;}        
    }
}