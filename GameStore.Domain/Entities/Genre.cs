using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Domain.Entities.Domain.Metadata;

namespace GameStore.Domain.Entities.Domain.Entities.Domain.Entities
{
    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public int? ParentGenreId { get; set; }

        public virtual Genre ParentGenre { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
