using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Payments;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderService> mock;
        private Mock<IGameStoreLogger> loggerMock;
        private Mock<HttpContextBase> context;
        private Mock<HttpRequestBase> request;
        
        private List<OrderDTO> orders;
        private string testGameKey = "SCII";

        private string visaPaymentKey = "Visa";
        private string bankPaymentKey = "Bank";
        private string iboxPaymentKey = "Ibox";


        private OrderController controller;

        #region Initialize

        private void InitializeCollections()
        {
            
        }

        private void InitializeMocks()
        {
            mock = new Mock<IOrderService>();
            loggerMock = new Mock<IGameStoreLogger>();


            mock.Setup(x => x.AddItem(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>()));
            mock.Setup(x => x.GetCurrent(It.IsAny<string>())).Returns(new OrderDTO());
            mock.Setup(x => x.CalculateAmount(It.IsAny<int>())).Returns(256);
            mock.Setup(x => x.Get(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<OrderDTO>());

            
            context = new Mock<HttpContextBase>();
            request = new Mock<HttpRequestBase>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Session.SessionID).Returns("session");

            loggerMock.Setup(x => x.Warn(It.IsAny<Exception>()));
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

            controller = new OrderController(mock.Object, loggerMock.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
        }
        #endregion

        [TestMethod]
        public void Order_Add_Get_Is_Redirect_Result()
        {
            var result = controller.Add("test", 2);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Order_Details_Get_Model_Is_Not_Null()
        {
            var result = controller.Details() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Order_Make_Get_Model_Is_Not_Null()
        {
            var result = controller.Make() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Order_Pay_Visa_Model_Is_VisaPayment_View()
        {
            mock.Setup(x => x.Make(It.IsAny<int>(), It.IsAny<string>())).Returns(new VisaPayment());

            var result = controller.Pay(visaPaymentKey) as ViewResult;

            Assert.AreEqual("VisaPayment", result.ViewName);
        }

        [TestMethod]
        public void Order_Pay_Ibox_Model_Is_VisaPayment_View()
        {
            mock.Setup(x => x.Make(It.IsAny<int>(), It.IsAny<string>())).Returns(new IboxPayment());

            var result = controller.Pay(iboxPaymentKey) as ViewResult;

            Assert.AreEqual("IboxPayment", result.ViewName);
        }

        [TestMethod]
        public void Order_Pay_Bank_Is_File_Result()
        {
            mock.Setup(x => x.Make(It.IsAny<int>(), It.IsAny<string>())).Returns(new BankPayment());

            var result = controller.Pay(iboxPaymentKey);

            Assert.IsInstanceOfType(result, typeof(FileResult));
        }

        [TestMethod]
        public void Order_Get_History_Get_Model_Is_Not_Null()
        {
            var result = controller.GetHistory() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Order_Get_History_Psot_Model_Is_Not_Null()
        {
            var result = controller.GetHistory(new DateTime(1990, 1, 1), new DateTime(2014, 1, 1)) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Order_Add_Get_Exception_Error_Message_Is_Not_Null()
        {
            mock.Setup(x => x.AddItem(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<short>())).Throws<ValidationException>();

            controller.Add("gameKey", 1);

            Assert.IsNotNull(controller.TempData["ErrorMessage"]);
        }

    }
}
