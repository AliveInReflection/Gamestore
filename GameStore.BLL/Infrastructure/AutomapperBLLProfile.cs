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


            Mapper.CreateMap<GameDTO, Game>();

            Mapper.CreateMap<CommentDTO, Comment>();

            Mapper.CreateMap<GenreDTO, Genre>();

            Mapper.CreateMap<PlatformTypeDTO, PlatformType>();

            Mapper.CreateMap<PublisherDTO, Publisher>();
            
            Mapper.CreateMap<OrderDetailsDTO, OrderDetails>();
            
            Mapper.CreateMap<OrderDTO, Order>();

            Mapper.CreateMap<UserDTO, User>();


        }
    }
}
