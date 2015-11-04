using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.Infrastructure.DTO
{
    public class OrderDetailsDTO
    {
        public int OrderDetailsId { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public GameDTO Product { get; set; }
        public OrderDTO Order { get; set; }
    }
}
