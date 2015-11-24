using CreditCardService.Abstract;

namespace CreditCardService.Concrete
{
    public class MessageService : IMessageService
    {
        public void SendEmail(string email, string message)
        {
            throw new System.NotImplementedException();
        }

        public void SendSms(string phoneNumber, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}