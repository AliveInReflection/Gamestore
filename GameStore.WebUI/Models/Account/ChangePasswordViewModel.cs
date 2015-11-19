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
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountOldPassword")]
        public string OldPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountNewPassword")]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ModelRes), Name = "AccountNewPasswordRepeat")]
        public string NewPasswordRepeat { get; set; }
    }
}