<%@ Page Title="" Language="C#" MasterPageFile="~/SiteView.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Management.Lands.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false" type="text/javascript"></script>
    <script src="../JS/geoxml3.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="ContentLeft">
        <div id="TitleCategory" style="padding-left: 5px;">
            DANH MỤC</div>
        <div class="Line1">
        </div>
        <div class="ListCategories" style="padding-top: 0px">
            <div id="Location" class="ItemParentActive" onclick="SetItemParent(this.id)">
                Địa Danh</div>
            <div class="Line2">
            </div>
            <div id="ViewLocation">
                <div style="height: 100px">
                    <ul>
                        <li class="ItemSubActivate">Ranh giới hành chính</li>
                    </ul>
                </div>
                <div class="Line2">
                </div>
            </div>
            <div class="ItemParentActive">
                Danh mục nông nghiệp</div>
            <div class="Line2">
            </div>
            <div>
                <ul>
                    <li class="ItemSub">Danh mục phân bón </li>
                    <li class="ItemSub"><a href="../Categories/Default.aspx"> Danh mục hóa chất/ thuốc BVTV</a></li>
                    <li class="ItemSub">Danh mục điều kiện môi trường</li>
                    <li class="ItemSub">Danh mục tác nhân ô nhiễm</li>
                </ul>
            </div>
        </div>
    </div>
    <div id="ContentRight">
      <div id="map-canvas">
        </div>
    </div>
    <script type="text/javascript">
        function SetItemParent(id) {

            if ($('#' + id).hasClass('ItemParentActive') == false) {
                $('#' + id).removeClass('ItemParent');
                $('#' + id).addClass('ItemParentActive');
                $('#View' + id).show();
            }
            else {
                $('#' + id).removeClass('ItemParentActive');
                $('#' + id).addClass('ItemParent');
                $('#View' + id).hide();

            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var nWidth = $(document).width() - 260;
            var nHeight = $(document).height()-95;          

            $("#ContentRight").css("width", nWidth);
            $("#ContentRight").css("height", nHeight);

            $("#ListButton").click(function () {
                if ($("#TitleCategory").is(":visible")) {
                    $("#ContentLeft").css("width", 40);
                    $("#ContentRight").css("margin-left", 40);
                    $("#ContentRight").css("width", nWidth + 210);
                    $("#TitleCategory").hide();
                    $("#ContentCategory").hide();
                }
                else {
                    $("#ContentLeft").css("width", 250);
                    $("#ContentRight").css("margin-left", 250);
                    $("#ContentRight").css("width", nWidth);
                    $("#TitleCategory").show();
                    $("#ContentCategory").show();
                }
            });

        });         
    </script>
    <script type="text/javascript">

        var map;
        var gxml_view;

        function initialize() {
            var mapOptions = {
                zoom: 10,
                center: new google.maps.LatLng(10.576922006062158, 106.42318725585938),
                mapTypeId: google.maps.MapTypeId.HYBRID
            };
            
            map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            loadKML();
        }
        /*-----------------------------*/
        function loadKML() {
            var nLand = "/KML/Longanmap.kml";
            gxml_view = new GeoXml("gxml_view", map, nLand, {
                publishdirectory: "/Img/",
                iwwidth: 280,
                dohilite: true
            });
            gxml_view.parse("Info");

        }
        /*-----------------------------------------------------*/

        google.maps.event.addDomListener(window, 'load', initialize);

    </script>
</asp:Content>
