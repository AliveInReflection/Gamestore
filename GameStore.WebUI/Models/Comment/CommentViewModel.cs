using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class CommentViewModel
    {
        public IEnumerable<DisplayCommentViewModel> Comments { get; set; }
        public CreateCommentViewModel NewComment { get; set; }
    }
}