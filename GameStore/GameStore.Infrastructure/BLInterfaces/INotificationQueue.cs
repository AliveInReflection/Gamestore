using System.Collections.Generic;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface INotificationQueue
    {
        void Enqueue(INotification notification);
        void Enqueue(IEnumerable<INotification> notifications);
        void Notify();
    }
}
