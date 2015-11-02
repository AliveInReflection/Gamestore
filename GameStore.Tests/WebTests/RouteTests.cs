using System.Web.Routing;
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
                new { controller = "Game", action = "List" });
        }

        [TestMethod]
        public void Create_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/games/new",
                new { controller = "Game", action = "Create" });
        }

        [TestMethod]
        public void Update_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/games/update",
                new { controller = "Game", action = "Update" });
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
                new { controller = "Game", action = "List"});
        }

        //[TestMethod]
        //public void Delete_Game_Route()
        //{
        //    RouteAssert.HasRoute(routes, "/games/remove",
        //        new { controller = "Games", action = "Delete" });
        //}

        [TestMethod]
        public void New_Comment_For_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gamekey/newcomment",
                new { controller = "Comment", action = "Create", gamekey = "gamekey" });
        }

        [TestMethod]
        public void Get_Comments_For_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gamekey/comments",
                new { controller = "Comment", action = "List", gamekey = "gamekey" });
        }

        [TestMethod]
        public void Download_Game_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gamekey/download",
                new { controller = "Game", action = "Download", gamekey = "gamekey" });
        }

        [TestMethod]
        public void Publisher_Details_Route()
        {
            RouteAssert.HasRoute(routes, "/publisher/CompanyName",
                new { controller = "Publisher", action = "Details", companyName = "CompanyName" });
        }

        [TestMethod]
        public void Publisher_Create_Route()
        {
            RouteAssert.HasRoute(routes, "/publisher/new",
                new { controller = "Publisher", action = "Create"});
        }

        [TestMethod]
        public void Busket_Route()
        {
            RouteAssert.HasRoute(routes, "/busket",
                new { controller = "Order", action = "Details" });
        }

        [TestMethod]
        public void Game_Buy_Route()
        {
            RouteAssert.HasRoute(routes, "/game/gameKey/buy",
                new { controller = "Order", action = "Add", gameKey = "gameKey"});
        }
    }
}
