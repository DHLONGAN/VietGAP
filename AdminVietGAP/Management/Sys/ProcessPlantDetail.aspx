<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessPlantDetail.aspx.cs" Inherits="Management.Sys.ProcessPlantDetail" %>
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
             //$("#txtDatetime").datepicker();
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
            SỬA THÔNG TIN CÔNG VIỆC</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 160px; vertical-align:top">
                        Loại công việc
                    </td>
                    <td>
                         <asp:DropDownList ID="DDLType" runat="server" Width="263px" 
                             AutoPostBack="True" Height="24px" 
                             onselectedindexchanged="DDLType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                  
                </tr>  
  
                <tr>
                    <td style="width: 160px; vertical-align:top">
                        Ghi chú
                    </td>
                    <td>
                         <asp:TextBox ID="txtDescription" runat="server" Width="255px"></asp:TextBox>
                    </td>
                  
                </tr>   
                <asp:Panel ID="PnLandUse" runat="server">
                <tr>
                    <td style="width: 160px; vertical-align:top">
                       Cách ngày trồng (ngày)
                    </td>
                    <td>
                         <asp:TextBox ID="txtDateNum" runat="server" Width="255px" Text="0"></asp:TextBox>
                    </td>
                  
                </tr> 
                 <tr>
                    Nếu ngày thực hiện trước ngày gieo trồng thì điền số âm                  
                </tr> 
                 <tr>
                    <td style="vertical-align:top" class="style1">
                        Công việc
                    </td>
                    <td class="style2">
                          <asp:TextBox ID="txtAction" runat="server" Width="255px"></asp:TextBox>
                    </td>
                  
                </tr>
                 <tr>
                    <td style="width: 120px; vertical-align:top">
                       Lý do thực hiện
                    </td>
                         <td>
                         <asp:TextBox ID="txtReason" runat="server" Width="255px"></asp:TextBox>
                    </td>                  
                </tr>   
                  <tr>
                    <td style="width: 180px; vertical-align:top">
                       Phương pháp thực hiện
                    </td>
                         <td>
                         <asp:TextBox ID="txtSolution" runat="server" Width="255px"></asp:TextBox>
                    </td>                  
                </tr>      
                </asp:Panel>  
                 

                <asp:Panel ID="PNSeed" runat="server" Visible ="false">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Ngày mua (Trước khi gieo):
                    </td>
                    <td>
                       <asp:TextBox ID="txtDateBuy" runat="server" Width="255px" Text="0"></asp:TextBox>
                    </td>
                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Ngày trồng (ngày bắt đầu):
                    </td>
                    <td>
                       <asp:TextBox ID="txtDateOfManufacture" runat="server" Width="255px" Text="0"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Ngày gieo (ngày):
                    </td>
                    <td>
                       <asp:TextBox ID="txtDateSowing" runat="server" Width="255px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Ngày thu hoạch cách ngày trồng:
                    </td>
                    <td>
                       <asp:TextBox ID="txtEndNum" runat="server" Width="255px" Text="0"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Nguồn gốc:
                    </td>
                    <td>
                       <asp:TextBox ID="txtCompany" runat="server" Width="255px" Text="Giống địa phương"></asp:TextBox>
                    </td>
                  
                </tr>
                </asp:Panel>  


                <asp:Panel ID="PNFerU" runat="server" Visible ="false">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Cách ngày trồng
                    </td>
                    <td>
                        
                        <asp:TextBox ID="txtDateNum2" runat="server" Width=255px Text="0"></asp:TextBox>
                        
                    </td>
                  
                </tr>

                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Công việc
                    </td>
                         <td>
                         <asp:TextBox ID="txtParcel2" runat="server" Width=255px Text="Bón phân"></asp:TextBox>
                    </td>                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Lý do áp dụng/ Mật độ
                    </td>
                         <td>
                        <asp:TextBox ID="txtArea2" runat="server" Width=255px></asp:TextBox>
                    </td>                  
                </tr> 
                  <tr>
                    <td style="width: 120px; vertical-align:top">
                       Phương pháp  
                    </td>
                         <td>
                         <asp:TextBox ID="txtHowtouse" runat="server" Width=255px Text="Rải"></asp:TextBox>
                    </td>                  
                </tr>
                <tr>
                    <td>
                        Thiết bị sử dụng
                    </td>
                    <td>
                        <asp:DropdownList ID="DDLEquipment" runat="server" Width=255px></asp:DropdownList>
                    </td>
                  
                </tr>

                <tr>
                    <td>
                        Nguyên vật liệu 
                    </td>
                    <td>
                         <asp:DropdownList ID="DDLFertilizer" runat="server" Width=255px></asp:DropdownList>
                    </td>
                  
                </tr>
                <tr>
                    <td style="width: 120px; vertical-align:top">
                      Liều lượng
                    </td>
                         <td>
                        <asp:TextBox ID="txtFormulaUsed" runat="server"></asp:TextBox>&nbsp;<asp:DropdownList ID="DDLUnit" runat="server"></asp:DropdownList>
                    </td>                  
                </tr>

                </asp:Panel>   


                <asp:Panel ID="PNPesU" runat="server" Visible ="false">
                <tr>
                    <td style="width: 120px; vertical-align:top">
                        Cách ngày trồng
                    </td>
                    <td>
                        
                         <asp:TextBox ID="txtDatetime" runat="server" Width=255px></asp:TextBox>
                        
                    </td>
                  
                </tr>
                
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Công việc
                    </td>
                         <td>
                         <asp:TextBox ID="txtArea3" runat="server" Width=255px Text="Phun Thuốc"></asp:TextBox>
                    </td>                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Lý do áp dụng/ Mật độ
                    </td>
                         <td>
                         <asp:TextBox ID="txtPestName" runat="server" Width=255px Text="Sâu bệnh"></asp:TextBox>
                    </td>                  
                </tr> 
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Phương pháp
                    </td>
                         <td>
                         <asp:TextBox ID="txtSolution2" runat="server" Width=255px Text="Phun"></asp:TextBox>
                    </td>                  
                </tr> 
                <tr>
                    <td style="width: 160px; vertical-align:top">
                       Thiết bị sử dụng
                    </td>
                    <td>
                         <asp:DropdownList ID="DDLEquip" runat="server">
                        </asp:DropdownList>
                    </td>
                  
                </tr> 
                
                <tr>
                    <td style="width: 160px; vertical-align:top" Width=255px>
                        Nguyên vật liệu
                    </td>
                    <td>
                         <asp:DropdownList ID="DDLPesticide" runat="server" >
                        </asp:DropdownList>
                    </td>
                  
                </tr>

                <tr>
                    <td style="width: 120px; vertical-align:top">
                     Liều lượng
                    </td>
                         <td>
                         <asp:TextBox ID="txtDose" runat="server"></asp:TextBox>&nbsp;<asp:DropdownList 
                                 ID="DDLU" runat="server">
                                 </asp:DropdownList>
                    </td>                  
                </tr>   
                <tr>
                    <td style="width: 120px; vertical-align:top">
                       Thời gian cách ly (ngày)
                    </td>
                         <td>
                         <asp:TextBox ID="txtGT" runat="server" Width=255px></asp:TextBox>
                    </td>                  
                </tr>  
                </asp:Panel>  
                 
            </table>
            <br />
            <br />
            <asp:Label ID="Err" runat="server" ForeColor="#FF3300"></asp:Label>
        </div>
    </div>
    <asp:Label ID="txtKey" runat="server" Text="Label" Visible="false"></asp:Label>
    <div class="FooterFormSub">
        <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" OnClick="cmdSave_Click" />
    </div>
    </form>
</body>
</html>
