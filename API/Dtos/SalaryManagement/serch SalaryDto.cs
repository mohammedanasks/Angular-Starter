using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.SalaryManagement
{
    public class SearchSalaryDto: BaseEntityDto
    {
    
        public DateTime Date{get;set;}

       
    }
}