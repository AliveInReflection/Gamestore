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
        public Mock<IUnitOfWork> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IUnitOfWork>();
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
                new Comment() {CommentId = 1, SendersName = "Ghost", Content = "Is it miltiplayer only?", Game = games[1]},
                new Comment() { CommentId = 2, SendersName = "Shooter", Content = "No. It has offline mode to play with bots.", Game = games[1]},
                new Comment() { CommentId = 3, SendersName = "Sarah Kerrigan", Content = "Nice game", Game = games[0]}
            };
            comments[0].ChildComments = new List<Comment> { comments[1] };
            comments[1].ParentComment = comments[0];

            games[0].Comments = new List<Comment> { comments[2] };
            games[1].Comments = new List<Comment> { comments[0], comments[1] };

            genres[0].Games = new List<Game> { games[0] };
            genres[1].Games = new List<Game> { games[1] };

            platformTypes[0].Games = new List<Game> { games[0], games[1] };
            platformTypes[1].Games = new List<Game> { games[1] };

            mock.Setup(x => x.Comments.GetAll()).Returns(comments);
            mock.Setup(x => x.Comments.Get(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => comments.FirstOrDefault(predicate.Compile()));
            mock.Setup(x => x.Comments.GetMany(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => comments.Where(predicate.Compile()));
            mock.Setup(x => x.Comments.Add(It.IsAny<Comment>())).Callback((Comment comment) => comments.Add(comment));
            mock.Setup(x => x.Comments.Add(It.IsAny<Comment>())).Callback((Comment comment) => comments.Add(comment));
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Comment_With_Null_Reference()
        {
            CommentDTO commentToAdd = null;
            using (var service = new CommentService(mock.Object))
            {
                service.AddComment("SCII", commentToAdd);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Comment_Without_Senders_Name()
        {
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            var commentToAdd = new CommentDTO()
            {
                CommentId = 5,
                SendersName = null,
                Content = "Test comment",
                GameId = 1
            };
            using (var service = new CommentService(mock.Object))
            {

                service.AddComment("SCII", commentToAdd);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Comment_Without_Content()
        {
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            var commentToAdd = new CommentDTO()
            {
                CommentId = 5,
                SendersName = "Test",
                Content = null,
                GameId = 1
            };
            using (var service = new CommentService(mock.Object))
            {

                service.AddComment("SCII", commentToAdd);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Comment_For_Not_Existed_Game()
        {
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            var commentToAdd = new CommentDTO()
            {
                CommentId = 5,
                SendersName = "Test",
                Content = "Test comment",
                GameId = 1
            };
            using (var service = new CommentService(mock.Object))
            {

                service.AddComment("test", commentToAdd);
            }
        }

        [TestMethod]
        public void Add_Comment_For_Game_By_Key()
        {
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            var commentToAdd = new CommentDTO()
            {
                CommentId = 5,
                SendersName = "Test",
                Content = "Test comment",
                GameId = 1
            };
            var count = comments.Count;
            using (var service = new CommentService(mock.Object))
            {

                service.AddComment("CSGO", commentToAdd);
            }
            
            Assert.AreEqual(comments.Count, count+1);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Get_Comments_For_Not_Existed_Game()
        {
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            using (var service = new CommentService(mock.Object))
            {
                var result = service.GetCommentsByGameKey("test");
            }
        }

        [TestMethod]
        public void Get_Comments_For_Game()
        {
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            using (var service = new CommentService(mock.Object))
            {
                var result = service.GetCommentsByGameKey("SCII");
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void Get_Comments_For_Game_As_IEnumerable_CommentDTO()
        {
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.FirstOrDefault(predicate.Compile()));
            using (var service = new CommentService(mock.Object))
            {
                var result = service.GetCommentsByGameKey("SCII");
                Assert.IsInstanceOfType(result,typeof(IEnumerable<CommentDTO>));
            }
        }


    }
}
