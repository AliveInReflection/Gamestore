using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Account
{
    public class ManageUserViewModel
    {
        public IEnumerable<SelectListItem> RoleItems { get; set; }

        public DisplayUserViewModel User { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "AccountRoles")]
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        public IEnumerable<int> Roles { get; set; }

        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}