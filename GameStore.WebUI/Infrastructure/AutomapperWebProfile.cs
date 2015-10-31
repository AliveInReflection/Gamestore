using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


            Mapper.CreateMap<CreateGameViewModel, GameDTO>();
            Mapper.CreateMap<UpdateGameViewModel, GameDTO>();

            Mapper.CreateMap<CreateCommentViewModel, CommentDTO>();
            Mapper.CreateMap<UpdateCommentViewModel, CommentDTO>();

            Mapper.CreateMap<CreateGenreViewModel, GenreDTO>();
            Mapper.CreateMap<UpdateGenreViewModel, GenreDTO>();

            Mapper.CreateMap<CreatePlatformTypeViewModel, PlatformTypeDTO>();
            Mapper.CreateMap<UpdatePlatformTypeViewModel, PlatformTypeDTO>();

            Mapper.CreateMap<CreatePublisherViewModel, PublisherDTO>();
            Mapper.CreateMap<UpdatePublisherViewModel, PublisherDTO>();

            Mapper.CreateMap<PaymentMethod, DisplayPaymentMethodViewModel>();
        }
    }
}