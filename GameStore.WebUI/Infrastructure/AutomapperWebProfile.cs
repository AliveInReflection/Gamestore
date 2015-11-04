using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.WebUI.Models;
using AutoMapper;

namespace GameStore.WebUI.Infrastructure
{
    public class AutomapperWebProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<GameDTO, DisplayGameViewModel>();
            Mapper.CreateMap<GameDTO, UpdateGameViewModel>();

            Mapper.CreateMap<CommentDTO, DisplayCommentViewModel>();
            Mapper.CreateMap<CommentDTO, UpdateCommentViewModel>();
            
            Mapper.CreateMap<GenreDTO, DisplayGenreViewModel>();
            Mapper.CreateMap<GenreDTO, UpdateGenreViewModel>();

            Mapper.CreateMap<PlatformTypeDTO, DisplayPlatformTypeViewModel>();
            Mapper.CreateMap<PlatformTypeDTO, UpdatePlatformTypeViewModel>();

            Mapper.CreateMap<PublisherDTO, DisplayPublisherViewModel>();
            Mapper.CreateMap<PublisherDTO, UpdatePublisherViewModel>();

            Mapper.CreateMap<OrderDTO, OrderViewModel>();
            Mapper.CreateMap<OrderDetailsDTO, OrderDetailsViewModel>();


            Mapper.CreateMap<int, GenreDTO>().ForMember(m => m.GenreId, opt => opt.MapFrom(t => t));
            Mapper.CreateMap<int, PlatformTypeDTO>().ForMember(m => m.PlatformTypeId, opt => opt.MapFrom(t => t));
            Mapper.CreateMap<int, PublisherDTO>().ForMember(m => m.PublisherId, opt => opt.MapFrom(t => t));

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
                .ForMember(m => m.Text, opt => opt.MapFrom(t => t.GenreName))
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
        }
    }
}