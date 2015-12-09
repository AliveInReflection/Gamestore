using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.BLInterfaces.Services;

namespace GameStore.BLL.NotificationServices
{
    public class SmsNotificationObject : INotificationObject
    {
        private string _mobilePhone;

        public SmsNotificationObject(string mobilePhone)
        {
            _mobilePhone = mobilePhone;
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }
}
