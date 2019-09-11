<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMains.Master" AutoEventWireup="true" CodeBehind="RootVegetables.aspx.cs" Inherits="Management.RootVegetables" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Rau ăn củ VietGap.info</title>
    <script type='text/javascript'>
    //------------Map Start------------
        var urldomain = <% = Urldomain("") %>
        var map;
        var LCooperative=[<% = LoadDSHTX("") %>]
        //var LSeedCoop = ""//[<% = LoadSeedCooperative("") %>]
        var latlngs = <% = LoadLatlng("") %>
        
        //------------TabContent Start------------
        $(document).ready(function () {
            SetTNTabs();
            $("#tnTabs ul li").click(function () {
                var i;
                for (i = 1; i <= 4; i++) {
                    $("#Tab" + i + "_Content").hide();
                    $("#Tab" + i).removeClass("active");
                }
                $("#" + this.id + "_Content").show();
                $("#" + this.id).addClass("active");
            });
        });
        function SetTNTabs() {
            for (i = 1; i <= 4; i++) {
                $("#Tab" + i + "_Content").hide();
                $("#Tab" + i).removeClass("active");
            }
            $("#Tab1_Content").show();
            $("#Tab1").addClass("active");
            DSSeed(1,getServer('GetLoadSeedCooperativeTypeKey'));
        }
        $(document).ready(function () {
            $("#Tab2").click(function () {
            if ($('#lt2').hasClass('ok') == false) {
                DSSeed(2,getServer('GetLoadSeedCooperativeNewTypeKey'));
                }
            });
            $("#Tab3").click(function () {
            if ($('#lt3').hasClass('ok') == false) {
                DSSeed(3,getServer('GetLoadSeedCooperativePriceTypeKey'));
                }
            });
            $("#Tab4").click(function () {
            if ($('#lt4').hasClass('ok') == false) {
                DSHTX(4,"");
                }
            });
        });
        
        
        
        //------------Call AjaxServer-------------

        function getServer(name) {
            var result = new Array;
            $.ajax({
                type: "POST",
                url: "Ajax.aspx/"+name,
                data: '{TypeKey: 4 }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    result = response.d;
                }
            });
            return result;
        }
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="height: 100%;">
        <div class="aside" id='box-info'>
            <div class="reference">
                <div id="wd-link" style="cursor: pointer;">
                    <h2>Thông Tin</h2>
                </div>
                <div id="tnTabs">
                    <div>
                        <ul class="tabs">
                            <li id="Tab1">Nổi bật</li>
                            <li id="Tab2">Mới nhất</li>
                            <li id="Tab3">Theo Giá</li>
                            <li id="Tab4">DS. HTX</li>
                        </ul>
                    </div>
                    <div class="tabcontent_tab">
                        <div id='Tab1_Content'>
                            <table border="0" cellpadding="2" cellspacing="0">
                                <div class="block-info">
                                    <asp:Label ID='lbkey' runat='server' Visible="false" value='0'></asp:Label>
                                    <div id="lt1" style='height: 474px; overflow: auto;'>
                                    </div>
                                </div>
                            </table>
                        </div>
                        <div id='Tab2_Content'>
                            <table border="0" cellpadding="2" cellspacing="0">
                                <div class="block-info">
                                    <div id="lt2" style='height: 474px; overflow: auto;'>
                                    </div>
                                </div>
                            </table>
                        </div>
                        <div id='Tab3_Content'>
                            <table border="0" cellpadding="2" cellspacing="0">
                                <div class="block-info">
                                    <div id="lt3" style='height: 474px; overflow: auto;'>
                                </div>
                            </table>
                        </div>
                        <div id='Tab4_Content'>
                            <table border="0" cellpadding="2" cellspacing="0">
                                <div class="block-info">
                                    <div id="lt4" style='height: 474px; overflow: auto;'>
                                </div>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="map-canvas">
        </div>
    </div>
</asp:Content>
