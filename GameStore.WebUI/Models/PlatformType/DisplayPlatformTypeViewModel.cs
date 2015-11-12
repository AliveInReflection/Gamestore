using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    

    

    public class DisplayPlatformTypeViewModel
    {
        [Required]
        public int PlatformTypeId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(ResourceType = typeof(ModelRes), Name = "PlatformTypeName")]
        public string PlatformTypeName { get; set; }
    }
}
