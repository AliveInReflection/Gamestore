using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(CommentMetadata))]
    public class Comment
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
