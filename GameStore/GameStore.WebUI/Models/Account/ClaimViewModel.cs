using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.WebUI.Models.Account
{
    public class ClaimViewModel
    {
        public IEnumerable<SelectListItem> ClaimTypes { get; set; }
        public IEnumerable<SelectListItem> Permissions { get; set; }
        public int  Number { get; set; }
        
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}