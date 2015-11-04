﻿using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IOrderService
    {
        decimal CalculateAmount(int orderId);
        OrderDTO GetCurrent(string customerId);
        void AddItem(string customerId, string gameId, short quantity);
        IPayment Make(int orderId, string paymentKey);
    }
}