using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.BLL.Services;

namespace GameStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private IGameService gameService;

        public HomeController()
        {
            gameService = new GameService();
        }

        public ActionResult Index()
        {
            //var games = gameService.GetAllGames();
            gameService.AddGame(new GameDTO()
            {
                GameKey = "ASd",
                GameName = "asdasdasdasd",
                Description = "asdasdasdasknakdsfamdfkqwehbffasfbqk"

            });
            var games = gameService.GetAllGames();
            return View();
        }

    }
}
