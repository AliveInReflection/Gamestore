using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Northwind.Interfaces;
using GamestoreOrder = GameStore.Domain.Entities.Order;

namespace GameStore.DAL.Northwind.Concrete
{
    public class NorthwindOrderRepository : INorthwindRepository<GamestoreOrder>
    {
        private NorthwindContext context;

        public NorthwindOrderRepository(NorthwindContext context)
        {
            this.context = context;
        }

        public IEnumerable<GamestoreOrder> GetAll(IEnumerable<int> idsToExclude)
        {
            var orders = context.Orders.Where(m => !idsToExclude.Contains(m.OrderID));
            return Mapper.Map<IEnumerable<Order>, IEnumerable<GamestoreOrder>>(orders);
        }

        public GamestoreOrder Get(int id)
        {
            var order = context.Orders.First(m => m.OrderID.Equals(id));
            return Mapper.Map<Order, GamestoreOrder>(order);
        }
    }
}
