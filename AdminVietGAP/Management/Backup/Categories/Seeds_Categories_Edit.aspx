<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seeds_Categories_Edit.aspx.cs" Inherits="Management.Categories.Seeds_Categories_Edit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/Main.css" rel="stylesheet" type="text/css" />
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
    <div class="HeaderFormSub">
        THÔNG TIN LOẠI GIỐNG</div>
    <div style="padding: 10px">
        <div style="float: left">
            <table border="0" cellpadding="4" cellspacing="0">
                <tr>
                    <td style="width: 140px; vertical-align: top; font-weight:bold;">
                        Tên loại giống
                    </td>
                    <td>
                        <asp:TextBox ID="txtCategoryName" runat="server" Width="250px" Height="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px; vertical-align: top; font-weight:bold;">
                        Mã màu
                    </td>
                    <td>
                        <asp:TextBox ID="txtColor" runat="server" Width="250px" Height="50px"></asp:TextBox>
                    </td>
                </tr>
                
            </table>
        </div>
    </div>
    <div class="FooterFormSub">
        <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" OnClick="cmdSave_Click" />
    </div>
    <asp:Label ID="txtKey" runat="server" Text="Label" Visible="false"></asp:Label>
    </form>
</body>
</html>
