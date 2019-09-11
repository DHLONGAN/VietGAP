<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CooperativePurchasing_Other_Edit.aspx.cs" Inherits="Management.Culture.CooperativePurchasing_Other_Edit" %>
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
    <div style="padding: 10px">
        <div class="HeaderFormSub">
                THÔNG TIN THU MUA</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                        Ngày mua
                    </td>
                    <td>
                        <asp:TextBox ID="txtDatetime" runat="server"  Width="245px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Loại cây
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLSeeds" runat="server">
                        </asp:DropDownList>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Mã số truy vết
                    </td>
                    <td>
                        <asp:TextBox ID="txtcode" runat="server"  Width="245px" Enabled="true"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Khối lượng
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server"  Width="182px"></asp:TextBox>&nbsp;<asp:DropDownList 
                                 ID="DDLUnit" runat="server"></asp:DropDownList>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Số lượng sọt/rổ
                    </td>
                    <td>
                        <asp:TextBox ID="txtBaskets" runat="server"  Width="245px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Tổng tiền
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server"  Width="245px" Text="0"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Xử lý sau thu hoạch
                    </td>
                    <td>
                        <asp:TextBox ID="txtSolution" runat="server"  Width="245px" 
                            Text="Rửa sạch, phơi khô, đóng gói"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Đánh giá
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLEvaluate" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Xã viên
                    </td>
                    <td>
                        <asp:TextBox ID="txtMember" runat="server"  Width="245px" Enabled="true"></asp:TextBox>
                    </td>
                  
                </tr>
            </table>
            <asp:Label ID="lberr" runat="server" ForeColor="Red" style="text-align: center"></asp:Label>
        </div>
    </div>
        <div class="FooterFormSub">
        <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" OnClick="cmdSave_Click" />
    </div>
    <asp:Label ID="txtKey" runat="server" Text="Label" Visible="false"></asp:Label>
    </form>
</body>
</html>