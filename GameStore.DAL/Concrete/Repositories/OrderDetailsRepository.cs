using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class OrderDetailsRepository : IRepository<OrderDetails>
    {
        private GameStoreContext context;

        public OrderDetailsRepository(GameStoreContext context)
        {
            this.context = context;
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
            return context.OrderDetailses.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            return context.OrderDetailses.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<OrderDetails> GetMany(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            return context.OrderDetailses.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.OrderDetailses.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            return context.OrderDetailses.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
