using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class DisplayOrderViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderId { get; set; }

        [Display(Name="Customer")]
        public string CustomerId { get; set; }

        [Display(Name="Date")]
        public DateTime Date { get; set; }

        public IEnumerable<DisplayOrderDetailsViewModel> OrderDetailses { get; set; }
    }
}