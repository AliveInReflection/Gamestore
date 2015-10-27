using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;

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
            return 45m;
        }

        public OrderDTO GetCurrent(int UserId)
        {
            return new OrderDTO();
        }
    }
}
