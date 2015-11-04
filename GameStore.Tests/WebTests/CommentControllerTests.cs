using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
                new CommentDTO() {CommentId = 1, UserName = "Pol", Content = "Is it miltiplayer only?"},
                new CommentDTO() { CommentId = 2, UserName = "John", Content = "No. It has offline mode to play with bots."},
                new CommentDTO() { CommentId = 3, UserName = "Lissa", Content = "Nice game"}
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<ICommentService>();

            mock.Setup(x => x.Get(It.IsAny<string>())).Returns(comments);
            mock.Setup(x => x.Create(It.IsAny<CommentDTO>()));

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

            controller = new CommentController(mock.Object, null);
        }
        #endregion


        [TestMethod]
        public void Comment_List_Model_Is_Not_Null()
        {
            var result = controller.Index(testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }


    }

}
