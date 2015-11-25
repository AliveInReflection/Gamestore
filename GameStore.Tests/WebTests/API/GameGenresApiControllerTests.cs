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
    public class GameGenresApiControllerTests
    {

        private ServiceMocks mocks;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;


        private GameGenresController controller;


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

            controller = new GameGenresController(mocks.Logger.Object, mocks.GameService.Object);
        }


        #endregion


        [TestMethod]
        public void Api_Games_For_Genre_Get_Status_Code_Ok()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);

            var result = controller.Get(1);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void Api_Games_For_Genre_Exception_Get_Status_Code_Bad_Request()
        {
            TestHelper.CreateRequest(controller, HttpMethod.Get);
            mocks.GameService.Setup(x => x.Get(It.IsAny<int>())).Throws<ValidationException>();

            var result = controller.Get(1);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
