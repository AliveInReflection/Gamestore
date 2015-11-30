using System.Net;
using System.Net.Mail;
using CreditCardService.Abstract;

namespace CreditCardService.Concrete
{
    public class EmailService : IMessageService
    {
        private string email;

        public EmailService(string email)
        {
            this.email = email;
        }

        public void Send(string message)
        {
            SmtpClient client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587
            };

            NetworkCredential credentials = new System.Net.NetworkCredential("etn008@gmail.com", "GameStore");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("etn008@gmail.com");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "GameStore purchase confirmation";
            mail.IsBodyHtml = true;
            mail.Body = BuildMailMessage(message);

            client.SendMailAsync(mail);
        }

        private string BuildMailMessage(string message)
        {
            return string.Format("<html><head></head><body><section><h2>GameStore</h2><p>{0}</p></section></body></html>", message);
        }
    }
}