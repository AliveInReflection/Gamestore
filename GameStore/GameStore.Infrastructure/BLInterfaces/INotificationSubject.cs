using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.BLInterfaces.Services;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface INotificationSubject
    {
        void Attach(INotificationObject service);
        void Detach(INotificationObject service);
        void Notify(string message);
    }
}
