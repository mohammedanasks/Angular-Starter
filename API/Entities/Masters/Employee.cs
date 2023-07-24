using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace API.Entities.Masters
{
    public class Employee : BaseEntity

    {

        public string? FileName {get;set;}

        [NotMapped]
        public IFormFile? File {get;set;}

        [Required]
        public string? FirstName {get;set;}

        [Required]
        public string? LastName {get;set;}

        public Department? Department {get;set;}
        public int? DepartmentId {get;set;}

        public Designation? Designation {get;set;}
        public int? DesignationId {get;set;} 

        public int Contact {get;set;}
        public DateTime JoinDate {get;set;}
        public int BasicSalary {get;set;}



    }
}