<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seeds_Edit.aspx.cs" Inherits="Management.Categories.Seeds_Edit" %>

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
        THÔNG TIN GIỐNG CÂY TRỒNG</div>
    <div style="padding: 10px">
        <div style="float: left">
            <table border="0" cellpadding="4" cellspacing="0">
                <tr>
                    <td style="width: 140px; vertical-align: top; font-weight:bold;">
                        Tên giống
                    </td>
                    <td>
                        <asp:TextBox ID="txtSeedsName" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
<%--                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold;">
                        Cây trồng
                    </td>
                    <td>
                        <asp:DropDownList ID="DLLCategoryID" runat="server" Width="255px">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold;">
                        Loại
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLType" runat="server" Width="255px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold;">
                        Cơ quan tác giả
                    </td>
                    <td>
                        <asp:DropDownList ID="DLLCompany" runat="server" Width="255px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold;">
                        Vụ/Vùng công nhận
                    </td>
                    <td>
                        <asp:DropDownList ID="DLLSeason" runat="server" Width="255px">
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold;">
                        Mức độ công nhận
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdbStatus" runat="server" RepeatDirection="Horizontal" Width="255px">
                            <asp:ListItem Text="Chính thức" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Sản xuất thử" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 140px; vertical-align: top; font-weight:bold;">
                        Diễn giải
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetail" runat="server" Width="250px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left; padding-left: 20px; ">
            <div style="border: solid 1px Gray">
                <asp:Image ID="imgLink" runat="server" Style="margin-top: 1px; margin-left: 10px;"
                    Height="247px" Width="204px" />
            </div>
            <div>
                <asp:FileUpload ID="FileUploadKML" runat="server" />
            </div>
        </div>
    </div>
    <div class="FooterFormSub">
        <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" OnClick="cmdSave_Click" />
    </div>
    <asp:Label ID="txtKey" runat="server" Text="Label" Visible="false"></asp:Label>
    </form>
</body>
</html>
