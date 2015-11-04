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
        public ActionResult Ban(int userId)
        {
            ViewBag.UserId = userId;
            var durations = BanDurationManager.GetKeys();
            return View("Ban", durations);
        }

        [HttpPost]
        public ActionResult Ban(int userId, string duration)
        {
            userService.Ban(userId, BanDurationManager.Get(duration));
            return RedirectToAction("List", "Game");
        }

    }
}
