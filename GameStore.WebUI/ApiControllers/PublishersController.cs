using System;
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
    public class PublishersController : BaseApiController
    {
        private IPublisherService publisherService;

        public PublishersController(IGameStoreLogger logger, IPublisherService publisherService) : base(logger)
        {
            this.publisherService = publisherService;
        }

        // GET api/<controller>
        [ClaimsApi(GameStoreClaim.Publishers, Permissions.Retreive)]
        public HttpResponseMessage Get()
        {
            var publishers = publisherService.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK,
                Mapper.Map<IEnumerable<DisplayPublisherViewModel>>(publishers));
        }

        // GET api/<controller>/5
        [ClaimsApi(GameStoreClaim.Publishers, Permissions.Retreive)]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var publisher = publisherService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<DisplayPublisherViewModel>(publisher));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST api/<controller>
        [ClaimsApi(GameStoreClaim.Publishers, Permissions.Create)]
        public HttpResponseMessage Post([FromBody]CreatePublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                var publisher = Mapper.Map<PublisherDTO>(model);
                publisherService.Create(publisher);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // PUT api/<controller>/5
        [ClaimsApi(GameStoreClaim.Publishers, Permissions.Update)]
        public HttpResponseMessage Put(int id, [FromBody]UpdatePublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                var publisher = Mapper.Map<PublisherDTO>(model);
                publisherService.Update(publisher);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // DELETE api/<controller>/5
        [ClaimsApi(GameStoreClaim.Publishers, Permissions.Delete)]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                publisherService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}