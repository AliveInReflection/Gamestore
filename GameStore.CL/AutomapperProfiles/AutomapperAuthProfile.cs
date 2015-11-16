using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.DTO;

namespace GameStore.CL.AutomapperProfiles
{
    public class AutomapperAuthProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<UserClaim, UserClaimDTO>();

            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<UserClaimDTO, UserClaim>();
        }
    }
}
