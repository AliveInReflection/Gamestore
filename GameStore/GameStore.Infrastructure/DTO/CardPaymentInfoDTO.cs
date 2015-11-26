using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.Enums;

namespace GameStore.Infrastructure.DTO
{
    public class CardPaymentInfoDTO
    {
        public int OrderId { get; set; }

        public string FullName { get; set; }

        public string CardNumber { get; set; }

        public int SecureCode { get; set; }

        public int ExpirationMonth { get; set; }

        public int ExpirationYear { get; set; }

        public string Receiver { get; set; }

        public string Purpose { get; set; }

        public decimal Amount { get; set; }

        public CardType CardType { get; set; }

    }
}
