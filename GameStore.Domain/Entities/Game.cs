using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }


        public virtual IEnumerable<Comment> Comments { get; set; }
        public virtual IEnumerable<Genre> Genres { get; set; }
        public virtual IEnumerable<PlatformType> PlatformTypes { get; set; }
    }
}
