using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;

namespace GameStore.WebUI.Controllers
{
    public class CommentController : Controller
    {

        private ICommentService commentService;


        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public ActionResult Create(string gamekey, CommentDTO comment)
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
                return Json("Validation error");
            }
        }

        [HttpPost]
        public ActionResult GetAll(string gamekey)
        {
            try
            {
                var comments = commentService.Get(gamekey);
                return Json(comments);
            }
            catch (ValidationException e)
            {
                return Json("Validation error");
            }

        }

    }
}
