using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Account
{
    public class CreateRoleViewModel
    {
        [Display(ResourceType = typeof(ModelRes), Name = "AccountRoleName")]
        public string RoleName { get; set; }

        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}