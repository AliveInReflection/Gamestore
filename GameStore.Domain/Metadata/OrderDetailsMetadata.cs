using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Metadata
{
    public partial class OrderDetailsMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int OrderDetailsId { get; set; }

        [Required]
        public Int16 Quantity { get; set; }

        [Required]
        public float Discount { get; set; }

    }
}
