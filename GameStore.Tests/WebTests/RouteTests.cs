﻿using System.Web.Routing;
using GameStore.WebUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class RouteTests
    {
        private static RouteCollection routes;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
        }

        [TestMethod]
        public void Default_Route()
        {
            RouteAssert.HasRoute(routes, "/",
                new { controller = "Home", action = "Index" });
        }

        [TestMethod]
        public void Create_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/games/new",
                new { controller = "Games", action = "New" });
        }

        [TestMethod]
        public void Edit_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/games/update",
                new { controller = "Games", action = "Update" });
        }

        [TestMethod]
        public void Game_Details_By_Key_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gamekey",
                new { controller = "Game", action = "Details", key = "gamekey" });
        }

        [TestMethod]
        public void Get_All_Games_Route()
        {
            RouteAssert.HasRoute(routes, "/games",
                new { controller = "Games", action = "Index"});
        }

        [TestMethod]
        public void Delete_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/games/remove",
                new { controller = "Games", action = "Remove" });
        }

        [TestMethod]
        public void New_Comment_For_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gamekey/newcomment",
                new { controller = "Game", action = "NewComment", gamekey = "gamekey" });
        }

        [TestMethod]
        public void Get_Comments_For_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gamekey/comments",
                new { controller = "Game", action = "Comments", gamekey = "gamekey" });
        }

        [TestMethod]
        public void Download_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gamekey/download",
                new { controller = "Game", action = "Download", gamekey = "gamekey" });
        }
    }
}
