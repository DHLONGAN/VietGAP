<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Soiltreatment_Edit.aspx.cs"
    Inherits="Management.Culture.Soiltreatment_Edit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/Main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Lib/calendar/jquery-ui.css">
    <script src="/Lib/calendar/jquery-1.10.2.js"></script>
    <script src="/Lib/calendar/jquery-ui.js"></script>
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
            // GetRadWindow().BrowserWindow.location.href = nPath;
            GetRadWindow().BrowserWindow.RefeshGvOnClose();
            GetRadWindow().close();
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 0px">
        <div class="HeaderFormSub">
            XỬ LÝ ĐẤT Ô NHIỄM</div>
        <div style="float: left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 160px; vertical-align: top; font-weight:bold">
                        Ngày xử lý
                    </td>
                    <td>
                        <asp:Label ID="txtDatetime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px; vertical-align: top; font-weight:bold">
                        Hóa chất/ Phụ gia sử dụng
                    </td>
                    <td>
                        <asp:Label ID="DLLAdditives" runat="server" >
                        </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold">
                        Số lượng
                    </td>
                    <td>
                        <asp:Label ID="txtQuantity" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold">
                        Cách xử lý
                    </td>
                    <td>
                        <asp:Label ID="txtHowtouse" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold">
                        Diện tích
                    </td>
                    <td>
                        <asp:Label ID="txtArea" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold">
                        Thời tiết khi sử dụng
                    </td>
                    <td>
                        <asp:Label ID="txtWeather" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="FooterFormSub">
    </div>
    <asp:Label ID="txtKey" runat="server" Text="Label" Visible="false"></asp:Label>
    </form>
</body>
</html>
