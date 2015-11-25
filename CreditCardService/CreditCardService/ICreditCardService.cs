using CreditCardService.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CreditCardService.Models;

namespace CreditCardService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICreditCardService" in both code and config file together.
    [ServiceContract]
    public interface ICreditCardService
    {
        [OperationContract]
        PaymentStatus PayVisa(PaymentInfo info);

        [OperationContract]
        PaymentStatus PayMasterCard(PaymentInfo info);

        [OperationContract]
        bool Confirm(string confirmationCode);
    }
}
