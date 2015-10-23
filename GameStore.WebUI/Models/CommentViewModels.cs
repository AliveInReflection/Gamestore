using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class CreateCommentViewModel
    {
        [Required]
        [Display(Name="User id")]
        public int UserId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(400)]
        [Display(Name="Message")]
        public string Content { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ParentCommentId { get; set; }

    }

    public class DisplayCommentViewModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public IEnumerable<DisplayCommentViewModel> ChildComments { get; set; }
    }
}