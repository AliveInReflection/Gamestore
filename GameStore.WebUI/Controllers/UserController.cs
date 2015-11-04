using System.Web.Mvc;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.WebUI.Infrastructure;

namespace GameStore.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Ban(string userName)
        {
            ViewBag.UserId = userName;
            var durations = BanDurationManager.GetKeys();
            return View("Ban", durations);
        }

        [HttpPost]
        public ActionResult Ban(string userName, string duration)
        {
            userService.Ban(userName, BanDurationManager.Get(duration));
            return RedirectToAction("Index", "Game");
        }

    }
}
