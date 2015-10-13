using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities.Domain.Metadata;

namespace GameStore.Domain.Entities.Domain.Entities.Domain.Entities
{
    [MetadataType(typeof(CommentMetadata))]
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string SendersName { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int? ParentId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; }

    }
}
