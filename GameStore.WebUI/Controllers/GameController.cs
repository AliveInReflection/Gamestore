using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GameStore.BLL.DTO;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameService gameService;


        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public ActionResult Index()
        {
            return Json(gameService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(GameDTO game)
        {
            if (!ModelState.IsValid)
                return Json("Model error");
            try
            {
                gameService.Create(game);
                return Json("Added");
            }
            catch (ValidationException e)
            {
                return Json("Validation error");
            }

        }

        [HttpPost]
        public ActionResult Update(GameDTO game)
        {
            if (!ModelState.IsValid)
                return Json("Model error");
            try
            {
                gameService.Update(game);
                return Json("Updated");
            }
            catch (ValidationException e)
            {
                return Json("Validation error");
            }

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                gameService.Delete(id);
                return Json("Removed");
            }
            catch (ValidationException e)
            {
                return Json("Validation error");
            }

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
                return Json("Validation error");
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
                return Json("Validation error");
            }
        }

    }
}
