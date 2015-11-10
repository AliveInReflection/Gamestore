using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameStore.WebUI.Helpers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.WebTests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void Build_Comment_Tree_Returns_Mvc_String()
        {
            var result = GameStoreHtmlHelper.BuildCommentsTree(new HtmlHelper(new ViewContext(), new ViewPage()),
                new List<DisplayCommentViewModel>(), "");

            Assert.IsInstanceOfType(result, typeof(MvcHtmlString));
        }
    }
}
