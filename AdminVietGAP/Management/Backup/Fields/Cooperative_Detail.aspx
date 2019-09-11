<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cooperative_Detail.aspx.cs"
    Inherits="Management.Fields.Cooperative_Detail" %>

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
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseOnReload() {
            GetRadWindow().BrowserWindow.RefeshGvOnSave();
            GetRadWindow().close();
        }
        
    </script>
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
                <li id="Tab2">Cơ sở chứng nhận</li>
                <li id="Tab3">Chi tiết cơ sở</li>
                <li id="Tab4">Hình ảnh</li>
                <li id="Tab5">Bản đồ</li>
            </ul>
        </div>
        <div class="tabcontent" style="height:290px">
            <div id='Tab1_Content'>  
                <table border="0" cellpadding="2" cellspacing="0">
                <asp:Label ID='lbkey' runat='server' Visible='false' value ='0'></asp:Label>
                <tr>
                    <td class="editlab"><b>Mã hợp tác xã : </b></td>
                    <td><asp:Label ID='lbCooperative_ID' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtCooperative_ID" runat="server" class="editlabtxt"></asp:TextBox></td>
                    
                </tr>
                <tr>
                    <td class="editlab"><b>Tên hợp tác xã : </b></td>
                    <td><asp:Label ID='lbCooperative_Name' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtCooperative_Name" runat="server" class="editlabtxt"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Tỉnh : </b></td>
                    <td><asp:Label ID='lbProvincesCitiesID' runat='server'></asp:Label></td>
                    <td><asp:DropDownList ID="ddProvincesCitiesID" runat="server" class="editlabtxt"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Địa chỉ : </b></td>
                    <td><asp:Label ID='lbAddress' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtAddress" runat="server" class="editlabtxt"></asp:TextBox></td>                  
                </tr>
                <tr>
                    <td class="editlab"><b> Liên hệ(sđt) : </b></td>
                    <td><asp:Label ID='lbPhone' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtPhone" runat="server" class="editlabtxt"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Ghi chú : </b></td>
                    <td><asp:Label ID='lbDescription' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtDescription" runat="server" class="editlabtxt" Height="150px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
				</table> 
            </div>
            <div id='Tab2_Content'>
                <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="editlab"><b>Mã số VietGAP : </b></td>
                    <td><asp:Label ID='lbVietGAPCode' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtVietGAPCode" runat="server" class="editlabtxt"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Tổ chức chứng nhận : </b></td>
                    <td><asp:Label ID='lbCertifiedOrganization' runat='server'></asp:Label></td>
                    <td><asp:DropDownList ID="ddCertifiedOrganization" runat="server" class="editlabtxt"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="editlab"><b>Ngày cấp : </b></td>
                    <td><asp:Label ID='lbDateRange' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtDateRange" runat="server" Width="70px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Ngày hết hạn : </b></td>
                    <td><asp:Label ID='lbDateExpiration' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtDateExpiration" runat="server" Width="70px"></asp:TextBox></td>
                </tr>
                </table>
            </div>
            <div id='Tab3_Content'>
                <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="editlab"><b>Sản phẩm : </b></td>
                    <td><asp:Label ID='lbTreeType' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtTreeType" runat="server" class="editlabtxt" Height="100px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Tổng diện tích (ha) : </b></td>
                    <td><asp:Label ID='lbArea' runat='server' ></asp:Label></td>
                    <td><asp:TextBox ID="txtArea" runat="server" class="editlabtxt" onkeypress="return ValidateKeypress(/\d/,event);" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Sản lượng dự kiến : </b></td>
                    <td><asp:Label ID='lbQuantity' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtQuantity" runat="server" class="editlabtxt" onkeypress="return ValidateKeypress(/\d/,event);" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><b>Chủ cơ sở sản xuất : </b></td>
                    <td><asp:Label ID='lbOwner' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtOwner" runat="server" class="editlabtxt" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab" value ='0'><b>Số lượng xã viên : </b></td>
                    <td><asp:Label ID='lbMembers' runat='server'></asp:Label></td>
                    <td><asp:TextBox ID="txtMembers" runat="server" class="editlabtxt" onkeypress="return ValidateKeypress(/\d/,event);" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="editlab"><asp:Label ID='lbimage' runat='server'>Chọn ảnh đại diện : </asp:Label></td>
                    <td></td>
                    <td><asp:DropDownList ID="ddImage" runat="server" class="editlabtxt" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="editlab"><asp:Label ID='lbupimage' runat='server'>Upload ảnh : </asp:Label></td>
                    <td></td>
                    <td><asp:FileUpload ID="FileUploadControl" runat="server" class="editlabtxt" /></td>
                </tr>
                </table>
            </div>
            <div id='Tab4_Content'>
                <table border="0" cellpadding="2" cellspacing="0">
                    <tr>
                        <td class="editlab"><asp:Label ID='Label1' runat='server'>Ảnh 1 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload1" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image1" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton1" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton1_Click" Visible="False" /></td>
                                
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label2' runat='server'>Ảnh 2 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload2" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image2" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton2_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label3' runat='server'>Ảnh 3 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload3" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image3" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton3" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton3_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label4' runat='server'>Ảnh 4 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload4" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image4" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton4" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton4_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label5' runat='server'>Ảnh 5 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload5" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image5" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton5" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton5_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label6' runat='server'>Ảnh 6 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload6" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image6" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton6" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton6_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label7' runat='server'>Ảnh 7 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload7" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image7" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton7" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton7_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label8' runat='server'>Ảnh 8 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload8" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image8" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton8" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton8_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label9' runat='server'>Ảnh 9 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload9" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image9" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton9" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton9_Click" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td class="editlab"><asp:Label ID='Label10' runat='server'>Ảnh 10 : </asp:Label></td>
                        <td></td>
                        <td><asp:FileUpload ID="FileUpload10" runat="server" class="editlabtxt" Visible="False" /></td>
                        <td><asp:Image ID="Image10" runat="server" style ='max-height:25px; max-width:50px'/></td>
                        <td><asp:ImageButton ID="ImageButton10" runat="server" 
                                ImageUrl="~/Img/Icons/Deltetee.png" onclick="ImageButton10_Click" Visible="False" /></td>
                    </tr>

                </table>
            </div>
            <div id='Tab5_Content'>
                <div>
                    <b>
                        <asp:TextBox ID="txtlat" runat="server" Width="100%"></asp:TextBox>
                        <div id="Message">
                        </div>
                        <div id='map-canvas' style='height: 280px'>
                        </div>
                </div>
            </div>
    </div>
