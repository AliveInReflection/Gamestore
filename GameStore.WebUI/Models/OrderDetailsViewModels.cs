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
        public string GameKey { get; set; }

        [Display(Name="Quantity")]
        public Int16 Quantity { get; set; }

        [Display(Name="Discount")]
        public float Discount { get; set; } 
    }
}