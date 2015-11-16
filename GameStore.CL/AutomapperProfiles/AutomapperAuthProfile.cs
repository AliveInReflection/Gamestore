using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

            Mapper.CreateMap<UserClaim, Claim>()
                .ForMember(m => m.Type, opt => opt.MapFrom(x => x.ClaimType))
                .ForMember(m => m.Value, opt => opt.MapFrom(x => x.ClaimValue))
                .ForMember(m => m.Issuer, opt => opt.MapFrom(x => "GameStore"));

            Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<UserClaimDTO, UserClaim>();

            Mapper.CreateMap<Claim, UserClaim>()
                .ForMember(m => m.ClaimType, opt => opt.MapFrom(x => x.Type))
                .ForMember(m => m.ClaimValue, opt => opt.MapFrom(x => x.Value));
        }
    }
}
