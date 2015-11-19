$(function () {
    var claimsCount = 1;

    $("#add-claim").click(function(event) {
        event.preventDefault();
        console.log($(this).data("url") + "/" + claimsCount);
        $.ajax({
            url: $(this).data("url") + "?number=" + claimsCount,
            method: "get",
            success: function (data) {

                $("#claims").append(data);
            }

        });
        claimsCount += 1;
        console.log(claimsCount);
    });

    $("#delete-claim").click(function (event) {
        event.preventDefault();
        if (claimsCount > 1) {
            $("#claims").children().last().remove();
            claimsCount -= 1;
        }
    });


});