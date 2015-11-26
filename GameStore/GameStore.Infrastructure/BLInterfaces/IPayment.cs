
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IPayment
    {
        PaymentMode Pay(int orderId, decimal amount);
    }
}
