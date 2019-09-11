<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBook.Master" AutoEventWireup="true"
    CodeBehind="Map.aspx.cs" Inherits="BookVietGAP.Map" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=drawing"></script>
    <script src="http://google-maps-utility-library-v3.googlecode.com/svn/trunk/geolocationmarker/src/geolocationmarker-compiled.js"></script>
    <script src="http://rilwis.googlecode.com/svn/trunk/weather.min.js"></script>
    <script src="/JS/shapesSave.js" type="text/javascript"></script>
    <script type='text/javascript'>

<%--load map--%>
    var map;
    var selectColor = '#1E90FF';
    var latlngs= <% = LoadLatlng("") %>;
    var center=<% = LoadCenter("") %>;
    var nlat = <% = LoadLat("") %>;
    var zooms = 17;
    var colors = ['#1E90FF', '#FF1493', '#32CD32', '#FF8C00', '#4B0082'];
    var colorButtons = {};
    var drawingCon =true;
    function initialize() {    
        var shapesMap = new ShapesMap(
        document.getElementById("map-canvas"),
        document.getElementById("delete-button"),
        document.getElementById("mylocation-button"));
    }

    google.maps.event.addDomListener(window, 'load', initialize);
    $(document).ready(function () {
         $("#map-canvas").height($(document).height()-$("#navmenu").height());
         document.querySelector("#Area").style.top = ($(document).height()-$("#navmenu").height()).toString() +"px";
         document.querySelector("#mylocation").style.top = ($(document).height()-$("#navmenu").height()-50).toString() +"px";
     });
<%--Save map--%>

    function getServer(name) {
    var result = false;
        $.ajax({
            type: "POST",
            url: "Map.aspx/"+name,
            data: JSON.stringify({ Lat : nlat}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                result = response.d;
            }
        });
    return result;      
    }
    $(document).ready(function () {
            $("#saveconfirm").click(function () {
                if(!getServer('SaveInfo')) { $('#confirmMessError').modal('show');return false; }
                ShowMess('confirmMessSucc',3000);
            });
            $("#delconfirm").click(function () {
                document.getElementById('delete-button').click();
                if(!getServer('SaveInfo')) { $('#confirmMessError').modal('show');return false; }
                ShowMess('confirmDelSucc',3000);
            });
     });
     function weatherhide(){
        $("#weather").slideToggle();
        $("#Area").slideToggle();
     }
     function myLocation(){
        document.getElementById('mylocation-button').click();
     }
     function MyGarden() {
        map.panTo(center);
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='map-canvas'>
    </div>
    <a id='delete-button' href='#' style='display: none; visibility: hidden;'></i>Xóa</a>
    <div id="weather" style="color: white;left: 10px;top: 80px;position: fixed; z-index: 1000;  background: rgba(25, 25, 25, 0.4);border-radius: 10px;">
    <h4>Thời tiết tỉnh Long An</h4>
    <div style="margin-top: -20px;">
        <script>showWeather('hochiminh, vietnam')</script>
        </div>
    </div>
    <div id="mylocation" style="top:200px;right: 10px;position: fixed; z-index: 1000;">
        <img id="mylocation-button" src="Img/ic_mylocation.png" class="mylocationimg"/>
    </div>
    <div id="Area" style="color: white;right: 10px;position: fixed; z-index: 1000;background: rgba(25, 25, 25, 0.4);border-radius: 10px;">
        <h4><asp:Label ID="lbArea" runat="server" Text="Diện tích : "></asp:Label> m&sup2;</h4>
    </div>
</asp:Content>
