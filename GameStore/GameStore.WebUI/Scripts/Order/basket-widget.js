$(function () {
    UpdateBasketWidget();
});


UpdateBasketWidget = function () {
    var promice = $.ajax({
        url: "/api/en/orders/",
        type: "get"
    });

    promice.done(OnUpdateBasketWidget);
};

OnUpdateBasketWidget = function (data) {

    var count = 0;
    for (var i = 0; i < data.OrderDetailses.length; i++) {
        count += data.OrderDetailses[i].Quantity;
    }

    $("#count").html(count);
    $("#amount").html(data.Amount);
};