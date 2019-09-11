﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Pesticide_Use.aspx.cs" Inherits="Management.Culture.Pesticide_Use" %>
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
            <font size=6>SỬ DỤNG THUỐC BẢO VỆ THỰC VẬT</font>
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
                            onclick="cmdView_Click1" />
                    </td>
                </tr>
            </table>
        </div>
         <div style="padding-right: 20px">
            <asp:GridView ID="GV_Pesticide_Use" runat="server" Width="100%" AutoGenerateColumns="false"
                CellPadding="4" GridLines="Both" CssClass="Border_GridView" ShowHeader="true"
                AllowSorting="True" >
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="txtNo" Text='<%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày">
                        <ItemTemplate>
                            <div style="cursor: pointer" onclick="Edit('<%# Eval("PesticideUseKey")%>')">
                                <asp:Label ID="txtDatetime" Text='<%# ConvertDate(Eval("DatetimeUse").ToString())%>' runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText="Tên cây trồng">
                        <ItemTemplate>
                            <asp:Label ID="txtSeedsName" Text='<%# Eval("SeedsName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Công việc">
                        <ItemTemplate>
                            <asp:Label ID="txtArea" Text='<%# Eval("Area")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lý do áp dụng/ Mật độ">
                        <ItemTemplate>
                            <asp:Label ID="txtPestName" Text='<%# Eval("PestName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="270px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phương pháp">
                        <ItemTemplate>
                            <asp:Label ID="txtSolution" Text='<%# Eval("Solution")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>    
                                    
                    <asp:TemplateField HeaderText="Thiết bị sử dụng">
                        <ItemTemplate>
                            <asp:Label ID="txtEquipmentName" Text='<%# Eval("EquipmentName") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center"/>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nguyên vật liệu">
                        <ItemTemplate>
                            <asp:Label ID="txtTrade_Name" Text='<%# Eval("Trade_Name")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Liều lượng<br>(Nồng độ)">
                        <ItemTemplate>
                            <asp:Label ID="txtDose" Text='<%# Eval("Dose").ToString() + Eval("UNN").ToString()%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center"/>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tổng lượng/ Lít nước">
                        <ItemTemplate>
                            <asp:Label ID="txtDosage" Text='<%# Eval("Dosage") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="230px" HorizontalAlign="Center"/>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="TGCL">
                        <ItemTemplate>
                            <asp:Label ID="txtTGLC" Text='<%# Eval("QuarantinePeriod") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText="Xã viên">
                        <ItemTemplate>
                            <asp:Label ID="txtName" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center"/>
                    </asp:TemplateField>    --%>                   
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
            //$("#ContentPlaceHolder1_txttoDatetime  ").datepicker("option", "dateFormat", "dd/mm/yyyy");
        });
        function Edit(ID) {
            var oWnd = $find("<%= rwPesticide.ClientID %>");
            oWnd.show();
            oWnd.setUrl("Pesticide_Use_Edit.aspx?Key=" + ID);
            // oWnd.maximize();
            oWnd.setSize(500, 380);
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