using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoMapper;
using GameStore.Auth.Infrastructure;
using GameStore.DAL.Concrete;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Enums;
using Microsoft.Owin.Security.DataHandler;

namespace GameStore.Auth
{
    public class ClaimBasedAuthenticationModule : IHttpModule
    {
        private IUnitOfWork database;

        public ClaimBasedAuthenticationModule()
        {
            this.database = new UnitOfWork("GameStoreContext");
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
            if (cookie != null)
            {
                var ticketDataFormat = new TicketDataFormat(new TicketProtector(AuthenticationService.Purpose));
                var ticket = ticketDataFormat.Unprotect(cookie.Value);

                if (ticket == null)
                {
                    AuthenticateAsGuest();
                    return;
                }

                var userName = ticket.Identity.FindFirst(ClaimTypes.Name).Value;

                var user = database.Users.Get(m => m.UserName.Equals(userName));

                var userClaims = Mapper.Map < IEnumerable<UserClaim>, IEnumerable<Claim>>(user.Claims);
                var roles = user.Roles;
                List<Claim> roleClaims = new List<Claim>();
                foreach (var role in roles)
                {
                    roleClaims.AddRange(Mapper.Map<IEnumerable<RoleClaim>,IEnumerable<Claim>>(role.Claims));
                }

                ticket.Identity.AddClaims(userClaims.Union(roleClaims).Distinct());
                var principal = new ClaimsPrincipal(ticket.Identity);

                HttpContext.Current.User = principal;

            }
            else
            {
                AuthenticateAsGuest();
            }
        }

        private void AuthenticateAsGuest()
        {
            var claimEntries = database.Roles.Get(m => m.RoleName.Equals(DefaultRoles.Guest)).Claims;
            var claims = Mapper.Map<IEnumerable<RoleClaim>, IEnumerable<Claim>>(claimEntries);
            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims.Concat(new[] { new Claim(ClaimTypes.Role, DefaultRoles.Guest, "GameStore") })));

            HttpContext.Current.User = principal;
        }
    }
}
