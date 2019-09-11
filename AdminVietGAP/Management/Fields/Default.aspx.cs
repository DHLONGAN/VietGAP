using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using TNLibrary;
using TNLibrary.Fields;
using TNLibrary.SYS;

namespace Management.Fields
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
            Response.Redirect("~/Login.aspx");
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            Session["CoopKey"] = nUserLogin.CooperativeKey.ToString();
            Session["GroupKey"] = nUserLogin.GroupKey.ToString();
            if (nUserLogin.GroupKey < 2 | nUserLogin.GroupKey > 4)
            {
                Image1.Visible = false;
                Image2.Visible = false;
            }
            if (nUserLogin.GroupKey == 2)
            {
                Image1.Visible = false;
                Image2.Visible = true;
            }
            if (nUserLogin.GroupKey == 3)
            {
                Image1.Visible = true;
                Image2.Visible = false;
            }
            if (nUserLogin.GroupKey == 4)
            {
                Image1.Visible = true;
                Image2.Visible = true;
            }
            LoadData();
            
        }
        private void LoadData()
        {
            DataTable nTable = TNLibrary.Fields.Cooperative_Data.GetList_City();
            string zHTX = "";
            string zString = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                string DateRange = "";
                string DateExpiration = "";
                if (nTable.Rows[i]["DateRange"].ToString() != "")
                {
                    DateRange = Convert.ToDateTime(nTable.Rows[i]["DateRange"]).ToString("dd/MM/yyyy");
                }
                if (nTable.Rows[i]["DateExpiration"].ToString() != "")
                {
                    DateExpiration = Convert.ToDateTime(nTable.Rows[i]["DateExpiration"]).ToString("dd/MM/yyyy");
                }
                int zNum = i + 1;
                zHTX += "<li id='Li" + zNum + "' class='ListItem' onclick='SetStatusLayer(this.id," + i + ")'>" + nTable.Rows[i]["Cooperative_Name"] + @"</li>
                    ";
                zString += "{Cooperative_Key:'" + nTable.Rows[i]["Cooperative_Key"] + "', CorpName:'" + nTable.Rows[i]["Cooperative_Name"] + "',ProvincesCitiesID:'" + nTable.Rows[i]["ProvincesCities_Name"] + "',VietGAPCode:'" + nTable.Rows[i]["VietGAPCode"] + "',Members:'" + nTable.Rows[i]["Members"] + "',DateRange:'" + DateRange + "',DateExpiration:'" + DateExpiration + "',Owner:'" + nTable.Rows[i]["Owner"] + "',CertifiedOrganization:'" + nTable.Rows[i]["CertifiedOrganization_Name"] + "',Address:'" + nTable.Rows[i]["Address"] + "',Phone:'" + nTable.Rows[i]["Phone"] + "',Email:'" + nTable.Rows[i]["Email"] + "',Area:'" + nTable.Rows[i]["Area"] + "',Quantity:'" + nTable.Rows[i]["Quantity"] + "',TreeType:'" + nTable.Rows[i]["TreeType"] +  "',Key:" + zNum + ",zIndex:-1,Lat:" + nTable.Rows[i]["Lat"] + ",Lng:" + nTable.Rows[i]["Lng"] + ",Icon:'" + nTable.Rows[i]["Images"] + @"',FileKML:'/KML/HTX_PhuocHoa.kml'},
";
            }
            ltfunmap.Text = @"<script type='text/javascript'>
                                var CorporationAmount = " +nTable.Rows.Count+@";
                                var CorporationList = [" + zString + @"];
            </script>";
            Literal1.Text = zHTX;

            
        }

        protected void btview_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
        protected string listMember(string id)
        {
            DataTable nMember = Member_Info.GetList();
            string listMember = "";
            for (int j = 0; j < nMember.Rows.Count; j++)
            {
                //listMember += nMember.Rows[j]["Cooperative_Key"] + " " + nMember.Rows[j]["Name"] + "</p>";
                listMember += "{Cooperative_Key:'" + nMember.Rows[j]["Cooperative_Key"] + "',Member_Key:'" + nMember.Rows[j]["Key"] + "',Member_Name:'"+ nMember.Rows[j]["Name"]+"'},";
            }
            return listMember;
        }
        protected string LoadLatlng(string id)
        {
            String latlng = "";
            String loadll = "";
            DataTable info = Member_Info.GetList();
            for (int i = 0; i < info.Rows.Count; i++)
            {
                if (info.Rows[i]["LatLng"].ToString() != "")
                {
                    loadll += info.Rows[i]["LatLng"].ToString() + ",";
                    
                }
            }
            
            //string buf = '{"shapes":[';
            latlng = "'shapes= {shapes:[" + loadll + "]}';";
            return latlng;
        }
        
    }
}