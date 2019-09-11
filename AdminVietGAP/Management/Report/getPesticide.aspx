<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="getPesticide.aspx.cs" Inherits="Management.Report.getPesticide" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/GridView.css" rel="stylesheet" />
    <link href="../Lib/Perfect/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Lib/Perfect/perfect-scrollbar.js" type="text/javascript"></script>
    <script src="../JS/jquery.confirm.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="../JS/jquery.easy-confirm-dialog.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/blitzer/jquery-ui.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="ListLayer" style="border: 0px solid red; width:100%; overflow: hidden; 
        position: absolute;">
    <div id="mainOperation">
        <p style="font-size:16pt; font-weight: bold; text-align: center;">THỐNG KÊ SỬ DỤNG THUỐC THEO TỪNG NĂM</p>
        <p>Lọc dữ liệu: <asp:DropDownList ID="DropDownListProvinces" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListProvinces_SelectedIndexChanged"></asp:DropDownList> <asp:DropDownList ID="DropDownListCooperativeVentures" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownListCooperativeVentures_SelectedIndexChanged"> </asp:DropDownList> <asp:DropDownList ID="DropDownListCooperatives" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownListCooperatives_SelectedIndexChanged"></asp:DropDownList> <asp:DropDownList ID="DropDownListMembers" runat="server"></asp:DropDownList> Từ năm: <asp:DropDownList ID="DropDownListFromYear" runat="server"></asp:DropDownList> đến năm <asp:DropDownList ID="DropDownListToYear" runat="server"></asp:DropDownList> <asp:Button ID="ButtonPreview" runat="server" Text="Xem báo cáo" OnClick="ButtonPreview_Click" /></p>
    </div>
    <div>
        <%
            Response.Write(Session["dataRpt"].ToString()); %>
    </div>
        </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var nWidth = $(document).width() - 260;
            var nHeight = $(document).height() - 95;

            $("#ListLayer").css("width", nWidth);
            $("#ListLayer").css("height", nHeight);
            $("#ListLayer").perfectScrollbar();
        });

    </script>   
</asp:Content>
