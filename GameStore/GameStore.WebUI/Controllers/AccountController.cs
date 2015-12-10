using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models.Account;
using HttpContext = System.Web.HttpContext;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private IUserService userService;
        private IAuthenticationService auth;

        public AccountController(IUserService userService, IAuthenticationService auth, IGameStoreLogger logger)
            :base(logger)
        {
            this.userService = userService;
            this.auth = auth;
        }

        #region User
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.ReturnUrl = HttpContext.Request.UrlReferrer.AbsolutePath;
            return View(new RegisterViewModel()
            {
                CountryItems = Mapper.Map<IEnumerable<SelectListItem>>(CountryManager.Items.Keys)
            });
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

            if (userService.IsNameUsed(register.UserName))
            {
                ModelState.AddModelError("UserName", ValidationRes.ValidationUserNameOccupied);
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
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

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Game");
        }

        [Authorize]
        public ActionResult Logout()
        {
            auth.Logout();
            return RedirectToAction("Index", "Game");
        }

        [Authorize]
        public ActionResult Details()
        {
            var userId = int.Parse((User as ClaimsPrincipal).FindFirst(ClaimTypes.SerialNumber).Value);
            var user = Mapper.Map<DisplayUserViewModel>(userService.Get(userId));
            return View(user);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var modelDTO = Mapper.Map<ChangePasswordDTO>(model);
                modelDTO.UserId = int.Parse((User as ClaimsPrincipal).FindFirst(ClaimTypes.SerialNumber).Value);
                userService.ChangePassword(modelDTO);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Details");
        }

        public ActionResult UpdateProfile()
        {
            try
            {
                var user = userService.Get(int.Parse((User as ClaimsPrincipal).FindFirst(ClaimTypes.SerialNumber).Value));

                var userVM = Mapper.Map<UpdateUserViewModel>(user);
                userVM.NotificationMethodItems = Mapper.Map<IEnumerable<SelectListItem>>(NotificationMethodManager.Items.Keys);

                return View(userVM);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
                return RedirectToAction("Details");
            }
            
        }


        [HttpPost]
        public ActionResult UpdateProfile(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.NotificationMethodItems = Mapper.Map<IEnumerable<SelectListItem>>(NotificationMethodManager.Items.Keys);
                return View(model);
            }
            
            try
            {
                userService.Update(Mapper.Map<UserDTO>(model));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
            }

            return RedirectToAction("Details");
        }



        [Claims(GameStoreClaim.Users, Permissions.Ban)]
        public ActionResult Ban(int userId)
        {
            ViewBag.UserId = userId;
            var durations = BanDurationManager.Items.Keys;
            return View("Ban", durations);
        }

        [HttpPost]
        [Claims(GameStoreClaim.Users, Permissions.Ban)]
        public ActionResult Ban(int userId, string duration)
        {
            userService.Ban(userId, BanDurationManager.Items[duration]);
            return RedirectToAction("Index", "Game");
        }


        [Claims(GameStoreClaim.Users, Permissions.Retreive)]
        public ActionResult Index()
        {
            var users = Mapper.Map<IEnumerable<UserDTO>, IEnumerable<DisplayUserViewModel>>(userService.GetAll());
            return View(users);
        }

        [Claims(GameStoreClaim.Users, Permissions.Update)]
        public ActionResult Manage(int userId)
        {
            return View(new ManageUserViewModel()
            {
                RoleItems = Mapper.Map<IEnumerable<RoleDTO>,IEnumerable<SelectListItem>>(userService.GetAllRoles()),
                User = Mapper.Map<UserDTO,DisplayUserViewModel>(userService.Get(userId))
            });
        }

        [HttpPost]
        [Claims(GameStoreClaim.Users, Permissions.Update)]
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [Claims(GameStoreClaim.Users, Permissions.Delete)]
        public ActionResult Delete(int userId)
        {
            try
            {
                userService.Delete(userId);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Index", "Account");
        }

        #endregion


        #region roles
        [Claims(GameStoreClaim.Roles, Permissions.Retreive)]
        public ActionResult IndexRoles()
        {
            var roles = userService.GetAllRoles();
            return View(Mapper.Map<IEnumerable<RoleDTO>, IEnumerable<DisplayRoleViewModel>>(roles));
        }

        [Claims(GameStoreClaim.Roles, Permissions.Create)]
        public ActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        [Claims(GameStoreClaim.Roles, Permissions.Create)]
        public ActionResult CreateRole(CreateRoleViewModel model)
        {
            try
            {
                userService.CreateRole(Mapper.Map<CreateRoleViewModel, RoleDTO>(model));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("IndexRoles");
        }

        [Claims(GameStoreClaim.Roles, Permissions.Update)]
        public ActionResult ManageRole(int roleId)
        {
            return View(Mapper.Map<RoleDTO, ManageRoleViewModel>(userService.GetRole(roleId)));
        }

        [HttpPost]
        [Claims(GameStoreClaim.Roles, Permissions.Update)]
        public ActionResult ManageRole(ManageRoleViewModel model)
        {
            try
            {
                userService.UpdateRole(Mapper.Map<ManageRoleViewModel, RoleDTO>(model));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
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
        [Claims(GameStoreClaim.Roles, Permissions.Delete)]
        public ActionResult DeleteRole(int roleId)
        {
            try
            {
                userService.DeleteRole(roleId);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("IndexRoles");
        }

        

        

        #endregion
    }
}
