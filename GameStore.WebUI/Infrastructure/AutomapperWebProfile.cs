using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Infrastructure.DTO;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;
using GameStore.WebUI.Models.Account;
using GameStore.WebUI.Models.Order;

namespace GameStore.CL.AutomapperProfiles
{
    public class AutomapperWebProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<GameDTO, DisplayGameViewModel>()
                .ForMember(g => g.Description, opt => opt.ResolveUsing(ResolveCulture));


            Mapper.CreateMap<GameDTO, UpdateGameViewModel>();

            Mapper.CreateMap<CommentDTO, DisplayCommentViewModel>();
            Mapper.CreateMap<CommentDTO, UpdateCommentViewModel>();
            
            Mapper.CreateMap<GenreDTO, DisplayGenreViewModel>()
                .ForMember(g => g.GenreName, opt => opt.ResolveUsing(ResolveCulture));
            Mapper.CreateMap<GenreDTO, UpdateGenreViewModel>();

            Mapper.CreateMap<PlatformTypeDTO, DisplayPlatformTypeViewModel>();
            Mapper.CreateMap<PlatformTypeDTO, UpdatePlatformTypeViewModel>();

            Mapper.CreateMap<PublisherDTO, DisplayPublisherViewModel>()
                .ForMember(p => p.Description, opt => opt.ResolveUsing(ResolveCulture));
            Mapper.CreateMap<PublisherDTO, UpdatePublisherViewModel>();

            Mapper.CreateMap<OrderDTO, OrderViewModel>();
            Mapper.CreateMap<OrderDetailsDTO, OrderDetailsViewModel>();

            Mapper.CreateMap<OrderDTO, DisplayOrderViewModel>();


            

            Mapper.CreateMap<UserDTO, DisplayUserViewModel>()
                .ForMember(m => m.Roles, opt => opt.MapFrom(m => m.Roles.Select(x => x.RoleName)));

            Mapper.CreateMap<RoleDTO, DisplayRoleViewModel>();

            Mapper.CreateMap<RoleDTO, ManageRoleViewModel>()
                .ForMember(m => m.Claims, opt => opt.Ignore());

            Mapper.CreateMap<RoleDTO, SelectListItem>()
                .ForMember(m => m.Text, opt => opt.MapFrom(m => m.RoleName))
                .ForMember(m => m.Value, opt => opt.MapFrom(m => m.RoleId.ToString()));


            //===================Reverce=============================

            Mapper.CreateMap<CreateGameViewModel, GameDTO>()
                .ForMember(m => m.Genres, opt => opt.MapFrom(t => t.GenreIds))
                .ForMember(m => m.PlatformTypes, opt => opt.MapFrom(t => t.PlatformTypeIds))
                .ForMember(m => m.Publisher, opt => opt.MapFrom(t => t.PublisherId));
            Mapper.CreateMap<UpdateGameViewModel, GameDTO>()
                .ForMember(m => m.Genres, opt => opt.MapFrom(t => t.GenreIds))
                .ForMember(m => m.PlatformTypes, opt => opt.MapFrom(t => t.PlatformTypeIds))
                .ForMember(m => m.Publisher, opt => opt.MapFrom(t => t.PublisherId));

            Mapper.CreateMap<CreateCommentViewModel, CommentDTO>();
            Mapper.CreateMap<UpdateCommentViewModel, CommentDTO>();

            Mapper.CreateMap<CreateGenreViewModel, GenreDTO>();
            Mapper.CreateMap<UpdateGenreViewModel, GenreDTO>();

            Mapper.CreateMap<CreatePlatformTypeViewModel, PlatformTypeDTO>();
            Mapper.CreateMap<UpdatePlatformTypeViewModel, PlatformTypeDTO>();

            Mapper.CreateMap<CreatePublisherViewModel, PublisherDTO>();
            Mapper.CreateMap<UpdatePublisherViewModel, PublisherDTO>();

            Mapper.CreateMap<PaymentMethod, DisplayPaymentMethodViewModel>();


