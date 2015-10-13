using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public int? ParentGenreId { get; set; }

        public virtual Genre ParentGenre { get; set; }
        public virtual IEnumerable<Game> Games { get; set; }
    }
}
