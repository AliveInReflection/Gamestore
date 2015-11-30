using CreditCardService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardService.Concrete
{
    public class SmsService : IMessageService
    {
        private string phoneNumber;

        public SmsService(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }
}