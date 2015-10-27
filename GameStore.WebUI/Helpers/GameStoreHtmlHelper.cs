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

                var answer = new TagBuilder("a");
                answer.AddCssClass("comment-answer");
                answer.MergeAttribute("href", "#");
                answer.MergeAttribute("data-id", comment.CommentId.ToString());
                answer.SetInnerText("Answer");

                var quote = new TagBuilder("a");
                quote.AddCssClass("comment-quote");
                quote.MergeAttribute("href", "#");
                quote.MergeAttribute("data-message", comment.Content);

                var deleteLink = new TagBuilder("a");
                deleteLink.AddCssClass("comment-delete");
                deleteLink.MergeAttribute("data-href", (new UrlHelper(HttpContext.Current.Request.RequestContext)).Action("Delete", "Comment"));
                deleteLink.MergeAttribute("data-commentId", comment.CommentId.ToString());
                deleteLink.MergeAttribute("data-gameKey", gameKey);
                deleteLink.MergeAttribute("href", "#");
                deleteLink.SetInnerText("Delete");

                
                li.InnerHtml += author.ToString();
                li.InnerHtml += content.ToString();
                li.InnerHtml += answer.ToString();
                li.InnerHtml += deleteLink.ToString();
                
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