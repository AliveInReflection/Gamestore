using System.Collections.Generic;
using System.Web;
using GameStore.WebUI.Models;
using System.Web.Mvc;




namespace GameStore.WebUI.Helpers
{
    public static class GameStoreHtmlHelper
    {

        public static MvcHtmlString BuildCommentsTree(this HtmlHelper _this, IEnumerable<DisplayCommentViewModel> comments, string gameKey)
        {
            TagBuilder ul = new TagBuilder("ul");
            foreach (var comment in comments)
            {
                var li = new TagBuilder("li");
                var author = new TagBuilder("span");
                author.AddCssClass("comment-author");
                author.SetInnerText(comment.UserId.ToString() + ": ");

                var content = new TagBuilder("span");
                content.AddCssClass("comment-content");
                content.SetInnerText(comment.Content);
                content.InnerHtml = comment.Quote + content.InnerHtml;

                var answerLink = new TagBuilder("a");
                answerLink.AddCssClass("comment-answer");
                answerLink.MergeAttribute("href", "#");
                answerLink.MergeAttribute("data-id", comment.CommentId.ToString());
                answerLink.SetInnerText("Answer");

                var quoteLink = new TagBuilder("a");
                quoteLink.AddCssClass("comment-quote");
                quoteLink.MergeAttribute("href", "#");
                quoteLink.MergeAttribute("data-id", comment.CommentId.ToString());
                quoteLink.SetInnerText("Quote");

                var deleteLink = new TagBuilder("a");
                deleteLink.AddCssClass("comment-delete");
                deleteLink.MergeAttribute("data-href", (new UrlHelper(HttpContext.Current.Request.RequestContext)).Action("Delete", "Comment"));
                deleteLink.MergeAttribute("data-commentId", comment.CommentId.ToString());
                deleteLink.MergeAttribute("data-gameKey", gameKey);
                deleteLink.MergeAttribute("href", "#");
                deleteLink.SetInnerText("Delete");

                var banLink = new TagBuilder("a");
                banLink.AddCssClass("comment-ban");
                banLink.MergeAttribute("href", (new UrlHelper(HttpContext.Current.Request.RequestContext)).Action("Ban", "User", new{userId = comment.UserId}));
                banLink.SetInnerText("Ban");

                
                li.InnerHtml += author.ToString();
                li.InnerHtml += content.ToString();
                li.InnerHtml += answerLink.ToString();
                li.InnerHtml += quoteLink.ToString();
                li.InnerHtml += deleteLink.ToString();
                li.InnerHtml += banLink.ToString();
                
                if (comment.ChildComments != null)
                {
                    li.InnerHtml += BuildCommentsTree(_this, comment.ChildComments, gameKey);
                }
                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }        
    }
}