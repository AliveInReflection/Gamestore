using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;
using System.Threading;

namespace GameStore.WebUI.Filters
{
    public class AgeClaimAttribute : AuthorizeAttribute
    {
        private const int daysInYear = 365;
        private int years;

        public AgeClaimAttribute(int years)
        {
            this.years = years;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user != null && user.Claims.Any(m => m.Type.Equals(ClaimTypes.DateOfBirth)))
            {
                var dateOfBirth = DateTime.Parse(user.FindFirst(ClaimTypes.DateOfBirth).Value, Thread.CurrentThread.CurrentCulture);
                if((DateTime.UtcNow - dateOfBirth) > TimeSpan.FromDays(daysInYear * years))
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