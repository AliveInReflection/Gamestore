using System;
using System.Collections.Generic;
using System.Web.Mvc;
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
    public class CommentControllerTests
    {
        private Mock<ICommentService> mock;
        private Mock<IGameStoreLogger> loggerMock;
        private List<CommentDTO> comments;

        private string testGameKey = "SCII";
        private int testCommentId = 1;

        private CommentViewModel commentToSave;

        private CommentController controller;

        #region Initialize

        private void InitializeCollections()
        {
            comments = new List<CommentDTO>
            {
                new CommentDTO() {CommentId = 1, UserName = "Pol", Content = "Is it miltiplayer only?"},
                new CommentDTO() { CommentId = 2, UserName = "John", Content = "No. It has offline mode to play with bots."},
                new CommentDTO() { CommentId = 3, UserName = "Lissa", Content = "Nice game"}
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<ICommentService>();
            loggerMock = new Mock<IGameStoreLogger>();

            mock.Setup(x => x.Get(It.IsAny<string>())).Returns(comments);
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns(new CommentDTO());
            mock.Setup(x => x.Create(It.IsAny<CommentDTO>()));

            loggerMock.Setup(x => x.Warn(It.IsAny<Exception>()));

        }

        private void InitializeTestEntities()
        {
            commentToSave = new CommentViewModel()
            {
                GameKey = "BlaBla",
                NewComment = new CreateCommentViewModel()
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperWebProfile());
            });
            InitializeCollections();
            InitializeMocks();
            InitializeTestEntities();

            controller = new CommentController(mock.Object, loggerMock.Object);
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
            mock.Setup(x => x.Create(It.IsAny<CommentDTO>())).Throws<ValidationException>();

            controller.Index(commentToSave);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Comment_Delete_Post_Exception_Error_Message_Is_Not_Null()
        {
            mock.Setup(x => x.Delete(It.IsAny<int>())).Throws<ValidationException>();

            controller.Delete(testCommentId, testGameKey);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Comment_Update_Get_Exception_Error_Message_Is_Not_Null()
        {
            mock.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            controller.Update(testCommentId, testGameKey);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Comment_Update_Get_Exception_Result_Is_Redirect()
        {
            mock.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            var result = controller.Update(testCommentId, testGameKey);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Comment_Update_Post_Exception_Error_Message_Is_Not_Null()
        {
            mock.Setup(x => x.Update(It.IsAny<CommentDTO>())).Throws<ValidationException>();

            controller.Update(new UpdateCommentViewModel(), testGameKey);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

    }

}
