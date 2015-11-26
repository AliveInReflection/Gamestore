using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models
{
    public class OrderDetailsViewModel
    {

        [Display(ResourceType = typeof(ModelRes), Name = "OrderDetailsProduct")]
        public DisplayGameViewModel Product { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderDetailsQuantity")]
        public short Quantity { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderDetailsDiscount")]
        public float Discount { get; set; } 
    }
}