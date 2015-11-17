using System.Web.Mvc;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Infrastructure;

namespace GameStore.WebUI.Controllers
{
    public class UserController : BaseController
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
            var durations = BanDurationManager.Items.Keys;
            return View("Ban", durations);
        }

        [HttpPost]
        public ActionResult Ban(string userName, string duration)
        {
            userService.Ban(userName, BanDurationManager.Items[duration]);
            return RedirectToAction("Index", "Game");
        }

    }
}
