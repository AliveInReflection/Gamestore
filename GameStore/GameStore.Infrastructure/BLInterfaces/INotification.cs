using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface INotification
    {
        void Send();
        Task SendAsync();
    }
}
