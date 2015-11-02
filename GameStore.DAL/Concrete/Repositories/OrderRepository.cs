using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Concrete.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private GameStoreContext context;

        public OrderRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(Order entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(Order entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.Comments.First(m => m.CommentId.Equals(id));
            entry.IsDeleted = true;
        }

        public Order Get(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            return context.Orders.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<Order> GetAll()
        {
            return context.Orders.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<Order> GetMany(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            return context.Orders.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.Orders.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Order, bool>> predicate)
        {
            return context.Orders.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
