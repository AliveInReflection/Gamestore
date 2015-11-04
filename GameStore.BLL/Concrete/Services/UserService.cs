using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;

namespace GameStore.BLL.Services
{
    public class UserService : IUserService
    {
        public void Ban(int userId, TimeSpan duration)
        {
            throw new NotImplementedException();
        }
    }
}
