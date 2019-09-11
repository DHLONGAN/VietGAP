<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandUse_Edit.aspx.cs" Inherits="Management.Culture.LandUse_Edit" %>
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
    <div>
        <div class="HeaderFormSub">
            THÔNG TIN SỬ DỤNG ĐẤT</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Ngày thực hiện
                    </td>
                    <td>
                        
                         <asp:Label ID="txtDatetime" runat="server"></asp:Label>
                        
                    </td>
                  
                </tr>
              
                <tr>
                    <td style="width: 160px; vertical-align:top">
                        Loại Giống
                    </td>
                    <td>
                         <asp:Label ID="DDLSeeds" runat="server">
                        </asp:Label>
                    </td>
                  
                </tr>
                <tr>
                    <td style="vertical-align:top" class="style1">
                        Công việc
                    </td>
                    <td class="style2">
                          <asp:Label ID="txtAction" runat="server"></asp:Label>
                    </td>
                  
                </tr>
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Lý do thực hiện
                    </td>
                         <td>
                         <asp:Label ID="txtReason" runat="server"></asp:Label>
                    </td>                  
                </tr>   
                  <tr>
                    <td style="width: 180px; vertical-align:top">
                       Phương pháp thực hiện
                    </td>
                         <td>
                         <asp:Label ID="txtSolution" runat="server"></asp:Label>
                    </td>                  
                </tr>      
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Ghi chú
                    </td>
                         <td>
                         <asp:Label ID="txtNotice" runat="server"></asp:Label>
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