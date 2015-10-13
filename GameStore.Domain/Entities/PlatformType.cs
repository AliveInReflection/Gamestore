using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Domain.Entities.Domain.Metadata;

namespace GameStore.Domain.Entities.Domain.Entities.Domain.Entities
{
    [MetadataType(typeof(PlatformTypeMetadata))]
    public class PlatformType
    {
        public int PlatformTypeId { get; set; }
        public string PlatformTypeName { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
