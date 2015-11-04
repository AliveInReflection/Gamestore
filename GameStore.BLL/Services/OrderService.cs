using System;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

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
            var orderDetailses = database.OrderDetailses.GetMany(m => m.Order.OrderId.Equals(orderId));
            decimal amount = orderDetailses.Sum(orderDetailse => orderDetailse.Product.Price*orderDetailse.Quantity*(decimal)(1-orderDetailse.Discount));
            return amount;
        }

        public OrderDTO GetCurrent(string customerId)
        {
            var entry = GetCurrentOrder(customerId);
            return Mapper.Map<Order, OrderDTO>(entry);
        }

        public IPayment Make(int orderId, string paymentKey)
        {
            var order = database.Orders.Get(m => m.OrderId.Equals(orderId));
            var orderDetailses = database.OrderDetailses.GetMany(m => m.Order.OrderId.Equals(orderId));

            if (!orderDetailses.Any())
            {
                throw new ValidationException(String.Format("Basket is empty ({0})", orderId));
            }

            foreach (var orderDetailse in orderDetailses)
            {
                var game = orderDetailse.Product;
                if (game.UnitsInStock > orderDetailse.Quantity)
                {
                    game.UnitsInStock -= orderDetailse.Quantity;
                }
                else
                {
                    throw new ValidationException(String.Format("Not enough units ({0}({1} items)) in stock to make your order({2} items)",game.GameKey, game.UnitsInStock,orderDetailse.Quantity));
                }
            }

            order.OrderState = OrderState.NotPayed;
            order.Date = DateTime.UtcNow;
            database.Save();

            return PaymentManager.Get(paymentKey);
        }


        public void AddItem(string customerId, string gameKey, short quantity)
        {
            var game = database.Games.Get(m => m.GameKey.Equals(gameKey));

            if (game.UnitsInStock < quantity)
            {
                throw new ValidationException(String.Format("Quantity {0} greater then available games ({1}) in stock", quantity, gameKey));
            }

            var order = GetCurrentOrder(customerId);
                       
            if (!database.OrderDetailses.IsExists(m => m.Order.OrderId.Equals(order.OrderId) && m.Product.GameKey.Equals(gameKey)))
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
                var orderDetails = database.OrderDetailses.Get(m => m.Order.OrderId.Equals(order.OrderId) && m.Product.GameKey.Equals(gameKey));
                orderDetails.Quantity += quantity;
                if (game.UnitsInStock < orderDetails.Quantity)
                {
                    throw new ValidationException(String.Format("Total quantity {0} greater then available games ({1}) in stock", orderDetails.Quantity, gameKey));
                }
            }
            database.Save();
        }

        private Order GetCurrentOrder(string customerId)
        {
            Order entry;                
            if (!database.Orders.IsExists(m => m.CustomerId.Equals(customerId) && m.OrderState == OrderState.NotIssued)) 
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
            else
            {
                entry = database.Orders.Get(m => m.CustomerId.Equals(customerId) && m.OrderState == OrderState.NotIssued);
            }
            return entry;
        }
    }
}
