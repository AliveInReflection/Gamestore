using GameStore.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime Date { get; set; }
        public OrderState OrderState { get; set; }

        public virtual ICollection<OrderDetails> OrderDetailses { get; set; }
    }

    public enum OrderState
    {
        NotIssued = 1,
        NotPayed = 2,
        CheckingOut = 3,
        Complete = 4
    }
}
