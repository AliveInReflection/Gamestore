using System.Web;
using System.Web.Mvc;

namespace GameStore.Domain.Entities.Domain.Entities.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}