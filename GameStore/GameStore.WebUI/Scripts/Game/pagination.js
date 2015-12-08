$(function () {
    $(".pagination-page").click(PageOnClickHandler)
});


PageOnClickHandler = function (event) {
    event.preventDefault();

    var pageNumber = $(this).data("page");

    var input = $("#CurrentPage");
    input.attr("value", pageNumber);

    $("#transformer").submit();
};