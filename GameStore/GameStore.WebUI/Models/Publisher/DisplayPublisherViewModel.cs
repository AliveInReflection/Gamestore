using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    

    
    public class DisplayPublisherViewModel
    {      
        [ScaffoldColumn(false)]
        public int PublisherId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "PublisherCompanyName")]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "PublisherDescription")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "PublisherHomePage")]
        public string HomePage { get; set; }
    }
}