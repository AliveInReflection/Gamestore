using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameStore.WebUI.Helpers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.IO;
using System.Security.Claims;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void Build_Comment_Tree_Returns_Mvc_String()
        {
            var context = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(new StringWriter()));
            context.User = new ClaimsPrincipal();
            HttpContext.Current = context;
 
            var result = GameStoreHtmlHelper.BuildCommentsTree(new HtmlHelper(new ViewContext(), new ViewPage()),
                new List<DisplayCommentViewModel>(), "");

            Assert.IsInstanceOfType(result, typeof(MvcHtmlString));
        }
    }
}
