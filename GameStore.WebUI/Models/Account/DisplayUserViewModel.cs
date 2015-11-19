using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Account
{
    public class DisplayUserViewModel
    {
        [Display(ResourceType = typeof(ModelRes), Name = "AccountUserId")]
        public int UserId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "AccountUserName")]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "AccountRoles")]
        public IEnumerable<string> Roles { get; set; }
    }
}
