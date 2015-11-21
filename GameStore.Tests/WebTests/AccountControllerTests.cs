using AutoMapper;
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

        private AccountController controller;

        private RegisterViewModel registerModel;

        #region Initialize

        private void InitializeMocks()
        {
            context = new Mock<HttpContextBase>();
            request = new Mock<HttpRequestBase>();

            context.Setup(c => c.Request).Returns(request.Object);
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
    }
}
