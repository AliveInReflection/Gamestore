using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.Enums;

namespace GameStore.BLL.Payments
{
    public class VisaPayment : IPayment
    {
        public PaymentMode Pay(int orderId, decimal amount)
        {
            return PaymentMode.Visa;
        }
    }
}