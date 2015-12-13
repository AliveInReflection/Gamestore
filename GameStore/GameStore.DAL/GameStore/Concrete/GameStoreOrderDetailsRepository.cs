using System.Data;
using System.Linq;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStoreOrderDetailsRepository : BaseGameStoreRepository<OrderDetails>
    {
        public GameStoreOrderDetailsRepository(GameStoreContext context)
            :base(context)
        {
            
        }

        public override void Delete(int id)
        {
            var entry = context.OrderDetailses.First(m => m.OrderDetailsId.Equals(id));
            context.Entry(entry).State = EntityState.Deleted;
        }

        public override void Update(OrderDetails entity)
        {
            var entry = context.OrderDetailses.First(m => m.OrderDetailsId.Equals(entity.OrderDetailsId));
            Mapper.Map(entity, entry);
        }
    }
}
