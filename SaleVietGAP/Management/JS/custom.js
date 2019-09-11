function CheckText(id, num) {
    if ($("#" + id).val().length < num || $("#" + id).val() == null || $("#" + id).val() == "") {
        var div = $("#" + id).closest("div");
        div.addClass("has-error");
        $("#sp-" + id).addClass("glyphicon glyphicon-remove form-control-feedback");
        return false;
    }
    else {
        var div = $("#" + id).closest("div");
        div.removeClass("has-error");
        div.addClass("has-success");
        $("#sp-" + id).removeClass("glyphicon glyphicon-remove form-control-feedback");
        $("#sp-" + id).addClass("glyphicon glyphicon-ok form-control-feedback");
        return true;
    }
}
function CheckNum(id) {
    $("#" + id).keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 40)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}
