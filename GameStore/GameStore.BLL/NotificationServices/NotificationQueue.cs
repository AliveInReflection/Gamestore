using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.NotificationServices
{
    public class NotificationQueue : INotificationQueue
    {
        private ConcurrentQueue<INotification> _notifications;

        public NotificationQueue()
        {
            _notifications = new ConcurrentQueue<INotification>();

            Task.Factory.StartNew(() =>
            {
                INotification currentnotification;
                while (true)
                {
                    if (_notifications.TryDequeue(out currentnotification))
                    {
                        currentnotification.SendAsync();
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        public void Enqueue(INotification notification)
        {
            _notifications.Enqueue(notification);
        }

        public void Enqueue(IEnumerable<INotification> notifications)
        {
            foreach (var notification in notifications)
            {
                _notifications.Enqueue(notification);
            }
        }

        public void Notify()
        {
            INotification currentnotification;
            
            while(_notifications.TryDequeue(out currentnotification))
                currentnotification.Send();
            }
        }
    }
}
