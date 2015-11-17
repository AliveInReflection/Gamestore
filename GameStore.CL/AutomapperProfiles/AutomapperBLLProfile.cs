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
    public class AutomapperBLLProfile : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<Game, GameDTO>();
            Mapper.CreateMap<Genre, GenreDTO>();
            Mapper.CreateMap<Comment, CommentDTO>()
                .ForMember(c => c.UserName, opt => opt.MapFrom(m => m.User.UserName));


            Mapper.CreateMap<PlatformType, PlatformTypeDTO>();
            Mapper.CreateMap<Publisher, PublisherDTO>();
            Mapper.CreateMap<Order, OrderDTO>();
            Mapper.CreateMap<OrderDetails, OrderDetailsDTO>();
            Mapper.CreateMap<User, UserDTO>();

            Mapper.CreateMap<Role, RoleDTO>()
                .ForMember(m => m.Claims, opt => opt.MapFrom(m => m.RoleClaims));

            //====================================================


            Mapper.CreateMap<GameDTO, Game>();

            Mapper.CreateMap<CommentDTO, Comment>()
                .ForMember(m => m.User, opt => opt.MapFrom(t => t.UserName))
                .ForMember(m => m.ParentComment, opt => opt.MapFrom(t => t.ParentCommentId));

            Mapper.CreateMap<GenreDTO, Genre>();

            Mapper.CreateMap<PlatformTypeDTO, PlatformType>();

            Mapper.CreateMap<PublisherDTO, Publisher>();
            
            Mapper.CreateMap<OrderDetailsDTO, OrderDetails>();
            
            Mapper.CreateMap<OrderDTO, Order>();

            Mapper.CreateMap<UserDTO, User>();

            Mapper.CreateMap<string, User>().ForMember(m => m.UserName, opt => opt.MapFrom(t => t));
            Mapper.CreateMap<int, Comment>().ForMember(m => m.CommentId, opt => opt.MapFrom(t => t));

            Mapper.CreateMap<RoleDTO, Role>()
                .ForMember(m => m.RoleClaims, opt => opt.MapFrom(m => m.Claims));
            


        }
    }
}
