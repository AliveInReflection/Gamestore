$(function() {
    $(".pagination-page").click(function(event) {
        event.preventDefault();
        var pageNumber = $(this).data("page");
        console.log(pageNumber);
        var input = $("#CurrentPage");
        console.log(input);
        input.attr("value", pageNumber);
        $("form").submit();
    });
})