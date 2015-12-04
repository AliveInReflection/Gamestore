$(function () {

    $("#form").on('click', function (event) {
        event.preventDefault();

        _that = $(this);

        var formData = _that.serializeArray();

        var jsn = $.parseJSON(JSON.stringify(formData));

        var obj = new Object();

        for (var i = 0; i < jsn.length; i++) {
            var key = jsn[i].name.replace("NewComment.", "");
            var val = jsn[i].value;
            obj[key] = val;
        }

        var culture = $("#Culture").val();
        var gameKey = $("#gameKey").val();
        

        var promice = $.ajax({
            url: "/api/" + culture + "/games/" + gameKey + "/comments/?gameId=1",
            method: "POST",
            data: obj
        });

        promice.done(ParseContent);
        promice.error(ProcessError);

    });

});

ParseContent = function () {
    $("#content-error-message").html("");
    RefreshComments($("#Culture").val(), $("#GameKey").val());   
}

ProcessError = function (error) {

    var modelErrors = error.responseJSON["model.Content"]._errors;
    if (modelErrors.length > 0)
    {
        console.log(modelErrors[0]["<ErrorMessage>k__BackingField"]);
        $("#content-error-message").html(modelErrors[0]["<ErrorMessage>k__BackingField"]);
    }
    
    
}