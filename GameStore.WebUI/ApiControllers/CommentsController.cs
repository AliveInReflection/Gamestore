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
using GameStore.Logger.Interfaces;
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
        public HttpResponseMessage Get([FromUri]int gameId)
        {
            var game = gameService.Get(gameId);

            var comments = commentService.Get(game.GameKey);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<IEnumerable<DisplayCommentViewModel>>(comments));
        }

        // GET api/<controller>
        public HttpResponseMessage Get([FromUri]int gameId, int id)
        {
            var comment = commentService.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<DisplayCommentViewModel>(comment));
        }

        // POST api/<controller>
        public HttpResponseMessage Post(int gameId, [FromBody]CreateCommentViewModel model)
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
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int gameId, int id)
        {
            try
            {
                commentService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}