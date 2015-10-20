using System.Collections.Generic;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class GamesControllerTests
    {
        private Mock<IGameService> mockGame;
        private List<GameDTO> games;


        [TestInitialize]
        public void Initialize()
        {
            mockGame = new Mock<IGameService>();
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

            mockGame.Setup(x => x.GetGameByKey(It.IsAny<string>())).Returns(games[0]);
        }

        
    }
}
