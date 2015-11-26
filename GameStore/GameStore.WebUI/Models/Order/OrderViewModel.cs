using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class OrderViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderCustomerId")]
        public string CustomerId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderDate")]
        public DateTime Date { get; set; }

        public List<OrderDetailsViewModel> OrderDetailses { get; set; }
    }
}