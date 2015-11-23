using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.ApiControllers
{
    public class PublisherGamesController : BaseApiController
    {
        private IPublisherService publisherService;

        public PublisherGamesController(IGameStoreLogger logger, IPublisherService publisherService) : base(logger)
        {
            this.publisherService = publisherService;
        }

        // GET api/<controller>/5
        [ClaimsApi(GameStoreClaim.Games, Permissions.Retreive)]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var games = publisherService.Get(id).Games;
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<IEnumerable<DisplayGameViewModel>>(games));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        
    }
}