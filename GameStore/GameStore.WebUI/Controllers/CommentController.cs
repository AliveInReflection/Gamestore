using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models;
using GameStore.WebUI.Models.Account;

namespace GameStore.WebUI.Controllers
{
    public class CommentController : BaseController
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService, IGameStoreLogger logger)
            :base(logger)
        {
            this.commentService = commentService;
        }

        [Claims(GameStoreClaim.Comments, Permissions.Retreive)]
        [Claims(GameStoreClaim.Comments, Permissions.Create)]
        [AgeClaim(21)]
        public ActionResult Index(string gameKey)
        {
            var viewModel = new CommentViewModel()
            {
                GameKey = gameKey,
                Comments =
                    Mapper.Map<IEnumerable<CommentDTO>, IEnumerable<DisplayCommentViewModel>>(commentService.Get(gameKey)),
                NewComment = new CreateCommentViewModel()
                {
                    User = new DisplayUserViewModel()
                    {
                        UserId = int.Parse((User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.SerialNumber).Value),
                        UserName = (User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Name).Value
                    }
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        [Claims(GameStoreClaim.Comments, Permissions.Retreive)]
        [Claims(GameStoreClaim.Comments, Permissions.Create)]
        [AgeClaim(21)]
        public ActionResult Index(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                comment.Comments = Mapper.Map<IEnumerable<CommentDTO>, IEnumerable<DisplayCommentViewModel>>(commentService.Get(comment.GameKey));
                return View(comment);
            }
                
            try
            {
                var commentToSave = Mapper.Map<CreateCommentViewModel, CommentDTO>(comment.NewComment);
                commentToSave.Game = Mapper.Map<string, GameDTO>(comment.GameKey);
                commentService.Create(commentToSave);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }

            return RedirectToAction("Index", "Comment", new{gameKey = comment.GameKey});
        }

        [HttpPost]
        [Claims(GameStoreClaim.Comments, Permissions.Delete)]
        public ActionResult Delete(int commentId, string gameKey)
        {
            try
            {
                commentService.Delete(commentId);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError); 
            }
            return RedirectToAction("Index", "Comment", new {gameKey = gameKey});
        }

        [Claims(GameStoreClaim.Comments, Permissions.Retreive)]
        [Claims(GameStoreClaim.Comments, Permissions.Update)]
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Index", "Comment", new { gameKey = gameKey });
        }

        [HttpPost]
        [Claims(GameStoreClaim.Comments, Permissions.Retreive)]
        [Claims(GameStoreClaim.Comments, Permissions.Update)]
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Index", "Comment", new { gameKey = gameKey });
        }     

    }
}
