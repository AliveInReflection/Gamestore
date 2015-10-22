using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.BLL.Infrastructure;

namespace GameStore.WebUI.Controllers
{
    public class GamesController : Controller
    {
        private IGameService gameService;

        public GamesController()
        {

        }

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public ActionResult Index()
        {
            return Json(gameService.GetAll(),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult New(GameDTO game)
        {
            if(!ModelState.IsValid)
                return Json("Model error");
            try
            {
                gameService.Create(game);
                return Json("Added");
            }
            catch (ValidationException e)
            {
                return Json(e.Message);
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
                return Json(e.Message);
            }

        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            try
            {
                gameService.Delete(id);
                return Json("Removed");
            }
            catch (ValidationException e)
            {
                return Json(e.Message);
            }

        }

    }
}
