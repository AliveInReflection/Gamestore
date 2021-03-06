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

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class GameControllerTests
    {
        private Mock<IGameService> mockGame;
        private Mock<ICommentService> mockComment;
        private List<GameDTO> games;


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

            mockGame.Setup(x => x.GetGameByKey(It.IsAny<string>())).Returns(games[0]);
        }

        [TestMethod]
        public void Game_Controller_Details_Result_Is_Json()
        {
            using (var controller = new GameController(mockGame.Object, mockComment.Object))
            {
                Assert.IsInstanceOfType(controller.Details("key"), typeof(JsonResult));
            }
        }

        [TestMethod]
        public void Game_Controller_Details_Result_Data_Is_Not_Null()
        {
            using (var controller = new GameController(mockGame.Object, mockComment.Object))
            {
                Assert.IsNotNull((controller.Details("key") as JsonResult).Data);
            }
        }



    }
}
