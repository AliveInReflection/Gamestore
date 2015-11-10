using System;
using System.Collections.Generic;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using Moq;

namespace GameStore.Tests.WebTests
{
    public class Mocks
    {
        private Mock<ICommentService> mockComment;
        private Mock<IGameService> mockGame;
        private Mock<IGenreService> mockGenre;
        private Mock<IPlatformTypeService> mockPlatformType;
        private Mock<IPublisherService> mockPublisher;
        private Mock<IOrderService> mockOrder;
        private Mock<IGameStoreLogger> mockLogger;

        public Mocks()
        {
            mockComment = new Mock<ICommentService>();
            mockGame = new Mock<IGameService>();
            mockGenre = new Mock<IGenreService>();
            mockPlatformType = new Mock<IPlatformTypeService>();
            mockPublisher = new Mock<IPublisherService>();
            mockOrder = new Mock<IOrderService>();
            mockLogger = new Mock<IGameStoreLogger>();

            Initialize();
        }

        public Mock<ICommentService> CommentService { get { return mockComment; } }
        public Mock<IGameService> GameService { get { return mockGame; } }
        public Mock<IGenreService> GenreService { get { return mockGenre; } }
        public Mock<IPlatformTypeService> PlatformTypeService { get { return mockPlatformType; } }
        public Mock<IPublisherService> PublisherService { get { return mockPublisher; } }
        public Mock<IOrderService> OrderService { get { return mockOrder; } }
        public Mock<IGameStoreLogger> Logger { get { return mockLogger; } }


        private void Initialize()
        {
            mockGame.Setup(x => x.GetAll()).Returns(new List<GameDTO>());
            mockGame.Setup(x => x.Get(It.IsAny<string>())).Returns(new GameDTO());
            mockGame.Setup(x => x.Create(It.IsAny<GameDTO>()));
            mockGame.Setup(x => x.Get(It.IsAny<GameFilteringMode>())).Returns(new PaginatedGames());
            mockGame.Setup(x => x.GetCount()).Returns(100);

            mockComment.Setup(x => x.Get(It.IsAny<string>())).Returns(new List<CommentDTO>());
            mockComment.Setup(x => x.Get(It.IsAny<int>())).Returns(new CommentDTO());
            mockComment.Setup(x => x.Create(It.IsAny<CommentDTO>()));

            mockGenre.Setup(x => x.GetAll()).Returns(new List<GenreDTO>());
            mockPlatformType.Setup(x => x.GetAll()).Returns(new List<PlatformTypeDTO>());


            mockPublisher.Setup(x => x.Get(It.IsAny<string>())).Returns(new PublisherDTO());
            mockPublisher.Setup(x => x.Get(It.IsAny<int>())).Returns(new PublisherDTO());
            mockPublisher.Setup(x => x.Create(It.IsAny<PublisherDTO>()));

            mockOrder.Setup(x => x.AddItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>()));
            mockOrder.Setup(x => x.GetCurrent(It.IsAny<string>())).Returns(new OrderDTO());
            mockOrder.Setup(x => x.CalculateAmount(It.IsAny<int>())).Returns(256);
            mockOrder.Setup(x => x.Get(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<OrderDTO>());

            mockLogger.Setup(x => x.Warn(It.IsAny<Exception>()));
        }
    }
}
