using AutoMapper;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class OrderServiceTests
    {
        private TestCollections collections;
        public UOWMock mock;


        private string testPaymentKey = "Visa";
        private string testCustomerId = "1";


        private OrderService service;

        #region initialize
        

        private void InitializeTestEntities()
        {

            service = new OrderService(mock.UnitOfWork);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });
            collections = new TestCollections();
            mock = new UOWMock(collections);
            InitializeTestEntities();
        }
        #endregion

        [TestMethod]
        public void Calculate_Amount()
        {
            var expectedAmount = 149.98m;

            var amount = service.CalculateAmount(1);

            Assert.AreEqual(expectedAmount, amount);
        }

        [TestMethod]
        public void Get_Current()
        {
            var order = service.GetCurrent(testCustomerId);

            Assert.IsNotNull(order);
        }

        [TestMethod]
        public void Make_Order()
        {
            service.Make(1,testPaymentKey);

            Assert.AreEqual(OrderState.NotPayed, collections.Orders[0].OrderState);
        }

        [TestMethod]
        public void Add_Existed_Item()
        {
            var expectedCount = collections.Orders[0].OrderDetailses.Count;
            service.AddItem(testCustomerId, collections.Games[0].GameKey, 2);

            Assert.AreEqual(expectedCount, collections.Orders[0].OrderDetailses.Count);
        }

        [TestMethod]
        public void Add_New_Item()
        {
            var expectedCount = collections.OrderDetailses.Count + 1;
            service.AddItem(testCustomerId, collections.Games[1].GameKey, 2);

            Assert.AreEqual(expectedCount, collections.OrderDetailses.Count);
        }

        
    }
}
