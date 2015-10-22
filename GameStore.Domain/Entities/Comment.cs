﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Domain.Entities
{
    [MetadataType(typeof(CommentMetadata))]
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }

        public virtual Game Game { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual User User { get; set; }

    }
}
