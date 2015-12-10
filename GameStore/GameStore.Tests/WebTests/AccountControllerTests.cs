using AutoMapper;
using GameStore.Auth;
using GameStore.CL.AutomapperProfiles;
using GameStore.Tests.Mocks;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class AccountControllerTests
    {
        private ServiceMocks mocks;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;
        private Mock<IPrincipal> principal;

        private AccountController controller;

        private RegisterViewModel registerModel;

        #region Initialize

        private void InitializeMocks()
        {
            context = new Mock<HttpContextBase>();
            request = new Mock<HttpRequestBase>();
            principal = new Mock<IPrincipal>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.User).Returns(new GameStoreClaimsPrincipal(new List<Claim> 
            { 
                new Claim(ClaimTypes.SerialNumber, "1")
            }));
            request.Setup(r => r.UrlReferrer).Returns(new Uri("http://www.site.com"));
        }

        private void InitializeTestEntities()
        {
            registerModel = new RegisterViewModel()
            {
                UserName = "",
                RepeatPassword = "",
                Password = "",
                DateOfBirth = DateTime.UtcNow,
                Country = CountryManager.Items.Keys.First()
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperWebProfile());
            });
            InitializeMocks();
            InitializeTestEntities();

            mocks = new ServiceMocks();
            controller = new AccountController(mocks.UserService.Object, mocks.AuthService.Object, mocks.Logger.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            
        }
        #endregion

        [TestMethod]
        public void Register_Get_Model_Is_Not_Null()
        {
            var result = controller.Register() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Register_Post_Invalid_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("", "");
            var result = controller.Register(registerModel, null) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Register_Post_Is_Redirect_Result()
        {
            var result = controller.Register(registerModel, null);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Login_Get_Model_Is_Not_Null()
        {
            var result = controller.Login() as ViewResult;

            Assert.IsNotNull(result.Model);
        }


        [TestMethod]
        public void Login_Post_Is_Redirect_Result()
        {
            var result = controller.Login(new LoginViewModel(), "/");

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
        }


        [TestMethod]
        public void Logout_Get_Is_Redirect_Result()
        {
            var result = controller.Logout();

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Details_Get_Model_Is_Not_Null()
        {
            var result = controller.Details() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Change_Password_Get_Model_Is_Not_Null()
        {
            var result = controller.ChangePassword() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Change_Password_Post_Invalid_Model_State_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("", "");
            var result = controller.ChangePassword(new ChangePasswordViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Change_Password_Post_Is_Redirect_Result()
        {
            var result = controller.ChangePassword(new ChangePasswordViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Ban_Get_Model_Is_Not_Null()
        {
            var result = controller.Ban(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Ban_Post_Is_Redirect_Result()
        {
            var result = controller.Ban(1, BanDurationManager.Items.Keys.First());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Manage_Get_Model_Is_Not_Null()
        {
            var result = controller.Manage(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Manage_Post_Invalid_Model_State_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("", "");
            var result = controller.Manage(new ManageUserViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Manage_Post_Is_Redirect_Result()
        {
            var result = controller.Manage(new ManageUserViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Delete_Post_Is_Redirect_Result()
        {
            var result = controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Index_Roles_Get_Model_Is_Not_Null()
        {
            var result = controller.IndexRoles() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Create_Role_Get_Model_Is_Not_Null()
        {
            var result = controller.CreateRole() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Create_Role_Post_Is_Redirect_Result()
        {
            var result = controller.CreateRole(new CreateRoleViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Manage_Role_Get_Model_Is_Not_Null()
        {
            var result = controller.ManageRole(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Manage_Role_Post_Is_Redirect_Result()
        {
            var result = controller.ManageRole(new ManageRoleViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Add_Claim_Get_Is_Partial()
        {
            var result = controller.AddClaim(1) as PartialViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Delete_Role_Post_Is_Redirect_Result()
        {
            var result = controller.DeleteRole(1);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }


        [TestMethod]
        public void Update_Profile_Get_Model_Is_Not_Null()
        {
            var result = controller.UpdateProfile() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Update_Profile_Post_Invalid_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("", "");
            var result = controller.UpdateProfile(new UpdateUserViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Update_Profile_Post_Is_Redirect_Result()
        {
            var result = controller.UpdateProfile(new UpdateUserViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
