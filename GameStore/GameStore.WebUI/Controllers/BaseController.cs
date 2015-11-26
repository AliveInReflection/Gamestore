using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public string Language { get; protected set; }
        protected IGameStoreLogger Logger { get; private set; }

        public BaseController(IGameStoreLogger logger)
        {
            Logger = logger;
        }     

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                Language = requestContext.RouteData.Values["lang"] as string;
                var ci = new CultureInfo(Language);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = ci;
            }
            base.Initialize(requestContext);
        }


        protected void SetErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }

        protected void SetInfoMessage(string message)
        {
            TempData["InfoMessage"] = message;
        }
        

    }
}
