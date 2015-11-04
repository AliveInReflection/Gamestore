using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class OrderServiceTests
    {
        private List<Game> games;
        private List<Order> orders;
        private List<OrderDetails> orderDetailses; 

        public Mock<IUnitOfWork> mock;


        private Order testOrder;
        private string testGameKey;
        private string notOrderedGameKey;


        private OrderService service;

        #region initialize
        private void InitializeCollections()
        {
            games = new List<Game>
            {
                new Game()
                {
                    GameId = 1,
                    GameKey = "SCII",
                    GameName = "StarCraftII",
                    Description = "DescriptionSCII",
                    Price = 10,
                    UnitsInStock = 10,
                    Discontinued = false,
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>()
                },
                new Game()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                    Price = 10,
                    UnitsInStock = 10,
                    Discontinued = false,
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>()
                },
                new Game()
                {
                    GameId = 2,
                    GameKey = "NFS",
                    GameName = "Need for speed",
                    Description = "DescriptionNFS",
                    Price = 10,
                    UnitsInStock = 10,
                    Discontinued = false,
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>()
                }

            };

            orders = new List<Order>
            {
                new Order()
                {
                    OrderId = 1,
                    CustomerId = "1",
                    OrderState = OrderState.NotIssued,
                    Date = DateTime.UtcNow,
                    OrderDetailses = new List<OrderDetails>
                    {
                        new OrderDetails() {OrderDetailsId = 1, Product = games[0], Quantity = 2, Discount = 0},
                        new OrderDetails() {OrderDetailsId = 2, Product = games[1], Quantity = 2, Discount = 0}
                    }
                }
            };

            orderDetailses = new List<OrderDetails>(orders[0].OrderDetailses);

        }

        private void InitializeMocks()
        {
            mock = new Mock<IUnitOfWork>();
            
            mock.Setup(x => x.Games.GetAll()).Returns(games);
            
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()).First());

            mock.Setup(x => x.Orders.Get(It.IsAny<Expression<Func<Order, bool>>>()))
                 .Returns((Expression<Func<Order, bool>> predicate) => orders.Where(predicate.Compile()).First());
            
            mock.Setup(x => x.Orders.Create(It.IsAny<Order>()))
                .Callback((Order order) => orders.Add(order));
            mock.Setup(x => x.OrderDetailses.Create(It.IsAny<OrderDetails>()))
                .Callback((OrderDetails orderDetailse) => orderDetailses.Add(orderDetailse));

        }

        private void InitializeTestEntities()
        {
            testGameKey = "SCII";
            notOrderedGameKey = "NFS";

            service = new OrderService(mock.Object);
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

        [TestMethod]
        public void Calculate_Amount()
        {
            var amount = service.CalculateAmount(1);

            Assert.AreEqual(40,amount);
        }

        [TestMethod]
        public void Get_Current()
        {
            var order = service.GetCurrent("1");

            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void Make_Order()
        {
            service.Make(1);

            Assert.AreEqual(OrderState.NotPayed, orders[0].OrderState);
        }

        [TestMethod]
        public void Add_Existed_Item()
        {
            var expectedCount = orders[0].OrderDetailses.Count;
            service.AddItem("1",testGameKey,2);

            Assert.AreEqual(expectedCount, orders[0].OrderDetailses.Count);
        }

        [TestMethod]
        public void Add_New_Item()
        {
            var expectedCount = orderDetailses.Count + 1;
            service.AddItem("1", notOrderedGameKey, 2);

            Assert.AreEqual(expectedCount, orderDetailses.Count);
        }

        
    }
}
