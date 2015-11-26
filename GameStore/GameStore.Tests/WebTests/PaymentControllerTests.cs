using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Tests.Mocks;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using GameStore.WebUI.Models.Payment;
using Moq;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class PaymentControllerTests
    {
        private ServiceMocks mocks;

        private PaymentController controller;

        private CardPaymentInfoViewModel info;

        #region Initialize
        private void InitializeTestEntities()
        {
            info = new CardPaymentInfoViewModel()
            {
                CardType = "Visa"
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperWebProfile());
            });

            InitializeTestEntities();

            mocks = new ServiceMocks();
            controller = new PaymentController(mocks.Logger.Object, mocks.CreditCardService.Object, mocks.OrderService.Object);
        }
        #endregion



        [TestMethod]
        public void Payment_Pay_Card_Get_Model_Is_Not_Null()
        {
            var result = controller.PayCard(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Payment_Pay_Card_Post_Invalid_Model_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("","");
            var result = controller.PayCard(info) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Payment_Pay_Card_Post_Redirects_To_Confirmation()
        {
            var result = controller.PayCard(info);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Payment_Pay_Card_Post_Exception_Redirect()
        {
            mocks.OrderService.Setup(x => x.CalculateAmount(It.IsAny<int>())).Throws<ValidationException>();
            var result = controller.PayCard(info);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }


        [TestMethod]
        public void Payment_Confirm_Card_Get_Model_Is_Not_Null()
        {
            var result = controller.ConfirmCard("",1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Payment_Confirm_Card_Post_Invalid_Model_Result_Model_Is_Not_Null()
        {
            controller.ModelState.AddModelError("", "");
            var result = controller.ConfirmCard(new CardPaymentConfirmationViewModel()) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void Payment_Confirm_Card_Post_Redirects_To_Main()
        {
            var result = controller.ConfirmCard(new CardPaymentConfirmationViewModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Payment_Confirm_Card_Post_Failed_Result_Model_Is_Not_Null()
        {
            mocks.CreditCardService.Setup(x => x.Confirm(It.IsAny<string>(),It.IsAny<string>())).Returns(CardConfirmationStatus.Failed);
            var result = controller.PayCard(info);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }
    }
}
