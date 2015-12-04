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
    public class CommentsController : BaseApiController
    {
        private IGameService gameService;
        private ICommentService commentService;

        public CommentsController(IGameStoreLogger logger, IGameService gameService, ICommentService commentService) : base(logger)
        {
            this.gameService = gameService;
            this.commentService = commentService;
        }


        // GET api/<controller>
        [ClaimsApi(GameStoreClaim.Comments, Permissions.Retreive)]
        public HttpResponseMessage Get([FromUri]string gameKey)
        {
            var comments = commentService.Get(gameKey);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<IEnumerable<DisplayCommentViewModel>>(comments));
        }

        // GET api/<controller>
        [ClaimsApi(GameStoreClaim.Comments, Permissions.Retreive)]
        public HttpResponseMessage Get([FromUri]string gameKey, int id)
        {
            var game = gameService.Get(gameKey);
            
            var comment = commentService.Get(game.GameId);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<DisplayCommentViewModel>(comment));
        }

        // POST api/<controller>
        [ClaimsApi(GameStoreClaim.Comments, Permissions.Create)]
        public HttpResponseMessage Post(int gameId, [FromBody]CreateCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                var game = gameService.Get(gameId);
                var comment = Mapper.Map<CommentDTO>(model);
                comment.Game = game;
                commentService.Create(comment);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        [ClaimsApi(GameStoreClaim.Comments, Permissions.Update)]
        public HttpResponseMessage Put(int gameId, [FromBody]UpdateCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, model);
            }

            try
            {
                var game = gameService.Get(gameId);
                var comment = Mapper.Map<CommentDTO>(model);
                comment.Game = game;
                commentService.Update(comment);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        [ClaimsApi(GameStoreClaim.Comments, Permissions.Delete)]
        public HttpResponseMessage Delete(string gameKey, int id)
        {
            try
            {
                commentService.Delete(id);
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