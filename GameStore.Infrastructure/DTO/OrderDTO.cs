using System;
using System.Collections.Generic;
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<OrderDetailsDTO> OrderDetailses { get; set; }
    }
}
