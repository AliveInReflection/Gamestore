using GameStore.WebUI.Infrastructure.LoggerInterfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace GameStore.WebUI.Filters
{
    public class PerformanceLoggingAttribute : FilterAttribute, IActionFilter
    {
        private readonly Stopwatch stopwatch;
        private static readonly ILogger logger = LogManager.GetLogger("PerformanceLogger");
        private StringBuilder message;
        
        public PerformanceLoggingAttribute()
        {
            stopwatch = new Stopwatch();
            message = new StringBuilder();
        }

        

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopwatch.Start();
            

        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopwatch.Stop();

            //    Controller/Action
            message.Append(filterContext.Controller.GetType().Name + "/" + filterContext.ActionDescriptor.ActionName);
            message.Append(": \t");
            message.Append(stopwatch.ElapsedMilliseconds);
            message.Append("ms\n");
            logger.Info(message);
            message.Clear();
        }
    }
}