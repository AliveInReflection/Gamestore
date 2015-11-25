using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.ApiControllers
{
    public class GameGenresController : BaseApiController
    {
        private IGameService gameService;

        public GameGenresController(IGameStoreLogger logger, IGameService gameService) : base(logger)
        {
            this.gameService = gameService;
        }

        // GET api/<controller>/5
        [ClaimsApi(GameStoreClaim.Genres, Permissions.Retreive)]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var genres = gameService.Get(id).Genres;
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<IEnumerable<DisplayGenreViewModel>>(genres));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
        }        
    }
}