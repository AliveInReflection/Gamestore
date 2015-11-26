using System.ComponentModel.DataAnnotations;
using GameStore.Domain.Metadata;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(ViewMetadata))]
    public partial class View : BaseEntity
    {
        public int ViewId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
