using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.AuthInterfaces
{
    public interface IAuthenticationService
    {
        bool Login(string userName, string password, bool isPersistent);
        void Logout();
    }
}
