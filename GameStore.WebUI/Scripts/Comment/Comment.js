$(function() {
    $(".comment-answer").click(function(event) {
        var parentId = $(this).data("id");
        console.log(parentId);
        $("#ParentCommentId").attr("value", parentId);
    });

    $(".comment-delete-btn").click(function (event) {
        var confirmation = confirm("Delete this comment?");
        if (confirmation == true) {
            $(this).parent().submit();
        } else {
            $(this).preventDefault();
        }
    });

});