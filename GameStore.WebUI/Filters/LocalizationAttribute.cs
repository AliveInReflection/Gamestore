using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Filters
{
    public class LocalizationAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var culture = filterContext.RouteData.Values["lang"] as string;
            if (culture == null)
            {
                HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
                if (cultureCookie != null)
                {
                    culture = cultureCookie.Value;
                }
                else
                {
                    culture = "en";
                }
                filterContext.RouteData.Values["lang"] = culture;
            }

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
