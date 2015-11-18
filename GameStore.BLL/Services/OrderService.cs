using System;
using System.Collections.Generic;
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
            var orderDetailses = database.OrderDetailses.GetMany(m => m.OrderId.Equals(orderId)).ToList();
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
            var orderDetailses = order.OrderDetailses;

            if (!orderDetailses.Any())
            {
                throw new ValidationException(String.Format("Basket is empty ({0})", orderId));
            }

            var gameIds = orderDetailses.Select(m => m.ProductId);
            var games = database.Games.GetMany(x => gameIds.Any(m => m == x.GameId));
            
            foreach (var orderDetails in orderDetailses)
            {
                var game = games.First(m => m.GameId.Equals(orderDetails.ProductId));
                if (game.UnitsInStock >= orderDetails.Quantity)
                {
                    game.UnitsInStock -= orderDetails.Quantity;
                    database.Games.Update(game);
                }
                else
                {
                    throw new ValidationException(String.Format("Not enough units ({0}({1} items)) in stock to make your order({2} items)", game.GameKey, game.UnitsInStock, orderDetails.Quantity));
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
                       
            if (!database.OrderDetailses.IsExists(m => m.OrderId.Equals(order.OrderId) && m.ProductId.Equals(game.GameId)))
            {
                database.OrderDetailses.Create(new OrderDetails()
                {
                    OrderId = order.OrderId,
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


        public IEnumerable<OrderDTO> Get(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom == null || dateTo == null)
            {
                throw new NullReferenceException("No content received");
            }

            var orders = database.Orders.GetMany(m => m.Date > dateFrom && m.Date < dateTo);
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(orders);
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
