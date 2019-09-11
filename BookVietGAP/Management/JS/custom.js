
//Chang datetime toString & - Num
function parseDate(str, num) {
    var mdy = str.split('/');
    return new Date(mdy[2], mdy[1], mdy[0] - num);
}
//Load Menu Right
function loadmenuR(name) {
    var conRmenu = "";
    conRmenu = "<div class='navbar-header'>" +
                                "<div class='navbar-header-menu'>" +
                                    "<ul class='nav navbar-nav' style='padding-left: 20px; height: 50px; padding-top: 0px;margin-top: -15px;'>" +
                                        "<li style='padding-top: 25px;  color: rgb(254, 254, 254);'>" + name + "</li>" +
                                        "<li style='padding: 15px;'><a href='#modal_UserInfo' data-toggle='modal'><img class='img_menu' src='/Img/user.png'></a></li>" +
                                    "</ul>" +
                                "</div>" +
                            "</div>";
    var widthName = $("#idnav").width();
    if (widthName < 285) { widthName = 285 }
    document.getElementById('menuR').innerHTML = conRmenu;
    if (($(document).width() - $("#idnav").width() - $("#menuR").width()) < 0) {
        document.getElementById('menuR').innerHTML = "";
    }
    document.querySelector("#idnav").style.left = (($(document).width() - widthName - $("#menuR").width()) / 2).toString() + "px";
    document.querySelector("#pwd").style.paddingLeft = ($(document).width() / 2).toString() + "px";
}
function ChangePass() {
    if (!CheckText("old_Password", 1)) { ShowMessConfi('Vui lòng nhập mật khẩu cũ', 4000, 'warning'); return false; }
    if (!SaveAnything("ajax", "CheckPassword", JSON.stringify({ Zold: $('#old_Password').val() }))) { ShowMessConfi('Mật khẩu cũ không đúng', 4000, 'warning'); return false; }
    if (!CheckPassWord("new_Password", 8)) { ShowMessConfi('Mật khẩu phải ít nhất 8 ký tự và phải bao gồm chữ thường(a-z),chữ hoa(A-Z) và(0-9)', 4000, 'warning'); return false; }
    if ($("#old_Password").val() == $("#new_Password").val()) { ShowMessConfi('Mật khẩu mới không được giống mật khẩu cũ', 4000, 'warning'); return false; }
//    if (!CheckText("new_Password_c", 6)) { ShowMessConfi('Nhập lại mật khẩu phải từ 6 ký tự', 4000, 'warning'); return false; }
    if ($("#new_Password").val() != $("#new_Password_c").val()) { ShowMessConfi('Chưa trùng khớp với mật khẩu mới', 4000, 'warning'); return false; }
    ShowSaveConfi("mật khẩu mới");
    $(document).ready(function () {
        $("#SsaveConfi").click(function () {
            if (!SaveAnything("ajax", "SaveChangePass", JSON.stringify({ Zold: $('#old_Password').val(), Znew: $('#new_Password').val() })))
            { ShowMessConfi('Lưu thất bại', 4000, 'success'); return false; }
            ShowMessConfi('Lưu thành công', 4000, 'success');
            $("#old_Password").val("");
            $("#new_Password").val("");
            $("#new_Password_c").val("");
        });
    });
}
function CheckPassWord(id, num) {
    var ID = $("#" + id);
    var reg = /^([a-z])([A-Z])([0-9])*$/;
    var Checkaz = /^([a-zA-Z0-9])*$/.test(ID.val());
    if (ID.val().length < num || ID.val() == null || ID.val() == "" || /^([a-zA-Z0-9])*$/.test(ID.val()) == false || /^([a-z])*$/.test(ID.val()) == true || /^([A-Z])*$/.test(ID.val()) == true || /^([0-9])*$/.test(ID.val()) == true || /^([a-zA-Z])*$/.test(ID.val()) == true || /^([a-z0-9])*$/.test(ID.val()) == true || /^([A-Z0-9])*$/.test(ID.val()) == true) {
        var div = ID.closest("div");
        div.addClass("has-error");
        $("#sp-" + id).addClass("glyphicon glyphicon-remove form-control-feedback");
        ShowPopover(ID, '', 'Mật khẩu phải ít nhất ' + num + ' ký tự và phải bao gồm chữ thường(a-z),chữ hoa(A-Z) và(0-9)', 'top');
        return false;
    }
    else {
        var div = $("#" + id).closest("div");
        div.removeClass("has-error");
        div.addClass("has-success");
        $("#sp-" + id).removeClass("glyphicon glyphicon-remove form-control-feedback");
        $("#sp-" + id).addClass("glyphicon glyphicon-ok form-control-feedback");
        $("#" + id).popover('hide');
        return true;
    }
}

function ShowPopover(id, titles, contents, placements) {
    $(id).popover({
        title: titles,
        content: contents,
        placement: placements
    }).popover('show');
}
function SaveAnything(book, name, valu) {
    var result = false;
    $.ajax(
                        {
                            type: "POST",
                            url: book + ".aspx/" + name,
                            data: valu,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (response) {
                                result = response.d;
                            },
                            error: function (response) {
                                
                            }
                        });
    return result;
}
function AjaxCall(book, name, valu, callback) {
    return $.ajax({
        type: "POST",
        url: book + ".aspx/" + name,
        data: valu,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: callback,
        error: function (response) {
            alert("fail");
        }
    });
}
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

