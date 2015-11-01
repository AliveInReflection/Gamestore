using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.App_Start;

namespace GameStore.WebUI.Filters
{
    public class IPAddressLoggingAttribute : ActionFilterAttribute, IActionFilter
    {
        private static readonly ILogger logger = LogManager.GetLogger("IPAddressLogger");
        private StringBuilder message;

        public IPAddressLoggingAttribute()
        {
            message = new StringBuilder();
        }

        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            message.Append("IP: " + filterContext.RequestContext.HttpContext.Request.UserHostAddress + " | ");
            message.Append("Method: " + filterContext.RequestContext.HttpContext.Request.HttpMethod + " | ");
            message.Append("Path: " + filterContext.RequestContext.HttpContext.Request.Path);
            logger.Info(message);
            message.Clear();
        }
    }
}