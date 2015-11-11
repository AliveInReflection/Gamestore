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

        public override void Create(Order entity)
        {
            gameStore.Orders.Create(entity);
        }

        public override void Update(Order entity)
        {
           gameStore.Orders.Update(entity);
        }

        public override void Delete(int id)
        {
            gameStore.Orders.Delete(id);
        }

        public override Order Get(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            var order = gameStore.Orders.GetMany(predicate).FirstOrDefault(m => !m.IsDeleted) ??
                        northwind.Orders.GetAll(new int[]{}).Where(predicate.Compile()).First();

            return order;
        }

        public override IEnumerable<Order> GetAll()
        {
            var orders = gameStore.Orders.GetMany(m => !m.IsDeleted).ToList();
            orders.AddRange(northwind.Orders.GetAll(new int[]{}));

            return orders;
        }

        public override IEnumerable<Order> GetMany(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            var orders = gameStore.Orders.GetMany(predicate).Where(m => !m.IsDeleted).ToList();
            orders.AddRange(northwind.Orders.GetAll(new int[]{}).Where(predicate.Compile()));

            return orders;
        }

        public override int Count()
        {
            var gameStoreCount = gameStore.Orders.GetMany(m => !m.IsDeleted).Count();
            var northwindCount = northwind.Orders.Count(new int[]{});

            return gameStoreCount + northwindCount;
        }

        public override bool IsExists(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            var gameStoreIsExists = gameStore.Orders.GetMany(m => !m.IsDeleted).Any(predicate);
            return gameStoreIsExists ? gameStoreIsExists : northwind.Orders.GetAll(new int[]{}).Any(predicate.Compile());
        }       
    }
}
