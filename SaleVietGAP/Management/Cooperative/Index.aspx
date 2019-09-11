<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCoop.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Management.Cooperative.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="LTtitle" runat="server"></asp:Literal>
    <asp:Literal ID="ltmap" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="carousel-example-generic" class="well well-sm carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <asp:Literal ID="ltSlideshow" runat="server"></asp:Literal>
        <!-- Controls -->
        
        <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span><span class="sr-only">
                Previous</span> </a><a class="right carousel-control" href="#carousel-example-generic"
                    role="button" data-slide="next"><span class="glyphicon glyphicon-chevron-right" aria-hidden="true">
                    </span><span class="sr-only">Next</span> </a>
    </div>

    <div id ="about" class="page-header">
        <h2>
            Giới thiệu</h2>
    </div>
    <div class="well" style="min-height: 400px;">
        <asp:Literal ID="LTNameCoop" runat="server"></asp:Literal>
    </div>
    <div id ="product" class="page-header">
        <h2>
            Sản Phẩm</h2>
    </div>
    <div class="row">
        <asp:Literal ID="LTPro" runat="server"></asp:Literal>
    </div>
    <div id ="contact" class="page-header">
        <h2>
            Bản đồ</h2>
    </div>
    <div class="well" id='map-canvas' style="min-height: 400px;">
    </div>
</asp:Content>
