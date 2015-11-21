using System;
using System.Collections.Generic;
using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IOrderService
    {
        /// <summary>Returns amount for specified order</summary>
        /// <param name="orderId">Order id</param>
        /// <returns>Amount</returns>
        /// <exception>ValidationException</exception>
        decimal CalculateAmount(int orderId);

        /// <summary>Returns order for current user. Creates in order not exists</summary>
        /// <param name="customerId">customer id</param>
        /// <returns>Order</returns>
        /// <exception>ValidationException</exception>
        OrderDTO GetCurrent(int customerId);

        /// <summary>Add new item to basket</summary>
        /// <param name="customerId">customer id</param>
        /// <param name="gameId">product id</param>
        /// <param name="quantity">quantity of selected product(1 by default)</param>
        /// <exception>ValidationException</exception>
        void AddItem(int customerId, string gameId, short quantity);

        /// <summary>Changes status of specified order to not payed</summary>
        /// <param name="orderId">order id</param>
        /// <param name="paymentKey">key of selected payment type</param>
        /// <exception>ValidationException</exception>
        IPayment Make(int orderId, string paymentKey);

        /// <summary>Returns orders between 2 dates(only completed orders)</summary>
        /// <param name="dateFrom">Date from</param>
        /// <param name="dateTo">Date to</param>
        /// <exception>ValidationException</exception>
        IEnumerable<OrderDTO> Get(DateTime dateFrom, DateTime dateTo);

        /// <summary>Changes order state to shipped</summary>
        /// <param name="orderId">Order id</param>
        /// <exception>ValidationException</exception>
        void ChangeState(int orderId);
    }
}
