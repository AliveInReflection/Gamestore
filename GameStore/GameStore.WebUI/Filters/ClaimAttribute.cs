using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

namespace GameStore.WebUI.Filters
{
    public class ClaimsAttribute : AuthorizeAttribute
    {
        private readonly string claimType;
        private readonly string claimValue;
        public ClaimsAttribute(string claimType, string claimValue)
        {
            this.claimType = claimType;
            this.claimValue = claimValue;

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user != null && (user.HasClaim(claimType, claimValue) || (user.HasClaim(claimType, Permissions.Crud) && !user.HasClaim(claimType, Permissions.Ban))))
            {
                return;
            }

            filterContext.Controller.TempData["ErrorMessage"] = ValidationRes.NoAccess;
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"action","Login"},
                    {"controller", "Account"}
                });
        }

    }
}