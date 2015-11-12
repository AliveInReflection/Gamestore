﻿using System;
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
               url: "game/{gamekey}/buy",
               defaults: new { controller = "Order", action = "Add", lang = "en" }
               );

            routes.MapRoute(
               name: "OrdersHistory",
               url: "orders/history",
               defaults: new { controller = "Order", action = "History", lang = "en" }
               );

            routes.MapRoute(
                name: "Comments",
                url: "game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "Index", lang = "en" }
                );

            routes.MapRoute(
                name: "NewComment",
                url: "game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "Create", lang = "en" }
                );

            routes.MapRoute(
                name: "Download",
                url: "game/{gamekey}/{action}",
                defaults: new { controller = "Game", action = "Details", lang = "en" }
                );          

            routes.MapRoute(
                name: "Games",
                url: "Games/{action}",
                defaults: new { controller = "Game", action = "Index", lang = "en" }
            );

            routes.MapRoute(
               name: "Basket",
               url: "Basket",
               defaults: new { controller = "Order", action = "Details", lang = "en" }
           );
            routes.MapRoute(
               name: "Order",
               url: "Order",
               defaults: new { controller = "Order", action = "Make", lang = "en" }
           );

            routes.MapRoute(
                name: "GameDetails",
                url: "Game/{key}",
                defaults: new { controller = "Game", action = "Details", lang = "en" }
                );


            routes.MapRoute(
                name: "Publisher",
                url: "Publisher/{action}",
                defaults: new { controller = "Publisher", action = "Index", lang = "en" }
                );

            routes.MapRoute(
                name: "DefaultLang",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            ); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "Index", id = UrlParameter.Optional, lang = "en" }
            );
            //=============== localized =================
            
            

            
        }
            
    }
}