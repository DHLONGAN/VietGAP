//***Load DataBook 1
function LoadBook(NameBook) {
    loaddataBook(NameBook);
}
function callBook(str) {
    var res = str.split("-");
    loaddataBook(res[0]);
    var timeday = res[1].split("/");
    $('.responsive-calendar').responsiveCalendar(timeday[2] + "-" + timeday[1]);
    $('.responsive-calendar').responsiveCalendar('edit', JSON.parse(getCalendar(JSON.stringify({ Year: timeday[2], Month: timeday[1] }))));
    loaddata(res[1]);
    var reid = res[2].split("_");
    loadDetaildata(reid[1]);
}
function loaddataBook(NameBook) {
    CheckSession();
    Bookid = "LoadCalendar" + NameBook;
    Bookdata = "Load" + NameBook;
    NameBooks = NameBook;
    //var d = new Date();
    var d = DateNow;
    var day = d.getDate();
    var mon = (d.getMonth() + 1);
    var year = d.getFullYear();
    var daynow = day + "/" + mon + '/' + year;
    DatetimeNow = daynow;
    $(".responsive-calendar").responsiveCalendar('clearAll');
    $(".responsive-calendar").responsiveCalendar({
        time: year + "-" + mon
    });
    $('.responsive-calendar').responsiveCalendar(year + "-" + mon);
    $('.responsive-calendar').responsiveCalendar('edit', JSON.parse(getCalendar(JSON.stringify({ Year: year, Month: mon }))));
    $('ul li.active').removeClass('active');
    $('#atli').addClass('active');
    loadHtmlBook(NameBook);
    loaddata(daynow);
    $('#txtDay').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true
    });
    $('#txtDateSowing').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true
    });
    $('#txtDateBuy').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true
    });
    $('#txtEndtime').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true
    });
    $('#txtExpireDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true
    });

    $('#tabright').prop('hidden', false);
    $('#DateNote').modal('show');
}

//***Load DataBook 2
function loaddata(nday) {
    $('#txtDay').val(nday);
    DDListDataSeed(nday);
    var ddl = JSON.parse(getDatabook(JSON.stringify({ day: nday })));
    if (ddl.length != 0) {
        loadDetaildata(ddl[0].Key);
        Checkbtndel(nday);
    }
    else {
        loadDetaildata("");
    }
    $('#ListCV').html(Listlb(ddl, "success"));
    checkdayIsActive(nday);
}
//***Book Delete page
function BookDelete() {
    if (SaveAnything("Book", "DelbyKey", JSON.stringify({ Key: NameBooks + "|" + Keyid }))) {
        ShowMess('confirmDelSucc', 3000);
        $('.responsive-calendar').responsiveCalendar('clearAll');
        $('.responsive-calendar').responsiveCalendar('edit', JSON.parse(getCalendar(JSON.stringify({ Year: y, Month: m }))));
        $("#divdel").html("");
        loaddata($('#txtDay').val());
        checkcount();
    }
    else {
        $('#confirmDelerro').modal('show');
    }
}

