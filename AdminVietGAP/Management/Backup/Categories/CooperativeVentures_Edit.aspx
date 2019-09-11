<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CooperativeVentures_Edit.aspx.cs" Inherits="Management.Sys.CooperativeVentures_Edit" %>
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
            THÔNG TIN Liên HTX</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Tên Liên HTX
                    </td>
                         <td>
                         <asp:TextBox ID="txtCooperativeVentures" runat="server" Width="255px"></asp:TextBox>
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