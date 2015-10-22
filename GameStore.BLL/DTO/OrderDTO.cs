using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<OrderDetailsDTO> OrderDetailses { get; set; }
    }
}
