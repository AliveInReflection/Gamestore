using GameStore.Domain.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(PublisherMetadata))]
    public partial class Publisher : BaseEntity
    { 
        public int PublisherId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string HomePage { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
