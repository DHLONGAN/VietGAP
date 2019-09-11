using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Book;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace BookVietGAP
{
    public partial class Map : System.Web.UI.Page
    {
        int Ckey = 0;
        int cp_key = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Ckey = Session["MemberID"].ToInt();
            MainLoad();
        }
        private void MainLoad()
        {
            Member_Info info = new Member_Info(Ckey);
            lbArea.Text = "Diện tích : "+ info.Area.ToString();
        }
        protected string LoadLatlng(string id)
        {
            Member_Info info = new Member_Info(Ckey);
            String latlng = "";
            latlng = @"'{shapes:[" + info.LatLng + "]}';";
            return latlng;
        }
        protected string LoadLat(string id)
        {
            Member_Info info = new Member_Info(Ckey);
            String latlng = "";
            latlng = "'"+info.LatLng+"'";
            return latlng;
        }
        
        protected string LoadCenter(string id)
        {
            string latlng = "";
            Member_Info info = new Member_Info(Ckey);
            if (info.LatLng != "")
            {
                string A = info.LatLng.Replace("\"", "");
                Regex myRegex = new Regex("lat");
                string[] sKetQua = myRegex.Split(A);
                latlng = "new google.maps.LatLng(" + sKetQua[1].Substring(1).Replace("},{", "").Replace("lon:", "") + ");";

            }
            else
            {
                cp_key = info.Cooperative_Key;
                Cooperative_Info Coo = new Cooperative_Info(cp_key);
                if (cp_key != 0)
                {
                    latlng = "new google.maps.LatLng(" + Coo.Lat + "," + Coo.Lng + ");";
                }
                else
                {
                    latlng = "new google.maps.LatLng(10.656559161780331, 106.13067626953125);";
                }
            }
            return latlng;
        }

        [WebMethod]
        public static bool SaveInfo(string Lat)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                Member_Info info = new Member_Info();
                info.Updatelat(memberID, Lat);
                return true;
            }
            catch { return false; }
        }

    }
}