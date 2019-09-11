<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cooperative_Edit.aspx.cs"
    Inherits="Management.Fields.Cooperative_Edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/Main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Fields.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp"></script>
    <script src="../JS/jquery-ui.js" type="text/javascript"></script>
    <link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //------------OpenTab Start------------
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        //------------OpenTab END------------

        //------------CloseTab Start-----------
        function CloseOnReload() {
            GetRadWindow().BrowserWindow.RefeshGvOnSave();
            GetRadWindow().close();
        }
        //------------CloseTab END-----------

        //------------TabContent Start------------
        $(document).ready(function () {
            SetTNTabs();
            $("#tnTabs ul li").click(function () {
                var i;
                for (i = 1; i <= 4; i++) {
                    $("#Tab" + i + "_Content").hide();
                    $("#Tab" + i).removeClass("active");
                }
                $("#" + this.id + "_Content").show();
                $("#" + this.id).addClass("active");
            });

        });
        function SetTNTabs() {
            for (i = 1; i <= 4; i++) {
                $("#Tab" + i + "_Content").hide();
                $("#Tab" + i).removeClass("active");
            }
            $("#Tab4_Content").show();
            $("#Tab4").addClass("active");
        }
        //------------TabContent END--------------
        //------------Chỉ nhập số Start------------
        function ValidateKeypress(numcheck, e) {
            var keynum, keychar, numcheck;
            if (window.event) {//IE
                keynum = e.keyCode;
            }
            else if (e.which) {// Netscape/Firefox/Opera
                keynum = e.which;
            }
            if (keynum == 8 || keynum == 127 || keynum == null || keynum == 9 || keynum == 0 || keynum == 13) return true;
            keychar = String.fromCharCode(keynum);
            var result = numcheck.test(keychar);
            return result;
        }
        //------------Chỉ nhập số END--------------

        //------------Check null Start--------------
        function validateForm() {
            var CoopID = document.getElementById('txtCooperative_ID').value;
            var CoopName = document.getElementById('txtCooperative_Name').value;
            var ProvincesCitiesID = document.getElementById('ddProvincesCitiesID').value;
            var Cooperative_Name = document.getElementById('txtCooperative_Name').value;
            var ddImage = document.getElementById('ddImage').value;
            var image = document.getElementById('FileUploadControl').value;
            
            var lat = document.getElementById('txtlat').value;
            if (CoopID == null || CoopID == "") {
                alert("Vui lòng nhập Mã hợp tác xã !");
                return false;
            }
            if (CoopName == null || CoopName == "") {
                alert("Vui lòng nhập Tên hợp tác xã !");
                return false;
            }
            if (ProvincesCitiesID == null || ProvincesCitiesID == "" || ProvincesCitiesID == "0") {
                alert("Vui lòng chọn tỉnh !");
                return false;
            }
            if (Cooperative_Name == null || Cooperative_Name == "") {
                alert("Vui lòng nhập tên chủ cơ sở !");
                return false;
            }
            if (image == "" && ddImage == "0") {
                alert("Vui lòng chọn ảnh đại diện hoặc up ảnh mới !");
                return false;
            }
            if (lat == null || lat == "") {
                alert("Vui lòng chọn vị trí của hợp tác xã trên bản đồ !");
                return false;
            }
        }
        //------------Check null  END--------------
        //------------Nhập ngày tháng Start------------
        $(function () {
            $("#txtDateRange").datepicker();
            $("#txtDateExpiration").datepicker();
        });
        //------------Nhập ngày tháng END--------------

        //------------Maps Start------------
        function initialize() {
            var marker = '';
            var myLatlng = new google.maps.LatLng(10.605501814265633, 106.52668833732605);
            var mapOptions = {
                zoom: 13,
                mapTypeId: google.maps.MapTypeId.SATELLITE,
                center: myLatlng
            }
            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            google.maps.event.addListener(map, 'click', function (e) {
                if (marker != '') {
                    marker.setMap(null);
                }
                marker = new google.maps.Marker({
                    position: e.latLng,
                    map: map,
                    draggable: true,
                    title: 'Thanh long'
                });
                document.getElementById('txtlat').value = e.latLng.lat().toString() + ', ' + e.latLng.lng().toString();
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);
        //------------Maps END--------------
    </script>
    <%--------------ShowMessage Start--------------%>
    <asp:Literal ID="LTShowMessage" runat="server"></asp:Literal>
    <%--<script type="text/javascript">
        alert('Thông báo');
        document.forms[0].item('txtCooperative_ID').focus();
    </script>--%>
    
    <%----------------ShowMessage END------------%>
    <style>
      html, body, #map-canvas {
        height: 280px;
        margin: 0px;
        padding: 0px
      }
      .editlab
      {
          width: 150px;
          vertical-align:top;
          float:left;
      }
      .editlabtxt
      {
          Width: 350px;
      }
    </style>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<body>
    <div style="padding: 10px">
        <div class="TitleForm">
            CHI TIẾT THÔNG TIN HỢP TÁC XÃ
        </div>
        <div id="tnTabs">
            <div style="padding: 10px 0 33px 0">
                <ul class="tabs">
                    <li id="Tab1">Thông tin chung</li>
                    <li id="Tab2">Giấy chứng nhận</li>
                    <li id="Tab3">Chi tiết cơ sở</li>
                    <li id="Tab4">Bản đồ</li>
                </ul>
            </div>
            <div class="tabcontent" style="height: 290px">
                <div id='Tab1_Content'>
                    <table border="0" cellpadding="2" cellspacing="0">
                        <asp:Label ID='lbkey' runat='server' Visible='false' value='0'></asp:Label>
                        <tr>
                            <td class="editlab">
                                <b>Mã hợp tác xã : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCooperative_ID" runat="server" class="editlabtxt" placeholder='Nhập vào mã cửa cơ sỡ, hợp tác xã'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Tên hợp tác xã : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCooperative_Name" runat="server" class="editlabtxt" placeholder='Nhập vào tên cửa cơ sỡ, hợp tác xã'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Tỉnh : </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddProvincesCitiesID" runat="server" class="editlabtxt">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Địa chỉ : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" class="editlabtxt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Liên hệ(sđt) : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhone" runat="server" class="editlabtxt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Ghi chú : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" class="editlabtxt" Height="150px"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id='Tab2_Content'>
                    <table border="0" cellpadding="2" cellspacing="0">
                        <tr>
                            <td class="editlab">
                                <b>Mã số : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVietGAPCode" runat="server" class="editlabtxt" placeholder='Mã số do tổ chức chứng nhận cấp'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Tổ chức chứng nhận : </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddCertifiedOrganization" runat="server" class="editlabtxt">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Ngày cấp : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateRange" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Ngày hết hạn : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateExpiration" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id='Tab3_Content'>
                    <table border="0" cellpadding="2" cellspacing="0">
                        <tr>
                            <td class="editlab">
                                <b>Sản phẩm : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTreeType" runat="server" class="editlabtxt" Height="100px" TextMode="MultiLine"
                                    placeholder='Giới thiệu về sản phẩm của cơ sở'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Tổng diện tích (ha) : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtArea" runat="server" class="editlabtxt" value='0' onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Sản lượng dự kiến : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuantity" runat="server" class="editlabtxt" value='0' onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <b>Chủ cơ sở sản xuất : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOwner" runat="server" class="editlabtxt" placeholder='Tên chủ cơ sở'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab" value='0'>
                                <b>Số lượng xã viên : </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMembers" runat="server" class="editlabtxt" value='0' onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <asp:Label ID='lbimage' runat='server'>Chọn ảnh đại diện : </asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddImage" runat="server" class="editlabtxt">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="editlab">
                                <asp:Label ID='lbupimage' runat='server'>Upload ảnh : </asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUploadControl" runat="server" class="editlabtxt" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id='Tab4_Content'>
                    <div>
                        <b>
                            <asp:TextBox ID="txtlat" runat="server" Style='display:none;visibility:hidden;' Width="100%" onkeypress="return ValidateKeypress(/\d/,event);"></asp:TextBox>
                            <div id="Message">
                            </div>
                            <div id='map-canvas' style='height: 280px'>
                            </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
<footer>
<div style='text-align:center'>
<asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" 
      OnClientClick="return validateForm()"  onclick="cmdSave_Click"></asp:ImageButton>
        </div>
</footer>
    <telerik:RadWindow ID="rrPesticide" runat="server" Skin="Vista" Behavior="Close" Left="0" Top="0"></telerik:RadWindow>
    <script type="text/javascript">
        
    </script>
    </form>
</body>
</html>