$(function () {
    $("#form-buy").on('submit', function (event) {

        event.preventDefault();

        _that = $(this);

        var formData = _that.serializeArray();

        var culture = $("#Culture").val();

        var promice = $.ajax({
            url: "/api/" + culture + "/orders/?gameKey=" + formData[0].value + "&quantity=" + formData[1].value,
            method: "post"
        });

        promice.done(UpdateBasketWidget);
    });
});