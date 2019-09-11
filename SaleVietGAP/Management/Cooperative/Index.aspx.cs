using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SAL;
using System.Data;
using TNLibrary.SYS;
using TNLibrary.Fields;

namespace Management.Cooperative
{
    public partial class Test : System.Web.UI.Page
    {
        int Ckey = 0;
        string urldomain="";
        protected void Page_Load(object sender, EventArgs e)
        {
            string nURL = Page.ResolveUrl(Request.RawUrl);
            //string nURL = "Cooperative/HTX/phuochoa";
            string[] _htxID = nURL.Split('/');
            string key = "0";
            key = _htxID[2];
            if (!IsPostBack)
            {
                if (key != "")
                {
                    Cooperative_Info Info = new Cooperative_Info(key);
                    Ckey = Info.Cooperative_Key.ToInt();
                }
                LoadInfo(Ckey);
            }
        }
        private void LoadInfo(int id)
        {
            
            loadPro(id);
            loadSlideshow(id);
            loadTitle(id);
            LoadMap(id);
        }
        private void loadPro(int id)
        {
            Domain_Info info = new Domain_Info(1);
            urldomain = info.Name;
            DataTable nTable = Product_Info.GetListbyCoop(id);
            string pro = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
               string Price="";
               if (nTable.Rows[i]["Price"].ToInt() == 0)
               {
                   Price = "Vui lòng liên hệ";
               }
               else{
                   Price = string.Format("{0:#,0.#} đ", nTable.Rows[i]["Price"]);
               }
                pro += @"<div class='col-sm-6 col-md-3'>
                <div class='thumbnail'>
                    <img style='height:200px;'  src='" + urldomain + "/" + nTable.Rows[i]["Images"].ToString().Substring(2) + @"'>
                    <div class='caption'>
                        <h3>" + nTable.Rows[i]["SeedsName"] + @"</h3>
                        <p>Giá bán : <a style='color: #FF0000;font-weight: bold;height: 15px;font-size: 15px;'>" + Price + @"</a></p>
                        <p><button type='button' class='btn btn-primary btn' data-toggle='modal' data-target='#pro" + nTable.Rows[i]["SeedKey"] + @"'>Chi tiết</button></p>
                    </div>
                </div>
            </div>
            <div id='pro" + nTable.Rows[i]["SeedKey"] + @"' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='gridModalLabel'
                        aria-hidden='true' style='display: none;'>
                        <div class='modal-dialog'>
                            <div class='modal-content'>
                                <div class='modal-header'>
                                    <button type='button' class='close' data-dismiss='modal' aria-label='Close'>
                                        <span aria-hidden='true'>×</span></button>
                                    <h1 class='modal-title' id='pro" + nTable.Rows[i]["SeedKey"] + @"'>
                                        " + nTable.Rows[i]["SeedsName"] + @"<a class='anchorjs-link' href='#pro" + nTable.Rows[i]["SeedKey"] + @"'><span class='anchorjs-icon'></span></a></h1>
                                </div>
                                <div class='modal-body'>
                                    <div class='container-fluid'>
                                        <div class='row'>
                                            <div class='col-md-4'>
                                                <div class='thumbnail'>
                                                    <img style='height:200px;' src='" + urldomain + "/" + nTable.Rows[i]["Images"].ToString().Substring(2) + @"'></div>
                                                </div>
                                            <div class='col-md-8'>
                                                <p><div class='col-md-4'><h4> Nơi sản xuất : </h4></div><div class='col-md-8'><h4>" + nTable.Rows[i]["Cooperative_Name"] + @"</h4></div></p>
                                                <div class='col-md-4'><h4>Loại cây : </h4></div><div class='col-md-8'><h4>" + nTable.Rows[i]["TypeName"] + @"</h4></div>
                                                <p><div class='col-md-4'><h4>Giá bán : </h4></div><div class='col-md-8'><h4 style='color: #FF0000;font-weight: bold;height: 15px;font-size: 15px;'>" + Price + @"</h4></div></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class='modal-footer'>
                                    <button type='button' class='btn btn-primary' data-dismiss='modal'>
                                        Close</button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>";
            }
            LTPro.Text = pro;
        }
        private void loadSlideshow(int id)
        {
            Cooperative_Image_Info Info = new Cooperative_Image_Info(id);
            DataTable nTable = Cooperative_Image_Data.GetListbyCoop(id);
            string shead = "";
            string sbody = "";
            for(int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i > 0)
                {
                    shead += "<li data-target='#carousel-example-generic' data-slide-to='" + i + "'></li>";
                    sbody += @"<div class='item'><img class='img-responsive' style='max-height: 400px;' height = '400px' width = '1200px' src='" + urldomain + "/" + nTable.Rows[i]["Images"].ToString().Substring(3) + "'></div>";
                }
                else 
                {
                    shead += "<li data-target='#carousel-example-generic' data-slide-to='" + i + "' class='active'></li>";
                    sbody += @"<div class='item active'><img class='img-responsive' style='max-height: 400px;' height = '400px' width = '1200px' src='" + urldomain + "/" + nTable.Rows[i]["Images"].ToString().Substring(3) + "'></div>";
                }
            }
            ltSlideshow.Text = @"<ol class='carousel-indicators'>"+shead+@"</ol> <div class='carousel-inner' role='listbox'>" + sbody + "</div>";
        }
        private void loadTitle(int id)
        {
            Cooperative_Info Info = new Cooperative_Info(id);
            LTtitle.Text = "<title>" + Info.Cooperative_Name + "</title>";
            LTNameCoop.Text = "<h1>" + Info.Cooperative_Name + "</h1><p>" + TextToHtml(Info.Description) + "</p>" + "</h1><p>Liên hệ :" + Info.Phone + "</p>" + "</h1><p>Địa chỉ :" + Info.Address + "</p>";
        }
        public static string TextToHtml(string text)
        {
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("\r\n", "\r");
            text = text.Replace("\n", "\r");
            text = text.Replace("\r", "<br>\r\n");
            text = text.Replace("  ", " &nbsp;");
            return text;
        }
        private void LoadMap(int id)
        {
            Cooperative_Info cop = new Cooperative_Info(id);
            ltmap.Text = @"<script type='text/javascript'>
                            function initialize() {
                                var myLatlng = new google.maps.LatLng(" + cop.Lat + "," + cop.Lng + @");
                                var mapOptions = {
                                    zoom: 13,
                                    mapTypeId: google.maps.MapTypeId.hybrid,
                                    center: myLatlng
                                }
                                var icon = { 
                                    url:'"+urldomain+@"/Img/Tree/" + cop.Images + @"',
                                    scaledSize: new google.maps.Size(40, 40)
                                };
                                var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
                                var marker = new google.maps.Marker({
                                    position: myLatlng,
                                    map: map,
                                    icon: icon,
                                    draggable: true,
                                    title:'" + cop.Cooperative_Name + @"'
                                });
                                google.maps.event.addListener(marker, 'click', function (e) {
                                    infowindow.open(map,marker);
                                });
                                var infowindow = new google.maps.InfoWindow({
                                      content: '" + cop.Cooperative_Name + @"'
                                });
                                infowindow.open(map,marker);
                            }
                            google.maps.event.addDomListener(window, 'load', initialize);
                        </script>";
        }
    }
}