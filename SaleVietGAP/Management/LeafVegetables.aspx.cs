using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TNLibrary.Fields;
using TNLibrary.SYS;
using System.Web.Services;

namespace Management
{
    public partial class LeafVegetables : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected string LoadDSHTX(string id)
        {

            DataTable nTable = Cooperative_Data.GetList_City();
            string listCooperative = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                //listMember += nMember.Rows[j]["Cooperative_Key"] + " " + nMember.Rows[j]["Name"] + "</p>";
                listCooperative += "{Cooperative_Key:'" + nTable.Rows[i]["Cooperative_Key"] + "',Cooperative_Name:'" + nTable.Rows[i]["Cooperative_Name"] + "',Lat:" + nTable.Rows[i]["Lat"] + ",Lng:" + nTable.Rows[i]["Lng"] + ",Icon:'" + nTable.Rows[i]["Images"] + "',Phone:'" + nTable.Rows[i]["Phone"] + "',Address:'" + nTable.Rows[i]["Address"] + "',Cooperative_ID:'" + nTable.Rows[i]["Cooperative_ID"] + "'},";
            }
            return listCooperative;
        }
        protected string LoadSeedCooperative(string id)
        {
            DataTable nTable = Seed_Cooperative_Info.GetListSeed();
            string LSeedCoop = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                LSeedCoop += "{SeedsKey:'" + nTable.Rows[i]["SeedsKey"] + "',SeedsName:'" + nTable.Rows[i]["SeedsName"] + "',Cooperative_Name:'" + nTable.Rows[i]["Cooperative_Name"] + "',Cooperative_Key:'" + nTable.Rows[i]["Cooperative_Key"] + "',Images:'" + nTable.Rows[i]["Images"].ToString().Substring(3) + "',Phone:'" + nTable.Rows[i]["Phone"] + "',Address:'" + nTable.Rows[i]["Address"] + "',VietGAPCode:'" + nTable.Rows[i]["VietGAPCode"] + "'},";
            }
            return LSeedCoop;
        }
        
        protected string LoadLatlng(string id)
        {
            String latlng = "";
            String loadll = "";
            DataTable info = Member_Info.GetList();
            for (int i = 0; i < info.Rows.Count; i++)
            {
                string LatLng = info.Rows[i]["LatLng"].ToString();
                if (LatLng != "")
                {
                    loadll += LatLng.Replace("\"type\"", "\"Member_Key\":\"" + info.Rows[i]["Key"].ToString() + "\"," + "\"Name\":\"" + info.Rows[i]["Name"].ToString() + "\",\"type\"") + ",";

                }
            }

            //string buf = '{"shapes":[';
            latlng = "'shapes= {shapes:[" + loadll + "]}';";
            return latlng;
        }

        public string Urldomain(string id)
        {
            Domain_Info info = new Domain_Info(1);
            string urldomain = "'" + info.Name + "'";
            urldomain.Substring(2);
            return urldomain;
        }
    }
}