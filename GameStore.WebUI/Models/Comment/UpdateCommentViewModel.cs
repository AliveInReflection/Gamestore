using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class UpdateCommentViewModel
    {
        [HiddenInput]
        public int CommentId { get; set; }

        [HiddenInput]
        public int? ParentCommentId { get; set; }

        [HiddenInput]
        public int? QuoteId { get; set; }

        [HiddenInput]
        public int UserId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(400)]
        [Display(Name = "Message")]
        public string Content { get; set; }


    }
}