using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardService.Static
{
    public enum PaymentStatus
    {
        Successful = 1,
        NotEnoughMoney = 2,
        NotExistedCard = 3,
        Failed = 4
    }
}