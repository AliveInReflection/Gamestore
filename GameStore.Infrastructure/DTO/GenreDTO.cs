using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.DTO
{
    public class GenreDTO
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public int? ParentGenreId { get; set; }

        public IEnumerable<GameDTO> Games { get; set; }
    }
}
