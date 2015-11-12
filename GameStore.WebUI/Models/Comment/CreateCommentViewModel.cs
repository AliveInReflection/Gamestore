using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class CreateCommentViewModel
    {
        [Required]
        [Display(ResourceType = typeof(ModelRes), Name = "CommentUserName")]
        public string UserName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(400)]
        [Display(ResourceType = typeof(ModelRes), Name = "CommentContent")]
        public string Content { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? QuoteId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ParentCommentId { get; set; }

    }
}