using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.BLInterfaces.Services;

namespace GameStore.BLL.NotificationServices
{
    public class MobileAppNotificationObject : INotificationObject
    {
        private int userId;

        public MobileAppNotificationObject(int userId)
        {
            this.userId = userId;
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }
}
