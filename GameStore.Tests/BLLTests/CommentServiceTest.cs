using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.DAL.Interfaces;
using System.Linq.Expressions;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.BLL.DTO;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class CommentServiceTest
    {
        private List<Genre> genres;
        private List<PlatformType> platformTypes;
        private List<Game> games;
        private List<Comment> comments;
        private Mock<IUnitOfWork> mock;
        private CommentDTO commentToAdd;
        private string testGameKey;
        private string notExistedGameKey;


        private void InitializeCollections()
        {
            genres = new List<Genre>
            {
                new Genre() {GenreId = 1, GenreName = "RTS", },
                new Genre() {GenreId = 2, GenreName = "Action"}
            };

            platformTypes = new List<PlatformType>
            {
                new PlatformType(){PlatformTypeId = 1, PlatformTypeName = "Desktop"},
                new PlatformType(){PlatformTypeId = 2, PlatformTypeName = "Console"},
            };

            games = new List<Game>
           {
               new Game(){GameId = 1, GameKey = "SCII", GameName = "StarCraftII", Description = "DescriptionSCII", Genres = new List<Genre>{genres[0]}, PlatformTypes = new List<PlatformType>{platformTypes[0]}},
               new Game(){GameId = 1, GameKey = "CSGO", GameName = "Counter strike: global offencive", Description = "DescriptionCSGO", Genres = new List<Genre>{genres[1]}, PlatformTypes = new List<PlatformType>{platformTypes[0],platformTypes[1]}}
           };

            comments = new List<Comment>
            {
                new Comment() {CommentId = 1, UserId = 1, Content = "Is it miltiplayer only?", Game = games[1]},
                new Comment() { CommentId = 2, UserId = 2, Content = "No. It has offline mode to play with bots.", Game = games[1]},
                new Comment() { CommentId = 3, UserId = 3, Content = "Nice game", Game = games[0]}
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


            mock.Setup(x => x.Comments.GetAll()).Returns(comments);
            mock.Setup(x => x.Comments.GetSingle(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => comments.FirstOrDefault(predicate.Compile()));
            mock.Setup(x => x.Comments.GetMany(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => comments.Where(predicate.Compile()));
            mock.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback((Comment comment) => comments.Add(comment));
            mock.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback((Comment comment) => comments.Add(comment));
            mock.Setup(x => x.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
        }

        private void InitializeTestEntities()
        {
            commentToAdd = new CommentDTO()
            {
                CommentId = 5,
                UserId = "Test",
                Content = "Test comment",
                GameId = 1
            };

            testGameKey = "SCII";

            notExistedGameKey = "Not Existed";
        }

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeCollections();
            InitializeMocks();
            InitializeTestEntities();
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Comment_With_Null_Reference_Expected_Exception()
        {
            CommentDTO commentToAdd = null;

            var service = new CommentService(mock.Object);

            service.Create(testGameKey, commentToAdd);
        }

       
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Add_Comment_For_Not_Existed_Game_Expected_Exception()
        {
            var service = new CommentService(mock.Object);
            
            service.Create(notExistedGameKey, commentToAdd);
        }

        [TestMethod]
        public void Add_Comment_For_Game_By_Key()
        {
            var service = new CommentService(mock.Object);
            var game = games.First(m => m.GameKey.Equals(testGameKey));
            var expectedCount = game.Comments.Count + 1;
            service.Create(testGameKey, commentToAdd);

            Assert.AreEqual(expectedCount, game.Comments.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Get_Comments_For_Not_Existed_Game_Expected_Exception()
        {
            var service = new CommentService(mock.Object);

            var result = service.Get(notExistedGameKey);
        }

        [TestMethod]
        public void Get_Comments_For_Game()
        {
            var service = new CommentService(mock.Object);
            
            var result = service.Get(testGameKey);
               
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_Comments_For_Game_As_IEnumerable_CommentDTO()
        {
            var service = new CommentService(mock.Object);
            
            var result = service.Get(testGameKey);
            
            Assert.IsInstanceOfType(result,typeof(IEnumerable<CommentDTO>));
        }


    }
}