</body>
<footer>
<div style='text-align:center'>
<asp:ImageButton ID="cmdEdit" runat="server" onclick="cmdEdit_Click" ImageUrl="~/Img/bt-edit.png"></asp:ImageButton>
</b><asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" 
        onclick="cmdSave_Click"></asp:ImageButton>
</b><asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="~/Img/bt-del.png" onclick="cmdDelete_Click" OnClientClick = "Confirm()"></asp:ImageButton>
        </div>
</footer>
    <telerik:RadWindow ID="rrPesticide" runat="server" Skin="Vista" Behavior="Close" Left="0" Top="0"></telerik:RadWindow>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Bạn có chắc muốn xóa hợp tác xã này ?")) {
                confirm_value.value = "Có";
            } else {
                confirm_value.value = "Không";
            }
            document.forms[0].appendChild(confirm_value);
        }
        $(document).ready(function () {
            SetTNTabs();
            $("#tnTabs ul li").click(function () {
                var i;
                for (i = 1; i <= 5; i++) {
                    $("#Tab" + i + "_Content").hide();
                    $("#Tab" + i).removeClass("active");
                }
                $("#" + this.id + "_Content").show();
                $("#" + this.id).addClass("active");
            });

        });
        function SetTNTabs() {   
            for (i = 1; i <= 5; i++) {
                $("#Tab" + i + "_Content").hide();
                $("#Tab" + i).removeClass("active");
            }
            $("#Tab5_Content").show();
            $("#Tab5").addClass("active");
        }
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
        $(function () {
            $("#txtDateRange").datepicker();
            $("#txtDateExpiration").datepicker();
        });
    </script>
    <asp:Literal ID="ltmap" runat="server"></asp:Literal>
    <%--<script type='text/javascript'>
        function initialize() {
            
            var myLatlng = new google.maps.LatLng(10.605501814265633, 106.52668833732605);
            var mapOptions = {
                zoom: 13,
                mapTypeId: google.maps.MapTypeId.SATELLITE,
                center: myLatlng
            }
            var icon = {
                url: '/Img/Tree/Vegetables.png',
                scaledSize: new google.maps.Size(40, 40)
            };
            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                icon: icon,
                draggable: true,
                title: 'Thanh long'
            });
            
            google.maps.event.addListener(map, 'click', function (e) {
                marker.setMap(null);
                marker = new google.maps.Marker({
                    position: e.latLng,
                    map: map,
                    icon: icon,
                    draggable: true,
                    title: 'Thanh long'
                });
                document.getElementById('txtlat').value = e.latLng.lat().toString() + ', ' + e.latLng.lng().toString();
            });
//            google.maps.event.addDomListener(marker, 'mouseup', function (e) {
//                document.getElementById("txtlat").value = e.latLng.lat().toString() + ', ' + e.latLng.lng().toString();
//            });
            
        }
        google.maps.event.addDomListener(window, 'load', initialize);
                        </script>--%>
    </form>
</body>
</html>