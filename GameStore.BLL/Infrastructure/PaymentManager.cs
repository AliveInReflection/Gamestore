using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Payments;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.Enums;

namespace GameStore.BLL.Infrastructure
{
    public static class PaymentManager
    {
        private static Dictionary<string, IPayment> payments;

        static PaymentManager()
        {
            payments = new Dictionary<string, IPayment>
            {
                {"Bank", new BankPayment()},
                {"Ibox", new IboxPayment()},
                {"Visa", new VisaPayment()}
            };
        }

        public static IPayment Get(string paymentKey)
        {
            return payments[paymentKey];
        }
    }
}
