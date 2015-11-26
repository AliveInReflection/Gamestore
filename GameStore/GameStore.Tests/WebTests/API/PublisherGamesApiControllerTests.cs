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
using GameStore.Tests.Mocks;
using GameStore.WebUI.ApiControllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests.API
{
    [TestClass]
    public class PublisherGamesApiControllerTests
    {
        private ServiceMocks mocks;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;


        private PublisherGamesController controller;


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

            controller = new PublisherGamesController(mocks.Logger.Object, mocks.PublisherService.Object);
        }


        #endregion


        [TestMethod]
        public void Api_Publisher_Games_Get_Status_Code_Ok()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);

            var result = controller.Get(1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Publisher_Games_Exception_Get_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);
            mocks.PublisherService.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            var result = controller.Get(1);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
