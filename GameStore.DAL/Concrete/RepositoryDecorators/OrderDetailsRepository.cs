using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Concrete.RepositoryDecorators;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class OrderDetailsRepository : BaseRepository<OrderDetails>
    {
        public OrderDetailsRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind)
            : base(gameStore, northwind)
        {
            
        }

        public void Create(OrderDetails entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(OrderDetails entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.OrderDetailses.First(m => m.OrderDetailsId.Equals(id));
            entry.IsDeleted = true;
        }

        public OrderDetails Get(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            var orderDetails = context.OrderDetailses.Where(predicate).First(m => !m.IsDeleted);
            BuildOrderDetails(orderDetails);
            return orderDetails;
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            var orderDetailses = context.OrderDetailses.Where(m => !m.IsDeleted).ToList();
            foreach (var orderDetails in orderDetailses)
            {
                BuildOrderDetails(orderDetails);
            }
            return orderDetailses;
        }

        public IEnumerable<OrderDetails> GetMany(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            var orderDetailses = context.OrderDetailses.Where(predicate).Where(m => !m.IsDeleted).ToList();
            foreach (var orderDetails in orderDetailses)
            {
                BuildOrderDetails(orderDetails);
            }
            return orderDetailses;
        }

        public int Count()
        {
            return context.OrderDetailses.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            return context.OrderDetailses.Where(predicate).Any(m => !m.IsDeleted);
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
