using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.BLInterfaces.Services
{
    public interface ICreditCardService
    {

            CardPaymentStatus Pay(CardPaymentInfoDTO info);

            CardConfirmationStatus Confirm(string cardNumber, string confirmationCode);
        }

}
