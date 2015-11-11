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

        public override void Create(OrderDetails entity)
        {
            gameStore.OrderDetailses.Create(entity);
        }

        public override void Update(OrderDetails entity)
        {
            gameStore.OrderDetailses.Update(entity);
        }

        public override void Delete(int id)
        {
            gameStore.OrderDetailses.Delete(id);
        }

        public override OrderDetails Get(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            var orderDetails = gameStore.OrderDetailses.GetMany(predicate).First(m => !m.IsDeleted);
            return orderDetails;
        }

        public override IEnumerable<OrderDetails> GetAll()
        {
            var orderDetailses = gameStore.OrderDetailses.GetMany(m => !m.IsDeleted).ToList();
            return orderDetailses;
        }

        public override IEnumerable<OrderDetails> GetMany(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            var orderDetailses = gameStore.OrderDetailses.GetMany(predicate).Where(m => !m.IsDeleted).ToList();
            return orderDetailses;
        }

        public override int Count()
        {
            return gameStore.OrderDetailses.GetMany(m => !m.IsDeleted).Count();
        }

        public override bool IsExists(System.Linq.Expressions.Expression<Func<OrderDetails, bool>> predicate)
        {
            return gameStore.OrderDetailses.GetMany(predicate).Any(m => !m.IsDeleted);
        }

    }
}
