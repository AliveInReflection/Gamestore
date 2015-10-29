using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class PaginatedGames
    {
        public IEnumerable<GameDTO> Games { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
