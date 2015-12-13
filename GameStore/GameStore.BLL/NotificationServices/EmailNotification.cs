using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.NotificationServices
{
    public class EmailNotification : INotification
    {
        private string _email;
        private string _message;
        private SmtpClient _client;

        public EmailNotification(string email, string message)
        {
            _email = email;
            _message = message;

            _client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587
            };

            NetworkCredential credentials = new NetworkCredential("etn008@gmail.com", "GameStore");
            _client.UseDefaultCredentials = false;
            _client.Credentials = credentials;
        }

        public void Send()
        {
            _client.Send(BuildMail());
        }

        public Task SendAsync()
        {
            return _client.SendMailAsync(BuildMail());
        }

        private MailMessage BuildMail()
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("etn008@gmail.com");
            mail.To.Add(new MailAddress(_email));
            mail.Subject = "Game store notification";
            mail.IsBodyHtml = true;
            mail.Body = _message;

            return mail;
        }
    }
}
