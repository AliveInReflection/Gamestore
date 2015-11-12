using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class UpdatePlatformTypeViewModel
    {
        [HiddenInput]
        public int PlatformTypeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [MinLength(3, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MinLengthError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(ModelRes), Name = "PlatformTypeName")]
        public string PlatformTypeName { get; set; }
    }
}