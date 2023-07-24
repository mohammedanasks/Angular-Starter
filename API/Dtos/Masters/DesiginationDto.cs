using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Base;

namespace API.Dtos.Masters
{
    public class DesignationDto: BaseEntityDto
    {
        public string? Name{get;set;}
        public string? Label{get;set;}
        public string? Description{get;set;}
    }
}