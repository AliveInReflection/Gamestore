using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IOrderService
    {
        decimal CalculateAmount(int orderId);
        OrderDTO GetCurrent(string customerId);
        void AddItem(string customerId, string gameId, short quantity);
        void Make(int orderId);
    }
}
