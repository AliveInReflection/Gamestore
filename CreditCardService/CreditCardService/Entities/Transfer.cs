using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreditCardService.Entities
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int ReceiverId { get; set; }
        public int PayerId { get; set; }
        public decimal Amount { get; set; }
        public string VerificationCode { get; set; }
        public DateTime InitTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public bool Confirmed { get; set; }
    }
}