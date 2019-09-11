<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeedProcess_Edit.aspx.cs" Inherits="Management.Culture.SeedProcess_Edit" %>
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
    <div>
        <div class="HeaderFormSub">
            THÔNG TIN XỬ LÝ QUẢN LÝ GIỐNG</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 140px; vertical-align:top">
                        Tên giống/Gốp ghép:
                    </td>
                    <td>
                        <asp:Label ID="DDLSeedsName" runat="server">
                        </asp:Label>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Ngày mua:
                    </td>
                    <td>
                       <asp:Label ID="txtDateBuy" runat="server"></asp:Label>
                    </td>
                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Ngày trồng:
                    </td>
                    <td>
                       <asp:Label ID="txtDateOfManufacture" runat="server"></asp:Label>
                    </td>
                  
                </tr>

                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Mã số lô:
                    </td>
                    <td>
                       <asp:Label ID="txtParcel" runat="server" ></asp:Label>
                    </td>
                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Diện tích:
                    </td>
                    <td>
                       <asp:Label ID="txtArea" runat="server" ></asp:Label>&nbsp;<asp:Label ID="DDLAreaUnit" runat="server">
                        </asp:Label>
                    </td>
                  
                </tr> 
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Tổng lượng giống:
                    </td>
                    <td>
                       <asp:Label ID="txtQuantity" runat="server" ></asp:Label>&nbsp;<asp:Label ID="DDLQuantityUnit" runat="server"></asp:Label>
                    </td>
                  
                </tr> 
            </table>
            
        </div>
    </div>
        <div class="FooterFormSub">
<%--        <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" OnClick="cmdSave_Click" />--%>
    </div>
    <asp:Label ID="txtKey" runat="server" Text="Label" Visible="false"></asp:Label>
    </form>
</body>
</html>