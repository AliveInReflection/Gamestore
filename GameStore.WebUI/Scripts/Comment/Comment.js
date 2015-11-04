$(function () {

    var CommentObjectToDelete = null;

    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        buttons: {
            "Yes": function () {
                $(this).dialog("close");
                $.post(CommentObjectToDelete.data("href"), {
                    commentId: CommentObjectToDelete.data("commentid"),
                    gameKey: CommentObjectToDelete.data("gamekey"),
                    success: function () {
                        setTimeout(window.location.reload(), 1500);
                    }
                });
            },
            "No": function () {
                $(this).dialog("close");
            }
        }
    });

    $(".comment-delete").click(function (event) {
        event.preventDefault();
        CommentObjectToDelete = $(this);
        $("#dialog").dialog("open");
    });

    $(".comment-answer").click(function (event) {
        event.preventDefault();
        var parentId = $(this).data("id");
        console.log(parentId);
        $("#ParentCommentId").attr("value", parentId);
    });
    $(".comment-quote").click(function (event) {
        event.preventDefault();
        var quoteId = $(this).data("id");
        console.log(quoteId);
        $("#QuoteId").attr("value", quoteId);
    });


});

