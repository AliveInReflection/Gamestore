using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Account
{
    public class ManageRoleViewModel
    {
        public int RoleId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "AccountRoleName")]
        public string RoleName { get; set; }

        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}