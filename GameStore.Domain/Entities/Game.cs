using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(GameMetadata))]
    public partial class Game : BaseEntity
    {
        public int GameId { get; set; }
        public string GameKey { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public short UnitsInStock { get; set; }
        public bool Discontinued { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ReceiptDate { get; set; }

        public int PublisherId { get; set; }
        
        public virtual ICollection<PlatformType> PlatformTypes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
