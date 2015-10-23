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
            Mapper.CreateMap<Genre, GenreDTO>();
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<PlatformType, PlatformTypeDTO>();
            Mapper.CreateMap<Publisher, PublisherDTO>();
            Mapper.CreateMap<Order, OrderDTO>();
            Mapper.CreateMap<OrderDetails, OrderDetailsDTO>();
            Mapper.CreateMap<User, UserDTO>();

            //====================================================


            Mapper.CreateMap<GameDTO, Game>()
                .ForMember(g => g.Genres, m => m.Ignore())
                .ForMember(g => g.PlatformTypes, m => m.Ignore()); ;
           
            Mapper.CreateMap<CommentDTO, Comment>()
                .ForMember(c => c.ParentComment, m => m.Ignore())
                .ForMember(c => c.User, m => m.Ignore());

            Mapper.CreateMap<GenreDTO, Genre>()
                .ForMember(g => g.ChildGenres, m => m.Ignore())
                .ForMember(g => g.Games, m => m.Ignore()); ;
            
            Mapper.CreateMap<PlatformTypeDTO, PlatformType>()
                .ForMember(pt => pt.Games, m => m.Ignore());

            Mapper.CreateMap<PublisherDTO, Publisher>();
            
            Mapper.CreateMap<OrderDetailsDTO, OrderDetails>();
            
            Mapper.CreateMap<OrderDTO, Order>();

            Mapper.CreateMap<UserDTO, User>();


        }
    }
}
