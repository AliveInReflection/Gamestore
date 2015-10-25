using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class GenreServiceTests
    {
        private List<Genre> genres;
        private List<Game> games;
        private Mock<IUnitOfWork> mock;
        private string testGameKey;
        private string notExistedGameKey;



        private void InitializeCollections()
        {
            genres = new List<Genre>
            {
                new Genre() {GenreId = 1, GenreName = "RTS",},
                new Genre() {GenreId = 2, GenreName = "Action"}
            };
            games = new List<Game>
            {
                new Game()
                {
                    GameId = 1,
                    GameKey = "SCII",
                    GameName = "StarCraftII",
                    Description = "DescriptionSCII",
                    Genres = new List<Genre> {genres[0]},
                    PlatformTypes = new List<PlatformType>()
                },
                new Game()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                    Genres = new List<Genre> {genres[1]},
                    PlatformTypes = new List<PlatformType>()
                }
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<IUnitOfWork>();


            mock.Setup(x => x.Genres.GetAll()).Returns(genres);
            mock.Setup(x => x.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>())).Returns((Expression<Func<Genre, bool>> predicate) => genres.Where(predicate.Compile()).First());
            mock.Setup(x => x.Genres.GetMany(It.IsAny<Expression<Func<Genre, bool>>>())).Returns((Expression<Func<Genre, bool>> predicate) => genres.Where(predicate.Compile()));
            mock.Setup(x => x.Genres.Create(It.IsAny<Genre>())).Callback((Genre genre) => genres.Add(genre));

            mock.Setup(x => x.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()).First());
        }

        private void InitializeTestEntities()
        {
            testGameKey = "SCII";
            notExistedGameKey = "Test";
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });
            InitializeCollections();
            InitializeMocks();
            InitializeTestEntities();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Get_Genres_By_Game_Key_Expected_Exception()
        {

            var service = new GenreService(mock.Object);

            service.Get(notExistedGameKey);
        }
        

        [TestMethod]
        public void Get_Genres_By_Game_Key_Is_Not_Null()
        {

            var service = new GenreService(mock.Object);

            var result = service.Get(testGameKey);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_All_Genres_Is_Not_Null()
        {

            var service = new GenreService(mock.Object);

            var result = service.GetAll();

            Assert.IsNotNull(result);
        }

    }
}
