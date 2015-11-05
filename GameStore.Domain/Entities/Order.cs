using GameStore.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime Date { get; set; }
        public OrderState OrderState { get; set; }


        
        public ICollection<OrderDetails> OrderDetailses { get; set; }
    }

    public enum OrderState
    {
        NotIssued = 1,
        NotPayed = 2,
        CheckingOut = 3,
        Complete = 4
    }
}
