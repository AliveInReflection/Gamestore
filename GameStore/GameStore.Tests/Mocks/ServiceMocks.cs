using System;
using System.Collections.Generic;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using Moq;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.BLInterfaces.Services;
using GameStore.Infrastructure.Enums;

namespace GameStore.Tests.Mocks
{
    public class ServiceMocks
    {
        private Mock<ICommentService> mockComment;
        private Mock<IGameService> mockGame;
        private Mock<IGenreService> mockGenre;
        private Mock<IPlatformTypeService> mockPlatformType;
        private Mock<IPublisherService> mockPublisher;
        private Mock<IOrderService> mockOrder;
        private Mock<IUserService> mockUser;
        private Mock<IAuthenticationService> mockAuth;
        private Mock<IGameStoreLogger> mockLogger;
        private Mock<ICreditCardService> mockCreditCard;

        private GameDTO game;

        public ServiceMocks()
        {
            mockComment = new Mock<ICommentService>();
            mockGame = new Mock<IGameService>();
            mockGenre = new Mock<IGenreService>();
            mockPlatformType = new Mock<IPlatformTypeService>();
            mockPublisher = new Mock<IPublisherService>();
            mockOrder = new Mock<IOrderService>();
            mockUser = new Mock<IUserService>();
            mockAuth = new Mock<IAuthenticationService>();
            mockLogger = new Mock<IGameStoreLogger>();
            mockCreditCard = new Mock<ICreditCardService>();

            InitializeModels();
            Initialize();
        }

        public Mock<ICommentService> CommentService { get { return mockComment; } }
        public Mock<IGameService> GameService { get { return mockGame; } }
        public Mock<IGenreService> GenreService { get { return mockGenre; } }
        public Mock<IPlatformTypeService> PlatformTypeService { get { return mockPlatformType; } }
        public Mock<IPublisherService> PublisherService { get { return mockPublisher; } }
        public Mock<IOrderService> OrderService { get { return mockOrder; } }
        public Mock<IUserService> UserService { get { return mockUser; } }
        public Mock<IAuthenticationService> AuthService { get { return mockAuth; } }
        public Mock<IGameStoreLogger> Logger { get { return mockLogger; } }
        public Mock<ICreditCardService> CreditCardService { get { return mockCreditCard; } }

        private void Initialize()
        {
            mockGame.Setup(x => x.GetAll()).Returns(new List<GameDTO>());
            mockGame.Setup(x => x.Get(It.IsAny<string>())).Returns(new GameDTO()
                {
                    Genres = new List<GenreDTO>(),
                    PlatformTypes = new List<PlatformTypeDTO>()
                });
            mockGame.Setup(x => x.Create(It.IsAny<GameDTO>()));
            mockGame.Setup(x => x.Get(It.IsAny<GameFilteringMode>(),It.IsAny<GamesSortingMode>(),It.IsAny<PaginationMode>())).Returns(new PaginatedGames());
            mockGame.Setup(x => x.Get(It.IsAny<int>())).Returns(game);
            mockGame.Setup(x => x.Get(It.IsAny<string>())).Returns(game);
            mockGame.Setup(x => x.GetCount()).Returns(100);
            mockGame.Setup(x => x.GetByGenre(It.IsAny<int>())).Returns(new List<GameDTO>());

            mockComment.Setup(x => x.Get(It.IsAny<string>())).Returns(new List<CommentDTO>());
            mockComment.Setup(x => x.Get(It.IsAny<int>())).Returns(new CommentDTO());
            mockComment.Setup(x => x.Create(It.IsAny<CommentDTO>()));

            mockGenre.Setup(x => x.GetAll()).Returns(new List<GenreDTO>());
            mockPlatformType.Setup(x => x.GetAll()).Returns(new List<PlatformTypeDTO>());


            mockPublisher.Setup(x => x.Get(It.IsAny<string>())).Returns(new PublisherDTO());
            mockPublisher.Setup(x => x.Get(It.IsAny<int>())).Returns(new PublisherDTO());
            mockPublisher.Setup(x => x.Create(It.IsAny<PublisherDTO>()));

            mockOrder.Setup(x => x.AddItem(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<short>()));
            mockOrder.Setup(x => x.GetCurrent(It.IsAny<int>())).Returns(new OrderDTO());
            mockOrder.Setup(x => x.CalculateAmount(It.IsAny<int>())).Returns(256);
            mockOrder.Setup(x => x.Get(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<OrderDTO>());

            mockUser.Setup(x => x.Create(It.IsAny<UserDTO>()));
            mockUser.Setup(x => x.Update(It.IsAny<UserDTO>()));
            mockUser.Setup(x => x.Delete(It.IsAny<int>()));
            mockUser.Setup(x => x.Get(It.IsAny<int>())).Returns(new UserDTO() { Country = "Ukraine", Roles = new[]{new RoleDTO()}});
            mockUser.Setup(x => x.Get(It.IsAny<string>())).Returns(new UserDTO());
            mockUser.Setup(x => x.GetAll()).Returns(new[]{new UserDTO()});
            mockUser.Setup(x => x.Ban(It.IsAny<int>(), It.IsAny<TimeSpan>()));
            mockUser.Setup(x => x.IsNameUsed(It.IsAny<string>())).Returns(false);

            mockUser.Setup(x => x.GetAllRoles()).Returns(new[]{new RoleDTO()});
            mockUser.Setup(x => x.GetRole(It.IsAny<int>())).Returns(new RoleDTO());
            mockUser.Setup(x => x.CreateRole(It.IsAny<RoleDTO>()));
            mockUser.Setup(x => x.UpdateRole(It.IsAny<RoleDTO>()));
            mockUser.Setup(x => x.DeleteRole(It.IsAny<int>()));

            mockAuth.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(true);
            mockAuth.Setup(x => x.Logout());

            mockLogger.Setup(x => x.Warn(It.IsAny<Exception>()));

            mockCreditCard.Setup(x => x.Pay(It.IsAny<CardPaymentInfoDTO>()))
                .Returns(CardPaymentStatus.ConfirmationRequired);
           
            mockCreditCard.Setup(x => x.Confirm(It.IsAny<string>(),  It.IsAny<string>()))
                .Returns(CardConfirmationStatus.Successfull);
        }

        private void InitializeModels()
        {
            game = new GameDTO()
            {
                GameId = 1,
                GameKey = "Test",
                GameName = "TestGame",
                Description = "Desc",
                DescriptionRu = "Descru",
                Discontinued = false,
                Genres = new List<GenreDTO>(),
                PlatformTypes = new List<PlatformTypeDTO>(),
                Price = 100,
                PublicationDate = DateTime.UtcNow,
                Publisher = new PublisherDTO(),
                ReceiptDate = DateTime.UtcNow,
                UnitsInStock = 100
            };
        }

        
    }
}
