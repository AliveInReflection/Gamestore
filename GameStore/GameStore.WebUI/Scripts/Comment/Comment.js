CommentObjectToDelete = null;
$(function () {

    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        buttons: {
            "Yes": function () {
                $(this).dialog("close");
                DeleteComment(CommentObjectToDelete);
            },
            "No": function () {
                $(this).dialog("close");
            }
        }
    });

    $(".comment-delete").click(OnDeleteClick);
    $(".comment-answer").click(OnAnswerClick);
    $(".comment-quote").click(OnQuoteClick);


});

OnDeleteClick = function (event) {
    event.preventDefault();
    CommentObjectToDelete = $(this);
    $("#dialog").dialog("open");
};

OnAnswerClick = function (event) {
    event.preventDefault();
    var parentId = $(this).data("id");
    console.log(parentId);
    $("#NewComment_ParentCommentId").attr("value", parentId);
};

OnQuoteClick = function (event) {
    event.preventDefault();
    var quoteId = $(this).data("id");
    console.log(quoteId);
    $("#NewComment_QuoteId").attr("value", quoteId);
};
