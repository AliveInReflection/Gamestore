using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI.Helpers
{
    public static class GameStoreUrlHelper
    {
        public static MvcHtmlString ChangeLanguage(this UrlHelper _this, string linkTitle, RouteData routeData,
            string lang)
        {
            var link = new TagBuilder("a");

            var route = new RouteValueDictionary(routeData.Values);
            route["lang"] = lang;

            link.MergeAttribute("href", _this.RouteUrl(route));
            link.SetInnerText(linkTitle);
            return new MvcHtmlString(link.ToString());
        }
    }
}