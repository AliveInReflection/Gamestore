using System;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IUserService
    {
        void Ban(string userName, TimeSpan duration);
    }
}
