<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Member_Detail.aspx.cs"
    Inherits="Management.Fields.Member_Detail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/Main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Fields.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=drawing"></script>
    <script src="../JS/jquery-ui.js" type="text/javascript"></script>
    <script src="../JS/shapesSave.js" type="text/javascript"></script>
    <%--<script src="../JS/Test.js" type="text/javascript"></script>--%>
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
        html, body, #map-canvas
        {
            height: 280px;
            margin: 0px;
            padding: 0px;
        }
        .editlab
        {
            width: 150px;
            vertical-align: top;
            float: left;
        }
        .editlabtxt
        {
            width: 350px;
        }
        #buttons
        {
            position: fixed;
            top: 60px;
            right: 35px;
        }
        #color-palette
        {
            clear: both;
        }
        .bthiden
        {
            display:none;
            visibility:hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <body>
        <div style="padding: 10px">
            <div class="TitleForm">
                CHI TIẾT THÔNG TIN XÃ VIÊN
            </div>
            <div id="tnTabs">
                <div style="padding: 10px 0 33px 0">
                    <ul class="tabs">
                        <li id="Tab1">Thông tin chung</li>
                        <li id="Tab2">Bản đồ</li>
                    </ul>
                </div>
                <div class="tabcontent" style="height: 290px">
                    <div id='Tab1_Content'>
                        <table border="0" cellpadding="2" cellspacing="0">
                            <asp:Label ID='lbkey' runat='server' Visible='false' value='0'></asp:Label>
                            <tr>
                                <td class="editlab">
                                    <b>Mã thành viên : </b>
                                </td>
                                <td>
                                    <asp:Label ID='lbMemID' runat='server'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMemID" runat="server" class="editlabtxt" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="editlab">
                                    <b>Họ tên : </b>
                                </td>
                                <td>
                                    <asp:Label ID='lbName' runat='server'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" class="editlabtxt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="editlab">
                                    <b>Hợp tác xã : </b>
                                </td>
                                <td>
                                    <asp:Label ID='lbCooperative_Key' runat='server'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ddCooperative_Key" runat="server" class="editlabtxt">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="editlab">
                                    <b>Địa chỉ : </b>
                                </td>
                                <td>
                                    <asp:Label ID='lbAddress' runat='server'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress" runat="server" class="editlabtxt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="editlab">
                                    <b>Email : </b>
                                </td>
                                <td>
                                    <asp:Label ID='lbEmail' runat='server'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" class="editlabtxt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="editlab">
                                    <b>Liên hệ(sđt) : </b>
                                </td>
                                <td>
                                    <asp:Label ID='lbPhone' runat='server'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhone" runat="server" class="editlabtxt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="editlab"><b>Tổng diện tích (m2) : </b></td>
                                <td><asp:Label ID='lbArea' runat='server' ></asp:Label></td>
                                <td><asp:TextBox ID="txtArea" runat="server" class="editlabtxt" onkeypress="return ValidateKeypress(/\d/,event);" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="editlab">
                                    <b>Ghi chú : </b>
                                </td>
                                <td>
                                    <asp:Label ID='lbDescription' runat='server'></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" class="editlabtxt" Height="100px"
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id='Tab2_Content'>
                        <div>
                            <div id='search'>
                                <form action="#" class="fromAddress printHidden">
                                <%--<asp:Literal ID="ltdel" runat="server"></asp:Literal>--%>
                                <asp:DropDownList ID="ddcolor"  runat="server" class="editlabtxt" style='position: fixed;display: none; visibility: hidden;
                                    top: 120px; right: 200px;width: 130px;'>
                                        </asp:DropDownList>
                                <input id='delete-button' type='button' value='Xóa' class='bthiden' style='position: fixed;
                                    top: 120px; right: 115px;' />
                                <input id='clear-button' type='button' value='Xóa tất cả' class='bthiden' style='position: fixed;
                                    top: 120px; right: 35px;' />
                                <div class="clear">
                                </div>
                                </form>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtlat" runat="server" Style='display:none;visibility:hidden;'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
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
    <asp:ImageButton ID="cmdEdit" runat="server" ImageUrl="~/Img/bt-edit.png" 
        onclick="cmdEdit_Click"></asp:ImageButton>
</b><asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" 
       OnClientClick="return validateForm()" onclick="cmdSave_Click"></asp:ImageButton>
</b><asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="~/Img/bt-del.png" onclick="cmdDelete_Click" OnClientClick = "Confirm()" 
        ></asp:ImageButton>
</div>
</footer>
    <telerik:RadWindow ID="rrPesticide" runat="server" Skin="Vista" Behavior="Close"
        Left="0" Top="0">
    </telerik:RadWindow>
    <script type="text/javascript">
        var selectColor = '#1E90FF';
        $(document).ready(function () {
            SetTNTabs();
            $("#tnTabs ul li").click(function () {
                var i;
                for (i = 1; i <= 2; i++) {
                    $("#Tab" + i + "_Content").hide();
                    $("#Tab" + i).removeClass("active");
                }
                $("#" + this.id + "_Content").show();
                $("#" + this.id).addClass("active");
            });
            $("#ddcolor").change(function () {
                //alert($("#ddcolor").context.activeElement.value);
                selectColor = $("#ddcolor").context.activeElement.value;
                drawingManagerCreate();
            });
            var content = $('#txtMemID').val();
            $("#txtMemID").keyup(function () {
                if ($('#txtMemID').val() != content) {
                    content = $('#txtMemID').val();
                    if ($('#txtMemID').val() == '') {
                        $("#cmdSave").addClass('hide');
                    }
                    else {
                        $("#cmdSave").removeClass('hide');
                        $("#cmdSave").addClass('unhide');
                    }

                }
            });

        });
        
        function SetTNTabs() {
            for (i = 1; i <= 2; i++) {
                $("#Tab" + i + "_Content").hide();
                $("#Tab" + i).removeClass("active");
            }
            $("#Tab2_Content").show();
            $("#Tab2").addClass("active");
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
        function validateForm() {
            var MemID = document.getElementById('txtMemID').value;
            var MemName = document.getElementById('txtName').value;
            var CooperativeKey = document.getElementById('ddCooperative_Key').value;
            
            if (MemID == null || MemID == "") {
                alert("Vui lòng nhập Mã thành viên !");
                return false;
            }
            if (MemName == null || MemName == "") {
                alert("Vui lòng nhập Tên thành viên !");
                return false;
            }
            if (CooperativeKey == null || CooperativeKey == "") {
                alert("Vui lòng chọn hợp tác xã !");
                return false;
            }
        }
//      
        $(function () {
            $("#txtDateRange").datepicker();
            $("#txtDateExpiration").datepicker();
        });
        $(document).ready(function () {
            if ($('#ddcolor').hasClass('editlabtxt') == true) {
                $('#delete-button').removeClass('bthiden');
                $('#delete-button').addClass('submitOn');
                $('#clear-button').removeClass('bthiden');
                $('#clear-button').addClass('submitOn');
                drawingCon = true;
            }

        });
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Bạn có chắc muốn xóa xã viên này ?")) {
                confirm_value.value = "Có";
            } else {
                confirm_value.value = "Không";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <%-- <asp:Literal ID="ltmap" runat="server"></asp:Literal>--%>
    <script type='text/javascript'>
    var latlngs=<% = LoadLatlng("") %>;
    var center=<% = LoadCenter("") %>;
    var zooms = 17;
    var colors = ['#1E90FF', '#FF1493', '#32CD32', '#FF8C00', '#4B0082'];
    var colorButtons = {};
    var drawingCon =false;
    function initialize() {    
        var shapesMap = new ShapesMap(
        document.getElementById("map-canvas"),
        document.getElementById("delete-button"),
        document.getElementById("clear-button"));
    }
    google.maps.event.addDomListener(window, 'load', initialize);
    
    </script>
    </form>
</body>
</html>
