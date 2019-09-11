<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMap.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Management.Fields.Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Lib/Perfect/perfect-scrollbar.css" rel="stylesheet" type="text/css" />
    <script src="../Lib/Perfect/perfect-scrollbar.js" type="text/javascript"></script>
    <script id="google_map_api_script" src="https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=drawing"></script>
    <script src="../JS/geoxml3.js" type="text/javascript"></script>
    <script src="../JS/Test.js" type="text/javascript"></script>
    <link href="../CSS/Fields.css" rel="stylesheet" type="text/css" />
   <script src="../JS/jquery-ui.js" type="text/javascript"></script>
    <asp:Literal ID="ltfunmap" runat="server"></asp:Literal>
    <script type='text/javascript'>
    /*------------------Var Start------------------*/
    var gxml_corp = [];
    var marker_corp =[];
    var markers = [];
    var zKey = 0;
    var map,gxml_corp1,gxml_vegetables,zNum,zoomFluid, zoomCoords;

    var menuwidth = '165px'
    var menubgcolor = 'lightyellow' 
    var disappeardelay = 250  
    var hidemenu_onclick = "yes"

    var LMember=[<% = listMember("") %>]
    var latlngs = <% = LoadLatlng("") %>
    
    /*------------------Var END------------------*/

    //------------Maps Start------------
    function initialize() {
        var mapOptions = {
            zoom: 10,
            center: new google.maps.LatLng(10.656559161780331, 106.13067626953125),
            mapTypeId: google.maps.MapTypeId.SATELLITE 
        };
        
        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        var shapesMap = new ShapesMap();
        SetStatusPLanLayer('Map1', '1');
        zoomFluid = map.getZoom();
        for (var i = 0; i < CorporationAmount; i++) {
                
            var nCorpInfo = CorporationList[i];
            var nLatLng = new google.maps.LatLng(nCorpInfo.Lat, nCorpInfo.Lng);
            var icon = { 
                            url:'/Img/Tree/' + nCorpInfo.Icon,
                            scaledSize: new google.maps.Size(40, 40)
                        };
            marker_corp[i] = new google.maps.Marker({
                map: map,
                icon: icon, // icon:'/Img/Tree/' + nCorpInfo.Icon,
                position: nLatLng,
                title: nCorpInfo.CorpName,
                zIndex: i
            });
            google.maps.event.addListener(marker_corp[i], 'click', function () {
                zoomCoords = this.getPosition();  
                map.panTo(zoomCoords);
                var nid=this.zIndex + 1;    
                loadCorpInfo(this.zIndex, this);
                SetStatusLayer('Li'+nid,this.zIndex);
            });
            google.maps.event.addListener(map, 'zoom_changed', function () {
                zoomFluid = map.getZoom();
            });
            google.maps.event.addListener(marker_corp[i], 'dblclick', function () {
                if(zoomFluid == 10){
                    zoomTo();
                }
                else{
                    zoomOut();
                }
            }); 
                
        }
    }
    function DSMember(id){
        if(id == "<%= Session["CoopKey"]%>"){
            $('#<%=Image2.ClientID %>').show();
        }
        else{
            $('#<%=Image2.ClientID %>').hide();
        }
        if("<%= Session["GroupKey"]%>" =='4'){
            $('#<%=Image2.ClientID %>').show();
        }
        var Name ='';
        var stt = 0;
        for (var i = 0; i < LMember.length; i++) {
           var DS = LMember[i];
               if(DS.Cooperative_Key == id){
               var Member_key='"MemberDetail('+DS.Member_Key+')"';
                   stt = stt+1;
                   Name += '<p><a Class="ListItemMember" onclick='+Member_key+'>'+stt+'. '+DS.Member_Name+'</p>';
               }
        }
        var ListMember = '<div>'+ Name  +'</li></div>';
        $("#lt_Member").html("");
        $("#lt_Member").html(ListMember);

    }
    //------------Maps END--------------

    //------------Maps Zoom Start--------------
    function zoomTo(){
        if(zoomFluid==17) return 0;
        else {
                zoomFluid ++;
                map.setZoom(zoomFluid);
                setTimeout("zoomTo()", 100);
        }
    }
    function zoomOut(){
        if(zoomFluid==10) return 0;
        else {
                zoomFluid --;
                map.setZoom(zoomFluid);
                setTimeout("zoomOut()", 100);
        }
    }
    //------------Maps Zoom END--------------

    //------------Maps Add Marker Start--------------
    function addMarker(location) {
        var marker = new google.maps.Marker({
            position: location,
            map: map
        });
        markers.push(marker);
    }
    //------------Maps Add Marker END----------------

    //------------Maps MarkerClick Start--------------
    function SetStatusPLanLayer(id, key) {
        if ($('#' + id).hasClass('ListItemActivate') == false) {
            $('#' + id).removeClass('ListItem');
            $('#' + id).addClass('ListItemActivate');
            LoadPlanKML(key,true);
        }
        else {
            $('#' + id).removeClass('ListItemActivate');
            $('#' + id).addClass('ListItem');
            LoadPlanKML(key,false);
        }
    }
    function SetStatusLayer(id, key) {
    $('#' + zNum).removeClass('ListItemActivate');
        $('#' + zNum).addClass('ListItem');
        if ($('#' + id).hasClass('ListItemActivate') == false) {
            $('#' + id).removeClass('ListItem');
            $('#' + id).addClass('ListItemActivate');
            loadCorpInfo(key, marker_corp[key]);
            map.panTo(marker_corp[key].getPosition());
            
        }
        else {
            $('#' + id).removeClass('ListItemActivate');
            $('#' + id).addClass('ListItem');
            loadCorpKML(key,false);
            loadCorpInfo(key, marker_corp[key]);
        }
    zNum = id;
    }
    //------------Maps MarkerClick END----------------
    


    /*------------------Event Click Menu Start------------------*/
    function show(Content,Cooperative_Key) {
        if ($('#box-info').hasClass('aside') == false) {
            $("#box-info").removeClass("aside_hide");
            $("#box-info").addClass("aside");
            $("#lt_info").html("");
            $("#lt_info").html(Content);
            $("#ContentPlaceHolder1_lbkey").text(Cooperative_Key);
            
        }
        else {
            $("#lt_info").html("");
            $("#lt_info").html(Content);
            $("#ContentPlaceHolder1_lbkey").text(Cooperative_Key);
        }
    }
    $(document).ready(
    function () {
        $('#wd-link').click(function () {
            if ($('#box-info').hasClass('aside') == true) {
                $("#box-info").removeClass("aside");
                $("#box-info").addClass("aside_hide");
            }
            else {
                $("#box-info").removeClass("aside_hide");
                $("#box-info").addClass("aside");
            }
        });
        $('#listPLanLayer').click(function () {
            $("#PLanLayer").slideToggle();
            if($('#listPLanLayer').hasClass('imgup')==true)
            {
                $('#listPLanLayer').removeClass('imgup');
                $('#listPLanLayer').addClass('imgdown');
            }
            else
            {
                $('#listPLanLayer').removeClass('imgdown');
                $('#listPLanLayer').addClass('imgup');
            }
        });
        $('#listLayer').click(function () {
            $("#Layer").slideToggle();
            if ($('#listLayer').hasClass('imgup') == true) {
                $('#listLayer').removeClass('imgup');
                $('#listLayer').addClass('imgdown');
            }
            else {
                $('#listLayer').removeClass('imgdown');
                $('#listLayer').addClass('imgup');
            }
        });
    });
    /*------------------Event Click Menu Start------------------*/

    /*------------------Maps Add Plan Start------------------*/
    player_plan_kml = 
    {
        1:{Name:'1mWOqhiHZIoO50AflgcFe_QxYAYWcAR7XsFtMLGbF',
            obj:Object},
        2:{Name:'1tFe_2W_VtSXbkWI1o9zxEzQh9UrfmjTniwSZ75wt', 
            obj:Object},
    };
    /*------------------Maps Add Plan END------------------*/

    /*------------------Maps Load Plan Start------------------*/
    function LoadPlanKML(id, flag) {
        if(flag)
        {
            var layer = new google.maps.FusionTablesLayer({
                query: {
                    select: '\'geometry\'',
                    from: player_plan_kml[id].Name
                    }, 
                    options: {
                    styleId: 2,
                    templateId: 2
                }
                });
            player_plan_kml[id].obj = layer;
            player_plan_kml[id].obj.setMap(map);
        }
        else
        {
            player_plan_kml[id].obj.setMap(null);
            delete player_plan_kml[id].obj;
        }
    }

    var infowindow = new google.maps.InfoWindow({
        maxWidth: 500
    });
    /*------------------Maps Load Plan END------------------*/
    google.maps.event.addDomListener(window, 'load', initialize);

    /*------------------Load Edit Start------------------*/
    function PesticidesAdd(ID) {
        CheckSession();
        var oWnd = $find('<%= rwPesticide.ClientID %>');
        oWnd.show();
        oWnd.setUrl('Cooperative_Edit.aspx?Key=' + ID);
        // oWnd.maximize();
        oWnd.setSize(601, 530);
        oWnd.set_modal(true);
        return false;
    }
    function PesticidesDetail() {
        CheckSession();
        var oWnd = $find('<%= rwPesticide.ClientID %>');
        oWnd.show();
        oWnd.setUrl('Cooperative_Detail.aspx?Key=' + zkey);
        // oWnd.maximize();
        oWnd.setSize(701, 530);
        oWnd.set_modal(true);
        return false;
    }
    function MemberDetail(ID) {
        CheckSession();
        var oWnd = $find('<%= rwPesticide.ClientID %>');
        oWnd.show();
        oWnd.setUrl('Member_Detail.aspx?Key=' + ID+'&coop='+keyHTX);
        // oWnd.maximize();
        oWnd.setSize(601, 520);
        oWnd.set_modal(true);
        return false;
    }
    
    function RefeshGvOnClose() {
    }
    function RefeshGvOnSave() {
        document.getElementById('<%=btview.ClientID%>').click();
    }
    function RefeshGvOnCloseDetail() {
    }
    /*------------------Load Edit END------------------*/

    /*------------------Load SEARCH Start------------------*/
    $(document).ready(function () {
        $("#btn_show_search_result").click(function () {
            // display code address
            codeAddress();
            return false;
        });
//            $("#search_address").change(function () {
//                // display code address
//                codeAddress();
//                return false;
//            });
        $('#search_address').bind("keyup keypress", function (e) {
            var code = e.keyCode || e.which;
            if (code == 13) {
                codeAddress();
                return false;
            }
        });
    });

    function codeAddress() {
        var geocoder = new google.maps.Geocoder();
        var address = document.getElementById('search_address').value;
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                map.panTo(results[0].geometry.location);
                placeMarker(results[0].geometry.location);
            } else {
                alert('Không tìm thấy vị trí ');
            }
        });
    }
    function autocomplete() {
        var input = document.getElementById('search_address');

        //var autocomplete = new google.maps.places.Autocomplete(input);
    }
    google.maps.event.addDomListener(window, 'load', autocomplete);
    function removeLastMarker() {
        var test = measure.mvcLine.pop();
        if (test) {
            measure.mvcPolygon.pop();
            var marker = measure.mvcMarkers.pop();
            marker.setMap(null);
            compute_length();
        }
    }
    function placeMarker(latLng) {
        var marker;
        if (markers != '') {
            markers[0].setMap(null);
            markers=[];
        }
        marker = new google.maps.Marker({ map: map, position: latLng, draggable: true });
        markers.push(marker);

    }
    /*------------------Load SEARCH END------------------*/

    /*------------------Load Content Start------------------*/
    var keyHTX;
    function loadCorpInfo(Index, marker) {
        var nCorpInfo = CorporationList[Index];
        var nContent = '<div><b><asp:HyperLink runat="server" href="#" onclick=zoomTo()>' + nCorpInfo.CorpName + '</asp:HyperLink></b></div>';
        var Content = '<div id="content">' +
                        '<h3 id="firstHeading" class="firstHeading">' + nCorpInfo.CorpName + '</h3>' +
                        '<div id="bodyContent">' +
                        '<p><b>Địa chỉ : </b> ' + nCorpInfo.Address +
                        '<p><b>Liên hệ : </b>' + nCorpInfo.Phone +
                        '<p><b>Sản phẩm : </b>' + nCorpInfo.TreeType +
                        '<p><b>Tổng diện tích (ha): </b>' + nCorpInfo.Area +
                        '<p><h3><b><asp:HyperLink runat="server" href="#" onclick=PesticidesDetail()>Chi tiết</asp:HyperLink> </b></h3>' +
                        '</div>' +
                        '</div>';
        DSMember(nCorpInfo.Cooperative_Key);
        keyHTX = nCorpInfo.Cooperative_Key;
        show(Content,nCorpInfo.Cooperative_Key);
        infowindow.setContent(nContent);
        infowindow.open(map, marker);
        zkey = nCorpInfo.Cooperative_Key;
    }
    /*------------------Load Content END------------------*/
    //------------TabContent Start------------
        $(document).ready(function () {
            SetTNTabs();
            $("#tnTabs ul li").click(function () {
                var i;
                for (i = 1; i <= 2; i++) {
                    $("#Tab" + i + "_Content").hide();
                    $("#Tab" + i).removeClass("active");
                }
                $("#" + this.id + "_Content").show();
                $("#" + this.id).addClass("active");
            });
            

        });
        function SetTNTabs() {
            for (i = 1; i <= 2; i++) {
                $("#Tab" + i + "_Content").hide();
                $("#Tab" + i).removeClass("active");
            }
            $("#Tab1_Content").show();
            $("#Tab1").addClass("active");

           
        }
        //------------TabContent END--------------
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                            <a href='#'><div id='listLayer1' class='imgup'></div></a>
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
                <div id="Message" style="color: White">
                    <asp:ImageButton ID="btview" Width="0px" Height="0px" style = '  display: none;  visibility: hidden;' runat="server" OnClick="btview_Click"  />
                </div>
            </div>
        </div>
    </div>
    <div id="ContentRight" style="padding: 0px;">
        <div class="aside_hide" id='box-info'>
            <div class="reference">
                    <div id="wd-link" style="cursor: pointer;">
                        <span class="ico-arrow"></span>
                        <h2>
                            Thông Tin</h2>
                    </div>
                <div id="tnTabs">
                    <div>
                        <ul class="tabs">
                            <li id="Tab1" style='width:49%'>Thông tin HTX</li>
                            <li id="Tab2" style='width:50%'>Danh sách xã viên
                            <asp:Image ID="Image2" runat="server" Style="padding-left: 5px; margin-top: 5px;cursor: pointer;"
                                ImageUrl="~/Img/bt-addnewb.png" onclick="MemberDetail('0')" /></li>
                        </ul>
                    </div>
                    <div class="tabcontent_tab">
                        <div id='Tab1_Content'style='padding:10px;'>
                            <table border="0" cellpadding="2" cellspacing="0">
                                <div class="block-info">
                                    <div id="lt_info" >
                                    </div>
                                </div>
                            </table>
                        </div>
                        <div id='Tab2_Content' style='padding-left: 10px;padding-bottom: 0px;'>
                            <table border="0" cellpadding="2" cellspacing="0">
                                <div class="block-info">
                                
                                <asp:Label ID='lbkey' runat='server' Visible="false" value ='0'></asp:Label>
                                    <div id="lt_Member" style = 'height:404px;overflow:auto;'>
                                    </div>
                                </div>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id='search'>
        <form action="#" class="fromAddress printHidden">
            <%--<input id="search_address" type="text" class="textBefore" placeholder="Tìm kiếm..." />
            <input id="btn_show_search_result" type="submit" value="Tìm kiếm" class="submitOn" />
            <div class="clear"></div>--%>
            </form>
        </div>
        <div id="map-canvas">
        </div>
    </div>
    <telerik:RadWindow ID="rwPesticide" runat="server" Skin="Vista" Behavior="Close"
        Left="0" Top="0">
    </telerik:RadWindow>
</asp:Content>
