using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Metadata;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(OrderDetailsMetadata))]
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        public Int16 Quantity { get; set; }
        public float Discount { get; set; }

        public virtual Game Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
