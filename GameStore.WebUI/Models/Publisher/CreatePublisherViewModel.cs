using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class CreatePublisherViewModel
    {
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Home page")]
        public string HomePage { get; set; }
    }
}