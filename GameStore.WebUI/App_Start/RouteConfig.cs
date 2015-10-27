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
               url: "game/{gamekey}/buy",
               defaults: new { controller = "Order", action = "Add" }
               );

            routes.MapRoute(
                name: "Download",
                url: "game/{gamekey}/download",
                defaults: new { controller = "Game", action = "download" }
                );

            routes.MapRoute(
                name: "NewComment",
                url: "game/{gamekey}/newcomment",
                defaults: new { controller = "Comment", action = "Create" }
                );


            routes.MapRoute(
                name: "GamesNew",
                url: "Games/New",
                defaults: new { controller = "Game", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PublisherNew",
                url: "Publisher/New",
                defaults: new { controller = "Publisher", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GamesUpdate",
                url: "Games/Update",
                defaults: new { controller = "Game", action = "Update", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "GamesRemove",
               url: "Games/Update",
               defaults: new { controller = "Game", action = "Delete", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Games",
               url: "Games",
               defaults: new { controller = "Game", action = "List"}
           );

            routes.MapRoute(
               name: "Busket",
               url: "Busket",
               defaults: new { controller = "Order", action = "Details" }
           );
            routes.MapRoute(
               name: "Order",
               url: "Order",
               defaults: new { controller = "Order", action = "Make"}
           );

            routes.MapRoute(
                name: "GameDetails",
                url: "Game/{key}",
                defaults: new { controller = "Game", action = "Details", key = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "PublisherDetails",
                url: "Publisher/{CompanyName}",
                defaults: new { controller = "Publisher", action = "Details"}
                );

            

           
            routes.MapRoute(
                name: "Comments",
                url: "game/{gamekey}/comments",
                defaults: new { controller = "Comment", action = "List"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "List", id = UrlParameter.Optional }
            );

            
        }
            
    }
}