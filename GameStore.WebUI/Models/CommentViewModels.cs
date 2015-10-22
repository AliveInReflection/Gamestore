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
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name="Your name")]
        public string SendersName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(400)]
        [Display(Name="Message")]
        public string Content { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int GameId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? ParentCommentId { get; set; }
    }

    public class DisplayCommentViewModel
    {
        public String SendersName { get; set; }
        public String Content { get; set; }
        public IEnumerable<DisplayCommentViewModel> ChildComments { get; set; }
    }
}