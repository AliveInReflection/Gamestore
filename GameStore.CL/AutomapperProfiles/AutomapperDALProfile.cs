using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Domain.Entities;

namespace GameStore.CL.AutomapperProfiles
{
    public class AutomapperDALProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Game, Game>().ForMember(m => m.Genres, opt => opt.Ignore())
                .ForMember(m => m.PlatformTypes, opt => opt.Ignore()).ForMember(m => m.Publisher, opt => opt.Ignore());
        }
    }
}
