using CreditCardService.Static;

namespace CreditCardService.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public CardType CardType { get; set; }
        public string CardNumber { get; set; }
        public int SecureCode { get; set; }
        public int ExperirationMonth { get; set; }
        public int ExperirationYear { get; set; }
        
    }
}
