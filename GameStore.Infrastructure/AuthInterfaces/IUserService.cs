using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.AuthInterfaces
{
    public interface IUserService
    {
        bool IsFree(string userName);
        void Ban(int userId, DateTime expirationDate);
        IEnumerable<UserClaimDTO> GetClaims(int userId);
    }
}
