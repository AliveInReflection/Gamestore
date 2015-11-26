using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Enums;

namespace GameStore.Domain.Metadata
{
    public partial class OrderMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        public virtual ICollection<OrderDetails> OrderDetailses { get; set; }
    }
}
