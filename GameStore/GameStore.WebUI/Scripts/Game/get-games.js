$(function() {


    $("#submit").click(function(event) {
        event.preventDefault();
        $("#transformator").submit();
    });


    $("#transformator").on("submit", function(event) {
        event.preventDefault();

        _that = $(this);

        var formData = _that.serializeArray();

        var culture = $("#Culture").val();
        var promice = $.ajax({
            url: "/api/"+ culture+ "/games/",
            type: "get",
            data: formData
        });

        promice.done(ParseContent);
    });
});

ParseContent = function (data) {

    var table = BuildTable(data.Games);
        $("#content-wrapper").html(table);

    var paginator = BuildPaginator(data.Transformer.CurrentPage, data.Transformer.PageCount);
    paginator.appendTo($("#content-wrapper"));
}


BuildTable = function (games) {
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
    };
    return table;
};

BuildPaginator = function (currentPage, pageCount) {

    var ul = $("<ul/>", {
        class: "pagination-container"
    });
    var firstPage = currentPage > 5 ? currentPage - 5 : 1;
    var lastPage = pageCount - currentPage > 5 ? currentPage + 5 : pageCount;

    if (currentPage > 1) {
        var a = $("<a/>", {
            class: "pagination-page",
            href: "/" + $("#Culture").val() + "/games",
            "data-page": currentPage - 1,
            text: "<"
        });
        a.click(PageOnClickHandler);
        var li = $("<li/>");
        a.appendTo(li);
        li.appendTo(ul);
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
        a.click(PageOnClickHandler);
        var li = $("<li/>");
        a.appendTo(li);
        li.appendTo(ul);
    }

    if (currentPage < pageCount) {
        var a = $("<a/>", {
            class: "pagination-page",
            href: "/" + $("#Culture").val() + "/games",
            "data-page": currentPage + 1,
            text: ">"
        });
        a.click(PageOnClickHandler);
        var li = $("<li/>");
        a.appendTo(li);
        li.appendTo(ul);
    }

    return ul;
};