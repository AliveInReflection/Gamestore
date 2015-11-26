using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "Buy",
               url: "{lang}/game/{gamekey}/buy",
               defaults: new { controller = "Order", action = "Add", lang = "en" },
               constraints: new { lang = @"ru|en" }
               );

            routes.MapRoute(
               name: "OrdersShortHistory",
               url: "{lang}/orders",
               defaults: new { controller = "Order", action = "GetShortHistory", lang = "en" },
               constraints: new { lang = @"ru|en" }
               );

            routes.MapRoute(
               name: "OrdersHistory",
               url: "{lang}/orders/history",
               defaults: new { controller = "Order", action = "History", lang = "en" },
               constraints: new { lang = @"ru|en" }
               );

            routes.MapRoute(
                name: "Comments",
                url: "{lang}/game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "Index", lang = "en" },
                constraints: new { lang = @"ru|en" }
                );

            routes.MapRoute(
                name: "NewComment",
                url: "{lang}/game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "Create", lang = "en" },
                constraints: new { lang = @"ru|en" }
                );

            routes.MapRoute(
                name: "Download",
                url: "{lang}/game/{gamekey}/{action}",
                defaults: new { controller = "Game", action = "Details", lang = "en" },
                constraints: new { lang = @"ru|en" }
                );

            routes.MapRoute(
                name: "Games",
                url: "{lang}/Games/{action}",
                defaults: new { controller = "Game", action = "Index", lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            routes.MapRoute(
               name: "Basket",
               url: "{lang}/Basket",
               defaults: new { controller = "Order", action = "Details", lang = "en" },
               constraints: new { lang = @"ru|en" }
           );
            routes.MapRoute(
               name: "Order",
               url: "{lang}/Order",
               defaults: new { controller = "Order", action = "Make", lang = "en" },
               constraints: new { lang = @"ru|en" }
           );

            routes.MapRoute(
                name: "GameDetails",
                url: "{lang}/Game/{key}",
                defaults: new { controller = "Game", action = "Details", lang = "en" },
                constraints: new { lang = @"ru|en" }
                );


            routes.MapRoute(
                name: "Publisher",
                url: "{lang}/Publisher/{action}",
                defaults: new { controller = "Publisher", action = "Index", lang = "en" },
                constraints: new { lang = @"ru|en" }
                ); 

            routes.MapRoute(
                name: "DefaultLang",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );
  
        }
            
    }
}