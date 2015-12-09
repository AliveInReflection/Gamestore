using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.BLInterfaces.Services;

namespace GameStore.BLL.NotificationServices
{
    public class EmailNotificationObject : INotificationObject
    {
        private string email;

        public EmailNotificationObject(string email)
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
            mail.Subject = "Game store notification";
            mail.IsBodyHtml = true;
            mail.Body = message;

            client.SendMailAsync(mail);
        }
    }
}
