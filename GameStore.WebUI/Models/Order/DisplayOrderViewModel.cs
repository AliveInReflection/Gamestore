using System;
using System.Collections.Generic;
using GameStore.Domain.Static;
using System.ComponentModel.DataAnnotations;
using GameStore.WebUI.App_LocalResources.Localization;


namespace GameStore.WebUI.Models.Order
{
    public class DisplayOrderViewModel
    {
        [Display(ResourceType = typeof(ModelRes), Name = "OrderOrderId")]
        public int OrderId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderOrderState")]
        public OrderState OrderState { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderDate")]
        public DateTime Date { get; set; }


        [Display(ResourceType = typeof(ModelRes), Name = "OrderOrderDetails")]
        public IEnumerable<OrderDetailsViewModel> OrderDetailses { get; set; }
    }
}