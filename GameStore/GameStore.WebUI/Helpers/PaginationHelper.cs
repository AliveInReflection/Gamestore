using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Helpers
{
    public static class PaginationHelper
    {
        public static MvcHtmlString PaginationBuilder(this HtmlHelper _this, int currentPage, int pageCount,
            string action, string controller)
        {
            var firstPage = currentPage > 5 ? currentPage - 5 : 1;
            var lastPage = pageCount - currentPage > 5 ? currentPage + 5 : pageCount;

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination-container");

            if (currentPage > 1)
            {
                var prev = new TagBuilder("a");
                prev.AddCssClass("pagination-page");
                prev.AddCssClass("pagination-prev");
                prev.MergeAttribute("href", url.Action(action,controller,new {page = currentPage}));
                prev.MergeAttribute("data-page", (currentPage - 1).ToString());
                prev.SetInnerText("<");

                var li = new TagBuilder("li");
                li.InnerHtml += prev.ToString();
                ul.InnerHtml += li;
            }

            for (int i = firstPage; i <= lastPage; i++)
            {
                var cur = new TagBuilder("a");

                if (currentPage == i)
                {
                    cur.AddCssClass("pagination-page");
                    cur.AddCssClass("pagination-page-current");
                }
                else
                {
                    cur.AddCssClass("pagination-page");
                }
                
                cur.MergeAttribute("href", url.Action(action, controller, new { page = i }));
                cur.MergeAttribute("data-page", i.ToString());
                cur.SetInnerText(i.ToString());

                var li = new TagBuilder("li");
                li.InnerHtml += cur.ToString();
                ul.InnerHtml += li;
            }

            if (currentPage < pageCount)
            {
                var next = new TagBuilder("a");
                next.AddCssClass("pagination-page");
                next.AddCssClass("pagination-next");
                next.MergeAttribute("href", url.Action(action, controller, new { page = currentPage }));
                next.MergeAttribute("data-page", (currentPage + 1).ToString());
                next.SetInnerText(">");

                var li = new TagBuilder("li");
                li.InnerHtml += next.ToString();
                ul.InnerHtml += li;
            }

            return new MvcHtmlString(ul.ToString());
        }
    }
}