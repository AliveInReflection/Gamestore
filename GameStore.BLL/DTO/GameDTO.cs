using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class GameDTO
    {
        
        public int GameId { get; set; }
        public string GameKey { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Int16 UnitsInStock { get; set; }
        public Boolean Discontinued { get; set; }
        public int PublisherId { get; set; }

        public IEnumerable<GenreDTO> Genres { get; set; }
        public IEnumerable<PlatformTypeDTO> PlatformTypes { get; set; }
        public PublisherDTO Publisher { get; set; }
    }
}
