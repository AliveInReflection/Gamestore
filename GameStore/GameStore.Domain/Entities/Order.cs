﻿using GameStore.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Infrastructure.Enums;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(OrderMetadata))]
    public partial class Order : BaseEntity
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ShippedDate { get; set; }
        public OrderState OrderState { get; set; }


        public virtual User Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetailses { get; set; }
    }   
}