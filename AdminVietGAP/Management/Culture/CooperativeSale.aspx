﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="CooperativeSale.aspx.cs" Inherits="Management.Culture.CooperativeSale" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../CSS/GridView.css" rel="stylesheet" type="text/css" />
    <link href="../Lib/Perfect/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Lib/Perfect/perfect-scrollbar.js" type="text/javascript"></script>
    <script src="../JS/jquery.confirm.js" type="text/javascript"></script>
    <%--<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>--%>
    <script src="../JS/jquery.easy-confirm-dialog.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/blitzer/jquery-ui.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <div id="ListLayer" style="border: 0px solid red; width:100%; overflow: hidden; 
        position: absolute;">
                <div class="TitleForm">
            <font size=6>Danh sách xuất bán</font>
        </div>  
        <div style="background-color: #e8f3f9; height: 35px; padding: 2px 0 5px 5px">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td>
                        Từ ngày <asp:TextBox ID="txtfromDatetime" runat="server" Width="120px"></asp:TextBox>
                        Đến ngày <asp:TextBox ID="txttoDatetime" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="cmdView" runat="server" ImageUrl="~/Img/search.png" 
                            onclick="cmdView_Click1" />
                    </td>
                </tr>
            </table>
        </div>
        
        <div>
            <asp:GridView ID="ListSale" runat="server" Width="100%" AutoGenerateColumns="false"
                CellPadding="4" GridLines="Both" CssClass="Border_GridView" ShowHeader="true"
                AllowSorting="True" >
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="txtNo" Text='<%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày bán">
                        <ItemTemplate>
                       <div style="cursor: pointer" onclick="Edit('<%# Eval("CooperativeSaleKey")%>')">
                               <asp:Label ID="txtDatetime" Text='<%# ConvertDate(Eval("Datetime").ToString())%>' runat="server"></asp:Label>
                            </div>
                             
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Loại cây">
                        <ItemTemplate>
                            <asp:Label ID="txtSeedsName" Text='<%# Eval("SeedsName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Mã số truy vết">
                        <ItemTemplate>
                            <asp:Label ID="txtCode" Text='<%# Eval("Code")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Số lượng bán">
                        <ItemTemplate>
                            <asp:Label ID="txtQuantity" Text='<%# Eval("Quantity").ToString() + Eval("Name").ToString()%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tổng tiền">
                        <ItemTemplate>
                            <asp:Label ID="txtPrice" Text='<%# Eval("Price")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nơi bán">
                        <ItemTemplate>
                            <asp:Label ID="txtAddress" Text='<%# Eval("Address")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                    <HeaderTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~\Img\Icons\add_one.png" style="cursor: pointer" onClick="Edit(0)"/>
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdDel" CommandName="ItemDel" OnCommand="GrDelete" ImageUrl="~\Img\Icons\Del.gif" runat="server" OnClientClick="javascript:return confirm('Bạn có chắc chắn xóa không?');"  CommandArgument='<%# Eval("CooperativeSaleKey")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />

                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header-GridView" />
                <RowStyle CssClass="Item-GridView" />
                <PagerStyle CssClass="Pages-GridView" HorizontalAlign="Right"/>
            </asp:GridView>
        </div> 
        <div style="padding-top: 10px">
            <center>
                <asp:DataList ID="DataList1" runat="server" BorderStyle="None" BorderWidth="0"
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
        <div>
         <asp:Label ID="txtPageNumber2" runat="server" Text="1" Visible="false"></asp:Label>
         <asp:Label ID="txtPageSize2" runat="server" Text="16" Visible="false"></asp:Label>
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
        function Edit(ID) {
            var oWnd = $find("<%= rwPesticide.ClientID %>");
            oWnd.show();
            oWnd.setUrl("CooperativeSale_Edit.aspx?Key=" + ID);
            // oWnd.maximize();
            oWnd.setSize(500, 370);
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
