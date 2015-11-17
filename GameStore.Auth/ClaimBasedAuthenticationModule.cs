using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GameStore.DAL.Interfaces;
using GameStore.Logger.Interfaces;

namespace GameStore.Auth
{
    public class ClaimBasedAuthenticationModule : IHttpModule
    {
        private IUnitOfWork database;
        private IGameStoreLogger logger;

        public ClaimBasedAuthenticationModule(IUnitOfWork database, IGameStoreLogger logger)
        {
            this.database = database;
            this.logger = logger;
        }

        public void Dispose()
        {
            database.Dispose();
        }

        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += UpdateUser;
        }

        private void UpdateUser(object sender, EventArgs args)
        {
            var cookie = HttpContext.Current.Request.Cookies[AuthenticationService.CookieName];
            if (cookie == null)
            {
                
            }
        }
    }
}
