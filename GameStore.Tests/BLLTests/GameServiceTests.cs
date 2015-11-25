using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Tests.Mocks;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class GameServiceTests
    {
        private TestCollections collections;

        private UOWMock mock;

        private GameDTO testGame;
        private string notExistedGameKey = "Not existed";
        private int notExistedGameId = 100;
        private IEnumerable<int> testPlatformTypeIds;

        private GameService service;

        #region initialize
        
        private void InitializeTestEntities()
        {
            service = new GameService(mock.UnitOfWork, null);

            testGame = new GameDTO()
            {
                GameId = 1,
                GameName = "TestName",
                GameKey = "TestKey",
                Description = "Desc",
                PublisherId = 1
            };
                  
            testPlatformTypeIds = new int[] {1, 2};
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });
            collections = new TestCollections();
            mock = new UOWMock(collections);
            InitializeTestEntities();
        }
        #endregion

        #region AddGame
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Create_Game_With_Null_Reference_Expected_Exception()
        {
            GameDTO gameToAdd = null;

            service.Create(gameToAdd);
        }


        [TestMethod]
        public void Create_Game()
        {
            var expectedCount = collections.Games.Count + 1;
            service.Create(testGame);

            Assert.AreEqual(expectedCount, collections.Games.Count);
        }
        #endregion

        #region UpdateGame
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Update_Game_With_Null_Reference_Expected_Exception()
        {
            testGame = null;

            service.Update(testGame);
        }

        


        [TestMethod]
        public void Update_Game()
        {
            testGame.GameId = 1;
            testGame.GameKey = collections.Games[0].GameKey;

            service.Update(testGame);

            var entry = collections.Games.First(m => m.GameId.Equals(testGame.GameId));
            Assert.AreEqual(entry.GameName, testGame.GameName);
            Assert.AreEqual(entry.GameKey, testGame.GameKey);
            Assert.AreEqual(entry.Description, testGame.Description);
        }
        #endregion

        #region DeleteGame
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_Not_Existed_Game_Expected_Exception()
        {           
            service.Delete(notExistedGameId);
        }

        [TestMethod]
        public void Delete_Game()
        {
            var expectedCount = collections.Games.Count - 1;
            
            service.Delete(collections.Games[0].GameId);

            Assert.AreEqual(expectedCount, collections.Games.Count);
        }

        

        #endregion

        #region GetGame(s)
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Get_Not_Existed_Game_By_Key_Expected_Exception()
        {
            service.Get(notExistedGameKey);
        }

        [TestMethod]
        public void Get_Game_By_Key()
        {
            var entry = service.Get(collections.Games[0].GameKey);
            Assert.IsInstanceOfType(entry, typeof(GameDTO));
        }


        [TestMethod]
        public void Get_All_Games()
        {
            var games = service.GetAll();
            Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
        }



        [TestMethod]
        public void Get_Games_By_Genre()
        {
                var games = service.GetByGenre(collections.Genres[0].GenreId);
                Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
        }


        [TestMethod]
        public void Get_Games_By_Platform()
        {
            var games = service.Get(testPlatformTypeIds);
            Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
        }

        [TestMethod]
        public void Get_Games_Count()
        {
            
            var count = service.GetCount();
            Assert.AreEqual(collections.Games.Count, count);
        }

        #endregion

    }
}
