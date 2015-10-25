using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class CommentControllerTests
    {
        private Mock<ICommentService> mock;
        private List<CommentDTO> comments;

        private string testGameKey = "SCII";

        private CommentController controller;

        #region Initialize

        private void InitializeCollections()
        {
            comments = new List<CommentDTO>
            {
                new CommentDTO() {CommentId = 1, UserId = 1, Content = "Is it miltiplayer only?"},
                new CommentDTO() { CommentId = 2, UserId = 2, Content = "No. It has offline mode to play with bots."},
                new CommentDTO() { CommentId = 3, UserId = 3, Content = "Nice game"}
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<ICommentService>();

            mock.Setup(x => x.Get(It.IsAny<string>())).Returns(comments);
            mock.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<CommentDTO>()));

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
            InitializeCollections();
            InitializeMocks();
            InitializeTestEntities();

            controller = new CommentController(mock.Object);
        }
        #endregion


        [TestMethod]
        public void Comment_List_Model_Is_Not_Null()
        {
            var result = controller.List(testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Create_Get_Returns_Partial()
        {
            var result = controller.Create(testGameKey);

            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void Comment_Create_Get_Model_Is_Not_Null()
        {
            var result = controller.Create(testGameKey) as PartialViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Create_Post_Is_Redirect_Result()
        {
            var result = controller.Create(testGameKey,new CreateCommentViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

    }
}
