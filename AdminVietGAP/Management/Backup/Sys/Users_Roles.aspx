<%@ Page Title="" Language="C#" MasterPageFile="~/null.Master" AutoEventWireup="true" CodeBehind="Users_Roles.aspx.cs" Inherits="Management.Sys.Users_Roles" %>
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
        <div style="background-color: #e8f3f9; height: 35px;">
                <table border="0" cellpadding="5" cellspacing="0">
                    <tr>
                        <td>
                            <font size=6>PHÂN QUYỀN NGƯỜI DÙNG</font><asp:ImageButton ID="cmdView" runat="server" ImageUrl="~/Img/search.png" Width="1px" Height="1px" OnClick="cmdView_Click1" />
                        </td>
                    </tr>
                </table>
        </div>
        <div>
            <asp:GridView ID="GV_Users_Roles" runat="server" runat="server" Width="100%" AutoGenerateColumns="false"
                CellPadding="4" GridLines="Both" CssClass="Border_GridView" ShowHeader="true"
                AllowSorting="True" onrowcommand="GV_Users_Roles_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="txtNo" Text='<%# ShowNo(Container.DataItemIndex)+1%> ' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Danh mục">
                        <ItemTemplate>                           
                                <div style="cursor: pointer" onclick="Users_RolesEdit('<%# Eval("UserKey").ToString() +"+"+ Eval("RoleKey").ToString()%>')">
                                <asp:Label ID="txtRoleName" Text='<%# Eval("RoleName")%>' runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Xem">
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdFrontPage" CommandName="Read" ImageUrl='<%# String.Format("/Img/Icons/{0}.png", ConvertResult(Eval("RoleRead").ToString()))%>'
                                runat="server" CommandArgument='<%# Eval("UserKey").ToString() +"+"+ Eval("RoleKey").ToString()%>' />
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Thêm">
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdFrontPage1" CommandName="Add" ImageUrl='<%# String.Format("/Img/Icons/{0}.png", ConvertResult(Eval("RoleAdd").ToString()))%>'
                                runat="server" CommandArgument='<%# Eval("UserKey").ToString() +"+"+ Eval("RoleKey").ToString()%>' />
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sửa">
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdFrontPage2" CommandName="Sua" ImageUrl='<%# String.Format("/Img/Icons/{0}.png", ConvertResult(Eval("RoleEdit").ToString()))%>'
                                runat="server" CommandArgument='<%# Eval("UserKey").ToString() +"+"+ Eval("RoleKey").ToString()%>' />
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Xóa">
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdFrontPage3" CommandName="Del" ImageUrl='<%# String.Format("/Img/Icons/{0}.png", ConvertResult(Eval("RoleDel").ToString()))%>'
                                runat="server" CommandArgument='<%# Eval("UserKey").ToString() +"+"+ Eval("RoleKey").ToString()%>' />
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>                    
                    <asp:TemplateField>
                    <HeaderTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~\Img\Icons\add_one.png" style="cursor: pointer" onClick="Users_RolesEdit(0)"/>
                    </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:ImageButton ID="cmdDel" CommandName="ItemDel" OnCommand="GrDelete" ImageUrl="~\Img\Icons\Del.gif " runat="server" OnClientClick="javascript:return confirm('Bạn có chắc chắn xóa không?');"  CommandArgument='<%# Eval("UserKey").ToString() +"+"+ Eval("RoleKey").ToString()%>' />
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

//            $("#ListLayer").css("width", nWidth);
//            $("#ListLayer").css("height", nHeight);
//            $("#ListLayer").perfectScrollbar();
            $("#ContentPlaceHolder1_txtfromDatetime").datepicker();
            $("#ContentPlaceHolder1_txttoDatetime").datepicker();
        });
        function Users_RolesEdit(ID) {
            var oWnd = $find("<%= rwPesticide.ClientID %>");
            oWnd.show();
            oWnd.setUrl("Users_Roles_Edit.aspx?Key=" + ID);
            // oWnd.maximize();
            oWnd.setSize(500, 400);
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