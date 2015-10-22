using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Domain.Entities;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Infrastructure
{
    public class AutomapperBLLProfile : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<Game, GameDTO>();
           
            Mapper.CreateMap<Genre, GenreDTO>()
                .ForMember(g => g.Games, m => m.Ignore());

            Mapper.CreateMap<Comment, CommentDTO>();


            Mapper.CreateMap<PlatformType, PlatformTypeDTO>()
                .ForMember(pt => pt.Games, m => m.Ignore());

            Mapper.CreateMap<Publisher, PublisherDTO>()
                .ForMember(p => p.Games, m => m.Ignore());

            Mapper.CreateMap<OrderDetails, OrderDetailsDTO>()
                .ForMember(od => od.Order, m => m.Ignore())
                .ForMember(od => od.Product, m => m.Ignore());

            //====================================================


            Mapper.CreateMap<GameDTO, Game>()
                .ForMember(g => g.Genres, m => m.Ignore())
                .ForMember(g => g.PlatformTypes, m => m.Ignore()); ;
           
            Mapper.CreateMap<CommentDTO, Comment>();

            Mapper.CreateMap<GenreDTO, Genre>()
                .ForMember(g => g.ChildGenres, m => m.Ignore())
                .ForMember(g => g.Games, m => m.Ignore()); ;
            
            Mapper.CreateMap<PlatformTypeDTO, PlatformType>()
                .ForMember(pt => pt.Games, m => m.Ignore());

            Mapper.CreateMap<PublisherDTO, Publisher>();
            Mapper.CreateMap<OrderDetailsDTO, OrderDetails>();
            Mapper.CreateMap<OrderDTO, Order>();

        }
    }
}
