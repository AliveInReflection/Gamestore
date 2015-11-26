using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Models;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI.ApiControllers
{
    public class GenreGamesController : BaseApiController
    {
        private IGameService gameService;

        public GenreGamesController(IGameStoreLogger logger, IGameService gameService) : base(logger)
        {
            this.gameService = gameService;
        }
        
        // GET api/<controller>/5
        [ClaimsApi(GameStoreClaim.Games, Permissions.Retreive)]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var games = gameService.GetByGenre(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<IEnumerable<DisplayGameViewModel>>(games));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

    }
}