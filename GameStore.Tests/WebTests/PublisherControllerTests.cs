using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Concrete;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class PublisherControllerTests
    {
        private Mock<IPublisherService> mock;
        private List<PublisherDTO> publishers;

        private string testCompanyName = "Blizzard";
        private int testPublisherId = 1;

        private PublisherController controller;

        #region Initialize

        private void InitializeCollections()
        {
            publishers = new List<PublisherDTO>
            {
                new PublisherDTO()
                {
                    PublisherId = 1,
                    CompanyName = "Blizzard",
                    Description = "The best company in the world",
                    HomePage = "battle.net"
                },
                new PublisherDTO()
                {
                    PublisherId = 2,
                    CompanyName = "Electronic Arts",
                    Description = "Only fast",
                    HomePage = "www.needforspeed.com"
                }
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<IPublisherService>();

            mock.Setup(x => x.Get(It.IsAny<string>())).Returns(publishers[0]);
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns(publishers[0]);
            mock.Setup(x => x.Create(It.IsAny<PublisherDTO>()));

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

            controller = new PublisherController(mock.Object, new NLogAdapter());
        }
        #endregion


        [TestMethod]
        public void Publisher_Details_Model_Is_Not_Null()
        {
            var result = controller.Details(testCompanyName) as ViewResult;

            Assert.IsNotNull(result.Model);
        }


        [TestMethod]
        public void Publisher_Create_Get_Model_Is_Not_Null()
        {
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Create_Post_Is_Redirect_Result()
        {
            var result = controller.Create(new CreatePublisherViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Publisher_Update_Get_Model_Is_Not_Null()
        {
            var result = controller.Update(testPublisherId) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Comment_Update_Post_Is_Redirect_Result()
        {
            var result = controller.Update(new UpdatePublisherViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Comment_Delete_Post_Is_Redirect_Result()
        {
            var result = controller.Delete(testPublisherId);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
