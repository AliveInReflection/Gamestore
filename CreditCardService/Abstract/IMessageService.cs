using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardService.Abstract
{
    public interface IMessageService
    {
        void SendEmail(string email, string message);
        void SendSms(string phoneNumber, string message);
    }
}