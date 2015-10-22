using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public int GameId { get; set; }
        public int? ParentId { get; set; }
    }
}
