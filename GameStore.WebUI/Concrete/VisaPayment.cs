using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.Abstract;

namespace GameStore.WebUI.Concrete
{
    public class VisaPayment : IPayment
    {
        public ActionResult Pay(int orderId, decimal amount)
        {
            return new ViewResult() {ViewName = "VisaPayment"};
        }
    }
}