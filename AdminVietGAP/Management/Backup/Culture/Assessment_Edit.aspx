<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Assessment_Edit.aspx.cs" Inherits="Management.Culture.Assessment_Edit" MasterPageFile="~/null.Master"%>
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
        <div style="background-color: #e8f3f9; height: 20px; padding: 2px 0 5px 5px">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:ImageButton ID="cmdView" runat="server" ImageUrl="~/Img/search.png" 
                            onclick="cmdView_Click1" Height="1px" Width="1px"/>
                    </td>
                    <td>
                        NGÀY ĐÁNH GIÁ <asp:TextBox ID="txtDatetime" runat="server" Width="120px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="GV_Assessment" runat="server" Width="100%" AutoGenerateColumns="false"
                CellPadding="4" GridLines="Both" CssClass="Border_GridView" ShowHeader="true"
                AllowSorting="True" AllowPaging="true" PageSize="20"
                OnRowDataBound="GV_Assessment_RowDataBound" OnRowCommand="GV_Assessment_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="txtNo" Text='<%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tên môi trường">
                        <ItemTemplate>
                            <asp:Label ID="txtAssessmentName" Text='<%# Eval("AssessmentName")%>' runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Loại tác nhân ô nhiễm">
                        <ItemTemplate>
                            
                                <asp:Label ID="txtCity" Text='<%# Eval("PollutionName")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Đạt">
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdFrontPage" CommandName="ItemFrontPage" ImageUrl='<%# String.Format("/Img/Icons/{0}.png", Eval("Status"))%>'
                                runat="server" CommandArgument='<%# Eval("ProcessEnvironmentalKey")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Chưa đạt">
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdFrontPage2" CommandName="ItemFrontPage" ImageUrl='<%# String.Format("/Img/Icons/{0}.png", ConvertResult(Eval("Status").ToString()))%>'
                                runat="server" CommandArgument='<%# Eval("ProcessEnvironmentalKey")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phương pháp xử lý">
                        <ItemTemplate>
                            <div style="cursor: pointer" onclick="Edit('<%# Eval("ProcessEnvironmentalKey")%>')">
                            <asp:Label ID="txtCountry" Text='<%# Eval("Solution")%>' runat="server" Width=250px></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField>
                    <HeaderTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~\Img\Icons\add_one.png" style="cursor: pointer" onClick="AssessmentEdit(0)"/>
                    </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdDel" CommandName="ItemDel" OnCommand="GrDelete" ImageUrl="~\Img\Icons\Del.gif " runat="server" OnClientClick="javascript:return confirm('Bạn có chắc chắn xóa không?');"  CommandArgument='<%# Eval("AssessmentKey")%>' />
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Center" />

                    </asp:TemplateField>--%>
                </Columns>
                <HeaderStyle CssClass="Header-GridView" />
                <RowStyle CssClass="Item-GridView" />
                <PagerStyle CssClass="Pages-GridView" HorizontalAlign="Right" />
            </asp:GridView>
                        <div style="background-color: #808080;">
        <div style="text-align:center; padding:10px"> <asp:ImageButton ID="cmdSave" runat="server" ImageUrl="~/Img/bt_save.png" OnClick="cmdSave_Click" /></div>
    </div>
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
            oWnd.setUrl("ProcessEnvironmental_Edit.aspx?Key=" + ID);
            // oWnd.maximize();
            oWnd.setSize(400, 200);
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
        $("#ContentPlaceHolder1_cmdSave").on("click", function () {
            if ($("#ContentPlaceHolder1_txtDatetime").val() == '') {
                alert("Bạn chưa chọn ngày đánh giá");
                return;
            };
        });
    </script>
</asp:Content>