using System;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.NotificationServices
{
    public class SmsNotification : INotification
    {
        private string _mobilePhone;
        private string _message;

        public SmsNotification(string mobilePhone, string message)
        {
            _mobilePhone = mobilePhone;
            _message = message;
        }

        public void Send()
        {
            throw new NotImplementedException();
        }


        public Task SendAsync()
        {
            throw new NotImplementedException();
        }
    }
}
