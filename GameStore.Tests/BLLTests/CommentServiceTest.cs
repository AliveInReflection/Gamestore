using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.DAL.Interfaces;
using System.Linq.Expressions;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using AutoMapper;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class CommentServiceTest
    {
        private List<Genre> genres;
        private List<PlatformType> platformTypes;
        private List<Game> games;
        private List<Comment> comments;
        private List<User> users; 
        private Mock<IUnitOfWork> mock;
        private CommentDTO commentToAdd;
        private string testGameKey;
        private string notExistedGameKey;

        private CommentService service;


        private void InitializeCollections()
        {
            users = new List<User>
            {
                new User(){UserId = 1, UserName = "User1"},
                new User(){UserId = 2, UserName = "User2"},
                new User(){UserId = 3, UserName = "User3"},
                new User(){UserId = 4, UserName = "User4"}
            };

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
                new Comment() {CommentId = 1, UserId = 1, Content = "Is it miltiplayer only?", Game = games[1], User = users[3]},
                new Comment() { CommentId = 2, UserId = 2, Content = "No. It has offline mode to play with bots.", Game = games[1], User = users[0]}, 
                new Comment() { CommentId = 3, UserId = 3, Content = "Nice game", Game = games[0], User = users[2]}
            };
            comments[1].ParentComment = comments[2];

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
            mock.Setup(x => x.Comments.Get(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => comments.Where(predicate.Compile()).First());
            mock.Setup(x => x.Comments.GetMany(It.IsAny<Expression<Func<Comment, bool>>>())).Returns((Expression<Func<Comment, bool>> predicate) => comments.Where(predicate.Compile()));
            mock.Setup(x => x.Comments.Create(It.IsAny<Comment>())).Callback((Comment comment) => comments.Add(comment));
            
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()).First());

            mock.Setup(x => x.Users.Get(It.IsAny<Expression<Func<User, bool>>>())).Returns((Expression<Func<User, bool>> predicate) => users.FirstOrDefault(predicate.Compile()));
        }

        private void InitializeTestEntities()
        {
            service = new CommentService(mock.Object);
            commentToAdd = new CommentDTO()
            {
                CommentId = 5,
                UserName = "Sarah",
                Content = "Test comment",
                ChildComments = new List<CommentDTO>()
            };

            testGameKey = "CSGO";

            notExistedGameKey = "Not Existed";
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
        [ExpectedException(typeof(ValidationException))]
        public void Add_Comment_With_Null_Reference_Expected_Exception()
        {
            CommentDTO commentToAdd = null;

            service.Create(commentToAdd);
        }

       

        [TestMethod]
        public void Add_Comment_For_Game_By_Key()
        {
            var expectedCount = comments.Count + 1;
            
            service.Create(commentToAdd);

            Assert.AreEqual(expectedCount, comments.Count);
        }

        [TestMethod]
        public void Get_Comments_By_Game_Key()
        {
            var result = service.Get(testGameKey);

            Assert.IsNotNull(result);
        }

        
         

    }
}
