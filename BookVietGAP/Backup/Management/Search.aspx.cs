using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using TNLibrary.Book;
using TNLibrary.Search;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;
using System.Text;

namespace BookVietGAP
{
    public partial class Search : System.Web.UI.Page
    {
        int Ckey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Ckey = Session["MemberID"].ToInt();
            MainLoad();
        }
        private void MainLoad()
        {
            
        }
        #region [ Load Danh sách giống cua member ]
        [WebMethod]
        public static string LoadSeedProces()
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = ListSearch_Data.GetListByMemberDay(memberID, DateTime.Now);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Mã truy vết ]
        [WebMethod]
        public static string LoadSearchCode(string Code)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = ListSearch_Data.GetListByCode(Code);
            string cities = "";
            if (nTable.Rows.Count > 0)
            {
                for (int i = 0; i < nTable.Rows.Count; i++)
                {
                    if (i + 1 < nTable.Rows.Count)
                    {
                        cities += "{\"Name\":\"" + nTable.Rows[i]["Name"] + "\"" +
                        ",\"Cooperative_Name\":\"" + nTable.Rows[i]["Cooperative_Name"] + "\"" +
                        ",\"Address\":\"" + nTable.Rows[i]["Address"] + "\"" +
                        ",\"VietGAPCode\":\"" + nTable.Rows[i]["VietGAPCode"] + "\"" +
                        ",\"Phone\":\"" + nTable.Rows[i]["Phone"] + "\"" +
                        ",\"Email\":\"" + nTable.Rows[i]["Email"] + "\"" +
                        ",\"DateRange\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DateRange"])) + "\"" +
                        ",\"DateExpiration\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DateExpiration"])) + "\"" +
                        ",\"SeedsName\":\"" + nTable.Rows[i]["SeedsName"] + "\"" +
                        ",\"DateOfManufacture\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DateOfManufacture"])) + "\"" +
                        ",\"DatetimeSale\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DatetimeSale"])) + "\"" +
                        ",\"WhereToBuy\":\"" + nTable.Rows[i]["WhereToBuy"] + "\"" +
                        ",\"TreeType\":\"" + nTable.Rows[i]["TreeType"] + "\"" +
                    "},";
                    }
                    else
                    {
                        cities += "{\"Name\":\"" + nTable.Rows[i]["Name"] + "\"" +
                        ",\"Cooperative_Name\":\"" + nTable.Rows[i]["Cooperative_Name"] + "\"" +
                        ",\"Address\":\"" + nTable.Rows[i]["Address"] + "\"" +
                        ",\"VietGAPCode\":\"" + nTable.Rows[i]["VietGAPCode"] + "\"" +
                        ",\"Phone\":\"" + nTable.Rows[i]["Phone"] + "\"" +
                        ",\"Email\":\"" + nTable.Rows[i]["Email"] + "\"" +
                        ",\"DateRange\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DateRange"])) + "\"" +
                        ",\"DateExpiration\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DateExpiration"])) + "\"" +
                        ",\"SeedsName\":\"" + nTable.Rows[i]["SeedsName"] + "\"" +
                        ",\"DateOfManufacture\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DateOfManufacture"])) + "\"" +
                        ",\"DatetimeSale\":\"" + Utils.DateTostring((DateTime)(nTable.Rows[i]["DatetimeSale"])) + "\"" +
                        ",\"WhereToBuy\":\"" + nTable.Rows[i]["WhereToBuy"] + "\"" +
                        ",\"TreeType\":\"" + nTable.Rows[i]["TreeType"] + "\"" +
                    "}";
                    }
                }
                cities = "[" + cities + "]";
            }
            else { 
                cities = "";
            }
            return cities;
        }
        #endregion

        
    }
}