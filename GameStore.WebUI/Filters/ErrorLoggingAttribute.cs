using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;


namespace GameStore.WebUI.Filters
{

    public class ErrorLoggingAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private ILogger logger = LogManager.GetLogger("ErrorsLogger");
        private StringBuilder message;

        public ErrorLoggingAttribute()
        {
            message = new StringBuilder();
        }

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                message.Append("Controller: " + exceptionContext.RouteData.Values["controller"] + " | ");
                message.Append("Action: " + exceptionContext.RouteData.Values["action"] + " | ");                
                message.Append("Message: " + exceptionContext.Exception.Message  + " | ");               
                message.Append("Where: " + exceptionContext.Exception.StackTrace.Split('\n')[0]);
                logger.Fatal(message);
                message.Clear();
                exceptionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {controller = "Home", action = "Index"}));
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}