using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Logger.Interfaces;
using System;

namespace GameStore.BLL.NotificationServices
{
    public class NotificationQueue : INotificationQueue
    {
        private IGameStoreLogger _logger;
        private ConcurrentQueue<INotification> _notifications;
        private bool _runFlag;

        public NotificationQueue(IGameStoreLogger logger)
        {
            _logger = logger;
            _notifications = new ConcurrentQueue<INotification>();
        }
        
        public void Enqueue(INotification notification)
        {
            _notifications.Enqueue(notification);
            
            if(!_runFlag)
            {
                Start();
            }
        }

        public void Enqueue(IEnumerable<INotification> notifications)
        {
            foreach (var notification in notifications)
            {
                _notifications.Enqueue(notification);
            }

            if (!_runFlag)
            {
                Start();
            }
        }

        private async void Notify()
        {
            INotification currentNotification;
            
            while (_notifications.TryDequeue(out currentNotification))
            {
                try
                {
                    await currentNotification.SendAsync();
                }
                catch(AggregateException e)
                {
                    _logger.Error(e.InnerException);
                }
                
            }

            _runFlag = false;
        }

        private Task Start()
        {
            _runFlag = true;
            return Task.Factory.StartNew(() => Notify());
        }
    }
}
