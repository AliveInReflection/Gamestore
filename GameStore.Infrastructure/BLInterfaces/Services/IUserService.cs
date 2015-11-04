using System;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IUserService
    {
        void Ban(int userId, TimeSpan duration);
    }
}
