using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

namespace GameStore.CL.AutomapperProfiles
{
    public class AutomapperBLLProfile : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<Game, GameDTO>();
            Mapper.CreateMap<Genre, GenreDTO>();
            Mapper.CreateMap<Comment, CommentDTO>()
                .ForMember(c => c.ParentCommentId, opt => opt.MapFrom(m => m.ParentComment.CommentId))
                .ForMember(c => c.User, opt => opt.MapFrom(m => m.User ?? new User(){UserName = DefaultRoles.Guest}));                           

            Mapper.CreateMap<PlatformType, PlatformTypeDTO>();
            Mapper.CreateMap<Publisher, PublisherDTO>();
            Mapper.CreateMap<Order, OrderDTO>();
            Mapper.CreateMap<OrderDetails, OrderDetailsDTO>();
            Mapper.CreateMap<User, UserDTO>();

            Mapper.CreateMap<Role, RoleDTO>()
                .ForMember(m => m.Claims, opt => opt.MapFrom(m => m.Claims));


            Mapper.CreateMap<UserClaim, Claim>()
                .ConstructProjectionUsing(m => new Claim(m.ClaimType, m.ClaimValue, "GameStore"));

            Mapper.CreateMap<RoleClaim, Claim>()
                .ConstructProjectionUsing(m => new Claim(m.ClaimType, m.ClaimValue, "GameStore"));

            //====================================================


            Mapper.CreateMap<GameDTO, Game>();

            Mapper.CreateMap<CommentDTO, Comment>()
                .ForMember(m => m.ParentComment, opt => opt.MapFrom(t => t.ParentCommentId));

            Mapper.CreateMap<GenreDTO, Genre>();

            Mapper.CreateMap<PlatformTypeDTO, PlatformType>();

            Mapper.CreateMap<PublisherDTO, Publisher>();
            
            Mapper.CreateMap<OrderDetailsDTO, OrderDetails>();
            
            Mapper.CreateMap<OrderDTO, Order>();

            Mapper.CreateMap<UserDTO, User>();

            Mapper.CreateMap<int, User>().ForMember(m => m.UserId, opt => opt.MapFrom(t => t));
            Mapper.CreateMap<int, Comment>().ForMember(m => m.CommentId, opt => opt.MapFrom(t => t));

            Mapper.CreateMap<Claim, RoleClaim>()
                .ForMember(m => m.ClaimType, opt => opt.MapFrom(m => m.Type))
                .ForMember(m => m.ClaimValue, opt => opt.MapFrom(m => m.Value));

            Mapper.CreateMap<Claim, UserClaim>()
                .ForMember(m => m.ClaimType, opt => opt.MapFrom(m => m.Type))
                .ForMember(m => m.ClaimValue, opt => opt.MapFrom(m => m.Value));

            Mapper.CreateMap<RoleDTO, Role>()
                .ForMember(m => m.Claims, opt => opt.MapFrom(m => m.Claims));


        }
    }
}
