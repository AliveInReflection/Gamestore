using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.BLInterfaces.Services
{
    public interface ICreditCardPaymentService
    {
        /// <summary>Used remote service to implement credit card payment</summary>
        /// <param name="info">Information about credit card</param>
        /// <exception>ValidationException</exception>
        /// <returns>Status code of operation</returns>
        CardPaymentStatus Pay(CardPaymentInfoDTO info);


        /// <summary>Used remote service to confirm payment</summary>
        /// <param name="cardNumber">Users card number</param>
        /// <param name="confirmationCode">Code received by email</param>
        /// <returns>Status code of operation</returns>
        CardConfirmationStatus Confirm(string cardNumber, string confirmationCode);
    }

}
