using System;
using GameStore.DAL.Interfaces;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using System.Web;
using System.Web.Security;
using Microsoft.Owin.Security;

namespace GameStore.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUnitOfWork database;
        private IGameStoreLogger logger;
        private const string cookieName = "GameStore_Auth";

        public AuthenticationService(IUnitOfWork database, IGameStoreLogger logger)
        {
            this.database = database;
            this.logger = logger;
        }

        public void Register(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public bool Login(string userName, string password, bool isPersistent)
        {
            if (!database.Users.IsExists(m => m.UserName.ToLower().Equals(userName.ToLower()) &&
                                             m.Password.Equals(password)))
            {
                return false;
            }

            var authenticationProperties = new AuthenticationProperties()
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddHours(24)
            };

            return true;
        }

        public void Logout()
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Value = string.Empty;
            }

        }
    }
}
