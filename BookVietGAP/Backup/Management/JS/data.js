
//OnLoad
function OnLoadAll() {
    //Load Count Nhắc nhở
    checkcount();
    //Load Menu Left
    $('ul li').on('click', function (event) {
        $('ul li.active').removeClass('active');
        $(this).addClass('active');
    });
    //Load Calendar
    var d = new Date();
    var mon = (d.getMonth() + 1);
    var year = d.getFullYear();
    m = mon;y = year;
    $(".responsive-calendar").responsiveCalendar({
        onDayClick: function (events) {
            var day = $(this).data('day') + "/" + $(this).data('month') + '/' + $(this).data('year');
            $("#divdel").html("");
            loaddata(day);
            if (DayClass != undefined) {
                $(DayClass).removeClass('daynow');
            }
            $(this).addClass('daynow');
            DayClass = this;
        },
        onDayHover: function (events) { },
        onActiveDayClick: function (events) {
        },
        onActiveDayHover: function (events) { },
        onMonthChange: function (events) {
            mon = this.currentMonth + 1;
            year = this.currentYear;
            m = mon; y = year;
            $('.responsive-calendar').responsiveCalendar('edit', JSON.parse(getCalendar(JSON.stringify({ Year: year, Month: mon }))));
        }
    });
    //Load danh sách cây trồng của bạn
    $("#ListSeedsClick").click(function () {
        $("#SeedsResult").html((getServer("ListSeeds")));
        $(document).ready(function () {
            $("#SeedsResult").css("overflow", "auto");
            $("#SeedsResult").css("max-height", ($(document).height() - 370) + "px");
        });
    });

}
//Book 1 - Add Html Book
function loadHtmlBook(NameBook) {
    var contact = "";
    var nheader = ""
    if (NameBooks == "LandUse") {
        nheader = "Quản lý sử dụng đất";
        contact = txtboxdisable("Ngày thực hiện", "txtDay") +
                DDList("Cây trồng", "ddlCategory", "LoadSeeds") +
                txtbox("Công việc", "txtAction") +
                txtbox("Lý do thực hiện", "txtReason") +
                txtbox("Phương pháp", "txtSolution") +
                txtbox("Ghi chú", "txtNote") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "SeedsProcess") {
        nheader = "Quản lý giống";
        contact = txtboxdisable("Ngày trồng", "txtDay") +
                txtbox("Ngày gieo hạt", "txtDateSowing") +
                DDList("Tên Giống", "ddlCategory", "LoadSeeds") +
                txtbox("Nguồn gốc giống", "txtCompany") +
                txtbox("Ngày mua", "txtDateBuy") +
                txtbox("Thời gian thu hoạch", "txtEndtime") +
                txtbox("Mã lô", "txtParcel") +
                txtboxdd("Diện tích", "txtArea", "ddlUnit1", "LoadUnit") +
                txtboxdd("Tổng lượng giống", "txtQuantity", "ddlUnit2", "LoadUnit") +
                txtbox("Tổng tiền (vnđ)", "txtTotal") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "CompostingOrganic") {
        nheader = "Quản lý ủ phân hữu cơ";
        contact = txtboxdisable("Ngày thực hiện", "txtDay") +
                DDList("Loại phân", "ddlCategory", "LoadFertilizerOrganic") +
                txtboxdd("Khối lượng", "txtQuantity", "ddlUnit", "LoadUnit") +
                txtbox("Phương pháp ủ", "txtMethod") +
                txtbox("Thời gian ủ (ngày)", "txtCompostingDates") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "Fertilizer_Buy") {
        nheader = "Quản lý mua phân bón";
        contact = txtboxdisable("Ngày mua", "txtDay") +
                DDListNodata("Cây trồng", "ddlSeedsKey") +
                DDList("Tên phân bón", "ddlCategory", "LoadFertilizers") +
                txtboxdd("Số lượng", "txtQuantity", "ddlUnit", "LoadUnit") +
                txtbox("Tổng tiền (vnđ)", "txtTotal") +
                txtbox("Tên & địa chỉ mua", "txtAddress") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "Fertilizer_Use") {
        nheader = "Quản lý sử dụng phân bón";
        contact = txtboxdisable("Ngày thực hiện", "txtDay") +
                DDListNodata("Cây trồng", "ddlSeedsKey") +
                txtbox("Công việc", "txtParcel") +
                txtbox("Lý do/Mật độ", "txtArea") +
                txtbox("Phương pháp", "txtHowtouse") +
                DDList("Thiết bị", "DDLEquipment", "Equipment") +
                DDList("Tên phân bón", "ddlCategory", "LoadFertilizers") +
                txtboxdd("Tồng lượng", "txtFormulaUsed", "ddlUnit", "LoadUnit") +
//                txtbox("Tổng lượng/ Lit nước", "txtQuantity") +
//                txtbox("TGCL(Ngày)", "txtQuarantinePeriod") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "PesticideBuy") {
        nheader = "Quản lý mua thuốc bảo vệ thực vật";
        contact = txtboxdisable("Ngày mua", "txtDay") +
                DDListNodata("Cây trồng", "ddlSeedsKey") +
                DDList("Tên Thuốc", "ddlCategory", "LoadPesticide") +
                txtboxddNo("Số lượng", "txtQuantity", "ddlUnit", "LoadUnit") +
                txtbox("Tổng tiền (vnđ)", "txtTotal") +
                txtbox("Tên & địa chỉ mua", "txtAddress") +
                checkBox("Thực hiện xong", "cbActive");
    }

    if (NameBooks == "Pesticide_Use") {
        nheader = "Quản lý sử dụng thuốc BVTV";
        contact = txtboxdisable("Ngày thực hiện", "txtDay") +
                DDListNodata("Cây trồng", "ddlSeedsKey") +
                txtbox("Công việc", "txtParcel") +
                txtbox("Lý do/Mật độ", "txtArea") +
                txtbox("Phương pháp", "txtHowtouse") +
                DDList("Thiết bị", "DDLEquipment", "Equipment") +
                DDList("Tên Thuốc", "ddlCategory", "LoadPesticide") +
                txtboxddNo("Liều lượng", "txtFormulaUsed", "ddlUnit", "LoadUnit") +
                txtbox("Tổng lượng/ Lít nước", "txtQuantity") +
                txtbox("Thời gian cách ly", "txtQuarantinePeriod") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "HarvestedForSale") {
        nheader = "Quản lý thu hoạch xuất bán";
        contact = txtboxdisable("Ngày thực hiện", "txtDay") +
                DDListNodata("Cây trồng", "ddlSeedsKey") +
                txtbox("Mã truy vết", "txtCode") +
                txtboxdd("Số lượng thu hoạch", "txtQuantityHarvested", "ddlUnit", "LoadUnit") +
                txtboxdd("Số lượng xuất bán", "txtQuantitySale", "ddlUnit2", "LoadUnit") +
                txtbox("Tổng tiền (vnđ)", "txtTotal") +
                txtbox("Nơi mua", "txtWhereToBuy") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "CheckEquipment") {
        nheader = "Quản lý kiểm tra thiết bị";
        contact = txtboxdisable("Ngày thực hiện", "txtDay") +
                DDListNodata("Loại giống", "ddlSeedsKey") +
                DDList("Loại thiết bị", "ddlEquipment", "Equipment") +
                txtbox("Hoạt động", "txtAction") +
                txtbox("Nội dung thực hiện", "txtInfo") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "HandlingPackaging") {
        nheader = "Quản lý xử lý chất thải";
        contact = txtboxdisable("Ngày xử lý", "txtDay") +
                txtbox("Loại chất thải", "txtType") +
                txtbox("Nơi lưu trữ", "txtPlace") +
                txtbox("Cách xử lý", "txtTreatment") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "Inventory") {
        nheader = "Kiểm kê tồn kho";
        contact = txtboxdisable("Ngày kiểm kê", "txtDay") +
                DDList("Loại hàng", "ddlType", "LoadInventory_Type") +
                DDListNodata("Tên hàng", "ddlFertilizersPesticides") +
                txtboxdd("Số lượng", "txtQuantity", "ddlUnit", "LoadUnit") +
                txtbox("Hạn dùng", "txtExpireDate") +
                checkBox("Thực hiện xong", "cbActive");
    }
    if (NameBooks == "CheckAssessment") {
        nheader = "Kiểm tra đánh giá";
        contact = txtboxdisable("Ngày kiểm tra", "txtDay") +
                DDListNodata("Loại cây", "ddlSeedsKey") +
                txtbox("Mô tả lỗi", "txtDescribesError") +
                txtbox("Phương pháp xử lý", "txtMethod") +
                checkBox("Thực hiện xong", "cbActive");
    }
    $('#nheader').html(nheader);
    $('#Divcontact').html(contact);
    $('.selectpicker').selectpicker({ style: 'btn-default' });
    $('.selectpicker').selectpicker('refresh');
    $("#divcalendar").prop("hidden", false);
    $(document).ready(function () {
        $("#cbActive").change(function () {
            if ($(this).is(":checked")) {
                IsActive = true;
            } else {
                IsActive = false;
            }
        });
        $("#ddlType").change(function () {
            DDListDataType($('#ddlType').val());
        });
        $("#txtDay").change(function () {
            var timeday = $('#txtDay').val().split("/");
            y = timeday[2]; m = timeday[1];
            $('.responsive-calendar').responsiveCalendar(timeday[2] + "-" + timeday[1]);
            $('.responsive-calendar').responsiveCalendar('edit', JSON.parse(getCalendar(JSON.stringify({ Year: timeday[2], Month: timeday[1] }))));
            loaddata($('#txtDay').val());
        });
    });

}
//Book 2 - Load data Book
function loadDetaildata(id) {
    var ddl = JSON.parse(getDatabookByID(JSON.stringify({ id: id })));
    if (ddl[0].IsActive == 'False' | ddl[0].IsActive == "" | ddl[0].IsActive == "0") { IsActive = false } else { IsActive = true }
    $("#cbActive").prop("checked", IsActive);
    if (id == "") { $("#cbActive").prop("checked", false); }
    if (NameBooks == "LandUse") {
        Keyid = ddl[0].Key;
        $('#ddlCategory').val(ddl[0].SeedsKey);
        $('#txtAction').val(ddl[0].txtAction);
        $('#txtReason').val(ddl[0].txtReason);
        $('#txtSolution').val(ddl[0].txtSolution);
        $('#txtNote').val(ddl[0].txtNote);
        if (id == "") {
            $('#txtAction').val('Làm đất, gieo hạt, bón phân');
            $('#txtReason').val('Chuẩn bị gieo hạt');
            $('#txtSolution').val('Xới dất, bón phân, làm cỏ...');
        }
    }
    if (NameBooks == "SeedsProcess") {
        Keyid = ddl[0].Key;
        $('#txtDateSowing').val(ddl[0].DateSowing);
        $('#ddlCategory').val(ddl[0].Name);
        $('#txtCompany').val(ddl[0].CompanyName);
        $('#txtDateBuy').val(ddl[0].txtDateBuy);
        $('#txtEndtime').val(ddl[0].txtEndtime);
        $('#txtQuantity').val(ddl[0].Quantity);
        $('#txtParcel').val(ddl[0].txtParcel);
        $('#txtArea').val(ddl[0].txtArea);
        $('#ddlUnit1').val(ddl[0].ddlUnit1);
        $('#ddlUnit2').val(ddl[0].ddlUnit2);
        $('#txtTotal').val(ddl[0].Total);
        if (id == "") {
            $('#txtQuantity').val('');
            $('#txtParcel').val('');
            $('#txtCompany').val('Giống địa phương...');
            $('#txtTotal').val('0');
            $('#txtArea').val('');
            $('#ddlUnit1').val(1);
            $('#ddlUnit2').val(3);
        }
    }

    if (NameBooks == "CompostingOrganic") {
        Keyid = ddl[0].Key;
        $('#ddlCategory').val(ddl[0].Name);
        $('#txtQuantity').val(ddl[0].Quantity);
        $('#ddlUnit').val(ddl[0].Unit);
        $('#txtMethod').val(ddl[0].Method);
        $('#txtCompostingDates').val(ddl[0].CompostingDates);
        if (id == "") {
            $('#txtQuantity').val('');
            $('#ddlUnit').val(3);
            $('#txtMethod').val('');
            $('#txtCompostingDates').val('');
        }
    }
    if (NameBooks == "Fertilizer_Buy") {
        Keyid = ddl[0].Key;
        $('#ddlCategory').val(ddl[0].Name);
        $('#txtQuantity').val(ddl[0].Quantity);
        $('#ddlUnit').val(ddl[0].Unit);
        $('#txtPrice').val(ddl[0].Price);
        $('#txtAddress').val(ddl[0].Address);
        $('#ddlSeedsKey').val(ddl[0].SeedsKey);
        $('#txtTotal').val(ddl[0].Total);
        if (id == "") {
            $('#txtQuantity').val('');
            $('#ddlUnit').val(3);
            $('#txtTotal').val('0');

        }
    }
    if (NameBooks == "Fertilizer_Use") {
        Keyid = ddl[0].Key;
        $('#ddlSeedsKey').val(ddl[0].SeedsKey);
        $('#txtParcel').val(ddl[0].txtParcel);
        $('#txtArea').val(ddl[0].txtArea);
        $('#txtHowtouse').val(ddl[0].txtHowtouse);
        $('#DDLEquipment').val(ddl[0].DDLEquipment);
        $('#ddlCategory').val(ddl[0].Name);
        $('#txtFormulaUsed').val(ddl[0].txtFormulaUsed);
//        $('#txtQuantity').val(ddl[0].txtQuantity);
//        $('#txtQuarantinePeriod').val(ddl[0].txtQuarantinePeriod);
        $('#ddlUnit').val(ddl[0].ddlUnit);
        if (id == "") {
            $('#txtParcel').val('Bón phân...');
            $('#txtArea').val('Bón lót, bón thúc...');
            $('#txtHowtouse').val('Rải thủ công...');
            $('#DDLEquipment').val(5);
            $('#ddlUnit').val(3);
            $('#txtFormulaUsed').val('');
        }
    }
    if (NameBooks == "PesticideBuy") {
        Keyid = ddl[0].Key;
        $('#ddlCategory').val(ddl[0].Name);
        $('#txtQuantity').val(ddl[0].Quantity);
        $('#ddlUnit').val(ddl[0].Unit);
        $('#txtPrice').val(ddl[0].Price);
        $('#ddlPesticideCompanie').val(ddl[0].CompanyKey);
        $('#txtAddress').val(ddl[0].Address);
        $('#ddlSeedsKey').val(ddl[0].SeedsKey);
        $('#txtTotal').val(ddl[0].Total);
        if (id == "") {
            $('#ddlUnit').val(7);
            $('#txtQuantity').val('');
            $('#txtTotal').val('0');
        }
    }
    if (NameBooks == "Pesticide_Use") {
        Keyid = ddl[0].Key;
        $('#ddlSeedsKey').val(ddl[0].SeedsKey);
        $('#txtParcel').val(ddl[0].txtParcel);
        $('#txtArea').val(ddl[0].txtArea);
        $('#txtHowtouse').val(ddl[0].txtHowtouse);
        $('#DDLEquipment').val(ddl[0].DDLEquipment);
        $('#ddlCategory').val(ddl[0].Name);
        $('#txtFormulaUsed').val(ddl[0].txtFormulaUsed);
        $('#txtQuantity').val(ddl[0].txtQuantity);
        $('#txtQuarantinePeriod').val(ddl[0].txtQuarantinePeriod);
        $('#ddlUnit').val(ddl[0].ddlUnit);
        if (id == "") {
            $('#txtParcel').val('Phun thuốc...');
            $('#txtArea').val('Sâu...');
            $('#txtHowtouse').val('Phun...');
            $('#DDLEquipment').val(1);
            $('#txtFormulaUsed').val('');
            $('#ddlUnit').val(7);
            $('#txtQuantity').val('');
            $('#txtQuarantinePeriod').val('');
        }
    }
    if (NameBooks == "HarvestedForSale") {
        Keyid = ddl[0].Key;
        $('#ddlSeedsKey').val(ddl[0].SeedsKey);
        $('#txtCode').val(ddl[0].txtCode);
        $('#txtQuantityHarvested').val(ddl[0].txtQuantityHarvested);
        $('#txtQuantitySale').val(ddl[0].txtQuantitySale);
        $('#txtWhereToBuy').val(ddl[0].txtWhereToBuy);
        $('#ddlUnit').val(ddl[0].txtUnitKey);
        $('#ddlUnit2').val(ddl[0].txtUnitKey);
        $('#txtTotal').val(ddl[0].Total);
        if (id == "") {
            $('#txtQuantityHarvested').val('');
            $('#txtQuantitySale').val('');
            $('#ddlUnit').val(3);
            $('#ddlUnit2').val(3);
            $('#txtTotal').val("0");
        }
    }
    if (NameBooks == "CheckEquipment") {
        Keyid = ddl[0].Key;
        $('#ddlSeedsKey').val(ddl[0].SeedsKey);
        $('#ddlEquipment').val(ddl[0].txtEquipmentKey);
        $('#txtAction').val(ddl[0].txtAction);
        $('#txtInfo').val(ddl[0].txtInfo);
        if (id == "") {
            $('#txtAction').val('Vệ sinh');
            $('#txtInfo').val('Rửa, phơi, xấy...');
        }
    }
    if (NameBooks == "HandlingPackaging") {
        Keyid = ddl[0].Key;
        $('#txtType').val(ddl[0].txtType);
        $('#txtPlace').val(ddl[0].txtPlace);
        $('#txtTreatment').val(ddl[0].txtTreatment);
        if (id == "") {
            $('#txtType').val('Bao nilon ,thủy tinh,chay nhựa...');
            $('#txtPlace').val('Kho...');
            $('#txtTreatment').val('Chôn, đốt...');
        }
    }

    if (NameBooks == "Inventory") {
        Keyid = ddl[0].Key;
        $('#ddlType').val(ddl[0].Type);
        DDListDataType($('#ddlType').val());
        $('#ddlFertilizersPesticides').val(ddl[0].FertilizersPesticides);
        $('#txtQuantity').val(ddl[0].Quantity);
        $('#ddlUnit').val(ddl[0].Unit);
        $('#txtExpireDate').val(ddl[0].ExpireDate);
        if (id == "") {
            $('#txtQuantity').val('');
            $('#ddlUnit').val(3);
            $('#txtExpireDate').val(DatetimeNow);
        }
    }
    if (NameBooks == "CheckAssessment") {
        Keyid = ddl[0].Key;
        $('#ddlSeedsKey').val(ddl[0].SeedsKey);
        $('#txtDescribesError').val(ddl[0].DescribesError);
        $('#txtMethod').val(ddl[0].Method);
        if (id == "") {
            $('#txtDescribesError').val('');
            $('#txtMethod').val('');
        }
    }
    $('.selectpicker').selectpicker('refresh');
}
//Book 3 Check Null
function BookcheckNull() {
    if (NameBooks == "LandUse") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlCategory", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtAction", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtReason", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtSolution", 1)) { ShowMess('confirmMessNull', 4000); return false; }
    }
    if (NameBooks == "SeedsProcess") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("txtDateSowing", 8)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlCategory", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtCompany", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("txtDateBuy", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtEndtime", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtArea", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit1", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtQuantity", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit2", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtTotal", 1)) { ShowMess('confirmNumNull', 4000); return false; }
    }
    if (NameBooks == "CompostingOrganic") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlCategory", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtQuantity", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtMethod", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtCompostingDates", 1)) { ShowMess('confirmNumNull', 4000); return false; }
    }
    if (NameBooks == "Fertilizer_Buy") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlSeedsKey", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!Checkddl("ddlCategory", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtQuantity", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtAddress", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtTotal", 1)) { ShowMess('confirmNumNull', 4000); return false; }
    }
    if (NameBooks == "Fertilizer_Use") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlSeedsKey", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtParcel", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtArea", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtHowtouse", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("DDLEquipment", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!Checkddl("ddlCategory", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtFormulaUsed", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit", 1)) { ShowMess('confirmddNull', 4000); return false; }
        //                    if (!CheckNum("txtQuantity", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        //                    if (!CheckNum("txtQuarantinePeriod", 1)) { ShowMess('confirmNumNull', 4000); return false; }
    }
    if (NameBooks == "PesticideBuy") {
        if (!CheckText("txtDay", 0)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlCategory", 0)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtQuantity", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtAddress", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtTotal", 1)) { ShowMess('confirmNumNull', 4000); return false; }
    }
    if (NameBooks == "Pesticide_Use") {
        if (!CheckText("txtDay", 0)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlSeedsKey", 0)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtParcel", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtArea", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtHowtouse", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("DDLEquipment", 0)) { ShowMess('confirmddNull', 4000); return false; }
        if (!Checkddl("ddlCategory", 0)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtFormulaUsed", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit", 0)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtQuantity", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtQuarantinePeriod", 1)) { ShowMess('confirmNumNull', 4000); return false; }
    }

    if (NameBooks == "CheckEquipment") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlSeedsKey", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!Checkddl("ddlEquipment", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtAction", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtInfo", 1)) { ShowMess('confirmMessNull', 4000); return false; }
    }
    if (NameBooks == "HarvestedForSale") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlSeedsKey", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtCode", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtQuantityHarvested", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtQuantitySale", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit2", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtWhereToBuy", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckNum("txtTotal", 1)) { ShowMess('confirmNumNull', 4000); return false; }
    }
    if (NameBooks == "HandlingPackaging") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtType", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtPlace", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtTreatment", 1)) { ShowMess('confirmMessNull', 4000); return false; }
    }
    if (NameBooks == "Inventory") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlType", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!Checkddl("ddlFertilizersPesticides", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckNum("txtQuantity", 1)) { ShowMess('confirmNumNull', 4000); return false; }
        if (!Checkddl("ddlUnit", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtExpireDate", 1)) { ShowMess('confirmMessNull', 4000); return false; }
    }
    if (NameBooks == "CheckAssessment") {
        if (!CheckText("txtDay", 5)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!Checkddl("ddlSeedsKey", 1)) { ShowMess('confirmddNull', 4000); return false; }
        if (!CheckText("txtDescribesError", 1)) { ShowMess('confirmMessNull', 4000); return false; }
        if (!CheckText("txtMethod", 1)) { ShowMess('confirmMessNull', 4000); return false; }
    }
    $('#confirmSaveEdit').modal('show');
}
//Book 4 Save info
function BookSaveSE(KeyID) {
    if (NameBooks == "SeedsProcess") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            DateSowing: $('#txtDateSowing').val(),
            company: $('#txtCompany').val(),
            DateBuy: $('#txtDateBuy').val(),
            Endtime: $('#txtEndtime').val(),
            SeedsKey: $('#ddlCategory').val(),
            txtQuantity: $('#txtQuantity').val(),
            txtParcel: $('#txtParcel').val(),
            txtArea: $('#txtArea').val(),
            AreaUnit: $('#ddlUnit1').val(),
            QuantityUnit: $('#ddlUnit2').val(),
            Total: $('#txtTotal').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "CompostingOrganic") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            FertilizerOrganicKey: $('#ddlCategory').val(),
            txtQuantity: $('#txtQuantity').val(),
            UnitKey: $('#ddlUnit').val(),
            Total: $('#txtTotal').val(),
            Method: $('#txtMethod').val(),
            CompostingDates: $('#txtCompostingDates').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "Fertilizer_Buy") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            FertilizerKey: $('#ddlCategory').val(),
            SeedsKey: $('#ddlSeedsKey').val(),
            txtQuantity: $('#txtQuantity').val(),
            txtAddress: $('#txtAddress').val(),
            UnitKey: $('#ddlUnit').val(),
            Total: $('#txtTotal').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "Fertilizer_Use") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            FertilizerKey: $('#ddlCategory').val(),
            DDLEquipment: $('#DDLEquipment').val(),
            SeedsKey: $('#ddlSeedsKey').val(),
            txtQuantity: '0', //txtQuantity: $('#txtQuantity').val(),
            UnitKey: $('#ddlUnit').val(),
            txtFormulaUsed: $('#txtFormulaUsed').val(),
            txtHowtouse: $('#txtHowtouse').val(),
            txtParcel: $('#txtParcel').val(),
            txtQuarantinePeriod: '0',// txtQuarantinePeriod: $('#txtQuarantinePeriod').val(),
            txtArea: $('#txtArea').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "PesticideBuy") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            PesticideKey: $('#ddlCategory').val(),
            Quantity: $('#txtQuantity').val(),
            Price: $('#txtPrice').val(),
            CompanyKey: $('#ddlPesticideCompanie').val(),
            Address: $('#txtAddress').val(),
            SeedsKey: $('#ddlSeedsKey').val(),
            UnitKey: $('#ddlUnit').val(),
            Total: $('#txtTotal').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
        $("#divcalendar").prop("hidden", false);
    }
    if (NameBooks == "Pesticide_Use") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            FertilizerKey: $('#ddlCategory').val(),
            DDLEquipment: $('#DDLEquipment').val(),
            SeedsKey: $('#ddlSeedsKey').val(),
            txtQuantity: $('#txtQuantity').val(),
            UnitKey: $('#ddlUnit').val(),
            txtFormulaUsed: $('#txtFormulaUsed').val(),
            txtHowtouse: $('#txtHowtouse').val(),
            txtParcel: $('#txtParcel').val(),
            txtQuarantinePeriod: $('#txtQuarantinePeriod').val(),
            txtArea: $('#txtArea').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "LandUse") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            txtAction: $('#txtAction').val(),
            SeedsKey: $('#ddlCategory').val(),
            txtReason: $('#txtReason').val(),
            txtSolution: $('#txtSolution').val(),
            txtNote: $('#txtNote').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "CheckEquipment") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            txtAction: $('#txtAction').val(),
            SeedsKey: $('#ddlSeedsKey').val(),
            txtEquipmentKey: $('#ddlEquipment').val(),
            txtAction: $('#txtAction').val(),
            txtInfo: $('#txtInfo').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "HarvestedForSale") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            txtAction: $('#txtAction').val(),
            SeedsKey: $('#ddlSeedsKey').val(),
            txtCode: $('#txtCode').val(),
            txtQuantityHarvested: $('#txtQuantityHarvested').val(),
            txtQuantitySale: $('#txtQuantitySale').val(),
            txtWhereToBuy: $('#txtWhereToBuy').val(),
            txtUnitKey: $('#ddlUnit').val(),
            Total: $('#txtTotal').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "HandlingPackaging") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            txtType: $('#txtType').val(),
            txtPlace: $('#txtPlace').val(),
            txtTreatment: $('#txtTreatment').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "Inventory") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            Type: $('#ddlType').val(),
            FertilizersPesticides: $('#ddlFertilizersPesticides').val(),
            txtQuantity: $('#txtQuantity').val(),
            UnitKey: $('#ddlUnit').val(),
            txtExpireDate: $('#txtExpireDate').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    if (NameBooks == "CheckAssessment") {
        if (!SaveDatabook(JSON.stringify({ Key: KeyID,
            day: $('#txtDay').val(),
            SeedsKey: $('#ddlSeedsKey').val(),
            DescribesError: $('#txtDescribesError').val(),
            Method: $('#txtMethod').val(),
            Active: IsActive
        }))) { $('#confirmMessError').modal('show'); return false; }
        ShowMess('confirmMessSucc', 3000);
    }
    $('.selectpicker').selectpicker('refresh');
    $('.responsive-calendar').responsiveCalendar('edit', JSON.parse(getCalendar(JSON.stringify({ Year: y, Month: m }))));
    loaddata($('#txtDay').val());
    checkcount();
}
