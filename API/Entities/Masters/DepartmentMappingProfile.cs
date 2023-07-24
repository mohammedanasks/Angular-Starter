using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Masters;
using API.Entities.Masters;
using AutoMapper;

namespace API.Entities.Masters
{
    public class DepartmentMappingProfile :Profile
    {
        public DepartmentMappingProfile(){

            CreateMap<Department,DepartmentDto>().
            ForMember(x=>x.DepartmentHeadName,option=>option.MapFrom(src=>src.DepartmentHead.LastName));
            CreateMap<DepartmentDto,Department>();
        }
    }
}