//***Even Menuleft Book 
function SubMenuClick(name) {
    var ddl = "";
    dayoflist = DatetimeNow;
    if (name == 'lich') {
        $("#divcalendar").prop("hidden", false);
        $('#txtDay').val(DatetimeNow);
        Checkbtndel(DatetimeNow);
        ddl = JSON.parse(getDatabook(JSON.stringify({ day: DatetimeNow })));
        $('#tabright').prop('hidden', false);
        DDListDataSeed(DatetimeNow);
        if (ddl.length != 0) {
            $('#ListCV').html(Listlb(ddl, "success"));
            loadDetaildata(ddl[0].Key);
            $("#divdel").html("<button type='button' class='btn btn-danger pull-right' style='margin-right: 15px;' id='Delbtn'>Xóa</button>");
        }
        else {
            $('#ListCV').html("");
            $("#divdel").html("");
            loadDetaildata("");
        }
    }
    else {
        $("#divcalendar").prop("hidden", true);
        ddl = JSON.parse(SaveAnything("Book", "LoadNewMenu", JSON.stringify({ Menu: name, Type: NameBooks, num: numday })));
        if (ddl.length != 0) {
            DDListDataSeed(ddl[0].DayGet);
            $('#ListCV').html(Listlb(ddl, "success"));
            loadDetaildata(ddl[0].Key);
            $('#txtDay').val(ddl[0].DayGet);
            checkdayIsActive(ddl[0].DayGet);
            Checkbtndel(ddl[0].DayGet);
            $('#tabright').prop('hidden', false);
        }
        else {
            $('#ListCV').html("<strong style='color: chocolate;'>Công việc của bạn hiện chưa có</strong>");
            loadDetaildata("");
            $('#txtDay').val(DatetimeNow);
            $("#divdel").html("");
            $('#tabright').prop('hidden', true);
        }
    }
    $(document).ready(function () {
        $('.list-group').on('click', '.list-group-item', function (event) {
            dayoflist = $(this).attr("value");
            checkdayIsActive(dayoflist);
            Checkbtndel(dayoflist);
            $('#txtDay').val(dayoflist);
        });

    });
}
//Check buttom Delete
function Checkbtndel(nday) {
    var contact = "";
    var dayht = parseDate(DatetimeNow, 0).getTime();
    var dayht7 = parseDate(DatetimeNow, numday).getTime();
    var DayClick = parseDate(nday, 0).getTime();
    if (DayClick >= dayht7) {
        contact = "<button type='button' class='btn btn-danger pull-right' style='margin-right: 15px;' id='Delbtn'>Xóa</button>";
    }
    $("#divdel").html(contact);
    $(document).ready(function () {
        $("#Delbtn").click(function () {
            $('#confirmDelete').modal('show');
        });
    });
}
//Đếm số công việc chưa làm
function checkcount() {
    countnow = SaveAnything("Book", "LoadCountsNotChecknow", JSON.stringify({}));
    countday = SaveAnything("Book", "LoadCountsNotCheckday", JSON.stringify({ num: numday }));
    countAllday = SaveAnything("Book", "LoadCountsNotCheckAll", JSON.stringify({ num: numday }));
    $("#divcountnow").html(countnow);
    $("#divcountday").html(countday);
    $("#divcountAllday").html(countAllday);
}
//Danh sách công việc chưa làm
function LoadCheckList(NameBook) {
    CheckSession();
    var contact = "";
    var nheader = ""
    if (NameBook == "checkdaynow") {
        nheader = "Công việc hôm nay";
        contact = checkBoxList('LoadListNotChecknow');
    } 
    if (NameBook == "checklastweek") {
        nheader = "Công việc tuần này";
        contact = checkBoxList('LoadListNotCheckday');
    }
    if (NameBook == "checkAllday") {
        nheader = "Công việc chưa làm";
        contact = checkBoxList('LoadListNotCheckAll');
    }
    $('#headerCheckListsCV').html(nheader);
    $('#CheckListsCV').html(contact);
    $(document).ready(function () {
        $("#CheckListsCV").css("overflow", "auto");
        $("#CheckListsCV").css("max-height", ($(document).height() - 270) + "px");
        $('input[type=checkbox]').on('change', function () {
            var KeyID = $(this).val();
            if ($(this).is(':checked')) {
                if (!SaveAnything("Book", "SaveListIsActive", JSON.stringify({ Key: KeyID, IsActive: 1 }))) { $('#confirmMessError').modal('show'); return false; }
            } else {
                if (!SaveAnything("Book", "SaveListIsActive", JSON.stringify({ Key: KeyID, IsActive: 0 }))) { $('#confirmMessError').modal('show'); return false; }
            }
            checkcount();
        });
    });
    $('#ListsCV').modal('show');
}
function checkdayIsActive(nday) {
    var dayht = parseDate(DatetimeNow, 0).getTime();
    var dayht7 = parseDate(DatetimeNow, numday).getTime();
    var DayClick = parseDate(nday, 0).getTime();
    $("#divcb").prop("hidden", false);
    if (DayClick > dayht) {
        $("#divcb").prop("hidden", true);
        $('#fieldset').prop('disabled', false);
    }
    if (DayClick < dayht7) {
        $("#cbActive").prop("disabled", true);
        $('#fieldset').prop('disabled', true);
    }
    if (DayClick >= dayht7 & DayClick <= dayht) {
        $("#cbActive").prop("disabled", false);
        $('#fieldset').prop('disabled', false);
    }
    $('.selectpicker').selectpicker('refresh');
}

