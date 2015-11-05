using System.ComponentModel.DataAnnotations;
using GameStore.Domain.Metadata;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(OrderDetailsMetadata))]
    public class OrderDetails : BaseEntity
    {
        public int OrderDetailsId { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }


        public Game Product { get; set; }
        public Order Order { get; set; }
    }
}
