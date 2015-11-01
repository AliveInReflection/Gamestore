using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.Domain.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.DTO;
using GameStore.BLL.Services;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class GameServiceTests
    {
        private List<Genre> genres;
        private List<PlatformType> platformTypes;
        private List<Game> games;
        private List<Comment> comments;
        private List<Publisher> publishers; 

        private Mock<IUnitOfWork> mock;

        private GameDTO testGame;
        private string testGameKey;
        private string notExistedGameKey;
        private int testGameId;
        private int notExistedGameId;
        private int testGenreId;
        private int notExistedGenreId;
        private IEnumerable<int> testPlatformTypeIds;
        private IEnumerable<int> notExistedPlatformTypeIds;

        private GameService service;

        #region initialize
        private void InitializeCollections()
        {
            genres = new List<Genre>
            {
                new Genre() {GenreId = 1, GenreName = "RTS",},
                new Genre() {GenreId = 2, GenreName = "Action"}
            };

            platformTypes = new List<PlatformType>
            {
                new PlatformType() {PlatformTypeId = 1, PlatformTypeName = "Desktop"},
                new PlatformType() {PlatformTypeId = 2, PlatformTypeName = "Console"},
            };

            publishers = new List<Publisher>
            {
                new Publisher()
                {
                    PublisherId = 1,
                    CompanyName = "Blizzard",
                    Description = "The best company in the world",
                    HomePage = "battle.net"
                },
                new Publisher()
                {
                    PublisherId = 2,
                    CompanyName = "Electronic Arts",
                    Description = "Only fast",
                    HomePage = "www.needforspeed.com"
                }
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
                    PlatformTypes = new List<PlatformType> {platformTypes[0]}
                },
                new Game()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                    Genres = new List<Genre> {genres[1]},
                    PlatformTypes = new List<PlatformType> {platformTypes[0], platformTypes[1]}
                }
            };

            comments = new List<Comment>
            {
                new Comment()
                {
                    CommentId = 1,
                    Content = "Is it miltiplayer only?",
                    Game = games[1]
                },
                new Comment()
                {
                    CommentId = 2,
                    Content = "No. It has offline mode to play with bots.",
                    Game = games[1]
                },
                new Comment() {CommentId = 3, Content = "Nice game", Game = games[0]}
            };
            comments[1].ParentComment = comments[0];

            games[0].Comments = new List<Comment> { comments[2] };
            games[1].Comments = new List<Comment> { comments[0], comments[1] };

            genres[0].Games = new List<Game> { games[0] };
            genres[1].Games = new List<Game> { games[1] };

            platformTypes[0].Games = new List<Game> { games[0], games[1] };
            platformTypes[1].Games = new List<Game> { games[1] };
            
        }

        private void InitializeMocks()
        {
            mock = new Mock<IUnitOfWork>();


            mock.Setup(x => x.Games.GetAll()).Returns(games);
            mock.Setup(x => x.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()).First());
            mock.Setup(x => x.Games.GetMany(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()));
            mock.Setup(x => x.Games.Create(It.IsAny<Game>())).Callback((Game game) => games.Add(game));
            mock.Setup(x => x.Games.Update(It.IsAny<Game>())).Callback(
                (Game game) =>
                {
                    var entry = games.First(m => m.GameId.Equals(game.GameId));
                    entry.GameKey = game.GameKey;
                    entry.GameName = game.GameName;
                    entry.Description = game.Description;
                });
            mock.Setup(x => x.Games.Delete(It.IsAny<int>())).Callback(
                (int id) =>
                {
                    games.Remove(games.First(m => m.GameId.Equals(id)));
                });

            mock.Setup(x => x.Games.Count()).Returns(games.Count);

            mock.Setup(x => x.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns((Expression<Func<Genre, bool>> predicate) => genres.First(predicate.Compile()));
            
            mock.Setup(x => x.PlatformTypes.GetSingle(It.IsAny<Expression<Func<PlatformType, bool>>>()))
                .Returns((Expression<Func<PlatformType, bool>> predicate) => platformTypes.First(predicate.Compile()));
           
            mock.Setup(x => x.Publishers.GetSingle(It.IsAny<Expression<Func<Publisher, bool>>>()))
                .Returns((Expression<Func<Publisher, bool>> predicate) => publishers.Where(predicate.Compile()).First());

            mock.Setup(x => x.Comments.GetMany(It.IsAny<Expression<Func<Comment, bool>>>()))
                .Returns((Expression<Func<Comment, bool>> predicate) => comments.Where(predicate.Compile()));

            mock.Setup(x => x.Comments.Delete(It.IsAny<int>())).Callback<int>(
                i => comments.Remove(comments.First(m => m.CommentId.Equals(i)))
                );
        }

        private void InitializeTestEntities()
        {
            service = new GameService(mock.Object, null);

            testGame = new GameDTO()
            {
                GameId = 1,
                GameName = "TestName",
                GameKey = "TestKey",
                Description = "Desc",
                PublisherId = 1
            };

            testGameKey = "SCII";            
            notExistedGameKey = "Not existed";

            testGameId = 2;
            notExistedGameId = 100;

            testGenreId = 1;
            notExistedGenreId = 100;

            testPlatformTypeIds = new int[] {1, 2};
            notExistedPlatformTypeIds = new int[] {100, 200};
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
        #endregion

        #region AddGame
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Create_Game_With_Null_Reference_Expected_Exception()
        {
            GameDTO gameToAdd = null;

            service.Create(gameToAdd, new int[] { 1, 2 }, new int[] { 1, 2 });
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Create_Game_With_Existing_Key_Expected_Exception()
        {
            testGame.GameKey = testGameKey;

            service.Create(testGame, new int[] { 1, 2 }, new int[] { 1, 2 });
        }

        [TestMethod]
        public void Create_Game()
        {                     
            var expectedCount = games.Count + 1;
            service.Create(testGame, new int[] {1,2}, new int[] {1,2});

            Assert.AreEqual(expectedCount, games.Count);
        }
        #endregion

        #region UpdateGame
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Game_With_Null_Reference_Expected_Exception()
        {
            testGame = null;

            service.Update(testGame, new int[] { 1, 2 }, new int[] { 1, 2 });
        }

        

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Game_With_Existing_Key_Expected_Exception()
        {
            testGame.GameKey = testGameKey;
            testGame.GameId = 2;

            service.Update(testGame, new int[] {1,2}, new int[] {1,2});
        }

        [TestMethod]
        public void Update_Game()
        {
            testGame.GameId = 1;
            testGame.GameKey = testGameKey;

            service.Update(testGame, new int[] { 1, 2 }, new int[] { 1, 2 });

            var entry = games.First(m => m.GameId.Equals(testGame.GameId));
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
            var expectedCount = games.Count - 1;
            
            service.Delete(testGameId);

            Assert.AreEqual(expectedCount, games.Count);
        }

        [TestMethod]
        public void Delete_Comments_For_Game()
        {
            var commentsCount = comments.Count;
            var gameCommentsCount = games.First(g => g.GameId.Equals(2)).Comments.Count;
            var expectedCount = commentsCount - gameCommentsCount;

            service.Delete(2);
            
            Assert.AreEqual(expectedCount, comments.Count);
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
            var entry = service.Get(testGameKey);
            Assert.IsInstanceOfType(entry, typeof(GameDTO));
        }


        [TestMethod]
        public void Get_All_Games()
        {
            var games = service.GetAll();
            Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Get_Games_By_Genre_With_Not_Existed_Genre_Expected_Exception()
        {
            var games = service.Get(notExistedGenreId);
        }

        [TestMethod]
        public void Get_Games_By_Genre()
        {
                var games = service.Get(testGenreId);
                Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Get_Games_By_Platform_With_Not_Existed_Platform_Expected_Exception()
        {
            var games = service.Get(notExistedPlatformTypeIds);
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
            Assert.AreEqual(2, count);
        }

        #endregion

    }
}
