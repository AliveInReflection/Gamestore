$(function() {
    $(".comment-answer").click(function(event) {
        var parentId = $(this).data("id");
        console.log(parentId);
        $("#ParentCommentId").attr("value", parentId);
    });
});