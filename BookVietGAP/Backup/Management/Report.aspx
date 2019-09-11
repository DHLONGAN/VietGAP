<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBook.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="BookVietGAP.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="JS/info.js" type="text/javascript"></script>
    <link href="/Lib/bootstrap/css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <script src="/Lib/bootstrap/js/bootstrap-select.min.js" type="text/javascript"></script>
    <script type='text/javascript'>
        var d = DateNow;
        var day = d.getDate();
        var mon = (d.getMonth() + 1);
        var year = d.getFullYear();
        var daynow = day + "/" + mon + '/' + year;
        $(document).ready(function () {
            var A = $(document).height() - $("#navmenu").height();
            var B = A / 198;
            var Content = "";
            for (var i = 1; i < B; i++) {
                Content += '<table style="width: 100%;">' +
                            '<tr>' +
                                '<td class="tdleft"></td>' +
                                '<td>' +
                                    '<div class="row">' +
                                    '</div>' +
                                '</td>' +
                                '<td class="tdright"></td>' +
                            '</tr>' +
                        '</table>';
            }
            $("#RowContent").html(Content);
        });

        function LoadDetailReport(name) {
            var nheader = "";
            var contactReport = "";
            ReportName = name;
            if (name == "Seed") {
                nheader = "BÁO CÁO SỔ NHẬT KÝ SẢN XUẤT";
                contactReport = "<div class='col-xs-12' style='margin: 0px;padding: 0px;'>" +
                             DDListStyle("Cây trồng", "ddlCategory", "LoadSeeds", 5, 7,"Tất cả") +
                             DateTimeFormTo("Từ", "Datetime1", "Đến", "Datetime2") +
                        "</div>";

            }
            if (name == "Cost") {
                nheader = "BÁO CÁO CHI PHÍ";
                contactReport = "<div class='col-xs-12' style='margin: 0px;padding: 0px;'>" +
                DDListStyle("Cây trồng", "ddlCategory", "LoadSeeds", 5, 7, "Tất cả") +
                             DateTimeFormTo("Từ", "Datetime1", "Đến", "Datetime2") +
                        "</div>";

            }
            $('#nheader').html(nheader);
            $('#nheader1').html(nheader);
            $('#DivcontactDetai').html(contactReport);
            $('#Datetime1').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            });
            $('#Datetime2').datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            });
            $('.selectpicker').selectpicker({ style: 'btn-default' });
            $('.selectpicker').selectpicker('refresh');
            $('#pro').modal('show');

            //$('#ddlCategory').val(985);

            $('.selectpicker').selectpicker('refresh');
            $('#Datetime1').val('01/01/2014');
            $('#Datetime2').val(daynow);
            $(document).ready(function () {
                $('#btnReport').on('click', function (event) {
                    var name;
                    if (ReportName == "Seed") {
                        //if (!Checkddl("ddlCategory", 1)) { ShowMess('confirmddNull', 4000); return false; }
                        if (!CheckText("Datetime1", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        if (!CheckText("Datetime2", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        name = SaveAnything("Report", "LoadReport" + ReportName, JSON.stringify({ seedkey: $('#ddlCategory').val(), fromdate: $('#Datetime1').val(), todate: $('#Datetime2').val() }));
                    }
                    if (ReportName == "Cost") {
                        if (!CheckText("Datetime1", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        if (!CheckText("Datetime2", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        name = SaveAnything("Report", "LoadReport" + ReportName, JSON.stringify({ seedkey: $('#ddlCategory').val(), fromdate: $('#Datetime1').val(), todate: $('#Datetime2').val() }));
                    }
                    
                    $('#ReportDetai').html(name);
                    $("#ReportDetai").css("overflow", "auto");
                    $("#ReportDetai").css("max-height", ($(document).height() - 150) + "px");
                    $('#modalReport').modal('show');
                });
                $('#btnReportPrint').on('click', function (event) {
                    if (ReportName == "Seed") {
                        //if (!Checkddl("ddlCategory", 1)) { ShowMess('confirmddNull', 4000); return false; }
                        if (!CheckText("Datetime1", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        if (!CheckText("Datetime2", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        self.location = "/ReportPDF/RpProductionDaybook.aspx?seedkey=" + $('#ddlCategory').val() + "&from=" + $('#Datetime1').val() + "&to=" + $('#Datetime2').val();
                    }
                    if (ReportName == "Cost") {
                        if (!CheckText("Datetime1", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        if (!CheckText("Datetime2", 5)) { ShowMess('confirmMessNull', 4000); return false; }
                        self.location = "/ReportPDF/RpCost.aspx?seedkey=" + $('#ddlCategory').val() + "&from=" + $('#Datetime1').val() + "&to=" + $('#Datetime2').val();
                    }
                });
                
                $('#Datetime1').change(function () {
                    if (parseDate(daynow, 0).getTime() < parseDate($('#Datetime1').val(), 0).getTime()) {
                        ShowPopover($('#Datetime1'), '', 'Không được lớn hơn ngày hiện tại', 'top');
                        $('#Datetime1').val(daynow);
                        return false;
                    }
                    if (parseDate($('#Datetime1').val(), 0).getTime() > parseDate($('#Datetime2').val(), 0).getTime()) {
                        ShowPopover($('#Datetime1'), '', 'Thời gian không được lớn hơn ngày Đến', 'top');
                        $('#Datetime1').val($('#Datetime2').val());
                        return false;
                    }
                });
                $('#Datetime2').change(function () {
                    if (parseDate(daynow, 0).getTime() < parseDate($('#Datetime2').val(), 0).getTime()) {
                        ShowPopover($('#Datetime2'), '', 'Không được lớn hơn ngày hiện tại', 'top');
                        $('#Datetime2').val(daynow);
                        return false;
                    }
                    if (parseDate($('#Datetime1').val(), 0).getTime() > parseDate($('#Datetime2').val(), 0).getTime()) {
                        ShowPopover($('#Datetime2'), '', 'Thời gian không được nhỏ hơn Từ ngày', 'top');
                        $('#Datetime2').val($('#Datetime1').val());
                        return false;
                    }
                });

            });
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="bookshelf">
    </div>
    <%--<img src="/Img/icon/cay-trong-cua-ban.png" class="imgcardBook" />
    <table id="ListSeed" style="width: 100%;">
            <tr>
                <td class="tdleft"></td>
                <td>
                    <div class="row">
                    <div id="ListSp">
                    </div>
                </td>
                <td class="tdright"></td>
            </tr>
        </table>--%>
    <img src="/Img/icon/danh-muc-bao-cao.png" class="imgcardBook" />
    <table id="Table1" style="width: 100%;">
        <tr>
            <td class="tdleft">
            </td>
            <td>
                <div class="row">
                    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                        <a href="#"  onclick='LoadDetailReport("Seed")' data-toggle="modal" class="thumbnail home-thumb">
                            <img style='height: 150px;' src="img/Book/bao-cao-san-xuat.png" alt="Báo cáo nhật ký sản xuất">
                        </a>
                    </div>
                    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                        <a href="#"  onclick='LoadDetailReport("Cost")' data-toggle="modal" class="thumbnail home-thumb">
                            <img style='height: 150px;' src="img/Book/bao-cao-chi-phi.png" alt="Báo cáo chi phi">
                        </a>
                    </div>
                </div>
                
            </td>
            <td class="tdright">
            </td>
        </tr>
    </table>
    <div id="RowContent">
    </div>
    <div id="pro" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header modal-header-info">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" style="text-align: center;"><div id="nheader"></div><a class="anchorjs-link" href="#pro16"><span class="anchorjs-icon"></span></a></h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div id="DivcontactDetai">
                            
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button id='btnReport' type="button" class="btn btn-success"> Xem báo cáo</button>
                    <button id='btnReportPrint' type="button" class="btn btn-danger"> In báo cáo</button>
                </div>
            </div>
        </div>
    </div>
    <div id="modalReport" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-default">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" style="text-align: center;"><div id="nheader1"></div><a class="anchorjs-link" href="#pro16"><span class="anchorjs-icon"></span></a></h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div id="ReportDetai">
                            
                        </div>
                    </div>
                </div>
<%--                <div class="modal-footer">
                    <button type="button" class="btnn btn-info center-block" data-dismiss="modal"> Đóng</button>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
