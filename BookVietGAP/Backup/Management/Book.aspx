<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBook.Master" AutoEventWireup="true"
    CodeBehind="Book.aspx.cs" Inherits="BookVietGAP.Book" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Lib/bootstrap/css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <script src="/Lib/bootstrap/js/bootstrap-select.min.js" type="text/javascript"></script>
    <link href="/CSS/bt.css" rel="stylesheet" type="text/css" />
    <script src="/JS/data.js" type="text/javascript"></script>
    <script src="JS/info.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Keyid = "0";
        
        var Bookid = "";
        var Bookdata = "";
        var NameBooks = "";
        var IsActive = false;
        var DayClass;
        var DatetimeNow;
        var dayoflist;
        var y, m;
        $(document).ready(function () {
            OnLoadAll();
            //Check Null page
            $("#Savebtn").click(function () {
                BookcheckNull();
            });
            //Save page
            $("#Sconfirm").click(function () {
                BookSaveSE("0");
            });
            //Edit page
            $("#Econfirm").click(function () {
                BookSaveSE(Keyid);
            });
            //Delete page
            $("#delconfirm").click(function () {
                BookDelete();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Carousel -->
    <div id="bookshelf">
    <img src="/Img/icon/congvieccanlam.png" class="imgcardBook" />
        <table style="width: 100%;">
            <tr>
                <td class="tdleft">
                </td>
                <td>
                    <div class="row">
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadCheckList("checkdaynow")' data-toggle='modal' class="thumbnail home-thumb">
                                <img  src="img/Book/congviechientai.png" style="width: 150px;height: 150px;box-shadow: 0px 0px 0px 0px #330F04;  -moz-box-shadow: 0px 0px 0px 0px #330F04;-webkit-box-shadow: 0px 0px 0px 0px #330F04;" alt="Công viêc hiện tại">
                            </a>
                            <div id='divcountnow' class='countsday'></div>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadCheckList("checklastweek")' data-toggle="modal" class="thumbnail home-thumb">
                                <img src="img/Book/Congviectuannay.png" style="width: 150px;height: 150px;box-shadow: 0px 0px 0px 0px #330F04;  -moz-box-shadow: 0px 0px 0px 0px #330F04;-webkit-box-shadow: 0px 0px 0px 0px #330F04;" alt="Công việc chưa làm">
                            </a>
                            <div id='divcountday' class='countsday'></div>
                        </div>
                         <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadCheckList("checkAllday")' data-toggle="modal" class="thumbnail home-thumb">
                                <img src="img/Book/Congviecchuathuchien.png" style="width: 150px;height: 150px;box-shadow: 0px 0px 0px 0px #330F04;  -moz-box-shadow: 0px 0px 0px 0px #330F04;-webkit-box-shadow: 0px 0px 0px 0px #330F04;" alt="Công việc chưa làm">
                            </a>
                            <div id='divcountAllday' class='countsday'></div>
                        </div>
                    </div>
                    
                </td>
                <td class="tdright">
                </td>
            </tr>
        </table>
    <img src="/Img/icon/QUAN-LY-DAT-GIONG.png" class="imgcardBook" />
        <table id="tb" style="width: 100%;">
            <tr>
                <td class="tdleft">
                </td>
                <td>
                    <div class="row">
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("LandUse")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/quan-ly-dat.png" alt="Sổ tay sử dụng đất">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("SeedsProcess")' class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/xu-ly-giong.png" alt="Sổ tay xử lý giống">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2" id="ListSeedsClick">
                            <a href="#ListSeeds" data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/danh-sach-cay-trong.png" alt="Sổ tay danh sách giống cây trồng">
                            </a>
                        </div>
                    </div>
                </td>
                <td class="tdright">
                </td>
            </tr>
        </table>
        <img src="/Img/icon/QUAN-LY-SAN-XUAT.png" class="imgcardBook" />
        <table style="width: 100%;">
            <tr>
                <td class="tdleft">
                </td>
                <td>
                    <div class="row">
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("CompostingOrganic")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/quanlyuphan.png" alt="Sổ tay mua phân bón">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("Fertilizer_Buy")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/mua-phan-bon.png" alt="Sổ tay mua phân bón">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("Fertilizer_Use")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/su-dung-phan-bon.png" alt="Sổ tay sử dụng phân bón">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("PesticideBuy")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/mua-thuoc-bvtv.png" alt="Sổ tay mua thuốc bvtv">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("Pesticide_Use")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/su-dung-thuoc-bvtv.png" alt="Sổ tay sử dụng thuốc BVTV">
                            </a>
                        </div>
                    </div>
                </td>
                <td class="tdright">
                </td>
            </tr>
        </table>
        <img src="/Img/icon/THU-HOACH-XUAT-BAN.png" class="imgcardBook" />
        <table style="width: 100%;">
            <tr>
                <td class="tdleft">
                </td>
                <td>
                    <div class="row">
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("CheckEquipment")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/kiemtra-thietbi_icon.png" alt="Sổ tay quản lý dụng cụ">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("HarvestedForSale")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/thu-hoach-xuat-ban.png" alt="Sổ tay quản lý thu hoạch">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("HandlingPackaging")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/xu-ly-chat-thai.png" alt="Sổ tay sử lý chất thải">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("Inventory")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/kiem-ke-ton-kho.png" alt="Kiểm kê tồn kho">
                            </a>
                        </div>
                        <div class="col-xs-4 col-sm-3 col-md-2 col-lg-2">
                            <a href="#" onclick='LoadBook("CheckAssessment")' data-toggle="modal" class="thumbnail home-thumb">
                                <img style='height: 150px;' src="img/Book/kiem-tra-danh-gia.png" alt="kiem tra danh gia">
                            </a>
                        </div>
                    </div>
                </td>
                <td class="tdright">
                </td>
            </tr>
        </table>
    </div>
    <!-- Placed at the end of the document so the pages load faster -->
    <div class="modal" id="DateNote" style='overflow-x: hidden;overflow-y: inherit;' role='dialog' aria-labelledby='' aria-hidden='true'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="load">
                <form class="form-horizontal" role="form">
                <div class="modal-header modal-header-default" style="height: 45px;">
                    <button id="CloseX" type="button" class="close" onclick="$('#checkdaynow').modal('show');" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4>
                        <div id="nheader" style="margin-top: -10px;text-align: center;">
                        </div>
                        <h4>
                </div>
                <div class="modal-body" style="padding: 0px; background-color: rgb(237, 237, 237);">
                    <div class="form-group has-feedback" style="  margin:0px;  margin-right: 15px;">
                        <div class="col-sm-12 col-md-12 col-lg-5" style="background-color: rgb(237, 237, 237);padding-left: 0px;">
                            <!-- Responsive calendar - START -->
                            <table style="width: 100%;">
                                <tr>
                                    <td class="" style="width: 66px; background-attachment: inherit; min-height: 198px;
                                        vertical-align: top;">
                                        <ul class="nav nav-pills nav-stacked" style="margin-top: -1px;  padding-top: 2px;">
                                            <li id="atli">
                                                <a href="#" onclick="SubMenuClick('lich')" data-toggle="modal" style="height: 74px; padding: 0px; margin: 0px; border-radius: 0px;text-align: center;  padding-top: 5px;margin-top: -2px;">
                                                    <img style="height: 30px; width:30px;  margin-top: -11px;margin-right: 4px;" src="img/Main_Sub_lich.png"><i class="fa fa-list fa-2x"></i>Lịch
                                                </a>
                                             </li>
                                            <li><a href="#" onclick="SubMenuClick('selam')" data-toggle="modal" style="height: 74px; padding: 0px; margin: 0px; border-radius: 0px;text-align: center;  padding-top: 5px;margin-top: -2px;">
                                                    <img style="height: 30px; width:30px;  margin-top: -11px;margin-right: 4px;" src="img/Main_Sub_Selam.png"><i class="fa fa-list fa-2x"></i>CV Sẽ   làm
                                                </a>
                                            </li>
                                            <li><a href="#" onclick="SubMenuClick('chualam')" data-toggle="modal" style="height: 74px; padding: 0px; margin: 0px; border-radius: 0px;text-align: center;  padding-top: 5px;margin-top: -2px;">
                                                    <img style="height: 30px; width:30px;  margin-top: -11px;margin-right: 4px;" src="img/Main_Sub_Chualam.png"><i class="fa fa-list fa-2x"></i>CV Chưa làm
                                                </a>
                                            </li>
                                            <li><a href="#" onclick="SubMenuClick('moi')" data-toggle="modal" style="height: 74px; padding: 0px; margin: 0px; border-radius: 0px;text-align: center;  padding-top: 5px;margin-top: -2px;">
                                                    <img style="height: 30px; width:30px;  margin-top: -11px;margin-right: 4px;" src="img/Main_Sub_moi.png"><i class="fa fa-list fa-2x"></i>CV Mới
                                                </a>
                                            </li>
                                        </ul>
                                    </td>
                                    <td style="padding-top: 15px; padding-left: 15px;vertical-align: top; ">
                                        <div id="divcalendar" class="responsive-calendar">
                                            <div class="controls">
                                                <a class="pull-left" data-go="prev">
                                                    <div class="btn btn-primary" style="font-size: 12px;">
                                                        <</div>
                                                </a>
                                                <h4 style="font-size: 15px; font-weight: bold;">
                                                    <span data-head-month></span>&#160<span data-head-year></span>
                                                </h4>
                                                <a class="pull-right" data-go="next">
                                                    <div class="btn btn-primary" style="font-size: 12px;">
                                                        ></div>
                                                </a>
                                            </div>
                                            <hr />
                                            <div class="day-headers">
                                                <div class="day header">
                                                    T2</div>
                                                <div class="day header">
                                                    T3</div>
                                                <div class="day header">
                                                    T4</div>
                                                <div class="day header">
                                                    T5</div>
                                                <div class="day header">
                                                    T6</div>
                                                <div class="day header">
                                                    T7</div>
                                                <div class="day header">
                                                    CN</div>
                                            </div>
                                            <div class="days" data-group="days">
                                            </div>
                                        </div>
                                        <strong>Danh sách công việc</strong>
                                        <div id="ListCV">
                                        </div>
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                            <!-- Responsive calendar - END -->
                        </div>
                        <div id="tabright" class="col-sm-12 col-md-12 col-lg-7" style="background-color: white; padding-top: 15px;margin-top: 15px;
                            margin-bottom: 15px; padding-bottom: 7px; border-radius: 7px;">
                            <fieldset id="fieldset">
                                <div id="Divcontact">
                                </div>
                                <hr />
                                <button type="button" class="btn btn-success pull-right" id="Savebtn">Lưu</button>
                                <div id='divdel'></div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>
    </div>

       <div class="modal fade" id="ListSeeds"  tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header modal-header-default">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Danh sách giống cây trồng</h4>
                </div>
                <div class="modal-body" id="SeedsResult">

                </div>
            </div>
        </div>
    </div>
    <div class='modal fade' id='ListsCV' role='dialog' aria-labelledby='' aria-hidden='true' style='z-index: 1000;'>
        <div class='modal-dialog'>
            <div class='modal-content'>
                <form class='form-horizontal' role='form'>
                <div class='modal-header modal-header-default'>
                    <button id="CloseXX" type='button' class='close' data-dismiss='modal' aria-hidden='true'>
                        &times;</button>
                    <h2 class='modal-title' id= 'headerCheckListsCV'>
                        </h2>
                </div>
                <div id='CheckListsCV' class='modal-body'> 
                </div>
                <%--<div class='modal-footer'>
                <button type='button' class='btn btn-success' data-dismiss='modal'>Lưu</button>
                        <button type='button' class='btn btn-default' data-dismiss='modal'>Đóng</button>
                    </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
