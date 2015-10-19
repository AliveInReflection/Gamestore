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

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class CommentServiceTest
    {
        private Mock<IRepository<Comment>> commentRepositoryMock;
        private Mock<IRepository<Game>> gameRepositoryMock;
        private Mock<IUnitOfWork> unitOfWorkMock;

        [TestInitialize]
        public void TestInitialize()
        {
            var sc = new Game()
            {
                GameId = 1,
                GameKey = "SC",
                GameName = "StarCraft",
                Description = "StarCraft Description"
            };

            var parentComment = new Comment()
            {
                CommentId = 1,
                SendersName = "Kerrigan",
                Content = "Nice game",
                GameId = 1
            };
            var childComment = new Comment()
            {
                CommentId = 2,
                SendersName = "Ghost",
                Content = "You are right",
                GameId = 1
            };
            parentComment.ChildComments = new List<Comment>{childComment};
            childComment.ParentComment = parentComment;

            gameRepositoryMock = new Mock<IRepository<Game>>();
            //gameRepositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns()
        }
    }
}
