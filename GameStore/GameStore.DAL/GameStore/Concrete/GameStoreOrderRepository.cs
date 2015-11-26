using System.Data;
using System.Linq;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStoreOrderRepository : BaseGameStoreRepository<Order>
    {
        public GameStoreOrderRepository(GameStoreContext context)
            :base(context)
        {
            
        }

        public override void Delete(int id)
        {
            var entry = context.Orders.First(m => m.OrderId.Equals(id));
            entry.IsDeleted = true;
        }

        public override void Update(Order entity)
        {
            context.Entry(entity).State  = EntityState.Modified;
        }
    }
}
