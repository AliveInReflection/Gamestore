using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardService.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Owner { get; set; }
        public decimal Amount { get; set; }
    }
}
