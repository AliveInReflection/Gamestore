using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models.Account;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private IUserService userService;
        private IAuthenticationService auth;
        private IGameStoreLogger logger;

        public AccountController(IUserService userService, IAuthenticationService auth, IGameStoreLogger logger)
        {
            this.userService = userService;
            this.auth = auth;
            this.logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.ReturnUrl = HttpContext.Request.UrlReferrer.AbsolutePath;
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel register, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(register);
            }
            try
            {
                var user = Mapper.Map<RegisterViewModel, UserDTO>(register);
                userService.Create(user);

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("Index", "Game");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = HttpContext.Request.UrlReferrer.AbsolutePath;
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            if (!auth.Login(login.UserName, login.Password, login.RememberMe))
            {
                ViewBag.ReturnUrl = returnUrl;
                ModelState.AddModelError("UserName", ValidationRes.LoginError);
                return View(login);
            }
            return Redirect(returnUrl);
        }

        [Authorize]
        public ActionResult Logout()
        {
            auth.Logout();
            return RedirectToAction("Index", "Game");
        }





        public ActionResult Ban(string userName)
        {
            ViewBag.UserId = userName;
            var durations = BanDurationManager.GetKeys();
            return View("Ban", durations);
        }

        [HttpPost]
        public ActionResult Ban(string userName, string duration)
        {
            userService.Ban(1, BanDurationManager.Get(duration));
            return RedirectToAction("Index", "Game");
        }
    }
}
