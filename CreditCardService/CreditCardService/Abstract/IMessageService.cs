using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardService.Abstract
{
    public interface IMessageService
    {
        void Send(string message);
    }
}