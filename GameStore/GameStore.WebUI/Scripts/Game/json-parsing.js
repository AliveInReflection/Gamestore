$(function() {


    $("#submit").click(function(event) {
        event.preventDefault();
        console.log("click");
        $("#transformator").submit();
    });


    $("#transformator").on("submit", function(event) {
        console.log("submit_start");
        event.preventDefault();

        _that = $(this);

        var formData = _that.serializeArray();

        var culture = $("#Culture").val();
        console.log(culture);

        var promice = $.ajax({
            url: "/api/"+ culture+ "/games/",
            type: "get",
            data: formData
        });

        promice.done(ParseContent);

        console.log("submit_end");
    });
});

ParseContent = function (data) {

    console.log("parse_start");

    var games = data.Games;

    var table = $("<table/>", {
        class: 'table'
    });

    for (var i = 0; i < games.length; i++) {
        var tr = $("<tr/>");

        $("<th/>", {
            text: games[i].GameKey
        }).appendTo(tr);

        $("<td/>", {
            text: games[i].GameName.substring(0)
        }).appendTo(tr);

        $("<td/>", {
            text: games[i].Description
        }).appendTo(tr);

        $("<td/>", {
            text: games[i].Price
        }).appendTo(tr);

        $("<td/>", {
            text: games[i].UnitsInStock
        }).appendTo(tr);


        var td = $("<td/>");

        $("<a/>", {
            class: 'button-small',
            href: "en/game/" + games[i].GameKey,
            text: $("#DetailsLink").val()
        }).appendTo(td);

        td.appendTo(tr);

        tr.appendTo(table);
    }

    $("#content-wrapper").html(table);


    var currentPage = data.CurrentPage;
    var pageCount = data.PageCount;

    var ul = $("<ul/>");
    var firstPage = currentPage > 5 ? currentPage - 5 : 1;
    var lastPage = pageCount - currentPage > 5 ? currentPage + 5 : pageCount;

    if (currentPage > 1) {
        var a = $("<a/>", {
            class: "pagination-page",
            href: "/" + $("#Culture").val() + "/games",
            "data-page": currentPage - 1,
            text: "<"
        });
        a.appendTo("<li/>").appendTo(ul);
    }

    for (var i = firstPage; i <= lastPage; i++) {
        var a = $("<a/>", {
            class: "pagination-page",
            href: "/" + $("#Culture").val() + "/games",
            "data-page": i,
            text: i
        });

        if (i == currentPage) {
            a.addClass("pagination-page-current");
        }

        a.appendTo("<li/>").appendTo(ul);
    }

    if (currentPage > pageCount) {
        var a = $("<a/>", {
            class: "pagination-page",
            href: "/" + $("#Culture").val() + "/games",
            "data-page": currentPage + 1,
            text: ">"
        });
        a.appendTo("<li/>").appendTo(ul);
    }

    ul.appendTo($("#content-wrapper"));


    console.log("parse_end");

    
}
