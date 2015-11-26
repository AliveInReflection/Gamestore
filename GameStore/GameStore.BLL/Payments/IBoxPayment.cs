using System;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.Enums;

namespace GameStore.BLL.Payments
{
    public class IboxPayment : IPayment
    {
        private int accountNumber;
        public IboxPayment()
        {
            
        }

        public IboxPayment(int accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        public PaymentMode Pay(int orderId, decimal amount)
        {
            return PaymentMode.Ibox;
        }
    }
}