using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(GenreMetadata))]
    public partial class Genre : BaseEntity
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public virtual Genre ParentGenre { get; set; }
        public virtual ICollection<Genre> ChildGenres { get; set; }
        public virtual ICollection<Game> Games { get; set; }
         
    }
}
