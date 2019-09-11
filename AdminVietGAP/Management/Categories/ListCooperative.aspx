<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListCooperative.aspx.cs" Inherits="Management.Sys.ListCooperative" MasterPageFile="~/SiteMain.Master"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Lib/Perfect/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Lib/Perfect/perfect-scrollbar.js" type="text/javascript"></script>
    <script src="../JS/jquery.confirm.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="../JS/jquery.easy-confirm-dialog.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/blitzer/jquery-ui.css"
        type="text/css" />
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="ListLayer" style="border: 0px solid red; width: 100%; overflow: hidden;
        position: absolute;">
               <div class="TitleForm">
            <font size=6>DANH SÁCH Liên HTX</font>
        </div>
        <div>
            <asp:GridView ID="GV_CooperativeVentures" runat="server" Width="100%" AutoGenerateColumns="false"
                CellPadding="4" GridLines="Both" CssClass="Border_GridView" ShowHeader="true"
                AllowSorting="True" AllowPaging="true" PageSize="20"
               >
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="txtNo" Text='<%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tài khoản">
                        <ItemTemplate>                           
                                <div style="cursor: pointer" onclick="Edit('<%# Eval("CooperativeVenturesKey")%>')">
                                <asp:Label ID="txtUserName" Text='<%# Eval("CooperativeVenturesName")%>' runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    
                                    </Columns>
                <HeaderStyle CssClass="Header-GridView" />
                <RowStyle CssClass="Item-GridView" />
                <PagerStyle CssClass="Pages-GridView" HorizontalAlign="Right" />
            </asp:GridView>
                        
        </div>
    </div>
    <telerik:RadWindow ID="rwPesticide" runat="server" Skin="Vista" Behavior="Close"  Left="0" Top="0">
    </telerik:RadWindow>
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
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtDatetime").datepicker();
            $("#ContentPlaceHolder1_txtDatetime").datepicker("option", "dateFormat", "dd/mm/yy");
        });
        function Edit(ID) {
            var oWnd = $find("<%= rwPesticide.ClientID %>");
            oWnd.show();
            oWnd.setUrl("ListCooperative_Edit.aspx?Key=" + ID);
            // oWnd.maximize();
            oWnd.setSize(600, 600);
            oWnd.set_modal(true);
            return false;
        }

        function confirmCallBackFn(arg) {
            var oConfirm = radconfirm(text, callBackFn, oWidth, oHeight, callerObj, oTitle, imgUrl);
            radalert("Confirm returned the following result: " + arg);
        }
        $(".confirm").easyconfirm({ locale: { title: 'Bạn thật sự muốn xóa', button: ['Không', 'Đúng']} });
        $("#alert").click(function () {
            alert("You approved the action");
        })
        $("#ContentPlaceHolder1_cmdSave").on("click", function () {
            if ($("#ContentPlaceHolder1_txtDatetime").val() == '') {
                alert("Bạn chưa chọn ngày đánh giá");
                return;
            };
        });
    </script>
</asp:Content>