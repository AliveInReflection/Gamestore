using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GameStore.WebUI.Abstract
{
    public interface IPayment
    {
        ActionResult Pay(int orderId, decimal amount);
    }
}
