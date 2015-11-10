using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
    public class GameControllerTests
    {
        private Mock<IGameService> mockGame;
        private Mock<IGenreService> mockGenre;
        private Mock<IPlatformTypeService> mockPlatformType;
        private Mock<IPublisherService> mockPublisher;
        private Mock<IGameStoreLogger> loggerMock;

        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;

        private List<GenreDTO> genres;
        private List<PlatformTypeDTO> platformTypes;
        private List<GameDTO> games;
        private List<PublisherDTO> publishers;

        private string testGameKey = "CSGO";
        private int testGameId = 1;


        private GameController controller;

        #region Initialize

        private void InitializeCollections()
        {
            genres = new List<GenreDTO>
            {
                new GenreDTO() {GenreId = 1, GenreName = "RTS",},
                new GenreDTO() {GenreId = 2, GenreName = "Action"}
            };

            platformTypes = new List<PlatformTypeDTO>
            {
                new PlatformTypeDTO() {PlatformTypeId = 1, PlatformTypeName = "Desktop"},
                new PlatformTypeDTO() {PlatformTypeId = 2, PlatformTypeName = "Console"},
            };

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


            games = new List<GameDTO>
            {
                new GameDTO()
                {
                    GameId = 1,
                    GameKey = "SCII",
                    GameName = "StarCraftII",
                    Description = "DescriptionSCII",
                    Genres = new List<GenreDTO> {genres[0]},
                    PlatformTypes = new List<PlatformTypeDTO> {platformTypes[0]}
                },
                new GameDTO()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                    Genres = new List<GenreDTO> {genres[1]},
                    PlatformTypes = new List<PlatformTypeDTO> {platformTypes[0], platformTypes[1]}
                }
            };

            
            
        }

        private void InitializeMocks()
        {
            mockGame = new Mock<IGameService>();
            mockGenre = new Mock<IGenreService>();
            mockPlatformType = new Mock<IPlatformTypeService>();
            mockPublisher = new Mock<IPublisherService>();
            loggerMock = new Mock<IGameStoreLogger>();

            mockGame.Setup(x => x.GetAll()).Returns(games);
            mockGame.Setup(x => x.Get(It.IsAny<string>())).Returns(games.First());
            mockGame.Setup(x => x.Create(It.IsAny<GameDTO>()));
            mockGame.Setup(x => x.Get(It.IsAny<GameFilteringMode>())).Returns(new PaginatedGames());
            mockGame.Setup(x => x.GetCount()).Returns(100);

            mockGenre.Setup(x => x.GetAll()).Returns(genres);
            mockPlatformType.Setup(x => x.GetAll()).Returns(platformTypes);
            mockPublisher.Setup(x => x.GetAll()).Returns(publishers);

            loggerMock.Setup(x => x.Warn(It.IsAny<Exception>()));

            context = new Mock<HttpContextBase>();
            request = new Mock<HttpRequestBase>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Session.SessionID).Returns("session");
            context.Setup(c => c.Server.MapPath(It.IsAny<string>())).Returns("/");
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

            controller = new GameController(mockGame.Object, mockGenre.Object, mockPlatformType.Object, mockPublisher.Object, loggerMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
        }
        #endregion

        [TestMethod]
        public void Game_Index_Model_Is_Not_Null()
        {
            var result = controller.Index(new FilteringViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Details_Model_Is_Not_Null()
        {
            var result = controller.Details(testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Create_Get_Model_Is_Not_Null()
        {
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Create_Post_Is_Redirect_Result()
        {
            var result = controller.Create(new CreateGameViewModel());

             Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Game_Create_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("Test", "Test");
            var result = controller.Create(new CreateGameViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Update_Get_Model_Is_Not_Null()
        {
            var result = controller.Update(testGameKey) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Update_Post_Is_Redirect_Result()
        {
            var result = controller.Update(new UpdateGameViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Game_Update_Post_Model_State_Is_Not_Valid_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("Test", "Test");
            var result = controller.Update(new UpdateGameViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Game_Delete_Post_Is_Redirect_Result()
        {
            var result = controller.Delete(testGameId);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Game_GetCount_Is_Partial()
        {
            var result = controller.GetCount();

            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void Game_Create_Post_Exception_Error_Message_Is_Not_Null()
        {
            mockGame.Setup(x => x.Create(It.IsAny<GameDTO>())).Throws<ValidationException>();

            controller.Create(new CreateGameViewModel());

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Game_Delete_Post_Exception_Error_Message_Is_Not_Null()
        {
            mockGame.Setup(x => x.Delete(It.IsAny<int>())).Throws<ValidationException>();

            controller.Delete(testGameId);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

        [TestMethod]
        public void Game_Update_Post_Exception_Error_Message_Is_Not_Null()
        {
            mockGame.Setup(x => x.Update(It.IsAny<GameDTO>())).Throws<ValidationException>();

            controller.Update(new UpdateGameViewModel());

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

    }
}
