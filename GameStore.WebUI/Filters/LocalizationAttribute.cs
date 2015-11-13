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
                    
            }
            else
            {
                HttpCookie cookie = filterContext.HttpContext.Request.Cookies["lang"];
                if (cookie != null)
                    cookie.Value = culture;
                else
                {

                    cookie = new HttpCookie("lang");
                    cookie.HttpOnly = false;
                    cookie.Value = culture;
                    cookie.Expires = DateTime.Now.AddYears(1);
                    filterContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }

            List<string> cultures = new List<string>() { "ru", "en"};
            if (!cultures.Contains(culture))
            {
                culture = "en";
            }
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
