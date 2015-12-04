DeleteComment = function (comment) {
    console.log(comment);
    var promice = $.ajax({
        url: comment.data("href") + "?id=" + comment.data("id"),
        method: "DELETE"
    });

    promice.done(function () {
        RefreshComments($("#Culture").val(), $("#GameKey").val());
    });
}