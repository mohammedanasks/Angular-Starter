using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace API.Entities.Masters
{
    public class Department : BaseEntity
    {
        
        public string? Name {get;set;}
    
        public Employee? DepartmentHead {get;set;}
        public int? DepartmentHeadId {get;set;}
        public int Contact{get;set;}
    }
}