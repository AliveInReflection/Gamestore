using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.ApiControllers
{
    public class GamesController : BaseApiController
    {
        private IGameService gameService;
        private IGenreService genreService;
        private IPlatformTypeService platformTypeService;
        private IPublisherService publisherService;

        public GamesController(IGameStoreLogger logger, IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService, IPublisherService publisherService) : base(logger)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.platformTypeService = platformTypeService;
            this.publisherService = publisherService;
        }


        // GET api/<controller>
        public HttpResponseMessage Get([FromUri] GameFilteringMode filter)
        {
            var games = gameService.Get(filter);
            var viewModels = Mapper.Map<IEnumerable<GameDTO>, IEnumerable<DisplayGameViewModel>>(games.Games);
            return Request.CreateResponse(HttpStatusCode.OK, viewModels);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]string value)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }


        #region private



        #endregion
    }
}