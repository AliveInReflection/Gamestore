using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI.Controllers
{
    [Localization]
    public class BaseController : Controller
    {
        public string Language { get; set; }

        public ActionResult ChangeLanguage(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;

            Language = lang;

            Response.Cookies["lang"].Value = lang;

            return RedirectToAction("Index", "Game");
        }

        public string CurrentLangCode { get; protected set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string;
                var ci = new CultureInfo(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            base.Initialize(requestContext);
        }

        

    }
}
