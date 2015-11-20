using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Account
{
    public class RegisterViewModel
    {
        public IEnumerable<SelectListItem> CountryItems { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MinLengthError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountUserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountDateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountCountry")]
        public string Country { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MinLengthError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [MinLength(6, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MinLengthError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MaxLengthError")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "PasswordsNotMatch")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountPasswordRepeat")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }
    }
}