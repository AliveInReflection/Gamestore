RefreshComments = function (culture, gameKey) {
    var promice = $.ajax({
        url: "/api/" + culture + "/games/" + gameKey + "/comments/",
        method: "GET"
    });

    promice.done(OnComplete);
}

OnComplete = function (data) {
    var comments = BuildTree(data, $("#GameKey").val());
    $(".comment-block").html(comments);
    $("textarea").val("");
}

BuildTree = function (comments, gameKey) {
    var ul = $("<ul/>");
    for (var i = 0; i < comments.length; i++) {
        var li = $("<li/>");

        var author = $("<span/>", {
            class: "comment-author",
            text: comments[i].User.UserName + ": "
        });
        
        var content = $("<span/>", {
            class: "comment-content",
            text: comments[i].Content
        });
        if (comments[i].Quote != null)
        {
            content.html(comments[i].Quote + content.html());
        }
        
        var answerLink = $("<a/>", {
            class: "comment-answer",
            href: "#",
            "data-id": comments[i].CommentId,
            text: $("#LinkAnswer").val()
        }).click(OnAnswerClick);

        var quoteLink = $("<a/>", {
            class: "comment-quote",
            href: "#",
            "data-id": comments[i].CommentId,
            text: $("#LinkQuote").val()
        }).click(OnQuoteClick);

        var deleteLink = $("<a/>", {
            class: "comment-delete",
            href: "#",
            "data-id": comments[i].CommentId,
            "data-href": "/api/en/games/" + gameKey + "/comments/",
            text: $("#LinkDelete").val()
        }).click(OnDeleteClick);

        var banLink = $("<a/>", {
            class: "comment-ban",
            href: "/" + $("#Culture").val() + "/Account/Ban/?UserId=" + comments[i].User.UserId,
            "data-id": comments[i].CommentId,
            text: $("#LinkBan").val()
        });

        author.appendTo(li);
        content.appendTo(li);
        answerLink.appendTo(li);
        quoteLink.appendTo(li);

        console.log($("#DeletePermission").val());
        console.log($("#BanPermission").val());

        if ($("#DeletePermission").val() == "True") {
            deleteLink.appendTo(li);
        }
        if ($("#BanPermission").val() == "True") {
            banLink.appendTo(li);
        }
      
        li.append(BuildTree(comments[i].ChildComments, gameKey));
        li.appendTo(ul);
    }
    return ul;
}