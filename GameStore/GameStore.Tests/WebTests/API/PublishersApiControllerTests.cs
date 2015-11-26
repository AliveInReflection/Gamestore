using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;
using GameStore.Tests.Mocks;
using GameStore.WebUI.ApiControllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests.API
{
    [TestClass]
    public class PublishersApiControllerTests
    {
        private ServiceMocks mocks;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;


        private PublishersController controller;


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

            controller = new PublishersController(mocks.Logger.Object, mocks.PublisherService.Object);
        }


        #endregion


        [TestMethod]
        public void Api_Publishers_Get_Status_Code_Ok()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);

            var result = controller.Get();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Get_By_Id_Status_Code_Ok()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);

            var result = controller.Get(1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Post_Status_Code_Created()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Post);

            var result = controller.Post(new CreatePublisherViewModel());

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Post_Model_Not_Valid_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Post);
            controller.ModelState.AddModelError("", "");

            var result = controller.Post(new CreatePublisherViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Post_Exception_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Post);
            mocks.PublisherService.Setup(x => x.Create(It.IsAny<PublisherDTO>())).Throws<ValidationException>();

            var result = controller.Post(new CreatePublisherViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }


        [TestMethod]
        public void Api_Publishers_Put_Status_Code_OK()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Put);

            var result = controller.Put(1, new UpdatePublisherViewModel());

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Put_Model_Not_Valid_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Put);
            controller.ModelState.AddModelError("", "");

            var result = controller.Put(1, new UpdatePublisherViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Put_Exception_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Put);
            mocks.PublisherService.Setup(x => x.Update(It.IsAny<PublisherDTO>())).Throws<ValidationException>();

            var result = controller.Put(1, new UpdatePublisherViewModel());

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Delete_Status_Code_OK()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Delete);

            var result = controller.Delete(1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publishers_Delete_Exception_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Delete);
            mocks.PublisherService.Setup(x => x.Delete(It.IsAny<int>())).Throws<ValidationException>();

            var result = controller.Delete(1);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
