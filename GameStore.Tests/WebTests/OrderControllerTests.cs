using System.Collections.Generic;
using AutoMapper;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderService> mock;
        private List<OrderDTO> orders;
        private string testGameKey = "SCII"; 


        private OrderController controller;

        #region Initialize

        private void InitializeCollections()
        {
            
        }

        private void InitializeMocks()
        {
            mock = new Mock<IOrderService>();
            mock.Setup(x => x.AddItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>()));
            mock.Setup(x => x.GetCurrent(It.IsAny<string>())).Returns(new OrderDTO());
            mock.Setup(x => x.CalculateAmount(It.IsAny<int>())).Returns(256);
        }

        private void InitializeTestEntities()
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperWebProfile());
            });
            InitializeCollections();
            InitializeMocks();
            InitializeTestEntities();

            controller = new OrderController(mock.Object, null);
        }
        #endregion

        

    }
}
