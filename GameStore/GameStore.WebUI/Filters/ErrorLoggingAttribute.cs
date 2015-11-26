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
using Ninject;
using GameStore.Logger.Interfaces;


namespace GameStore.WebUI.Filters
{

    public class ErrorLoggingAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private IGameStoreLogger logger;
        
        private StringBuilder message;

        public ErrorLoggingAttribute()
        {
            message = new StringBuilder();
            logger = DependencyResolver.Current.GetService<IGameStoreLogger>();
        }

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                message.Append("Controller: " + exceptionContext.RouteData.Values["controller"] + " | ");
                message.Append("Action: " + exceptionContext.RouteData.Values["action"] + " | ");                
                message.Append("Message: " + exceptionContext.Exception.Message  + " | ");               
                message.Append("Where: " + exceptionContext.Exception.StackTrace.Split('\n')[0]);
                logger.Fatal(message.ToString());
                message.Clear();
                exceptionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {controller = "Game", action = "Index"}));
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}