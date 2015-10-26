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

                var delete = new TagBuilder("form");
                delete.MergeAttribute("action", (new UrlHelper(HttpContext.Current.Request.RequestContext)).Action("Delete", "Comment"));
                delete.MergeAttribute("method", "post");

                var hiddenGameId = new TagBuilder("input");
                hiddenGameId.MergeAttribute("type", "hidden");
                hiddenGameId.MergeAttribute("name", "commentId");
                hiddenGameId.MergeAttribute("value", comment.CommentId.ToString());

                var hiddenGameKey = new TagBuilder("input");
                hiddenGameKey.MergeAttribute("type", "hidden");
                hiddenGameKey.MergeAttribute("name", "gameKey");
                hiddenGameKey.MergeAttribute("value", gameKey);

                var submitLink = new TagBuilder("a");
                submitLink.MergeAttribute("href", "#");
                submitLink.SetInnerText("Delete");
                submitLink.AddCssClass("comment-delete-btn");


                delete.InnerHtml += hiddenGameId.ToString();
                delete.InnerHtml += hiddenGameKey.ToString();


                li.InnerHtml += author.ToString();
                li.InnerHtml += content.ToString();
                li.InnerHtml += answer.ToString();
                li.InnerHtml += delete.ToString();
                li.InnerHtml += submitLink.ToString();
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