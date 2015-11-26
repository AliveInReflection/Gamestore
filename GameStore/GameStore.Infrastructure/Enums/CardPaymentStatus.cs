using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Enums
{
    public enum CardPaymentStatus
    {
        Successful = 1,
        NotEnoughMoney = 2,
        NotExistedCard = 3,
        ConfirmationRequired = 4,
        Failed = 5
    }
}