function CheckSession() {
    if (!SaveAnything("ajax", "CheckSession", JSON.stringify({}))) {
        window.location = "/login.aspx";
        return false;
    }
}


//HTMLload danh sách sự kiện của ngày
function Listlb(ddl, type) {
    var result = "";
    var ddlist = "";
    var bg = "background-color: #18BC9C;";
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        if (DS.IsActive != 'True') {
            type = "danger"
        }
        else {
            type = "success"
        }
        ddlist += "<a href='#' onclick='loadDetaildata(" + DS.Key + ")'  value='" + DS.DayGet + "' class='list-group-item list-group-item-" + type + "'>" + DS.Name + "</a>";
    }
    result = "<div class='list-group'>" + ddlist + "</div>";
    return result;
}
//HTML TextBox
function txtbox(name, id) {
    var result = "";
    result = "<div class='form-group has-feedback'>" +
                "<label for='contact-name' class='col-sm-4 control-label'>" + name + "</label>" +
                "<div class='col-sm-8 has-error'>" +
                    "<input type='text' class='form-control' id='" + id + "' placeholder=''>" +
                    "<span id='sp-" + id + "'></span>" +
                "</div>" +
              "</div>";
    return result;
}
//HTML Textbox block
function txtboxdisable(name, id) {
    var result = "";
    result = "<div class='form-group has-feedback'>" +
                "<label for='contact-name' class='col-sm-4 control-label'>" + name + "</label>" +
                "<div class='col-sm-8 has-error'>" +
                    //"<input type='text' class='form-control' id='" + id + "' placeholder='' disabled>" +
                    "<input type='text' class='form-control' id='" + id + "' placeholder=''>" +
                    "<span id='sp-" + id + "'></span>" +
                "</div>" +
              "</div>";
    return result;
}
//HTML 2 Textbox
function txtbox2(name, id1, id2) {
    var result = "";
    result = "<div class='form-group has-feedback'>" +
                "<label for='contact-name' class='col-sm-4 control-label'>" + name + "</label>" +
                "<div class='col-sm-4 has-error'>" +
                    "<input type='text' class='form-control' id='" + id1 + "' placeholder=''>" +
                    "<span id='sp-" + id1 + "'></span>" +
                "</div>" +
                "<div class='col-sm-4 has-error'>" +
                    "<input type='text' class='form-control' id='" + id2 + "' placeholder=''>" +
                    "<span id='sp-" + id2 + "'></span>" +
                "</div>" +
              "</div>"
    return result;
}
//HTML 2 DateTime Form To
function DateTimeFormTo(name1,id1,name2,id2) {
    var result = "";
    result = "<div class='form-group has-feedback'>" +
                "<label style='padding-top: 20px;' for='contact-name' class='col-xs-2 control-label'>" + name1 + "</label>" +
                "<div class='col-xs-4 has-error'>" +
                    "<input type='text' style='padding: 5px;' class='form-control' id='" + id1 + "' placeholder=''>" +
                    "<span id='sp-" + id1 + "'></span>" +
                "</div>" +
                "<label style='padding-top: 20px;' for='contact-name' class='col-xs-2 control-label'>" + name2 + "</label>" +
                "<div class='col-xs-4 has-error'>" +
                    "<input type='text' style='padding: 5px;' class='form-control' id='" + id2 + "' placeholder=''>" +
                    "<span id='sp-" + id2 + "'></span>" +
                "</div>" +
              "</div>"
    return result;
}

