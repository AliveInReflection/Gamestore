using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Metadata
{
    public partial class OrderDetailsMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderDetailsId { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Required]
        public float Discount { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }


        [NotMapped]
        public Game Product { get; set; }
        [NotMapped]
        public Order Order { get; set; }
    }
}
