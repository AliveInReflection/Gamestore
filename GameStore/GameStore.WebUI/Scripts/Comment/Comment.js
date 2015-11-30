﻿$(function () {

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
                    complete: function () {
                        setTimeout(window.location.reload(), 3000);
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
        $("#NewComment_ParentCommentId").attr("value", parentId);
    });
    $(".comment-quote").click(function (event) {
        event.preventDefault();
        var quoteId = $(this).data("id");
        console.log(quoteId);
        $("#NewComment_QuoteId").attr("value", quoteId);
    });


});

