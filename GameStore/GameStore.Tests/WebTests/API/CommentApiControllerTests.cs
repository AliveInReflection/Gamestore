using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Routing;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.CL.AutomapperProfiles;
using GameStore.DAL.GameStore.Concrete;
using GameStore.Infrastructure.DTO;
using GameStore.Tests.Mocks;
using GameStore.WebUI.ApiControllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests.API
{
    [TestClass]
    public class CommentApiControllerTests
    {
        private ServiceMocks mocks;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;


        private CommentsController controller;


        #region Initialize

        private void InitializeTestEntities()
        {

        }

        private void InitializeMocks()
        {
            context = new Mock<HttpContextBase>();
            request = new Mock<HttpRequestBase>();

            context.Setup(c => c.Request).Returns(request.Object);
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

            controller = new CommentsController(mocks.Logger.Object, mocks.GameService.Object, mocks.CommentService.Object);      
        }

        
        #endregion


        [TestMethod]
        public void Api_Comment_Get_Comments_For_Game_Status_Code_Ok()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);

            var result = controller.Get(1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Get_Comment_By_Id_Status_Code_Ok()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);

            var result = controller.Get(1, 1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Post_Status_Code_Created()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Post);

            var result = controller.Post(1, new CreateCommentViewModel());

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Post_Model_Not_Valid_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Post);
            controller.ModelState.AddModelError("","");

            var result = controller.Post(1, new CreateCommentViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Post_Exception_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Post);
            mocks.CommentService.Setup(x => x.Create(It.IsAny<CommentDTO>())).Throws<ValidationException>();

            var result = controller.Post(1, new CreateCommentViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }


        [TestMethod]
        public void Api_Comment_Put_Status_Code_OK()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Put);

            var result = controller.Put(1, new UpdateCommentViewModel());

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Put_Model_Not_Valid_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Put);
            controller.ModelState.AddModelError("", "");

            var result = controller.Put(1, new UpdateCommentViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Put_Exception_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Put);
            mocks.CommentService.Setup(x => x.Update(It.IsAny<CommentDTO>())).Throws<ValidationException>();

            var result = controller.Put(1, new UpdateCommentViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Delete_Status_Code_OK()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Delete);

            var result = controller.Delete(1, 1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Comment_Delete_Exception_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Delete);
            mocks.CommentService.Setup(x => x.Delete(It.IsAny<int>())).Throws<ValidationException>();

            var result = controller.Delete(1, 1);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }



    }
}
