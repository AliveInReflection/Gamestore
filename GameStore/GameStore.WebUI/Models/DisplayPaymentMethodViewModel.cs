using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class DisplayPaymentMethodViewModel
    {
        public string PaymentKey { get; private set; }
        public string PictureURL { get; private set; }
        public string Description { get; private set; }
    }
}