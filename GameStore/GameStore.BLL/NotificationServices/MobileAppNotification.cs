using System;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.NotificationServices
{
    public class MobileAppNotification : INotification
    {
        private int _userId;
        private string _message;

        public MobileAppNotification(int userId, string message)
        {
            _userId = userId;
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
