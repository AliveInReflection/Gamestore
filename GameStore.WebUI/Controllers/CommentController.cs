using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class CommentController : BaseController
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
            var viewModel = new CommentViewModel()
            {
                GameKey = gameKey,
                Comments =
                    Mapper.Map<IEnumerable<CommentDTO>, IEnumerable<DisplayCommentViewModel>>(commentService.Get(gameKey)),
                NewComment = new CreateCommentViewModel()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                comment.Comments = Mapper.Map<IEnumerable<CommentDTO>, IEnumerable<DisplayCommentViewModel>>(commentService.Get(comment.GameKey));
                return View(comment);;
            }
                
            try
            {
                var commentToSave = Mapper.Map<CreateCommentViewModel, CommentDTO>(comment.NewComment);
                commentToSave.Game = Mapper.Map<string, GameDTO>(comment.GameKey);
                commentService.Create(commentToSave);
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }

            return RedirectToAction("Index", "Comment", new{gameKey = comment.GameKey});
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
                TempData["ErrorMessage"] = ValidationRes.ValidationError; 
            }
            return RedirectToAction("Index", "Comment", new {gameKey = gameKey});
        }

        public ActionResult Update(int commentId, string gameKey)
        {
            try
            {
                var comment = commentService.Get(commentId);
                ViewBag.GameKey = gameKey;
                return View(Mapper.Map<CommentDTO, UpdateCommentViewModel>(comment));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
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
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("Index", "Comment", new { gameKey = gameKey });
        }     
    }
}
