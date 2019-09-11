<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertifiedOrganization_Edit.aspx.cs" Inherits="Management.Categories.CertifiedOrganization_edit" %>
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
            THÔNG TIN TỔ CHỨC CHỨNG NHẬN</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Mã tổ chức
                    </td>
                    <td>
                        <asp:TextBox ID="txtCertifiedOrganization_ID" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Tên tổ chức
                    </td>
                    <td>
                        <asp:TextBox ID="txtCertifiedOrganization_Name" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                <td>
                        Loại chứng nhận 
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLCategories" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Địa chỉ
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Điện thoại
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Fax
                    </td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Email
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Website
                    </td>
                    <td>
                        <asp:TextBox ID="txtWebsite" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>     
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Quy trình kiểm tra
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamination_Process" runat="server" Width="477px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Cơ sở hạ tầng
                    </td>
                    <td>
                        <asp:TextBox ID="txtInfrastructure" runat="server" Width="477px"></asp:TextBox>
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
