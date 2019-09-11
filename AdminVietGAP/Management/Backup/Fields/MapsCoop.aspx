<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="MapsCoop.aspx.cs" Inherits="Management.Fields.MapsCoop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html, body, #map-canvas
        {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }
    </style>
    <script id="google_map_api_script" src="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=drawing"></script>
    <script type="text/javascript">
        
        var map;
        function initialize() {
            map = new google.maps.Map(document.getElementById('map-canvas'), {
                zoom: 8,
                center: { lat: -34.397, lng: 150.644 }
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ContentLeft">
    <div id="TitleCategory" style="float: left; padding-left: 5px;">
            <asp:Label ID="txtTitleCategory" runat="server" Text="CƠ SỞ SẢN XUẤT"></asp:Label>
        </div>
        <div id="ListButton" style="float: right; padding: 7px 10px 0 0; cursor: pointer">
            <img src="/Img/List.png" />
        </div>
        <div class="Line1" style="clear: both">
        </div>
        <div id="ContentMenuSub">
            <div class="Categories">
                <table>
                    <tr>
                        <td>
                            <div style='padding: 7px 0 7px 10px; width: 200px; color: White;'>
                                Bản đồ quy hoạch</div>
                        </td>
                        <td style='text-align: right; width: 100%;'>
                            <a href='#'><div id='listPLanLayer' class='imgup'></div></a>
                        </td>
                    </tr>
                </table>
                <div class="Line2">
                </div>
                <div id='PLanLayer'>
                    <ul>
                        <li id="Map1" class="ListItem" onclick="SetStatusPLanLayer(this.id,'1')">Ranh giới hành
                            chính</li>
                        <li id="Map2" class="ListItem" onclick="SetStatusPLanLayer(this.id,'2')">Quy hoạch rau</li>
                    </ul>
                </div>
                <div class="Line2">
                </div>
                <table>
                    <tr>
                        <td>
                            <div style='padding: 7px 0 7px 10px; width: 200px; color: White;'>
                            Danh sách HTX<a href='#'>
                            <asp:Image ID="Image1" runat="server" Style="padding-left: 5px; margin-bottom: -5px;"
                                ImageUrl="~/Img/bt-addnew.png" onclick="PesticidesAdd('0')" /></a></div>
                        </td>
                        <td style='text-align: right; width: 100%;'>
                            <a href='#'><div id='listLayer' class='imgup'></div></a>
                        </td>
                    </tr>
                    </table>
                <div class="Line2">
                    <div id='Layer'>
                        <div>
                            <ul>
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </ul>
                        </div>
                        <div class="Line2"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ContentRight" style="padding: 0px;">
        <div id='map-canvas'>
        </div>
    </div>
</asp:Content>
