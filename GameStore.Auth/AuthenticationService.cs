using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using GameStore.Auth.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Logger.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;

namespace GameStore.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUnitOfWork database;
        private IGameStoreLogger logger;
        internal const string CookieName = "GameStore_Auth";
        internal const string Purpose = "GameStore";

        public AuthenticationService(IUnitOfWork database, IGameStoreLogger logger)
        {
            this.database = database;
            this.logger = logger;
        }

        public bool Login(string userName, string password, bool isPersistent)
        {
            if (!database.Users.IsExists(m => m.UserName.ToLower().Equals(userName.ToLower()) &&
                                             m.Password.Equals(password)))
            {
                return false;
            }
            var user = database.Users.Get(m => m.UserName.ToLower().Equals(userName.ToLower()) &&
                                               m.Password.Equals(password));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.SerialNumber, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            ClaimsPrincipal principal = new GameStoreClaimsPrincipal(claims);

            

            var authenticationProperties = new AuthenticationProperties()
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddHours(24)
            };

            var ticket = new AuthenticationTicket(principal.Identity as ClaimsIdentity, authenticationProperties);

            var ticketDataFormat = new TicketDataFormat(new TicketProtector(Purpose));
            var protectedTicket = ticketDataFormat.Protect(ticket);

            var cookie = new HttpCookie(CookieName, protectedTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);

            return true;
        }

        public void Logout()
        {
            var cookie = HttpContext.Current.Response.Cookies[CookieName];
            if (cookie != null)
            {
                cookie.Value = string.Empty;
            }

        }
    }
}
