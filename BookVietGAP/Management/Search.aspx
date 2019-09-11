<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBook.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="BookVietGAP.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="JS/info.js" type="text/javascript"></script>
    <link href="/Lib/bootstrap/css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <script src="/Lib/bootstrap/js/bootstrap-select.min.js" type="text/javascript"></script>
<script type='text/javascript'>
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
        $("#btn_search").click(function () {
            loadSearch();
        });
        $('#txt_search').bind("keyup keypress", function (e) {
        var code = e.keyCode || e.which;
        if (code == 13) {
            loadSearch();
            return false;
        }});
    });
function loadSearch() {
    if (!CheckText("txt_search", 1)) { ShowMess('confirmMessNull', 4000); return false; }
    var nbody = "";
    var Text = SaveAnything("Search", "LoadSearchCode", JSON.stringify({ Code: $("#txt_search").val() }))
    if (Text != '') {
        var ddl = JSON.parse(Text);
        for (var i = 0; i < ddl.length; i++) {
            var DS = ddl[i];
            nbody += "<div>" +
                        "<label for='contact-name' class='col-sm-12 control-label' style='margin-top: 15px;'><strong> Tên Hợp Tác Xã : " + DS.Cooperative_Name + "</strong></label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Ngày cấp chứng nhận : " + DS.DateRange + "</label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Sản phẩm : " + DS.TreeType + "</label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Số điện thoại : " + DS.Phone + "</label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Địa chỉ : " + DS.Address + "</label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Mã VietGAP : " + DS.VietGAPCode + "</label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'><strong> Sản phẩm : " + DS.SeedsName + "</strong></label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Ngày trồng : " + DS.DateOfManufacture + "</label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Ngày thu hoạch : " + DS.DatetimeSale + "</label>" +
                                "<label for='contact-name' class='col-sm-12 control-label'> Nơi thu mua : " + DS.WhereToBuy + "</label>" +
                     "</div><hr style='width: 100%; color: black; height: 1px; background-color:#eee;  margin: 0;'>";
        }
    }
    else {
        nbody = "<div class='form-group has-feedback'>" +
                        "<label for='contact-name' class='col-sm-12 control-label' style='margin-top: 15px;'><strong> Không tìm thấy thông tin về " + $("#txt_search").val() + "</strong></label>" +
                     "</div>";
    }
    $("#Divcount").html(nbody);
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
    <img src="/Img/icon/danh-muc-tra-cuu.png" class="imgcardBook" />
    <table id="tb" style="width: 100%;">
        <tr>
            <td class="tdleft">
            </td>
            <td>
                <div class="row">
                    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                        <a href="/SubSearch/seed.aspx?id=giong" target="_blank" data-toggle="modal" class="thumbnail home-thumb">
                            <img style='height: 150px;' src="img/Book/tra-cuu-giong.png" alt="Tra cứu giống">
                        </a>
                    </div>
                    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                        <a href="/SubSearch/seed.aspx?id=phanbon" target="_blank" data-toggle="modal" class="thumbnail home-thumb">
                            <img style='height: 150px;' src="img/Book/tra-cuu-phan-thuoc.png" alt="Tra cứu phân thuốc">
                        </a>
                    </div>
                    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                        <a href="/SubSearch/seed.aspx?id=saubenh" target="_blank" data-toggle="modal" class="thumbnail home-thumb">
                            <img style='height: 150px;' src="img/Book/tra-cuu-sau-benh.png" alt="Tra cứu sâu bệnh">
                        </a>
                    </div>
                    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                        <a href="#Code" target="_blank" data-toggle="modal" class="thumbnail home-thumb">
                            <img style='height: 150px;' src="img/Book/tra-cuu-ma-truy-vet.png" alt="Tra cứu mã truy vết">
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
    <div id="Code" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-default">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h4 class="modal-title" style="text-align: center;"><div>TRA CỨU MÃ TRUY VẾT</div><a class="anchorjs-link" href="#pro16"><span class="anchorjs-icon"></span></a></h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="col-md-12" style="margin-bottom: 5px; padding: 10px;">
                            <div class="col-lg-12">
                                <div id="custom-search-input">
                                    <div class="input-group col-md-12">
                                        <input id="txt_search" type="text" class="form-control input-lg" placeholder="Tìm kiếm" />
                                        <span class="input-group-btn">
                                            <button id="btn_search" class="btn btn-info btn-lg" type="button">
                                                <i class="glyphicon glyphicon-search"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div id="Divcount"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
