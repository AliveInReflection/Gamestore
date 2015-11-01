﻿using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;
using AutoMapper;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class GameControllerTests
    {
        private Mock<IGameService> mockGame;
        private Mock<IGenreService> mockGenre;
        private Mock<IPlatformTypeService> mockPlatformType;
        private Mock<IPublisherService> mockPublisher;

        private List<GenreDTO> genres;
        private List<PlatformTypeDTO> platformTypes;
        private List<GameDTO> games;
        private List<PublisherDTO> publishers;

        private string testGameKey = "CSGO";


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

            mockGame.Setup(x => x.GetAll()).Returns(games);
            mockGame.Setup(x => x.Get(It.IsAny<string>())).Returns(games.First());
            mockGame.Setup(x => x.Create(It.IsAny<GameDTO>(), It.IsAny<IEnumerable<int>>(),It.IsAny<IEnumerable<int>>()));
            mockGame.Setup(x => x.Get(It.IsAny<GameFilteringMode>())).Returns(new PaginatedGames());

            mockGenre.Setup(x => x.GetAll()).Returns(genres);
            mockPlatformType.Setup(x => x.GetAll()).Returns(platformTypes);
            mockPublisher.Setup(x => x.GetAll()).Returns(publishers);
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

            controller = new GameController(mockGame.Object, mockGenre.Object, mockPlatformType.Object, mockPublisher.Object);
        }
        #endregion

        [TestMethod]
        public void Game_List_Model_Is_Not_Null()
        {
            var result = controller.List(new FilteringViewModel()) as ViewResult;

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



    }
}
