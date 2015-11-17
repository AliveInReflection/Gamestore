using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IUserService
    {
        void Ban(string userName, TimeSpan duration);
    }
}
