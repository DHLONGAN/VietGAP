<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Edit.aspx.cs" Inherits="Management.Sys.User_Edit" %>
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
     <script type="text/javascript">
         $(function () {
             $("#txtDatetime").datepicker();
             //$("#txtDatetime").datepicker("option", "dateFormat", "dd-mm-yy");
});
        </script>
    
    <style type="text/css">
        .style1
        {
            width: 160px;
            height: 28px;
        }
        .style2
        {
            height: 28px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 10px">
        <div class="HeaderFormSub">
            THÔNG TIN NGƯỜI DÙNG</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Tài khoản
                    </td>
                         <td>
                         <asp:TextBox ID="txtUserName" runat="server" Width="255px"></asp:TextBox>
                    </td>                  
                </tr>   
                <asp:Panel ID="Panel5" runat="server" Visible ="false">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Mật khẩu cũ
                    </td>
                         <td>
                         <asp:TextBox ID="txtPassword" runat="server" Width="255px" TextMode="Password">ABC</asp:TextBox>
                    </td>                  
                </tr>   
                </asp:Panel>
                
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       <asp:Label ID="lbpassword" runat="server" Text="Label">Mật khẩu</asp:Label>
                    </td>
                         <td>
                         <asp:TextBox ID="txtpasswordnew" runat="server" Width="255px" TextMode="Password"></asp:TextBox>
                    </td>                  
                </tr> 
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Mô tả
                    </td>
                         <td>
                         <asp:TextBox ID="txtDescription" runat="server" Width="255px"></asp:TextBox>
                    </td>                  
                </tr>   
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:add;">
                        Trạng thái
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdbActivate" runat="server" RepeatDirection="Horizontal" Width="255px">
                            <asp:ListItem Text="Kích hoạt" Selected="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Không kích hoạt" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
<%--                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Ngày hết hạn
                    </td>
                    <td>
                        
                         <asp:TextBox ID="txtDatetime" runat="server" Width="255px"></asp:TextBox>
                        
                    </td>
                  
                </tr>--%>
                 <asp:Panel ID="Panel4" runat="server" Visible="false">
                                <tr>
                    <td style="width: 160px; vertical-align:top">
                        Loại tài khoản
                    </td>
                    <td>
                         <asp:DropDownList ID="DDLGroup" runat="server" Width="262px" Height="24px" 
                             onselectedindexchanged="DDLGroup_SelectedIndexChanged" AutoPostBack="True">
                             <asp:ListItem Value="4">Quản trị viên</asp:ListItem>
                             <asp:ListItem Value="3">Liên Hợp tác xã</asp:ListItem>
                             <asp:ListItem Value="2">Chủ nhiệm hợp tác xã</asp:ListItem>
                             <asp:ListItem Value="1">Xã viên</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                  
                </tr>  
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server">
                <tr>
                    <td style="width: 160px; vertical-align:top">
                        Thuộc Liên HTX
                    </td>
                    <td>
                         <asp:DropDownList ID="DDLCooperativeVentures" runat="server" Width="262px" Height="24px" 
                             AutoPostBack="True" 
                             onselectedindexchanged="DDLCooperativeVentures_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                  
                </tr>  
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                                <tr>
                    <td style="width: 160px; vertical-align:top">
                        Hợp Tác xã
                    </td>
                    <td>
                         <asp:DropDownList ID="DDLCooperative" runat="server" Width="263px" 
                             AutoPostBack="True" 
                             onselectedindexchanged="DDLCooperative_SelectedIndexChanged" Height="24px">
                        </asp:DropDownList>
                    </td>
                  
                </tr>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                 <tr>
                    <td style="width: 160px; vertical-align:top">
                        Hộ sản xuất
                    </td>
                    <td>
                         <asp:DropDownList ID="DDLMember" runat="server" Width="262px" Height="24px">
                        </asp:DropDownList>
                    </td>
                  
                </tr>   
                </asp:Panel>
               


                
               


            </table>
            <div style="text-align:center; padding:10px""> <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" OnClick="cmdSave_Click" /></div>
        <asp:Label ID="lberr" runat="server" ForeColor="Red" style="text-align: center"></asp:Label>
        </div>
    </div>
    <asp:Label ID="txtKey" runat="server" Text="Label" Visible="false"></asp:Label>
    </form>
</body>
</html>