//HTML TextBox & DropDownList
function txtboxdd(name, id1, id2, load) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(getList(load));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<div class='form-group has-feedback'>" +
                "<label for='contact-name' class='col-xs-12 col-sm-4 control-label'>" + name + "</label>" +
                "<div class='col-xs-6 col-sm-4 has-error'>" +
                    "<input type='number' class='form-control' id='" + id1 + "' placeholder=''>" +
                    "<span id='sp-" + id1 + "'></span>" +
                "</div>" +
                "<div class='col-xs-6 col-sm-4 has-error'>" +
                    "<select id='" + id2 + "' class='selectpicker' data-live-search='true' style='display: none;'data-size='auto' data-width='100%'>" +
                        "<option value='0'>Chọn</option>" +
                            ddlist +
                        "</select>" +
                        "<span id='sp-" + id2 + "'></span>" +
                "</div>" +
              "</div>"
    return result;
}
function txtboxddNo(name, id1, id2, load) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(getList(load));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<div class='form-group has-feedback'>" +
                "<label for='contact-name' class='col-xs-12 col-sm-4 control-label'>" + name + "</label>" +
                "<div class='col-xs-6 col-sm-4 has-error'>" +
                    "<input type='text' class='form-control' id='" + id1 + "' placeholder=''>" +
                    "<span id='sp-" + id1 + "'></span>" +
                "</div>" +
                "<div class='col-xs-6 col-sm-4 has-error'>" +
                    "<select id='" + id2 + "' class='selectpicker' data-live-search='true' style='display: none;'data-size='auto' data-width='100%'>" +
                        "<option value='0'>Chọn</option>" +
                            ddlist +
                        "</select>" +
                        "<span id='sp-" + id2 + "'></span>" +
                "</div>" +
              "</div>"
    return result;
}
//HTML CheckBox
function checkBox(name, id) {
    var result = "";
    result = "<div id='divcb' class='form-group has-feedback'>" +
                "<label for='contact-name' class='col-sm-4 control-label'>" + name + "</label>" +
                "<div class='col-sm-8 has-error'>" +
                    "<input type='checkbox' style='margin-bottom: -15px;' id='" + id + "'>" +
                "</div>" +
              "</div>";
    return result;
}
//HTML List CheckBox
function checkBoxList(name) {
    var result = "";
    var ddl = JSON.parse(SaveAnything("Book", name, JSON.stringify({ num: numday })));
    var type = ""
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        var htmlcheck = "<label class='checkbox-inline'><input type='checkbox' style='margin-top: -5px;' value='" + DS.Key + "'></label>";
        var padleft = "30px";
        if (DS.Color == "black") {
            htmlcheck = "";
            padleft = "50px";
        }
        if (DS.Type == type) {
            result += "<div class='checkbox' style='padding-left: "+padleft+";  padding-bottom: 10px; color: "+DS.Color+"'>" +
                        htmlcheck + "<a href='#' onClick='callBook(this.id)' style='color: " + DS.Color + "'  id ='" + DS.Bookname + "-" + DS.Datetime + "-" + DS.Key + "' data-toggle='modal'>" + DS.Name + "</a>" +
                   "</div>";
        }
        else {
            result += "<div class='form-group has-feedback' style='margin-bottom: -10px;'><label for='contact-name' class='control-label'>" + DS.Type + "</label></div>" +
            "<hr/><div class='checkbox' style='padding-left: " + padleft + ";  padding-bottom: 10px; color: " + DS.Color + "'>" +
                        htmlcheck + "<a href='#'  onClick='callBook(this.id)'  style='color: " + DS.Color + "'  id ='" + DS.Bookname + "-" + DS.Datetime + "-" + DS.Key + "' data-toggle='modal'>" + DS.Name + "</a>" +
                   "</div>";
        }
        type = DS.Type;
    }
    return result;
}
//HTML DropDown data theo HTX
function DDListDataSeed(nday) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(SaveAnything('Book', 'LoadSeedProcessDay', JSON.stringify({ dayclick: nday })));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<option value='0'>Chọn</option>" + ddlist;
    $('#ddlSeedsKey').html(result);
    $('.selectpicker').selectpicker('refresh');
}
//HTML DropDown data theo Type
function DDListDataType(type) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(SaveAnything('Book', 'LoadFertilizersORPesticides', JSON.stringify({ Type: type })));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<option value='0'>Chọn</option>" + ddlist;
    $('#ddlFertilizersPesticides').html(result);
    $('.selectpicker').selectpicker('refresh');
}
//HTML DropDown
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
//HTML DropDown
function DDListStyle(name, id, load,col1,col2,Text) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(getList(load));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<div class='form-group has-feedback'>" +
                                "<label style='  padding-top: 5px;' for='contact-name' class='col-xs-" + col1 + " control-label'>" + name + "</label>" +
                                "<div class='col-xs-" + col2 + " has-error' style='padding: 0px;'>" +
                                    "<select id='" + id + "' class='selectpicker' data-live-search='true' style='display: none;'data-size='auto' data-width='100%'>" +
                                    "<option value='0'>" + Text + "</option>" +
                                        ddlist +
                                    "</select>" +
                                    "<span id='sp-" + id + "'></span>" +
                                "</div>" +
                            "</div>";
    return result;
}
//HTML DropDown Null
function DDListNodata(name, id) {
    var result = "";
    result = "<div class='form-group has-feedback'>" +
                                "<label for='contact-name' class='col-sm-4 control-label'>" + name + "</label>" +
                                "<div class='col-sm-8 has-error'>" +
                                    "<select id='" + id + "' class='selectpicker' data-live-search='true' style='display: none;'data-size='auto' data-width='100%'>" +
                                    "<option value='0'>Chọn</option>" +
                                    "</select>" +
                                    "<span id='sp-" + id + "'></span>" +
                                "</div>" +
                            "</div>";
    return result;
}
//Ajax
function getCalendar(valu) {
    $.ajax(
                {
                    type: "POST",
                    url: "Book.aspx/" + Bookid,
                    data: valu,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        result = response.d;
                    }
                });
    return result;
}
function getList(name) {
    var result = "";
    
    $.ajax({
        type: "POST",
        url: "Book.aspx/" + name,
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            result = response.d;
        }
    });
    return result;
}
function getDatabookMenu(valu) {
    $.ajax(
                {
                    type: "POST",
                    url: "Book.aspx/" + Bookdata + "Menu",
                    data: valu,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        result = response.d;
                    }
                });
    return result;
}
function getDatabook(valu) {
    $.ajax(
                {
                    type: "POST",
                    url: "Book.aspx/" + Bookdata,
                    data: valu,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        result = response.d;
                    }
                });
    return result;
}
function getDatabookByID(valu) {
    $.ajax(
                {
                    type: "POST",
                    url: "Book.aspx/" + Bookdata + "ID",
                    data: valu,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        result = response.d;
                    }
                });
    return result;
}
function SaveDatabook(valu) {
    var result = false;
    $.ajax(
                {
                    type: "POST",
                    url: "Book.aspx/Save" + NameBooks,
                    data: valu,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        result = response.d;
                    }
                });
    return result;
}
function getServer(name) {
    var result = new Array;
    $.ajax({
        type: "POST",
        url: "ajax.aspx/" + name,
        data: [],
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            result = response.d;
        }
    });
    return result;
};
