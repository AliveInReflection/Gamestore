using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Metadata;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(GameMetadata))]
    public partial class Game
    {
        public int GameId { get; set; }
        public string GameKey { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
    }
}
