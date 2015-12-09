using System.Collections.Generic;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.NotificationServices
{
    public class NotificationSubject : INotificationSubject
    {
        private List<INotificationObject> _objects;

        public NotificationSubject()
        {
            _objects = new List<INotificationObject>();
        }



        public void Attach(INotificationObject _object)
        {
            _objects.Add(_object);
        }

        public void Detach(INotificationObject _object)
        {
            _objects.Remove(_object);
        }

        public void Notify(string message)
        {
            foreach (var _object in _objects)
            {
                _object.Send(message);
            }
        }
    }
}
