using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class OrderDetailsViewModel
    {

        [Display(Name="Game key")]
        public DisplayGameViewModel Product { get; set; }

        [Display(Name="Quantity")]
        public short Quantity { get; set; }

        [Display(Name="Discount")]
        public float Discount { get; set; } 
    }
}