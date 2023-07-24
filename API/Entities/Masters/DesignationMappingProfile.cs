using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Masters;
using AutoMapper;

namespace API.Entities.Masters
{
    public class DesignationMappingProfile :Profile
    {
        public DesignationMappingProfile(){

            CreateMap<Designation,DesignationDto>();
            CreateMap<DesignationDto,Designation>();
        }
    }
}