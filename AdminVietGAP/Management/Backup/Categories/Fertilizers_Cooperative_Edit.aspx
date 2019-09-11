<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fertilizers_Cooperative_Edit.aspx.cs" Inherits="Management.Categories.Fertilizers_Cooperative_Edit" %>
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
            THÔNG TIN PHÂN BÓN CỦA HTX</div>
        <div style="float:left">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td style="width: 120px; vertical-align: top; font-weight:bold;">
                        Tên phân bón
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLFertilizers" runat="server" Width="255px">
                        </asp:DropDownList>
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