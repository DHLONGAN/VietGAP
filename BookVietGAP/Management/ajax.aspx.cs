using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Globalization;
using TNLibrary.Book;
using TNLibrary.SYS.Users;
using TNLibrary.SYS;

namespace BookVietGAP
{
    public partial class ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["abc"].ToInt();
        }
        #region [ Load danh sách giống của quy trình ]
        [WebMethod]
        public static string LoadSeedsProcessPlant(string ddlCooperative_Key, string Type, string SeedsKeyProcess)
        {
            
            DataTable nTable = All_Data.GetListSeedProcess(Type.ToInt(), HttpContext.Current.Session["CooperativeKey"].ToInt(), HttpContext.Current.Session["MemberID"].ToInt(), DateTime.Now);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                string tday = "";
                if (nTable.Rows[i]["Datetime"].ToString() != "0")
                {
                    tday = "-"+Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                }
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + tday + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + tday + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách quy trình chi tiết ]
        [WebMethod]
        public static string LoadProcessDetail(string ddlCooperative_Key, string Type, string SeedsKeyProcess)
        {

            int CooperativeKey = ddlCooperative_Key.ToInt();
            int SeedsKey = SeedsKeyProcess.ToInt();
            if (CooperativeKey != 0)
            {
                CooperativeKey = HttpContext.Current.Session["CooperativeKey"].ToInt();
            }
            if (Type == "2")
            {
                SeedProces_Info info = new SeedProces_Info(SeedsKeyProcess.ToInt());
                SeedsKey = info.SeedsKey.ToInt();
            }
            DataTable nTable = All_Data.LoadProcessDetail(CooperativeKey, SeedsKey, Type.ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["ProcessPlantKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["ProcessPlantName"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["ProcessPlantKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["ProcessPlantName"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ SaveInfoExportProcess ]
        [WebMethod]
        public static bool SaveExportProcess(string Key, string Type, string SeedKey, string day, string Area, string AreaUnit, string Quantity, string QuantityUnit)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            try
            {
                All_Data.SaveExportProcessSeed(Key.ToInt(),Type.ToInt(), SeedKey.ToInt(), DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture), memberID, Area.ToInt(),AreaUnit.ToInt(), Quantity.ToInt(),QuantityUnit.ToInt());
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region [ Change Password ]
        [WebMethod]
        public static bool SaveChangePass(string Zold, string Znew)
        {
            string UserName = HttpContext.Current.Session["UserName"].ToString();
            try
            {
                string[] nResult = Users_Data.CheckUser(UserName, Zold);
                if (nResult[0] == "ERR")
                {
                    return false;
                }
                else
                {
                    Users_Data.UpdatePass(nResult[1], MyCryptography.HashPass(Znew));
                    return true;
                }
                
            }
            catch { return false; }
        }
        #endregion
        #region [ Check Password ]
        [WebMethod]
        public static bool CheckPassword(string Zold)
        {
            string UserName = HttpContext.Current.Session["UserName"].ToString();
            try
            {
                string[] nResult = Users_Data.CheckUser(UserName, Zold);
                if (nResult[0] == "ERR")
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            catch { return false; }
        }
        #endregion
        #region [ Check Day ]
        [WebMethod]
        public static string CheckDay(string day)
        {
            DateTime DayNow = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            string result = "0";
            try
            {
                if (DayNow > DateTime.Now )
                {
                    result = "1";
                }
                if (DayNow < (DateTime.Now - new TimeSpan(7, 0, 0, 0)))
                {
                    result = "2";
                }
                else
                {
                    result = "3";
                }

            }
            catch {  }
            return result;
        }
        #endregion
        #region [ Check Session ]
        [WebMethod]
        public static bool CheckSession()
        {
            try
            {
                if (HttpContext.Current.Session["MemberID"] == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch { return false; }
        }
        #endregion

        #region [Danh sách  Giống cây trồng ]
        [WebMethod]
        public static string ListSeeds()
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            var info = SeedProcess_Data.GetList(memberID, DateTime.Now);
            string result = "";
            for (int i = 0; i < info.Rows.Count; i++)
            {
                DateTime StartTime = Convert.ToDateTime(info.Rows[i]["DateOfManufacture"]);
                DateTime EndTime = new DateTime();
                if (info.Rows[i]["EndTime"].ToString() != "")
                {
                    EndTime = Convert.ToDateTime(info.Rows[i]["EndTime"]);
                }
                //if (EndTime > DateTime.Now)
                {
                    result += @"<table data-toggle='table' data-url='data1.json' data-cache='false' data-height='299' style='margin-bottom: -10px;margin-top: -10px;'>
                        <thead>
                            <tr>
                                <th class='col-sm-2'><img src='" + info.Rows[i]["Images"].ToString().Replace("..", "http://admin.vietgap.info") + "' style='width:80px;height:80px'/></th>" +
                                                                    "<th class='col-sm-10'><font color='rgb(87, 7, 65)'>" + info.Rows[i]["SeedsName"] + "</font><br />Ngày trồng &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: <b>" + StartTime.ToString("dd-MM-yyyy") + "</b><br />Ngày thu hoạch: <b>" + EndTime.ToString("dd-MM-yyyy") + "</b></th>" +
                                @"</tr>
                        </thead>
                        </table><hr/>";
                }
            }


            return result;
        }
        #endregion
        #region [ Load Danh sách đơn vị ]
        [WebMethod]
        public static string LoadUnit()
        {
            DataTable nTable = Unit_Info.GetList();
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["ID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["ID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion

        [WebMethod]
        public static string SeedProcess_Save(string txtDateBuy, string txtDateOfManufacture, string txtEndTime, string txtParcel, string txtArea, float txtQuantity, float txtAreaUnit, int DDLSeedsName, int txtKey, string DDLAreaUnit, string DDLQuantityUnit)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            SeedProces_Info info = new SeedProces_Info(txtKey);
            info.DateOfManufacture = DateTime.ParseExact(txtDateBuy, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.DateBuy = DateTime.ParseExact(txtDateOfManufacture, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.EndTime = DateTime.ParseExact(txtEndTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.SeedsKey = DDLSeedsName.ToString();
            info.Quantity = (txtQuantity);
            info.Parcel = txtParcel;
            info.Area = float.Parse(txtArea);
            info.MemberKey = memberID;
            info.AreaUnit = int.Parse(DDLAreaUnit);
            info.QuantityUnit = int.Parse(DDLQuantityUnit);
            if (txtKey == 0)
            {
                return info.Save();
            }
            else
            {
                return txtKey.ToString();
            }
        }
        [WebMethod]
        public static string SeedProcess_Add(string txtDateBuy, string txtDateOfManufacture, string txtEndTime, string txtParcel, string txtArea, float txtQuantity, float txtAreaUnit, int DDLSeedsName, int txtKey, string DDLAreaUnit, string DDLQuantityUnit)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            SeedProces_Info info = new SeedProces_Info(0);
            info.DateOfManufacture = DateTime.ParseExact(txtDateBuy, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.DateBuy = DateTime.ParseExact(txtDateOfManufacture, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.EndTime = DateTime.ParseExact(txtEndTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.SeedsKey = DDLSeedsName.ToString();
            info.Quantity = (txtQuantity);
            info.Parcel = txtParcel;
            info.Area = float.Parse(txtArea);
            info.MemberKey = memberID;
            info.AreaUnit = int.Parse(DDLAreaUnit);
            info.QuantityUnit = int.Parse(DDLQuantityUnit);
            info.Save();
            return "";
        }

        [WebMethod]
        public static string Listwork(string time)
        {
            string result = "";
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            var infos = SeedProcess_Data.GetList(time, memberID);
            if (infos.Rows.Count > 0)
            {
                for (int i = 0; i < infos.Rows.Count; i++)
                {
                    result += "$" + infos.Rows[i]["SeedProcessKey"].ToString() + "-" + infos.Rows[i]["SeedsName"].ToString();
                }
            }
            return result;
        }

        [WebMethod]
        public static string ListDate(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            var infos = SeedProcess_Data.GetListDate(memberID, Month.ToInt(), Year.ToInt());
            string cities = "";
            for (int i = 0; i < infos.Rows.Count; i++)
            {

                DateTime date = DateTime.Parse(infos.Rows[i]["DateOfManufacture"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < infos.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + infos.Rows[i]["Count"].ToInt() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + infos.Rows[i]["Count"].ToInt() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
    }
}