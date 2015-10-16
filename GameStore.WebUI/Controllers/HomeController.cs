using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.BLL.Services;
using GameStore.BLL.Infrastructure;

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
            return View();
        }

    }
}
