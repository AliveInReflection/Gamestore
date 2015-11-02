using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class DisplayCommentViewModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Quote { get; set; }
        public IEnumerable<DisplayCommentViewModel> ChildComments { get; set; }
    }
}