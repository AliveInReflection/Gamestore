using System;
using System.Linq;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork database;

        public OrderService(IUnitOfWork database)
        {
            this.database = database;
        }

        public decimal CalculateAmount(int orderId)
        {
            var entry = database.Orders.Get(m => (m.OrderId.Equals(orderId) && m.OrderState == OrderState.NotIssued));
            decimal amount = entry.OrderDetailses.Sum(orderDetailse => orderDetailse.Product.Price*orderDetailse.Quantity);
            return amount;
        }

        public OrderDTO GetCurrent(string customerId)
        {
            var entry = GetCurrentOrder(customerId);
            return Mapper.Map<Order, OrderDTO>(entry);
        }

        public void Make(int orderId)
        {
            var entry = database.Orders.Get(m => m.OrderId.Equals(orderId) && m.OrderState == OrderState.NotIssued);
            entry.OrderState = OrderState.NotPayed;
            entry.Date = DateTime.UtcNow;
            database.Save();
        }


        public void AddItem(string customerId, string gameKey, short quantity)
        {
            var order = GetCurrentOrder(customerId);
            var game = database.Games.Get(m => m.GameKey.Equals(gameKey));

            var orderDetails = order.OrderDetailses.FirstOrDefault(m => m.Product.GameKey.Equals(gameKey));
            if (orderDetails == null)
            {
                database.OrderDetailses.Create(new OrderDetails()
                {
                    Order = order,
                    Product = game,
                    Discount = 0,
                    Quantity = quantity
                });
            }
            else
            {
                orderDetails.Quantity += quantity;
            }
            database.Save();
        }



        private Order GetCurrentOrder(string customerId)
        {
            Order entry;
            try
            {
                entry = database.Orders.Get(m => m.CustomerId.Equals(customerId) && m.OrderState == OrderState.NotIssued);
            }
            catch (InvalidOperationException)
            {
                var newOrder = new Order()
                {
                    CustomerId = customerId,
                    Date = DateTime.UtcNow,
                    OrderState = OrderState.NotIssued
                };
                database.Orders.Create(newOrder);
                database.Save();
                entry = newOrder;
            }
            return entry;
        }
    }
}
