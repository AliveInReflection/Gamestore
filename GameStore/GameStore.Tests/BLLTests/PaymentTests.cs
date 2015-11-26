using GameStore.BLL.Payments;
using GameStore.Infrastructure.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class PaymentTests
    {
        private int testOrderId = 1;
        private int testAmount = 1;


        [TestMethod]
        public void Bank_Payment_Pay()
        {
            var payment = new BankPayment();

            var result = payment.Pay(testOrderId, testAmount);

            Assert.AreEqual(PaymentMode.Bank, result);
        }

        [TestMethod]
        public void Bank_Payment_Visa()
        {
            var payment = new VisaPayment();

            var result = payment.Pay(testOrderId, testAmount);

            Assert.AreEqual(PaymentMode.Visa, result);
        }

        [TestMethod]
        public void Bank_Payment_Ibox()
        {
            var payment = new IboxPayment();

            var result = payment.Pay(testOrderId, testAmount);

            Assert.AreEqual(PaymentMode.Ibox, result);
        }
    }
}
