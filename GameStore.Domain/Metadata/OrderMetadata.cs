using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Domain.Entities;
using GameStore.Domain.Static;

namespace GameStore.Domain.Metadata
{
    public partial class OrderMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        [NotMapped]
        public ICollection<OrderDetails> OrderDetailses { get; set; }
    }
}
