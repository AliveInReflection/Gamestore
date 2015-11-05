using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(CommentMetadata))]
    public class Comment : BaseEntity
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Quote { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual User User { get; set; }

    }
}
