using GameStore.BLL.DTO;
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


        [TestInitialize]
        public void Initialize()
        {
            mockGame = new Mock<IGameService>();
            mockComment = new Mock<ICommentService>();
            games = new List<GameDTO>()
            {
                new GameDTO()
                {
                    GameId = 1,
                    GameKey = "SCII",
                    GameName = "StarCraftII",
                    Description = "DescriptionSCII",
                },
                new GameDTO()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                }
            };

            mockGame.Setup(x => x.Get(It.IsAny<string>())).Returns(games[0]);
        }

        



    }
}
