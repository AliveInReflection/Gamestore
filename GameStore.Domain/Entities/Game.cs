using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(GameMetadata))]
    public partial class Game
    {
        public int GameId { get; set; }
        public string GameKey { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Int16 UnitsInStock { get; set; }
        public Boolean Discontinued { get; set; }
        public DateTime PublitingDate { get; set; }
        public DateTime AdditionDate { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
