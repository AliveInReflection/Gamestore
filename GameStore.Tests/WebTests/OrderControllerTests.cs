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
    public class OrderControllerTests
    {
        private Mock<IGameService> mock;
        private List<GameDTO> games;
        private string testGameKey = "SCII"; 


        private OrderController controller;

        #region Initialize

        private void InitializeCollections()
        {
            games = new List<GameDTO>
            {
                new GameDTO()
                {
                    GameId = 1,
                    GameKey = "SCII",
                    GameName = "StarCraftII",
                    Description = "DescriptionSCII",
                    Genres = new List<GenreDTO>(),
                    PlatformTypes = new List<PlatformTypeDTO>()
                },
                new GameDTO()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                    Genres = new List<GenreDTO>(),
                    PlatformTypes = new List<PlatformTypeDTO>()
                }
            };



        }

        private void InitializeMocks()
        {
            mock = new Mock<IGameService>();
            mock.Setup(x => x.Get(It.IsAny<string>())).Returns(games.First());
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

            controller = new OrderController(mock.Object);
        }
        #endregion

        [TestMethod]
        public void Order_Add_Is_Redirect_Result()
        {
            var result = controller.Add(testGameKey, new OrderViewModel(), 2);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Order_Details_Model_Is_Not_Null()
        {
            var result = controller.Details(new OrderViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }


    }
}
