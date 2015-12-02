$(function () {
    $(".pagination-page").click(PageOnClickHandler)
});


PageOnClickHandler = function (event) {
    event.preventDefault();
    var pageNumber = $(this).data("page");
    console.log(pageNumber);
    var input = $("#CurrentPage");
    console.log(input);
    input.attr("value", pageNumber);
    $("#transformator").submit();

    console.log("pagination click");
};