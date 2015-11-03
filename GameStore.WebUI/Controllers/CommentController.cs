using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService commentService;
        private IGameStoreLogger logger;

        public CommentController(ICommentService commentService, IGameStoreLogger logger)
        {
            this.commentService = commentService;
            this.logger = logger;
        }

        public ActionResult Index(string gameKey)
        {
            ViewBag.GameKey = gameKey;
            var comments = commentService.Get(gameKey);
            return View(Mapper.Map<IEnumerable<CommentDTO>, IEnumerable<DisplayCommentViewModel>>(comments));
        }

        [HttpGet]
        public ActionResult Create(string gameKey)
        {
            return PartialView(new CreateCommentViewModel());
        }

        [HttpPost]
        public ActionResult Create(string gameKey, CreateCommentViewModel comment)
        {
            if (!ModelState.IsValid)
                return PartialView(comment);
            try
            {
                commentService.Create(gameKey, Mapper.Map<CreateCommentViewModel, CommentDTO>(comment));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = "Validation error";
            }

            return RedirectToAction("Index", "Comment", new{gameKey = gameKey});
        }

        [HttpPost]
        public ActionResult Delete(int commentId, string gameKey)
        {
            try
            {
                commentService.Delete(commentId);
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = "Validation error"; 
            }
            return RedirectToAction("Index", "Comment", new {gameKey = gameKey});
        }

        public ActionResult Update(int id, string gameKey)
        {
            try
            {
                var comment = commentService.Get(id);
                ViewBag.GameKey = gameKey;
                return View(Mapper.Map<CommentDTO, UpdateCommentViewModel>(comment));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = "Validation error";
            }
            return RedirectToAction("Index", "Comment", new { gameKey = gameKey });
        }

        [HttpPost]
        public ActionResult Update(UpdateCommentViewModel comment, string gameKey)
        {
            if (!ModelState.IsValid)
            {
                return View(comment);   
            }

            try
            {
                commentService.Update(Mapper.Map<UpdateCommentViewModel, CommentDTO>(comment));
                return RedirectToAction("Index", "Comment", new { gameKey = gameKey });
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                ModelState.AddModelError("CommentId", e.Message);
                return View(comment);
            }
        }     
    }
}
