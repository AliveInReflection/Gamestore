﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Infrastructure
{
    public class AutomapperWebProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<GameDTO, DisplayGameViewModel>();
            Mapper.CreateMap<CommentDTO, DisplayCommentViewModel>();
            Mapper.CreateMap<GenreDTO, GenreViewModel>();
            Mapper.CreateMap<PlatformTypeDTO, PlatformTypeViewModel>();

            Mapper.CreateMap<CreateGameViewModel, GameDTO>();
            Mapper.CreateMap<EditGameViewModel, GameDTO>();
            Mapper.CreateMap<CreateCommentViewModel, CommentDTO>();
            Mapper.CreateMap<GenreViewModel, GenreDTO>();
            Mapper.CreateMap<PlatformTypeViewModel, PlatformTypeDTO>();
        }
    }
}