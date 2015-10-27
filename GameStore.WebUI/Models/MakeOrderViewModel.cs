using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class MakeOrderViewModel
    {
        public OrderViewModel Order { get; set; }
        public IEnumerable<DisplayPaymentMethodViewModel> PaymentMethods { get; set; }
        public decimal Amount { get; set; }
    }
}