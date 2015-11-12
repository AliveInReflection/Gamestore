using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class CreateGenreViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [MinLength(3, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MinLengthError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(ModelRes), Name = "GenreGenreName")]
        public string GenreName { get; set; }
    }
}