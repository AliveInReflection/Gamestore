﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string Quote { get; set; }
        public int? QuoteId { get; set; }
        public int? ParentCommentId { get; set; }
        public int? GameId { get; set; }
        
        public GameDTO Game { get; set; }
        public UserDTO User { get; set; }
        public List<CommentDTO> ChildComments { get; set; } 
    }
}