using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class PublisherViewModel
    {      
        [HiddenInput(DisplayValue = false)]
        public int PublisherId { get; set; }

        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        [Display(Name="Home page")]
        public string HomePage { get; set; }
    }
}