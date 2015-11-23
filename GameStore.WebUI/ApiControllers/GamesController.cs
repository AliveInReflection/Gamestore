﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.ApiControllers
{
    public class GamesController : BaseApiController
    {
        private IGameService gameService;

        public GamesController(IGameStoreLogger logger, IGameService gameService) : base(logger)
        {
            this.gameService = gameService;
        }


        // GET api/<controller>
        [ClaimsApi(GameStoreClaim.Games, Permissions.Retreive)]
        public HttpResponseMessage Get([FromUri] GameFilteringMode filter)
        {
            var games = gameService.Get(filter);
            var viewModels = Mapper.Map<IEnumerable<GameDTO>, IEnumerable<DisplayGameViewModel>>(games.Games);
            return Request.CreateResponse(HttpStatusCode.OK, viewModels);
        }

        // GET api/<controller>/5
        [ClaimsApi(GameStoreClaim.Games, Permissions.Retreive)]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var game = gameService.Get(id);
                var viewModel = Mapper.Map<DisplayGameViewModel>(game);
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST api/<controller>
        [ClaimsApi(GameStoreClaim.Games, Permissions.Create)]
        public HttpResponseMessage Post([FromBody]CreateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                gameService.Create(Mapper.Map<GameDTO>(model));
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        [ClaimsApi(GameStoreClaim.Games, Permissions.Update)]
        public HttpResponseMessage Put(int id, [FromBody]UpdateGameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                gameService.Update(Mapper.Map<GameDTO>(model));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        [ClaimsApi(GameStoreClaim.Games, Permissions.Delete)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                gameService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
        }


        #region private



        #endregion
    }
}