function DDListProcess(name, value) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(SaveAnything('ajax', value, JSON.stringify({ ddlCooperative_Key: $('#ddlCooperative_Key').val(), Type: $('#ddlProcessPlant_Type').val(), SeedsKeyProcess: $('#ddlSeedsKeyProcess').val() })));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<option value='0'>Chọn</option>" + ddlist;
    $('#' + name).html(result);
    $('.selectpicker').selectpicker('refresh');
}
function DDListProcessUnit(name, value) {
    var result = "";
    var ddlist = "";
    var ddl = JSON.parse(SaveAnything('ajax', value, JSON.stringify({})));
    for (var i = 0; i < ddl.length; i++) {
        var DS = ddl[i];
        ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
    }
    result = "<option value='0'>Chọn</option>" + ddlist;
    $('#' + name).html(result);
    $('.selectpicker').selectpicker('refresh');
}
function SaveProcess() {
    CheckSession();
    if (!Checkddl("ddlSeedsKeyProcess", 0)) { ShowMess('confirmddNull', 4000); return false; }
    if (!Checkddl("ddlProcessPlantDetail", 0)) { ShowMess('confirmddNull', 4000); return false; }
    if (!CheckText("txtDateStart", 0)) { ShowMess('confirmddNull', 4000); return false; }
    if ($('#ddlProcessPlant_Type').val() == 1)
    {
        if (!CheckNum("txtAreaP")) { ShowMess('confirmddNull', 4000); return false; }
        if (!Checkddl("ddlUnitP1", 0)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtQuantityP")) { ShowMess('confirmddNull', 4000); return false; }
        if (!Checkddl("ddlUnitP2", 0)) { ShowMess('confirmddNull', 4000); return false; }
    }
    if (!SaveAnything('ajax', 'SaveExportProcess', JSON.stringify({ Key: $('#ddlProcessPlantDetail').val(), Type: $('#ddlProcessPlant_Type').val(), SeedKey: $('#ddlSeedsKeyProcess').val(), day: $('#txtDateStart').val(), Area: $('#txtAreaP').val(), AreaUnit: $('#ddlUnitP1').val(), Quantity: $('#txtQuantityP').val(), QuantityUnit: $('#ddlUnitP2').val() }))) { $('#confirmMessError').modal('show'); return false; }
    if (this.location.pathname == "/Book.aspx") {
        checkcount();
    }
    ShowMess('confirmMessSucc', 3000);

}