﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class UpdatePublisherViewModel
    {
        [HiddenInput]
        public int PublisherId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PublisherCompanyName")]
        public string CompanyName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PublisherDescription")]
        public string Description { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "PublisherDescriptionRu")]
        public string DescriptionRu { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "FieldIsRequired")]
        [Display(ResourceType = typeof(ModelRes), Name = "PublisherHomePage")]
        public string HomePage { get; set; }
    }
}