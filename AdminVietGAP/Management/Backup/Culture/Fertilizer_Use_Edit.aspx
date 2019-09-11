<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fertilizer_Use_Edit.aspx.cs" Inherits="Management.Culture.Fertilizer_Use_Edit" %>
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
            THÔNG TIN SỬ DỤNG PHÂN BÓN</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Ngày mua
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
                    <td style="width: 120px; vertical-align:top">
                       Công việc
                    </td>
                         <td>
                         <asp:Label ID="txtParcel" runat="server"></asp:Label>
                    </td>                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Lý do áp dụng/ Mật độ
                    </td>
                         <td>
                         <asp:Label ID="txtArea" runat="server"></asp:Label>
                    </td>                  
                </tr> 
                  <tr>
                    <td style="width: 120px; vertical-align:top">
                       Phương pháp  
                    </td>
                         <td>
                         <asp:Label ID="txtHowtouse" runat="server"></asp:Label>
                    </td>                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Thiết bị sử dụng
                    </td>
                    <td>
                         <asp:Label ID="DDLEquipment" runat="server">
                        </asp:Label>
                    </td>
                  
                </tr>

                <tr>
                    <td style="width: 160px; vertical-align:top">
                        Nguyên vật liệu 
                    </td>
                    <td>
                         <asp:Label ID="DDLFertilizer" runat="server" >
                        </asp:Label>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                      Liều lượng
                    </td>
                         <td>
                         <asp:Label ID="txtFormulaUsed" runat="server"></asp:Label>&nbsp;<asp:Label 
                                 ID="DDLUnit" runat="server"></asp:Label>
                    </td>                  
                </tr>
<%--                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Diện tích
                    </td>
                         <td>
                         <asp:Label ID="txtArea" runat="server" Width="250px"></asp:Label>
                    </td>                  
                </tr>  --%> 
                
                   
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Tổng lượng/Lít nước
                    </td>
                         <td>
                         <asp:Label ID="txtQuantity" runat="server"></asp:Label>
                    </td>                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       TGCL
                    </td>
                         <td>
                         <asp:Label ID="txtQuarantinePeriod" runat="server"></asp:Label>
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