using System.Runtime.Serialization;

namespace CreditCardService.Models
{
    [DataContract]
    public class PaymentInfo
    {
        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public string CardNumber { get; set; }

        [DataMember]
        public int SecureCode { get; set; }       

        [DataMember]
        public int ExpirationMonth { get; set; }

        [DataMember]
        public int ExpirationYear { get; set; }

        [DataMember]
        public string Receiver { get; set; }

        [DataMember]
        public string Purpose { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

    }
}