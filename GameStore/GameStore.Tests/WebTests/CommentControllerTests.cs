using System;
using System.Collections.Generic;
using System.Security.Claims;
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
using GameStore.Tests.Mocks;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class CommentControllerTests
    {
        private ServiceMocks mocks;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;

        private ClaimsIdentity identity;

        private string testGameKey = "SCII";
        private int testCommentId = 1;

        private CommentViewModel commentToSave;

        private CommentController controller;

        #region Initialize

        private void InitializeTestEntities()
        {
            commentToSave = new CommentViewModel()
            {
                GameKey = "BlaBla",
                NewComment = new CreateCommentViewModel()
            };
        }

        private void InitializeMocks()
        {
            context = new Mock<HttpContextBase>();
            request = new Mock<HttpRequestBase>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.User.Identity).Returns(identity);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperWebProfile());
            });

             identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Test"),
                new Claim(ClaimTypes.SerialNumber, "1")
            });

            InitializeMocks();

            InitializeTestEntities();
            mocks = new ServiceMocks();
            controller = new CommentController(mocks.CommentService.Object, mocks.Logger.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
        }
        #endregion


        [TestMethod]
        public void Comment_Index_Get_Model_Is_Not_Null()
        {
            var result = controller.Index(testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Index_Post_Result_Is_Redirect()
        {
            var result = controller.Index(commentToSave);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Comment_Index_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("UserName", "Error");
            
            var result = controller.Index(commentToSave) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Delete_Post_Result_Is_Redirect()
        {
            var result = controller.Delete(testCommentId, testGameKey);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }


        [TestMethod]
        public void Comment_Update_Get_Model_Is_Not_Null()
        {
            var result = controller.Update(testCommentId, testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Update_Post_Result_Is_Redirect()
        {
            var result = controller.Update(new UpdateCommentViewModel(), testGameKey);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Comment_Update_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("UserName", "Error");

            var result = controller.Update(new UpdateCommentViewModel(), testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }


        [TestMethod]
        public void Comment_Index_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.CommentService.Setup(x => x.Create(It.IsAny<CommentDTO>())).Throws<ValidationException>();

            controller.Index(commentToSave);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Comment_Delete_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.CommentService.Setup(x => x.Delete(It.IsAny<int>())).Throws<ValidationException>();

            controller.Delete(testCommentId, testGameKey);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Comment_Update_Get_Exception_Error_Message_Is_Not_Null()
        {
            mocks.CommentService.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            controller.Update(testCommentId, testGameKey);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Comment_Update_Get_Exception_Result_Is_Redirect()
        {
            mocks.CommentService.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            var result = controller.Update(testCommentId, testGameKey);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Comment_Update_Post_Exception_Error_Message_Is_Not_Null()
        {
            mocks.CommentService.Setup(x => x.Update(It.IsAny<CommentDTO>())).Throws<ValidationException>();

            controller.Update(new UpdateCommentViewModel(), testGameKey);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

    }

}