            Mapper.CreateMap<GenreDTO, SelectListItem>()
                .ForMember(m => m.Text, opt => opt.MapFrom(t => t.GenreName))
                .ForMember(m => m.Value, opt => opt.MapFrom(t => t.GenreId));

            Mapper.CreateMap<PlatformTypeDTO, SelectListItem>()
                .ForMember(m => m.Text, opt => opt.MapFrom(t => t.PlatformTypeName))
                .ForMember(m => m.Value, opt => opt.MapFrom(t => t.PlatformTypeId));

            Mapper.CreateMap<PublisherDTO, SelectListItem>()
                .ForMember(m => m.Text, opt => opt.MapFrom(t => t.CompanyName))
                .ForMember(m => m.Value, opt => opt.MapFrom(t => t.PublisherId));

            Mapper.CreateMap<GenreDTO, CheckBoxViewModel>()
                .ForMember(m => m.Text, opt => opt.ResolveUsing(ResolveCulture))
                .ForMember(m => m.Id, opt => opt.MapFrom(t => t.GenreId));

            Mapper.CreateMap<PlatformTypeDTO, CheckBoxViewModel>()
                .ForMember(m => m.Text, opt => opt.MapFrom(t => t.PlatformTypeName))
                .ForMember(m => m.Id, opt => opt.MapFrom(t => t.PlatformTypeId));

            Mapper.CreateMap<PublisherDTO, CheckBoxViewModel>()
                .ForMember(m => m.Text, opt => opt.MapFrom(t => t.CompanyName))
                .ForMember(m => m.Id, opt => opt.MapFrom(t => t.PublisherId));

            Mapper.CreateMap<string, SelectListItem>()
                .ForMember(m => m.Text, opt => opt.MapFrom(t => t))
                .ForMember(m => m.Value, opt => opt.MapFrom(t => t));

            Mapper.CreateMap<string, RadiobuttonViewModel>()
                .ForMember(m => m.SelectedValue, opt => opt.MapFrom(t => t));

            Mapper.CreateMap<string, GameDTO>().ForMember(m => m.GameKey, opt => opt.MapFrom(t => t));

            Mapper.CreateMap<RegisterViewModel, UserDTO>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(m => m.UserName))
                .ForMember(m => m.Password, opt => opt.MapFrom(m => m.Password));

            Mapper.CreateMap<CreateRoleViewModel, RoleDTO>();
            Mapper.CreateMap<ClaimViewModel, Claim>()
                .ConstructProjectionUsing(m => new Claim(m.ClaimType, m.ClaimValue, "GameStore"));


            Mapper.CreateMap<int, GenreDTO>().ForMember(m => m.GenreId, opt => opt.MapFrom(t => t));
            Mapper.CreateMap<int, PlatformTypeDTO>().ForMember(m => m.PlatformTypeId, opt => opt.MapFrom(t => t));
            Mapper.CreateMap<int, PublisherDTO>().ForMember(m => m.PublisherId, opt => opt.MapFrom(t => t));
            Mapper.CreateMap<int, RoleDTO>().ForMember(m => m.RoleId, opt => opt.MapFrom(m => m));

            Mapper.CreateMap<ManageUserViewModel, UserDTO>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(m => m.User.UserId))
                .ForMember(m => m.UserName, opt => opt.MapFrom(m => m.User.UserName))
                .ForMember(m => m.Roles, opt => opt.MapFrom(m => m.Roles))
                .ForMember(m => m.Claims, opt => opt.MapFrom(m => m.Claims));

            Mapper.CreateMap<ManageRoleViewModel, RoleDTO>();

        }

        public string ResolveCulture(GameDTO game)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return game.DescriptionRu ?? game.Description;
            }
            return game.Description;
        }

        public string ResolveCulture(GenreDTO genre)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return genre.GenreNameRu ?? genre.GenreName;
            }
            return genre.GenreName;
        }

        public string ResolveCulture(PublisherDTO publisher)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return publisher.DescriptionRu ?? publisher.Description;
            }
            return publisher.Description;
        }
    }
}