using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class UpdateCommentViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CommentId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ParentCommentId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? QuoteId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? GameId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(400)]
        [Display(ResourceType = typeof(ModelRes), Name = "CommentContent")]
        public string Content { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(1000)]
        [Display(ResourceType = typeof(ModelRes), Name = "CommentQuote")]
        public string Quote { get; set; }


    }
}