using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountOldPassword")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MinLengthError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountNewPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MinLengthError")]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "MaxLengthError")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountNewPasswordRepeat")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "PasswordsNotMatch")]
        public string NewPasswordRepeat { get; set; }
    }
}