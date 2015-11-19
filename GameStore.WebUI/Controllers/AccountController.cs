using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
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

        #region User
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

        public ActionResult Index()
        {
            var users = Mapper.Map<IEnumerable<UserDTO>, IEnumerable<DisplayUserViewModel>>(userService.GetAll());
            return View(users);
        }

        public ActionResult Manage(int userId)
        {
            return View(new ManageUserViewModel()
            {
                RoleItems = Mapper.Map<IEnumerable<RoleDTO>,IEnumerable<SelectListItem>>(userService.GetAllRoles()),
                User = Mapper.Map<UserDTO,DisplayUserViewModel>(userService.Get(userId))
            });
        }

        [HttpPost]
        public ActionResult Manage(ManageUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleItems =  Mapper.Map<IEnumerable<RoleDTO>,IEnumerable<SelectListItem>>(userService.GetAllRoles());
                return View(model);
            }
            try
            {
                userService.Update(Mapper.Map<ManageUserViewModel, UserDTO>(model));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public ActionResult Delete(int userId)
        {
            try
            {
                userService.Delete(userId);
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("Index", "Account");
        }

        #endregion


        #region roles
        public ActionResult IndexRoles()
        {
            var roles = userService.GetAllRoles();
            return View(Mapper.Map<IEnumerable<RoleDTO>, IEnumerable<DisplayRoleViewModel>>(roles));
        }
               
        public ActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public ActionResult CreateRole(CreateRoleViewModel model)
        {
            try
            {
                userService.CreateRole(Mapper.Map<CreateRoleViewModel, RoleDTO>(model));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("IndexRoles");
        }

        public ActionResult ManageRole(int roleId)
        {
            return View(Mapper.Map<RoleDTO, ManageRoleViewModel>(userService.GetRole(roleId)));
        }

        [HttpPost]
        public ActionResult ManageRole(ManageRoleViewModel model)
        {
            try
            {
                userService.UpdateRole(Mapper.Map<ManageRoleViewModel, RoleDTO>(model));
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("IndexRoles");
        }


        public ActionResult AddClaim(int number)
        {
            return PartialView(new ClaimViewModel()
            {
                Number = number,
                ClaimTypes = Mapper.Map<IEnumerable<string>, IEnumerable<SelectListItem>>(GameStoreClaim.Items),
                Permissions = Mapper.Map<IEnumerable<string>, IEnumerable<SelectListItem>>(Permissions.Items)
            });
        }

        [HttpPost]
        public ActionResult DeleteRole(int roleId)
        {
            try
            {
                userService.DeleteRole(roleId);
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("IndexRoles");
        }

        

        

        #endregion
    }
}
