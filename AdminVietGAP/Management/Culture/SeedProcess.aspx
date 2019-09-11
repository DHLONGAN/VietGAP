<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="SeedProcess.aspx.cs" Inherits="Management.Culture.SeedProcess" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../CSS/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Lib/Perfect/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Lib/Perfect/perfect-scrollbar.js" type="text/javascript"></script>
    <script src="../JS/jquery.confirm.js" type="text/javascript"></script>
    <script src="../JS/jquery.easy-confirm-dialog.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/blitzer/jquery-ui.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div id="ListLayer" style="border: 0px solid red; width:100%; overflow: hidden; 
        position: absolute;">
        <div class="TitleForm">
            <font size=6>QUẢN LÝ GIỐNG</font>
        </div>
        <div style="background-color: #e8f3f9; height: 35px; padding: 2px 0 5px 5px">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                     <td>
                        Từ ngày <asp:TextBox ID="txtfromDatetime" runat="server" Width="120px"></asp:TextBox>
                        Đến ngày <asp:TextBox ID="txttoDatetime" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td>
                        Loại cây: <asp:DropDownList ID="DDLSeeds" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DDLSeeds_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:ImageButton ID="cmdView" runat="server" ImageUrl="~/Img/search.png" 
                            onclick="cmdView_Click1"/>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-right: 20px">
            <asp:GridView ID="GV_SeedProcess" runat="server" Width="100%" AutoGenerateColumns="false"
                CellPadding="4" GridLines="Both" CssClass="Border_GridView" ShowHeader="true"
                AllowSorting="True" OnRowCommand="GV_SeedProcess_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="txtNo" Text='<%# ShowNo(Container.DataItemIndex)+1%> ' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tên giống">
                        <ItemTemplate>                           
                                <div style="cursor: pointer" onclick="SeedProcessEdit('<%# Eval("SeedProcessKey")%>')">
                                <asp:Label ID="txtSeedsName" Text='<%# Eval("SeedsName")%>' runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nhà sản xuất/ Phân phối<br> (Nguồn gốc) giống">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtCompanyName" Text='<%# Eval("CompanyName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày mua">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtDateBuy" Text='<%# ConvertDate(Eval("DateBuy").ToString())%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày trồng">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtDateOfManufacture" Text='<%# ConvertDate(Eval("DateOfManufacture").ToString())%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>

<%--                    <asp:TemplateField HeaderText="Ngày mua">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtDateBy" Text='<%# DateTime.Parse(Eval("DateBuy").ToString()).ToString("dd/MM/yyyy")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Mã số lô">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtParcel" Text='<%# Eval("Parcel")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Diện tích">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtArea" Text='<%# Eval("Area").ToString() + Eval("UNA").ToString()%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tổng lượng giống">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtQuantity" Text='<%# Eval("Quantity").ToString() + Eval("UNQ").ToString()%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>

<%--                     <asp:TemplateField HeaderText="Đã kiểm định?">
                         <ItemTemplate>
                                        <asp:ImageButton ID="cmdFrontPage" CommandName="ItemFrontPage" ImageUrl='<%# String.Format("/Img/Icons/{0}.png", Eval("Status"))%>'
                                            runat="server" CommandArgument='<%# Eval("SeedProcessKey")%>' />
                                    </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center"/>
                    </asp:TemplateField>--%>
<%--                     <asp:TemplateField HeaderText="Hóa chất xử lý">
                        <ItemTemplate>
                             
                            <asp:Label ID="txtTrade_Name" Text='<%# Eval("Trade_Name")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Lý do">
                        <ItemTemplate>
                            <asp:Label ID="txtReasons" Text='<%# Eval("Reasons")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
<%--                    <asp:TemplateField HeaderText="Xã viên">
                        <ItemTemplate>
                            <asp:Label ID="txtName" Text='<%# Eval("Name")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
<%--                    <asp:TemplateField>
                    <HeaderTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~\Img\Icons\add_one.png" style="cursor: pointer" onClick="SeedProcessEdit(0)"/>
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdDel" CommandName="ItemDel" OnCommand="GrDelete" ImageUrl="~\Img\Icons\Del.gif " runat="server" OnClientClick="javascript:return confirm('Bạn có chắc chắn xóa không?');"  CommandArgument='<%# Eval("SeedProcessKey")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />

                    </asp:TemplateField>--%>
                </Columns>
                <HeaderStyle CssClass="Header-GridView" />
                <RowStyle CssClass="Item-GridView" />
                <PagerStyle CssClass="Pages-GridView" HorizontalAlign="Right"/>
            </asp:GridView>
        </div> 
                <div style="padding-top: 10px">
            <center>
                <asp:DataList ID="PageNumbers" runat="server" BorderStyle="None" BorderWidth="0"
                    DataKeyField="PageNumberName" CellPadding="0" CellSpacing="0" OnItemCommand="PageNumber_ItemCommand"
                    RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <asp:LinkButton ID="PageNumber" runat="server" Text='<%# Eval("PageNumberName") %>'
                            Width="25px" Height="18px"></asp:LinkButton>
                    </ItemTemplate>
                    <SelectedItemTemplate>
                        <asp:Label ID="PageNumberSelect" runat="server" Text='<%# Eval("PageNumberName") %>'
                            Width="25px" Height="18px"></asp:Label>
                    </SelectedItemTemplate>
                    <ItemStyle CssClass="PageNumber" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <SelectedItemStyle CssClass="PageNumberSelect" />
                    <SeparatorTemplate>
                    </SeparatorTemplate>
                    <SeparatorStyle Width="5px" />
                </asp:DataList>
            </center>
        </div>           
     </div>
     <telerik:RadWindow ID="rwPesticide" runat="server" Skin="Vista" Behavior="Close"
        Left="0" Top="0">
    </telerik:RadWindow>
    <script type="text/javascript">
        $(document).ready(function () {
            var nWidth = $(document).width() - 260;
            var nHeight = $(document).height() - 95;

            $("#ListLayer").css("width", nWidth);
            $("#ListLayer").css("height", nHeight);
            $("#ListLayer").perfectScrollbar();
            $("#ContentPlaceHolder1_txtfromDatetime").datepicker();
            $("#ContentPlaceHolder1_txttoDatetime").datepicker();
        });
        function SeedProcessEdit(ID) {
            var oWnd = $find("<%= rwPesticide.ClientID %>");
            oWnd.show();
            oWnd.setUrl("SeedProcess_Edit.aspx?Key=" + ID);
            // oWnd.maximize();
            oWnd.setSize(450, 280);
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
        function RefeshGvOnClose() {
            document.getElementById('<%=cmdView.ClientID%>').click();
        }
    </script>   
            <div>
        <asp:Label ID="txtPageNumber" runat="server" Text="1" Visible="false"></asp:Label>
        <asp:Label ID="txtPageSize" runat="server" Text="16" Visible="false"></asp:Label>
    </div> 
</asp:Content>