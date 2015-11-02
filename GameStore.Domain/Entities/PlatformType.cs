using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(PlatformTypeMetadata))]
    public partial class PlatformType : BaseEntity
    {
        public int PlatformTypeId { get; set; }
        public string PlatformTypeName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
