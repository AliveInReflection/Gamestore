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
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.ApiControllers
{
    public class GenresController : BaseApiController
    {
        private IGenreService genreService;

        public GenresController(IGameStoreLogger logger, IGenreService genreService) : base(logger)
        {
            this.genreService = genreService;
        }

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            var genres = genreService.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<IEnumerable<DisplayGenreViewModel>>(genres));
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var genre = genreService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<DisplayGenreViewModel>(genre));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]CreateGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                genreService.Create(Mapper.Map<GenreDTO>(model));
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]UpdateGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                genreService.Update(Mapper.Map<GenreDTO>(model));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                genreService.Delete(id);
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