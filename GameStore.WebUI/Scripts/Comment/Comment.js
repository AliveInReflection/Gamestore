$(function() {

    var CommentObjectToDelete = null;

    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        buttons: {
            "Yes": function() {
                $(this).dialog("close");
                $.post(CommentObjectToDelete.data("href"), {commentId: CommentObjectToDelete.data("commentid"),gameKey: CommentObjectToDelete.data("gamekey")});
            },
            "No": function() {
                $(this).dialog("close");
            }
        }
    });
    $(".comment-delete").click(function(event) {
        CommentObjectToDelete = $(this);
        $("#dialog").dialog("open");
    });
    $(".comment-answer").click(function(event) {
        var parentId = $(this).data("id");
        console.log(parentId);
        $("#ParentCommentId").attr("value", parentId);
    });
    


});