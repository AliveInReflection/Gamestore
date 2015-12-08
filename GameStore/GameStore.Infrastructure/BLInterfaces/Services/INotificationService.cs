using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.BLInterfaces.Services
{
    public interface INotificationService
    {
        void Send(string message);
    }
}
