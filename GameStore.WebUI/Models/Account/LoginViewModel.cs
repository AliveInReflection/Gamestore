using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountUserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountPassword")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "AccountRememberMe")]
        public bool RememberMe { get; set; }
    }
}