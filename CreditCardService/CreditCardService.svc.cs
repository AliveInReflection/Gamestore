using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CreditCardService.Models;
using CreditCardService.Static;

namespace CreditCardService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CreditCardService : ICreditCardService
    {

        public PaymentStatus PayVisa(PaymentInfo info)
        {
            throw new NotImplementedException();
        }

        public PaymentStatus PayMasterCard(PaymentInfo info)
        {
            throw new NotImplementedException();
        }

        public bool Confirm(string codeword)
        {
            throw new NotImplementedException();
        }

        private PaymentStatus Pay(PaymentInfo info, PaymentType type)
        {
            
        }
    }
}
