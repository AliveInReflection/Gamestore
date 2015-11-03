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
using GameStore.Logger.Concrete;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class PublisherControllerTests
    {
        private Mock<IPublisherService> mock;
        private List<PublisherDTO> publishers;

        private string testCompanyName = "Blizzard";

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
    }
}
