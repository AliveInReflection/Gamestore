using System.Security.Claims;
using System.Web;
using GameStore.Infrastructure.Enums;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GameStore.WebUI.Filters
{
    public class ClaimsApiAttribute : AuthorizeAttribute
    {
        private string claimType;
        private string claimValue;
        public ClaimsApiAttribute(string claimType, string claimValue)
        {
            this.claimType = claimType;
            this.claimValue = claimValue;

        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user != null && (user.HasClaim(claimType, claimValue) || (user.HasClaim(claimType, Permissions.Crud) && !user.HasClaim(claimType, Permissions.Ban))))
            {
                return;
            }

            HandleUnauthorizedRequest(actionContext);
        }
    }
}