using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using GameStore.WebUI.Models;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;


namespace GameStore.WebUI.Helpers
{
    public static class GameStoreHtmlHelper
    {

        public static MvcHtmlString BuildCommentsTree(this HtmlHelper _this, IEnumerable<DisplayCommentViewModel> comments, string gameKey)
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            TagBuilder ul = new TagBuilder("ul");
            foreach (var comment in comments)
            {
                var li = new TagBuilder("li");
                var author = new TagBuilder("span");
                author.AddCssClass("comment-author");
                author.SetInnerText(comment.User.UserName + ": ");

                var content = new TagBuilder("span");
                content.AddCssClass("comment-content");
                content.SetInnerText(comment.Content);
                content.InnerHtml = comment.Quote + content.InnerHtml;

                var answerLink = new TagBuilder("a");
                answerLink.AddCssClass("comment-answer");
                answerLink.MergeAttribute("href", "#");
                answerLink.MergeAttribute("data-id", comment.CommentId.ToString());
                answerLink.SetInnerText(ViewsRes.LinkAnswer);

                var quoteLink = new TagBuilder("a");
                quoteLink.AddCssClass("comment-quote");
                quoteLink.MergeAttribute("href", "#");
                quoteLink.MergeAttribute("data-id", comment.CommentId.ToString());
                quoteLink.SetInnerText(ViewsRes.LinkQuote);

                
                li.InnerHtml += author.ToString();
                li.InnerHtml += content.ToString();
                li.InnerHtml += answerLink.ToString();
                li.InnerHtml += quoteLink.ToString();


                if (identity.HasClaim(GameStoreClaim.Comments, Permissions.Delete) ||
                    identity.HasClaim(GameStoreClaim.Comments, Permissions.Crud))
                {
                    var deleteLink = new TagBuilder("a");
                    deleteLink.AddCssClass("comment-delete");
                    deleteLink.MergeAttribute("data-href", (new UrlHelper(HttpContext.Current.Request.RequestContext)).Action("Delete", "Comment"));
                    deleteLink.MergeAttribute("data-commentId", comment.CommentId.ToString());
                    deleteLink.MergeAttribute("data-gameKey", gameKey);
                    deleteLink.MergeAttribute("href", "#");
                    deleteLink.SetInnerText(ViewsRes.LinkDelete);
                    li.InnerHtml += deleteLink.ToString();
                }

                if (identity.HasClaim(GameStoreClaim.Users, Permissions.Ban))
                {
                    var banLink = new TagBuilder("a");
                    banLink.AddCssClass("comment-ban");
                    banLink.MergeAttribute("href", (new UrlHelper(HttpContext.Current.Request.RequestContext)).Action("Ban", "Account", new { userId = comment.User.UserId }));
                    banLink.SetInnerText(ViewsRes.LinkBan);
                    li.InnerHtml += banLink.ToString();
                }
                
                
                if (comment.ChildComments != null)
                {
                    li.InnerHtml += BuildCommentsTree(_this, comment.ChildComments, gameKey);
                }
                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }

        public static MvcHtmlString BuildMenu(this HtmlHelper _this)
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

            var paragraph = new TagBuilder("p");
            paragraph.AddCssClass("text-center");

            var gamesLink = new TagBuilder("a");
            gamesLink.MergeAttribute("href", url.Action("Index", "Game"));
            gamesLink.SetInnerText(ViewsRes.MenuGames);

            paragraph.InnerHtml += gamesLink.ToString();

            var publisherLink = new TagBuilder("a");
            publisherLink.MergeAttribute("href", url.Action("Index", "Publisher"));
            publisherLink.SetInnerText(" | " + ViewsRes.MenuPublishers);

            paragraph.InnerHtml = paragraph.InnerHtml + publisherLink.ToString();

            if (identity.HasClaim(GameStoreClaim.Users, Permissions.Retreive) ||
                identity.HasClaim(GameStoreClaim.Users, Permissions.Crud))
            {
                var link = new TagBuilder("a");
                link.MergeAttribute("href", url.Action("Index", "Account"));
                link.SetInnerText(" | " + ViewsRes.MenuUsers);

                paragraph.InnerHtml = paragraph.InnerHtml + link.ToString();
            }

            if (identity.HasClaim(GameStoreClaim.Roles, Permissions.Retreive) ||
                identity.HasClaim(GameStoreClaim.Roles, Permissions.Crud))
            {
                var link = new TagBuilder("a");
                link.MergeAttribute("href", url.Action("IndexRoles", "Account"));
                link.SetInnerText(" | " + ViewsRes.MenuRoles);

                paragraph.InnerHtml = paragraph.InnerHtml + link.ToString();
            }

            if (identity.HasClaim(GameStoreClaim.Orders, Permissions.Retreive) ||
                identity.HasClaim(GameStoreClaim.Orders, Permissions.Crud))
            {
                var link = new TagBuilder("a");
                link.MergeAttribute("href", url.Action("GetShortHistory", "Order"));
                link.SetInnerText(" | " + ViewsRes.MenuOrders);

                paragraph.InnerHtml = paragraph.InnerHtml + link.ToString();
            }

            if (identity.HasClaim(GameStoreClaim.Orders, Permissions.Retreive) ||
                identity.HasClaim(GameStoreClaim.Orders, Permissions.Crud))
            {
                var link = new TagBuilder("a");
                link.MergeAttribute("href", url.Action("History", "Order"));
                link.SetInnerText(" | " + ViewsRes.MenuOrderHistory);

                paragraph.InnerHtml = paragraph.InnerHtml + link.ToString();
            }
            return new MvcHtmlString(paragraph.ToString());
        }
    }
}