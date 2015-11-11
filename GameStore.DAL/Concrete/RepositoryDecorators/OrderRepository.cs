using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gamestore.DAL.Context;
using GameStore.DAL.Concrete.RepositoryDecorators;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind.Interfaces;

namespace GameStore.DAL.Concrete.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind)
            : base(gameStore, northwind)
        {
            
        }

        public void Create(Order entity)
        {
            if (entity.OrderId == 0)
            {
                entity.OrderId = GetId();
            }

            context.Orders.Add(entity);
        }

        public void Update(Order entity)
        {
            var database = KeyManager.GetDatabase(entity.OrderId);

            if (database == DatabaseType.Northwind)
            {
                Create(entity);
                return;
            }

            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var database = KeyManager.GetDatabase(id);

            if (database == DatabaseType.Northwind)
            {
                var order = northwind.Orders.Get(KeyManager.Decode(id));
                order.IsDeleted = true;
                Create(order);
                return;
            }
            var entry = context.Comments.First(m => m.CommentId.Equals(id));
            entry.IsDeleted = true;
        }

        public Order Get(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            var orderIdsToExclude = GetOrderIdsToExclude();

            var order = context.Orders.Where(predicate).FirstOrDefault(m => !m.IsDeleted);
            if (order == null)
            {
                order = northwind.Orders.GetAll(orderIdsToExclude).Where(predicate.Compile()).First();
            }
            else
            {
                BuildOrder(order);
            }

            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            var orderIdsToExclude = GetOrderIdsToExclude();
            var orders = context.Orders.Where(m => !m.IsDeleted).ToList();

            foreach (var order in orders)
            {
                BuildOrder(order);
            }

            orders.AddRange(northwind.Orders.GetAll(orderIdsToExclude));

            return orders;
        }

        public IEnumerable<Order> GetMany(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            var orderIdsToExclude = GetOrderIdsToExclude();
            var orders = context.Orders.Where(predicate).Where(m => !m.IsDeleted).ToList();

            foreach (var order in orders)
            {
                BuildOrder(order);
            }

            orders.AddRange(northwind.Orders.GetAll(orderIdsToExclude).Where(predicate.Compile()));

            return orders;
        }

        public int Count()
        {
            var orderIdsToExclude = GetOrderIdsToExclude();

            var gameStoreCount = context.Orders.Count(m => !m.IsDeleted);
            var northwindCount = northwind.Orders.GetAll(orderIdsToExclude).Count();

            return gameStoreCount + northwindCount;
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            var orderIdsToExclude = GetOrderIdsToExclude();

            var gameStoreIsExists = context.Orders.Where(m => !m.IsDeleted).Any(predicate);
            var northwindIsExists = northwind.Orders.GetAll(orderIdsToExclude).Any(predicate.Compile());

            return gameStoreIsExists || northwindIsExists;
        }

        private int GetId()
        {
            return context.Orders.Select(m => m.OrderId).OrderBy(m => m).ToList().Last() + KeyManager.Coefficient;
        }

        private IEnumerable<int> GetOrderIdsToExclude()
        {
            return context.Orders.ToList().Where(m => KeyManager.GetDatabase(m.OrderId) == DatabaseType.Northwind).Select(m => KeyManager.Decode(m.OrderId));
        }

        private void BuildOrder(Order order)
        {
            var orderDetailses =
                context.OrderDetailses.Where(m => m.OrderId.Equals(order.OrderId)).Where(m => !m.IsDeleted).ToList();

            foreach (var orderDetails in orderDetailses)
            {
                BuildOrderDetails(orderDetails);
            }

            order.OrderDetailses = orderDetailses;
        }

        private void BuildOrderDetails(OrderDetails orderDetails)
        {
            var product = context.Games.FirstOrDefault(m => m.GameId.Equals(orderDetails.ProductId));
            if (product == null)
            {
                product = northwind.Games.Get(KeyManager.Decode(orderDetails.ProductId));
            }
            orderDetails.Product = product;

        }
    }
}
