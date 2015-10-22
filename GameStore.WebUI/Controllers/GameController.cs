using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.DTO;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameService gameService;
        private ICommentService commentService;

        public GameController()
        {

        }

        public GameController(IGameService gameService, ICommentService commentService)
        {
            this.gameService = gameService;
            this.commentService = commentService;
        }

        [HttpGet]
        public ActionResult Details(string key)
        {
            try
            {
                var game = gameService.Get(key);
                return Json(game, JsonRequestBehavior.AllowGet);
            }
            catch (ValidationException e)
            {
                return Json(e.Message);
            }
        }


        [HttpPost]
        public ActionResult NewComment(string gamekey, CommentDTO comment)
        {
            if (!ModelState.IsValid)
                return Json("Model error");

            try
            {
                commentService.Create(gamekey, comment);
                return Json("Comment added");
            }
            catch (ValidationException e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public ActionResult Comments(string gamekey)
        {
            try
            {
                var comments = commentService.Get(gamekey);
                return Json(comments);
            }
            catch (ValidationException e)
            {
                return Json(e.Message);
            }
            
        }

        [HttpGet]
        public ActionResult Download(string gamekey)
        {
            try
            {
                //control of game existance in db
                var game = gameService.Get(gamekey);
                var rootPath = Server.MapPath("~/App_Data/Binary/");
                byte[] fileBytes = System.IO.File.ReadAllBytes(rootPath + "game.data");
                string fileName = "game.data";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (ValidationException e)
            {
                return Json(e.Message);
            }
        }

    }
}
