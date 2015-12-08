using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Models.Order
{
    public class DisplayOrderViewModel
    {
        [Display(ResourceType = typeof(ModelRes), Name = "OrderOrderId")]
        public int OrderId { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderOrderState")]
        public string OrderState { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderDate")]
        public DateTime Date { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderShippedDate")]
        public DateTime ShippedDate { get; set; }

        [Display(ResourceType = typeof(ModelRes), Name = "OrderOrderDetails")]
        public IEnumerable<OrderDetailsViewModel> OrderDetailses { get; set; }

        public decimal Amount { get; set; }
    }
}