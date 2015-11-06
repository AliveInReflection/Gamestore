using System;
using System.Collections.Generic;
using GameStore.Domain.Static;
using System.ComponentModel.DataAnnotations;


namespace GameStore.WebUI.Models.Order
{
    public class DisplayOrderViewModel
    {
        [Display(Name = "Order number")]
        public int OrderId { get; set; }

        [Display(Name = "Order state")]
        public OrderState OrderState { get; set; }

        [Display(Name = "Order date")]
        public DateTime Date { get; set; }


        [Display(Name = "Details")]
        public IEnumerable<OrderDetailsViewModel> OrderDetailses { get; set; }
    }
}