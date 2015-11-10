using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class GameControllerTests
    {
        private Mocks mocks;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;

        private string testGameKey = "CSGO";
        private int testGameId = 1;


        private GameController controller;

        #region Initialize

        private void InitializeMocks()
        {
            context = new Mock<HttpContextBase>();
            request = new Mock<HttpRequestBase>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Session.SessionID).Returns("session");
            context.Setup(c => c.Server.MapPath(It.IsAny<string>())).Returns("/");
        }

        private void InitializeTestEntities()
        {
            
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

            mocks = new Mocks();
            controller = new GameController(mocks.GameService.Object, mocks.GenreService.Object, mocks.PlatformTypeService.Object, mocks.PublisherService.Object, mocks.Logger.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
        }
        #endregion

        [TestMethod]
        public void Game_Index_Model_Is_Not_Null()
        {
            var result = controller.Index(new FilteringViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Details_Model_Is_Not_Null()
        {
            var result = controller.Details(testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Create_Get_Model_Is_Not_Null()
        {
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Create_Post_Is_Redirect_Result()
        {
            var result = controller.Create(new CreateGameViewModel());

             Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Game_Create_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("Test", "Test");
            var result = controller.Create(new CreateGameViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Update_Get_Model_Is_Not_Null()
        {
            var result = controller.Update(testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Update_Post_Is_Redirect_Result()
        {
            var result = controller.Update(new UpdateGameViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Game_Update_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("Test", "Test");
            var result = controller.Update(new UpdateGameViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Delete_Post_Is_Redirect_Result()
        {
            var result = controller.Delete(testGameId);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Game_GetCount_Is_Partial()
        {
            var result = controller.GetCount();

            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void Game_Create_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.GameService.Setup(x => x.Create(It.IsAny<GameDTO>())).Throws<ValidationException>();

            controller.Create(new CreateGameViewModel());

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Game_Delete_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.GameService.Setup(x => x.Delete(It.IsAny<int>())).Throws<ValidationException>();

            controller.Delete(testGameId);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Game_Update_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.GameService.Setup(x => x.Update(It.IsAny<GameDTO>())).Throws<ValidationException>();

            controller.Update(new UpdateGameViewModel());

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

    }
}
