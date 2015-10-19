using System.Web;
using System.Web.Mvc;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new OutputCacheAttribute() {Duration = 60});
            filters.Add(new PerformanceLoggingAttribute());
        }
    }
}