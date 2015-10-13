using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int? ParentId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Comment ParentComment { get; set; }
    }
}
