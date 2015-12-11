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

        public EmailNotification(string email)
        {
            _email = email;

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
            _client.Send(BuildMail(_message));
        }

        public Task SendAsync()
        {
            return _client.SendMailAsync(BuildMail(_message));
        }

        private MailMessage BuildMail(string message)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("etn008@gmail.com");
            mail.To.Add(new MailAddress(_email));
            mail.Subject = "Game store notification";
            mail.IsBodyHtml = true;
            mail.Body = message;

            return mail;
        }
    }
}
