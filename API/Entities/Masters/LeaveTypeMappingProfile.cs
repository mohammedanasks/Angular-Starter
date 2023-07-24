using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Masters;
using AutoMapper;

namespace API.Entities.Masters
{
    public class LeaveTypeMappingProfile : Profile
    {
       public LeaveTypeMappingProfile(){
            CreateMap<LeaveType,LeaveTypeDto>();
            CreateMap<LeaveTypeDto,LeaveType>();
        }
    }
}