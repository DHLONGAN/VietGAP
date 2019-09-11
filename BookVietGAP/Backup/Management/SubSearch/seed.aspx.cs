using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using TNLibrary.Search;
using TNLibrary.Book;

namespace BookVietGAP.SubSearch
{
    public partial class seed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["nCount"] = 0;
        }
        public string getsearchid(string id)
        {
            string key = "0";
            if (this.Request["id"] != null)
            {
                key = "'"+ this.Request["id"].ToString()+"'";
            }
            return key;
        }
        #region [ Load Danh sách giống ]
        [WebMethod]
        public static string LoadSeedProces(string Search)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = ListSearch_Data.GetListBySeed(Search);
            HttpContext.Current.Session["nTableSearch"] = nTable;
            HttpContext.Current.Session["nCount"] = nTable.Rows.Count;
            string cities = "";
            if (nTable.Rows.Count > 0)
            {
                for (int i = 0; i < nTable.Rows.Count & i < 10; i++)
                {
                    cities += ",{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\"" +
                        ",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\"" +
                        ",\"CategoryName\":\"" + nTable.Rows[i]["CategoryName"].ToString() + "\"" +
                        ",\"CompanyName\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"" +
                        ",\"StatusName\":\"" + nTable.Rows[i]["StatusName"].ToString() + "\"" +
                        ",\"SeasonName\":\"" + nTable.Rows[i]["SeasonName"].ToString() + "\"" +
                        ",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"" +
                    "}";
                }
                cities = cities.Substring(1);
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách phân bón ]
        [WebMethod]
        public static string LoadFertilizersProces(string Search)
        {
            DataTable nTable = ListSearch_Data.GetListByFertilizers(Search);
            HttpContext.Current.Session["nTableSearch"] = nTable;
            HttpContext.Current.Session["nCount"] = nTable.Rows.Count;
            string cities = "";
            if (nTable.Rows.Count > 0)
            {
                for (int i = 0; i< nTable.Rows.Count & i < 10; i++)
                {
                    cities += ",{\"Key\":\"" + nTable.Rows[i]["FertilizersKey"].ToString() + "\"" +
                            ",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"" +
                            ",\"CategoryName\":\"" + nTable.Rows[i]["CategoryName"].ToString() + "\"" +
                            ",\"CompanyName\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"" +
                            ",\"StatusName\":\"" + nTable.Rows[i]["Common_Name"].ToString() + "\"" +
                            ",\"SeasonName\":\"" + nTable.Rows[i]["Fertilizer_Unit_Name"].ToString() + "\"" +
                            ",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"" +
                        "}";
                }
                cities =  cities.Substring(1);
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách Thuốc & sâu bệnh ]
        [WebMethod]
        public static string LoadPesticidesProces(string Search)
        {
            DataTable nTable = ListSearch_Data.GetListByPesticides(Search);
            HttpContext.Current.Session["nTableSearch"] = nTable;
            HttpContext.Current.Session["nCount"] = nTable.Rows.Count;
            string cities = "";
            if (nTable.Rows.Count > 0)
            {
                for (int i = 0; i < nTable.Rows.Count & i < 10; i++)
                {
                    string SeasonName = "";
                    if (nTable.Rows[i]["SeasonName"].ToString() == "1") 
                        SeasonName = "Được sử dụng";
                    if (nTable.Rows[i]["SeasonName"].ToString() == "2") 
                        SeasonName = "Hạn chế sử dụng";
                    if (nTable.Rows[i]["SeasonName"].ToString() == "3") 
                        SeasonName = "Cấm sử dụng";
                    cities += ",{\"Key\":\"" + nTable.Rows[i]["PesticideKey"].ToString() + "\"" +
                            ",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"" +
                            ",\"CategoryName\":\"" + nTable.Rows[i]["Crop_Name"].ToString() + "\"" +
                            ",\"CompanyName\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"" +
                            ",\"StatusName\":\"" + nTable.Rows[i]["Common_Name"].ToString() + "\"" +
                            ",\"SeasonName\":\"" + SeasonName + "\"" +
                            ",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"" +
                        "}";
                }
                cities = cities.Substring(1);
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load trang ]
        [WebMethod]
        public static string PageSearch(string Num,string Name)
        {
            DataTable nTable = (DataTable)HttpContext.Current.Session["nTableSearch"];
            string cities = "";
            int number = Num.ToInt(); 
            if (nTable.Rows.Count > 0)
            {
                for (int i = (number * 10) - 10; i < nTable.Rows.Count & i < (number * 10); i++)
                {
                    if (Name == "Seed")
                    {
                        cities += ",{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\"" +
                                ",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\"" +
                                ",\"CategoryName\":\"" + nTable.Rows[i]["CategoryName"].ToString() + "\"" +
                                ",\"CompanyName\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"" +
                                ",\"StatusName\":\"" + nTable.Rows[i]["StatusName"].ToString() + "\"" +
                                ",\"SeasonName\":\"" + nTable.Rows[i]["SeasonName"].ToString() + "\"" +
                                ",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"" +
                            "}";
                    }
                    if (Name == "Fertilizers")
                    {
                        cities += ",{\"Key\":\"" + nTable.Rows[i]["FertilizersKey"].ToString() + "\"" +
                                ",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"" +
                                ",\"CategoryName\":\"" + nTable.Rows[i]["CategoryName"].ToString() + "\"" +
                                ",\"CompanyName\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"" +
                                ",\"StatusName\":\"" + nTable.Rows[i]["Common_Name"].ToString() + "\"" +
                                ",\"SeasonName\":\"" + nTable.Rows[i]["Fertilizer_Unit_Name"].ToString() + "\"" +
                                ",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"" +
                            "}";
                    }
                    if (Name == "Pesticides")
                    {
                        cities += ",{\"Key\":\"" + nTable.Rows[i]["PesticideKey"].ToString() + "\"" +
                            ",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"" +
                            ",\"CategoryName\":\"" + nTable.Rows[i]["Crop_Name"].ToString() + "\"" +
                            ",\"CompanyName\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"" +
                            ",\"StatusName\":\"" + nTable.Rows[i]["Common_Name"].ToString() + "\"" +
                            ",\"SeasonName\":\"" + nTable.Rows[i]["Common_Name"].ToString() + "\"" +
                            ",\"Images\":\"" + nTable.Rows[i]["Images"].ToString().Substring(2) + "\"" +
                        "}";
                    }
                }
                cities = cities.Substring(1);
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        [WebMethod]
        public static string LoadCount(string Search)
        {
            string str = HttpContext.Current.Session["nCount"].ToString();
            return str;
        }
    }
}