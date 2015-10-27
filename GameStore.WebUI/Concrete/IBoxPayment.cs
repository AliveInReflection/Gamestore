using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.WebUI.Abstract;
using System.Web.Mvc;

namespace GameStore.WebUI.Concrete
{
    public class IBoxPayment : IPayment
    {
        private int accountNumber;
        public IBoxPayment()
        {
            
        }

        public IBoxPayment(int accountNumber)
        {
            this.accountNumber = accountNumber;
        }

        public ActionResult Pay(int orderId, decimal amount)
        {
            return new RedirectResult(String.Format("http://ibox.com.ua/{0}?invoice_number={1}&amount={2}",accountNumber,orderId,amount));
        }
    }
}