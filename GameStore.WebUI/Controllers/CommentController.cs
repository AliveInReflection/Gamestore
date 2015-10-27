using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        public ActionResult List(string gameKey)
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
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error";
            }

            return RedirectToAction("List", "Comment", new { gameKey = gameKey });
        }

        [HttpPost]
        public ActionResult Delete(int commentId, string gameKey)
        {
            try
            {
                commentService.Delete(commentId);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error"; 
            }
            return RedirectToAction("List", "Comment", new {gameKey = gameKey});
        }

        public ActionResult Ban(int userId)
        {
            return View();
        }       
    }
}