function Checkddl(id, num) {
    if ($("#" + id).val().length < num || $("#" + id).val() == null || $("#" + id).val() == "" || $("#" + id).val() == "0") {
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
//function CheckNum(id) {
//    $("#" + id).keydown(function (e) {
//        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 40)) {
//            return;
//        }
//        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
//            e.preventDefault();
//        }
//    });
//}
function CheckNum(id) {
    var rar = $("#" + id).val().replace(" ", "");
    var chec = rar.replace("-", "");
    var res = chec.replace(",", ".");
    $("#" + id).val(res);
    if (res == null || res == "") {
        var div = $("#" + id).closest("div");
        div.addClass("has-error");
        $("#sp-" + id).addClass("glyphicon glyphicon-remove form-control-feedback");
        return false;
    }
    else {
        if ($.isNumeric(res) == true) {
            //$("#" + id).val(xugame);
            var div = $("#" + id).closest("div");
            div.removeClass("has-error");
            div.addClass("has-success");
            $("#sp-" + id).removeClass("glyphicon glyphicon-remove form-control-feedback");
            $("#sp-" + id).addClass("glyphicon glyphicon-ok form-control-feedback");
            return true;
        }
        else {
            $("#" + id).val('0');
            var div = $("#" + id).closest("div");
            div.addClass("has-error");
            $("#sp-" + id).addClass("glyphicon glyphicon-remove form-control-feedback");
            return false;
        }
    }
}
function ShowMess(id, settime) {
    $('#' + id).modal('show'); setTimeout(function () { $('#' + id).modal('hide'); }, settime);
}
function ShowMessConfi(val, settime, type) {
    var con = "<div class='modal-dialog modal-sm'>" +
                "<div class='modal-content'>" +
                "<form class='form-horizontal' role='form'>" +
                    "<div class='modal-header modal-header-" + type + "'>" +
                        "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>" +
                        "<h2 class='modal-title'>Thông Báo</h2>" +
                    "</div>" +
                "<div class='modal-body' style='margin-bottom: -30px;'>" +
                    "<div class='form-group has-feedback'>" +
                       "<div style='padding: 10px;'><p>" + val + "</p></div>" +
                     "</div>" +
                "</div>" +
                    "<div class='modal-footer'>" +
                        "<button type='button' class='btn btn-default center-block' data-dismiss='modal'>Đóng</button>" +
                    "</div>" +
                "</div>" +
            "</div>";
    document.getElementById('MessNew').innerHTML = con;
    $('#MessNew').modal('show'); setTimeout(function () { $('#MessNew').modal('hide'); }, settime);
}
function ShowSaveConfi(val) {
    var con = "<div class='modal-dialog modal-sm'>" +
                "<div class='modal-content'>" +
                "<form class='form-horizontal' role='form'>" +
                    "<div class='modal-header modal-header-success'>" +
                        "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>" +
                        "<h2 class='modal-title'>Thông Báo</h2>" +
                    "</div>" +
                "<div class='modal-body' style='margin-bottom: -30px;'>" +
                    "<div class='form-group has-feedback'>" +
                       "<div style='padding: 10px;'><p>Bạn có chắc muốn lưu lại " + val + " không ?</p></div>" +
                     "</div>" +
                "</div>" +
                    "<div class='modal-footer'>" +
                        "<button type='button' id='SsaveConfi' class='btn btn-success' data-dismiss='modal'>Có</button>" +
                        "<button type='button' class='btn btn-default' data-dismiss='modal'>Không</button>" +
                    "</div>" +
                "</div>" +
            "</div>";
    document.getElementById('MessSave').innerHTML = con;
    $('#MessSave').modal('show');
}

//HTML
function Listlb(ddl, type) {
    var result = "";
    var ddlist = "";
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<a href='#' onclick='loadDetaildata(" + DS.Key + ")' class='list-group-item list-group-item-" + type + "'>" + DS.Name + "</a>";
    }
    result = "<div class='list-group'>" + ddlist + "</div>";
    return result;
}
function txtbox(name, id, num, numcheck) {
    var result = "";
    result = "<div class='form-group has-feedback'>" +
                "<label for='contact-name' class='col-sm-4 control-label'>" + name + "</label>" +
                "<div class='col-sm-8 has-error'>" +
                    "<input type='number' class='form-control' id='" + id + "' placeholder=''>" +
                    "<span id='sp-" + id + "'></span>" +
                "</div>" +
              "</div>"
    return result;
}
function DDList(name, id, load) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(getList(load));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<div class='form-group has-feedback'>" +
                                "<label for='contact-name' class='col-sm-4 control-label'>" + name + "</label>" +
                                "<div class='col-sm-8 has-error'>" +
                                    "<select id='" + id + "' class='selectpicker' data-live-search='true' style='display: none;'data-size='auto' data-width='100%'>" +
                                    "<option value='0'>Chọn</option>" +
                                        ddlist +
                                    "</select>" +
                                    "<span id='sp-" + id + "'></span>" +
                                "</div>" +
                            "</div>";
    return result;
}
