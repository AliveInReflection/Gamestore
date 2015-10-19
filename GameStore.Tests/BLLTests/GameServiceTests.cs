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
        public Mock<IUnitOfWork> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IUnitOfWork>();
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
                    SendersName = "Ghost",
                    Content = "Is it miltiplayer only?",
                    Game = games[1]
                },
                new Comment()
                {
                    CommentId = 2,
                    SendersName = "Shooter",
                    Content = "No. It has offline mode to play with bots.",
                    Game = games[1]
                },
                new Comment() {CommentId = 3, SendersName = "Sarah Kerrigan", Content = "Nice game", Game = games[0]}
            };
            comments[0].ChildComments = new List<Comment> {comments[1]};
            comments[1].ParentComment = comments[0];

            games[0].Comments = new List<Comment> {comments[2]};
            games[1].Comments = new List<Comment> {comments[0], comments[1]};

            genres[0].Games = new List<Game> {games[0]};
            genres[1].Games = new List<Game> {games[1]};

            platformTypes[0].Games = new List<Game> {games[0], games[1]};
            platformTypes[1].Games = new List<Game> {games[1]};

            mock.Setup(x => x.Games.GetAll()).Returns(games);
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            mock.Setup(x => x.Games.GetMany(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()));
            mock.Setup(x => x.Games.Add(It.IsAny<Game>())).Callback((Game game) => games.Add(game));
            mock.Setup(x => x.Games.Update(It.IsAny<Game>())).Callback(
                (Game game) =>
                {
                    var entry = games.FirstOrDefault(m => m.GameId.Equals(game.GameId));
                    entry.GameKey = game.GameKey;
                    entry.GameName = game.GameName;
                    entry.Description = game.Description;
                });
            mock.Setup(x => x.Games.Delete(It.IsAny<int>())).Callback(
                (int id) =>
                {                    
                    games.Remove(games.FirstOrDefault(m => m.GameId.Equals(id)));
                });

            mock.Setup(x => x.Genres.Get(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns((Expression<Func<Genre, bool>> predicate) => genres.FirstOrDefault(predicate.Compile()));
            mock.Setup(x => x.PlatformTypes.Get(It.IsAny<Expression<Func<PlatformType, bool>>>()))
                .Returns((Expression<Func<PlatformType, bool>> predicate) => platformTypes.FirstOrDefault(predicate.Compile()));
        }

        #region AddGame
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Game_With_Null_Reference()
        {
            GameDTO gameToAdd = null;
            using (var service = new GameService(mock.Object))
            {
                service.AddGame(gameToAdd);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Game_Without_Game_Name()
        {
            GameDTO gameToAdd = new GameDTO()
            {
                GameId = 5,
                GameName = null,
                GameKey = "TestKey",
                Description = "TestDesc"
            };
            using (var service = new GameService(mock.Object))
            {
                service.AddGame(gameToAdd);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Game_Without_Game_Key()
        {
            GameDTO gameToAdd = new GameDTO()
            {
                GameId = 5,
                GameName = "TestName",
                GameKey = null,
                Description = "TestDesc"
            };
            using (var service = new GameService(mock.Object))
            {
                service.AddGame(gameToAdd);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Game_Without_Description()
        {
            GameDTO gameToAdd = new GameDTO()
            {
                GameId = 5,
                GameName = "TestName",
                GameKey = "TestKey",
                Description = null
            };
            using (var service = new GameService(mock.Object))
            {
                service.AddGame(gameToAdd);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Game_With_Existing_Key()
        {
            GameDTO gameToAdd = new GameDTO()
            {
                GameId = 5,
                GameName = "TestName",
                GameKey = "SCII",
                Description = "Desc"
            };
            using (var service = new GameService(mock.Object))
            {
                service.AddGame(gameToAdd);
            }
        }

        [TestMethod]
        public void Add_Game()
        {
            GameDTO gameToAdd = new GameDTO()
            {
                GameId = 5,
                GameName = "TestName",
                GameKey = "TestKey",
                Description = "Desc"
            };
            using (var service = new GameService(mock.Object))
            {
                var count = games.Count;
                service.AddGame(gameToAdd);
                Assert.AreEqual(games.Count, count+1);
            }
        }
#endregion

        #region EditGame
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Edit_Game_With_Null_Reference()
        {
            GameDTO gameToEdit = null;
            using (var service = new GameService(mock.Object))
            {
                service.EditGame(gameToEdit);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Edit_Game_Without_Game_Name()
        {
            GameDTO gameToEdit = new GameDTO()
            {
                GameId = 1,
                GameName = null,
                GameKey = "TestKey",
                Description = "TestDesc"
            };
            using (var service = new GameService(mock.Object))
            {
                service.EditGame(gameToEdit);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Edit_Game_Without_Game_Key()
        {
            GameDTO gameToEdit = new GameDTO()
            {
                GameId = 5,
                GameName = "TestName",
                GameKey = null,
                Description = "TestDesc"
            };
            using (var service = new GameService(mock.Object))
            {
                service.EditGame(gameToEdit);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Edit_Game_Without_Description()
        {
            GameDTO gameToEdit = new GameDTO()
            {
                GameId = 5,
                GameName = "TestName",
                GameKey = "TestKey",
                Description = null
            };
            using (var service = new GameService(mock.Object))
            {
                service.EditGame(gameToEdit);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Edit_Game_With_Existing_Key()
        {
            GameDTO gameToEdit = new GameDTO()
            {
                GameId = 2,
                GameName = "TestName",
                GameKey = "SCII",
                Description = "Desc"
            };
            using (var service = new GameService(mock.Object))
            {
                service.EditGame(gameToEdit);
            }
        }

        [TestMethod]
        public void Edit_Game()
        {
            GameDTO gameToEdit = new GameDTO()
            {
                GameId = 5,
                GameName = "TestName",
                GameKey = "TestKey",
                Description = "Desc"
            };
            using (var service = new GameService(mock.Object))
            {
                service.EditGame(gameToEdit);
                var entry = games.FirstOrDefault(m => m.GameId.Equals(gameToEdit.GameId));
                Assert.AreEqual(entry.GameName, gameToEdit.GameName);
                Assert.AreEqual(entry.GameKey, gameToEdit.GameKey);
                Assert.AreEqual(entry.Description, gameToEdit.Description);
            }
        }
        #endregion

        #region DeleteGame
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Delete_Not_Existed_Game()
        {
            using (var service = new GameService(mock.Object))
            {
                service.DeleteGame(100);
            }
        }
        [TestMethod]

        public void Delete_Game()
        {
            using (var service = new GameService(mock.Object))
            {
                var count = games.Count;
                service.DeleteGame(1);
                Assert.AreEqual(games.Count, count-1);
            }
        }
        #endregion

        #region GetGame(s)
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Not_Existed_Game_By_Key()
        {            
            using (var service = new GameService(mock.Object))
            {
                service.GetGameByKey("Test");
            }
        }

        [TestMethod]
        public void Get_Game_By_Key()
        {
            using (var service = new GameService(mock.Object))
            {
                var entry = service.GetGameByKey("SCII");
                Assert.IsInstanceOfType(entry, typeof(GameDTO));
            }
        }


        [TestMethod]
        public void Get_All_Games()
        {
            using (var service = new GameService(mock.Object))
            {
                var games = service.GetAllGames();
                Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Games_By_Genre_With_Not_Existed_Genre()
        {
            using (var service = new GameService(mock.Object))
            {
                var games = service.GetGamesByGenre(100);
            }
        }

        [TestMethod]
        public void Get_Games_By_Genre()
        {
            using (var service = new GameService(mock.Object))
            {
                var games = service.GetGamesByGenre(1);
                Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Games_By_Platform_With_Not_Existed_Platform()
        {
            using (var service = new GameService(mock.Object))
            {
                var games = service.GetGamesByPlatformTypes(new List<int>{1, 12});
            }
        }

        [TestMethod]
        public void Get_Games_By_Platform()
        {
            using (var service = new GameService(mock.Object))
            {
                var games = service.GetGamesByPlatformTypes(new List<int> { 1, 2 });
                Assert.IsInstanceOfType(games, typeof(IEnumerable<GameDTO>));
            }
        }

        #endregion

    }
}
