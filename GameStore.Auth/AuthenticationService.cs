using System;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.DTO;

namespace GameStore.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        public void Register(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string password, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
