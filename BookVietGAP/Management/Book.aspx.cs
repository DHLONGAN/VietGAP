using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TNLibrary.Book;
using System.Web.Services;
using System.Globalization;

namespace BookVietGAP
{
    public partial class Book : System.Web.UI.Page
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
        #region [ Book Đất ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarLandUse(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = LandUse_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["Datetime"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadLandUse(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = LandUse_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["LandUseKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Action"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["LandUseKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Action"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadLandUseID(string id)
        {
            int ID = id.ToInt();
            LandUse_Info info = new LandUse_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.LandUseKey + "\"" +
                ",\"txtAction\":\"" + info.Action + "\"" +
                ",\"txtReason\":\"" + info.Reason + "\"" +
                ",\"txtSolution\":\"" + info.Solution + "\"" +//công việc
                ",\"txtNote\":\"" + info.Note + "\"" +//phương pháp
                 ",\"SeedsKey\":\"" + info.SeedKey + "\"" +
                 ",\"IsActive\":\"" + info.IsActive + "\"" +

            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveLandUse(string Key, string day, string txtAction, string txtReason, string txtSolution, string txtNote, string SeedsKey, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                LandUse_Info info = new LandUse_Info(int.Parse(Key));
                info.Datetime = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.SeedKey = int.Parse(SeedsKey);
                info.Action = txtAction;
                info.Reason = txtReason;
                info.Solution = txtSolution;
                info.Note = txtNote;
                info.MemberKey = memberID;
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Quản lý giống ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarSeedsProcess(string Year, string Month)
        {
            if (HttpContext.Current.Session["MemberID"] == null)
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx");
            }
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = SeedProcess_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {

                DateTime date = DateTime.Parse(nTable.Rows[i]["DateOfManufacture"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadSeedsProcess(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = SeedProcess_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedProcessKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedProcessKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadSeedsProcessID(string id)
        {
            int ID = id.ToInt();
            SeedProces_Info info = new SeedProces_Info(ID);
            string cities = "";
            if (id == "")
            {
                cities = "[{\"Key\":\"" + info.SeedProcessKey + "\"" +
                    ",\"Name\":\"" + info.SeedsKey + "\"" +
                    ",\"DateSowing\":\"" + DateTime.Now.ToString("dd/MM/yyyy") + "\"" +
                    ",\"CompanyName\":\"" + "" + "\"" +
                    ",\"txtDateBuy\":\"" + DateTime.Now.ToString("dd/MM/yyyy") + "\"" +
                    ",\"Quantity\":\"" + info.Quantity + "\"" +
                    ",\"txtEndtime\":\"" + DateTime.Now.ToString("dd/MM/yyyy") + "\"" +
                    ",\"txtParcel\":\"1\"" +
                    ",\"txtArea\":\"1\"" +
                    ",\"ddlUnit1\":\"1\"" +
                    ",\"ddlUnit2\":\"1\"" +
                    ",\"IsActive\":\"0\"" +
                    ",\"Total\":\"0\"" +
                "}]";
            }
            else
            {
                cities = "[{\"Key\":\"" + info.SeedProcessKey + "\"" +
                   ",\"Name\":\"" + info.SeedsKey + "\"" +
                   ",\"DateSowing\":\"" + info.DateSowing.ToString("dd/MM/yyyy") + "\"" +
                   ",\"CompanyName\":\"" + info.CompanyName + "\"" +
                   ",\"txtDateBuy\":\"" + info.DateBuy.ToString("dd/MM/yyyy") + "\"" +
                   ",\"Quantity\":\"" + info.Quantity.ToDouble() + "\"" +
                   ",\"txtEndtime\":\"" + info.EndTime.ToString("dd/MM/yyyy") + "\"" +
                   ",\"txtParcel\":\"" + info.Parcel + "\"" +
                   ",\"txtArea\":\"" + info.Area.ToDouble() + "\"" +
                   ",\"ddlUnit1\":\"" + info.AreaUnit + "\"" +
                   ",\"ddlUnit2\":\"" + info.QuantityUnit + "\"" +
                   ",\"IsActive\":\"" + info.IsActive + "\"" +
                   ",\"Total\":\"" + info.Total.ToDouble() + "\"" +
               "}]";
            }
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveSeedsProcess(string Key, string day,string DateSowing,string company, string DateBuy, string Endtime, string SeedsKey, string txtQuantity, string txtParcel, string txtArea, string AreaUnit, string QuantityUnit,string Total, string Active)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            try
            {
                SeedProces_Info info = new SeedProces_Info();
                info.SeedProcessKey = Key.ToInt();
                info.CompanyName = company.ToString();
                info.DateSowing = DateTime.ParseExact(DateSowing, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.DateOfManufacture = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.DateBuy = DateTime.ParseExact(DateBuy, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.EndTime = DateTime.ParseExact(Endtime, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.SeedsKey = SeedsKey;
                info.Quantity = float.Parse(txtQuantity, CultureInfo.InvariantCulture);
                info.Parcel = txtParcel;
                info.Area = float.Parse(txtArea, CultureInfo.InvariantCulture);
                info.MemberKey = memberID;
                info.AreaUnit = int.Parse(AreaUnit);
                info.QuantityUnit = int.Parse(QuantityUnit);
                info.Total = Total.Tofloat();
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool() ;
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Ủ phân hửu cơ ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarCompostingOrganic(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = CompostingOrganic_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["StartDate"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadCompostingOrganic(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = CompostingOrganic_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadCompostingOrganicID(string id)
        {
            int ID = id.ToInt();
            CompostingOrganic_Info info = new CompostingOrganic_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.CompostingKey + "\"" +
                ",\"Name\":\"" + info.FertilizerOrganicKey + "\"" +
                ",\"Quantity\":\"" + info.Quantity.ToDouble() + "\"" +
                ",\"Unit\":\"" + info.UnitKey + "\"" +
                ",\"Method\":\"" + info.Method + "\"" +
                ",\"CompostingDates\":\"" + info.CompostingDates + "\"" +
                ",\"StartDate\":\"" + info.StartDate + "\"" +
                ",\"EndDate\":\"" + info.EndDate + "\"" +
                ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveCompostingOrganic(string Key, string day, string FertilizerOrganicKey, string txtQuantity, string UnitKey, string Method,string CompostingDates, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                CompostingOrganic_Info info = new CompostingOrganic_Info(int.Parse(Key));
                info.StartDate = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.FertilizerOrganicKey = int.Parse(FertilizerOrganicKey);
                info.Quantity = float.Parse(txtQuantity, CultureInfo.InvariantCulture);
                info.UnitKey = int.Parse(UnitKey);
                info.Method = Method;
                info.CompostingDates = CompostingDates.ToInt();
                info.EndDate = (DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture) + new TimeSpan(CompostingDates.ToInt(), 0, 0, 0));
                info.MemberKey = memberID;
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Mua Phân Bón ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarFertilizer_Buy(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Fertilizer_Buy_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["DatetimeBuy"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadFertilizer_Buy(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Fertilizer_Buy_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizerBuyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizerBuyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadFertilizer_BuyID(string id)
        {
            int ID = id.ToInt();
            Fertilizer_Buy_Info info = new Fertilizer_Buy_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.FertilizerBuyKey + "\"" +
                ",\"Name\":\"" + info.FertilizerKey + "\"" +
                ",\"Quantity\":\"" + info.Quantity.ToDouble() + "\"" +
                ",\"Unit\":\"" + info.UnitKey + "\"" +
                ",\"Price\":\"" + info.Price + "\"" +
                ",\"Address\":\"" + info.Address + "\"" +
                ",\"SeedsKey\":\"" + info.SeedsKey + "\"" +
                ",\"Total\":\"" + info.Total.ToDouble() + "\"" +
                ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveFertilizer_Buy(string Key, string day, string FertilizerKey, string txtQuantity, string txtAddress, string SeedsKey, string UnitKey, string Total, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                Fertilizer_Buy_Info info = new Fertilizer_Buy_Info(int.Parse(Key));
                info.DatetimeBuy = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.FertilizerKey = int.Parse(FertilizerKey);
                info.Quantity = float.Parse(txtQuantity, CultureInfo.InvariantCulture);
                info.CompanyKey = 1;
                info.MemberKey = memberID;
                //info.Price = txtPrice.Text;
                info.Address = txtAddress;
                info.SeedsKey = int.Parse(SeedsKey);
                info.UnitKey = int.Parse(UnitKey);
                info.Total = Total.Tofloat();
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Sử dụng Phân Bón ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarFertilizer_Use(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Fertilizer_Use_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["DateTimeUse"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadFertilizer_Use(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Fertilizer_Use_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizerUseKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizerUseKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadFertilizer_UseID(string id)
        {
            int ID = id.ToInt();
            Fertilizer_Use_Info info = new Fertilizer_Use_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.FertilizerUseKey + "\"" +
                ",\"Name\":\"" + info.FertilizerKey + "\"" +
                ",\"txtArea\":\"" + info.Area + "\"" +
                ",\"txtParcel\":\"" + info.Parcel + "\"" +
                ",\"txtHowtouse\":\"" + info.Howtouse + "\"" +
                ",\"DDLEquipment\":\"" + info.CooperativeKey + "\"" +
                 ",\"SeedsKey\":\"" + info.SeedKey + "\"" +
                ",\"txtFormulaUsed\":\"" + info.FormulaUsed.ToDouble() + "\"" +
                ",\"txtQuantity\":\"" + info.Quantity + "\"" +
                ",\"txtQuarantinePeriod\":\"" + info.QuarantinePeriod + "\"" +
                ",\"ddlUnit\":\"" + info.UnitKey + "\"" +
                ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveFertilizer_Use(string Key, string day, string FertilizerKey, string DDLEquipment, string txtQuantity, string SeedsKey, string UnitKey, string txtFormulaUsed, string txtHowtouse, string txtParcel, string txtQuarantinePeriod, string txtArea, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                Fertilizer_Use_Info info = new Fertilizer_Use_Info(int.Parse(Key));
                info.DateTimeUse = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.FertilizerKey = int.Parse(FertilizerKey);
                info.SeedKey = int.Parse(SeedsKey);
                info.CooperativeKey = int.Parse(DDLEquipment);
                info.Quantity = txtQuantity;
                info.FormulaUsed = float.Parse(txtFormulaUsed, CultureInfo.InvariantCulture);
                info.Howtouse = txtHowtouse;
                info.Parcel = txtParcel;
                info.MemberKey = memberID;
                info.QuarantinePeriod = txtQuarantinePeriod;
                info.Area = txtArea;
                info.UnitKey = int.Parse(UnitKey);
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Mua Thuốc BVTV ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarPesticideBuy(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Pesticide_Buy_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["DatetimeBuy"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadPesticideBuy(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Pesticide_Buy_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["PesticideBuyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Trade_Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["PesticideBuyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Trade_Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadPesticideBuyID(string id)
        {
            int ID = id.ToInt();
            Pesticide_Buy_Info info = new Pesticide_Buy_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.PesticideBuyKey + "\"" +
                ",\"Name\":\"" + info.PesticideKey + "\"" +
                ",\"Quantity\":\"" + info.Quantity.ToDouble() + "\"" +
                ",\"Unit\":\"" + info.UnitKey + "\"" +
                ",\"Address\":\"" + info.Address + "\"" +
                ",\"SeedsKey\":\"" + info.SeedsKey + "\"" +
                ",\"Total\":\"" + info.Total.ToDouble() + "\"" +
                ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SavePesticideBuy(string Key, string day, string PesticideKey, string Quantity, string Address, string SeedsKey, string UnitKey,string Total, string Active)
        {
            try
            {
                Pesticide_Buy_Info info = new Pesticide_Buy_Info();
                info.PesticideBuyKey = Key.ToInt();
                info.DatetimeBuy = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.PesticideKey = PesticideKey.ToInt();
                info.Quantity = Quantity.Tofloat();
                //info.Price = Price;
                //info.CompanyKey = CompanyKey.ToInt();
                info.Address = Address;
                info.MemberKey = HttpContext.Current.Session["MemberID"].ToInt();
                info.CooperativeKey = 0;
                info.SeedsKey = SeedsKey.ToInt();
                info.UnitKey = UnitKey.ToInt();
                info.Total = Total.Tofloat();
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Sử dụng Thuốc BVTV ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarPesticide_Use(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Pesticide_Use_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["DateTimeUse"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadPesticide_Use(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Pesticide_Use_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["PesticideUseKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Trade_Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["PesticideUseKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Trade_Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadPesticide_UseID(string id)
        {
            int ID = id.ToInt();
            Pesticide_Use_Info info = new Pesticide_Use_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.PesticideUseKey + "\"" +
                ",\"Name\":\"" + info.PesticideKey + "\"" +
                ",\"txtArea\":\"" + info.Area + "\"" +
                ",\"txtParcel\":\"" + info.PestName + "\"" +//công việc
                ",\"txtHowtouse\":\"" + info.Solution + "\"" +//phương pháp
                ",\"DDLEquipment\":\"" + info.EquipmentKey + "\"" +
                 ",\"SeedsKey\":\"" + info.SeedKey + "\"" +
                ",\"txtFormulaUsed\":\"" + info.Dose.ToDouble() + "\"" +//liều lượng
                ",\"txtQuantity\":\"" + info.Dosage + "\"" +//tổng lượng/lít
                ",\"txtQuarantinePeriod\":\"" + info.QuarantinePeriod + "\"" +
                ",\"ddlUnit\":\"" + info.UnitKey + "\"" +
                ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SavePesticide_Use(string Key, string day, string FertilizerKey, string DDLEquipment, string txtQuantity, string SeedsKey, string UnitKey, string txtFormulaUsed, string txtHowtouse, string txtParcel, string txtQuarantinePeriod, string txtArea, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                Pesticide_Use_Info info = new Pesticide_Use_Info(int.Parse(Key));
                info.DateTimeUse = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.PesticideKey = int.Parse(FertilizerKey);
                info.SeedKey = int.Parse(SeedsKey);
                info.EquipmentKey = int.Parse(DDLEquipment);
                info.Dosage = txtQuantity;
                info.Dose = float.Parse(txtFormulaUsed, CultureInfo.InvariantCulture);
                info.Solution = txtHowtouse;
                info.PestName = txtParcel;
                info.MemberKey = memberID;
                info.QuarantinePeriod = txtQuarantinePeriod;
                info.Area = txtArea;
                info.UnitKey = int.Parse(UnitKey);
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Kiem tra xuất bán ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarHarvestedForSale(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = HarvestedForSale_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["Datetime"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadHarvestedForSale(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = HarvestedForSale_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadHarvestedForSaleID(string id)
        {
            int ID = id.ToInt();
            HarvestedForSale_Info info = new HarvestedForSale_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.HarvestedForSaleKey + "\"" +
                ",\"txtCode\":\"" + info.Code + "\"" +
                ",\"txtQuantityHarvested\":\"" + info.QuantityHarvested.ToDouble() + "\"" +
                ",\"txtQuantitySale\":\"" + info.QuantitySale.ToDouble() + "\"" +//công việc
                ",\"txtWhereToBuy\":\"" + info.WhereToBuy + "\"" +//công việc
                ",\"txtUnitKey\":\"" + info.UnitKey + "\"" +//công việc
                 ",\"SeedsKey\":\"" + info.SeedsKey + "\"" +
                 ",\"Total\":\"" + info.Total.ToDouble() + "\"" +
                 ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveHarvestedForSale(string Key, string day, string SeedsKey, string txtCode, string txtQuantityHarvested, string txtQuantitySale, string txtWhereToBuy, string txtUnitKey,string Total, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                HarvestedForSale_Info info = new HarvestedForSale_Info(int.Parse(Key));
                info.Datetime = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.Code = txtCode;
                info.QuantityHarvested = float.Parse(txtQuantityHarvested, CultureInfo.InvariantCulture);
                info.QuantitySale = float.Parse(txtQuantitySale, CultureInfo.InvariantCulture);
                info.SeedsKey = SeedsKey.ToInt();
                info.MemberKey = memberID;
                info.WhereToBuy = txtWhereToBuy;
                info.UnitKey = txtUnitKey.ToInt();
                info.Total = Total.Tofloat();
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();

                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Kiểm tra thiết bị ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarCheckEquipment(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = CheckEquipment_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["Datetime"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadCheckEquipment(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = CheckEquipment_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["CheckEquipmentKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["EquipmentName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["CheckEquipmentKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["EquipmentName"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadCheckEquipmentID(string id)
        {
            int ID = id.ToInt();
            CheckEquipment_Info info = new CheckEquipment_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.CheckEquipmentKey + "\"" +
                ",\"txtEquipmentKey\":\"" + info.EquipmentKey + "\"" +
                ",\"txtAction\":\"" + info.Action + "\"" +
                ",\"txtInfo\":\"" + info.Info + "\"" +//công việc
                 ",\"SeedsKey\":\"" + info.SeedsKey + "\"" +
                 ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveCheckEquipment(string Key, string day, string txtEquipmentKey, string txtAction, string txtInfo, string SeedsKey, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                CheckEquipment_Info info = new CheckEquipment_Info(int.Parse(Key));
                info.Datetime = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.EquipmentKey = txtEquipmentKey.ToInt();
                info.Action = txtAction;
                info.Info = txtInfo;
                info.SeedsKey = SeedsKey.ToInt();
                info.MemberKey = memberID;
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book chất thải ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarHandlingPackaging(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = HandlingPackaging_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["Datetime"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadHandlingPackaging(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = HandlingPackaging_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["HandlingPackagingKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["HandlingPackagingKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadHandlingPackagingID(string id)
        {
            int ID = id.ToInt();
            HandlingPackaging_Info info = new HandlingPackaging_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.HandlingPackagingKey + "\"" +
                ",\"txtType\":\"" + info.Type + "\"" +
                ",\"txtPlace\":\"" + info.Place + "\"" +
                ",\"txtTreatment\":\"" + info.Treatment + "\"" +//công việc
                ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveHandlingPackaging(string Key, string day, string txtType, string txtPlace, string txtTreatment, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                HandlingPackaging_Info info = new HandlingPackaging_Info(int.Parse(Key));
                info.Datetime = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.Type = txtType;
                info.Place = txtPlace;
                info.Treatment = txtTreatment;
                info.MemberKey = memberID;
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();

                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Kiểm ke tồn kho ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarInventory(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Inventory_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["Datetime"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadInventory(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = Inventory_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadInventoryID(string id)
        {
            int ID = id.ToInt();
            Inventory_Info info = new Inventory_Info(ID);
            string cities = "";
            if (id == "")
            {
                cities = "[{\"Key\":\"" + info.InventoryKey + "\"" +
                    ",\"Type\":\"" + "0" + "\"" +
                    ",\"FertilizersPesticides\":\"" + "0" + "\"" +
                    ",\"Quantity\":\"" + "" + "\"" +
                    ",\"Unit\":\"" + "0" + "\"" +
                    ",\"ExpireDate\":\"" + DateTime.Now.ToString("dd/MM/yyyy") + "\"" +
                    ",\"IsActive\":\"" + "0" + "\"" +
                "}]";
            }
            else
            {
                cities = "[{\"Key\":\"" + info.InventoryKey + "\"" +
                    ",\"Type\":\"" + info.Type + "\"" +
                    ",\"FertilizersPesticides\":\"" + info.FertilizersPesticidesKey + "\"" +
                    ",\"Quantity\":\"" + info.Quantity.ToDouble() + "\"" +
                    ",\"Unit\":\"" + info.UnitKey + "\"" +
                    ",\"ExpireDate\":\"" + info.ExpireDate.ToString("dd/MM/yyyy") + "\"" +
                    ",\"IsActive\":\"" + info.IsActive + "\"" +
                "}]";
            }
            return cities;
        }
        #endregion
        #region [ SaveInfo ]

        [WebMethod]
        public static bool SaveInventory(string Key, string day, string Type, string FertilizersPesticides, string txtQuantity, string UnitKey, string txtExpireDate, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                Inventory_Info info = new Inventory_Info(int.Parse(Key));
                info.Datetime = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.Type = int.Parse(Type);
                info.FertilizersPesticidesKey = int.Parse(FertilizersPesticides);
                info.Quantity = float.Parse(txtQuantity, CultureInfo.InvariantCulture);
                info.UnitKey = int.Parse(UnitKey);
                info.ExpireDate = DateTime.ParseExact(txtExpireDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.MemberKey = memberID;
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();
                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion
        #region [ Book Kiem tra đánh giá ]
        #region [ Load Calendar ]
        [WebMethod]
        public static string LoadCalendarCheckAssessment(string Year, string Month)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = CheckAssessment_Data.GetListByMember(memberID, Year, Month);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                DateTime date = DateTime.Parse(nTable.Rows[i]["Datetime"].ToString());
                string day = date.ToString("yyyy-MM-dd");
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"},";
                }
                else
                {
                    cities += '"' + day + "\":{\"number\":\"" + nTable.Rows[i]["Count"].ToString() + "\"}";
                }
            }
            cities = "{" + cities + "}";
            return cities;
        }
        #endregion
        #region [ load Danh sách Công việc theo ngày ]
        [WebMethod]
        public static string LoadCheckAssessment(string day)
        {
            DateTime dt = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = CheckAssessment_Data.GetListByMemberDay(memberID, dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"IsActive\":\"" + nTable.Rows[i]["IsActive"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Chi tiết công việc theo ID ]
        [WebMethod]
        public static string LoadCheckAssessmentID(string id)
        {
            int ID = id.ToInt();
            CheckAssessment_Info info = new CheckAssessment_Info(ID);
            string cities = "";
            cities = "[{\"Key\":\"" + info.CheckAssessmentKey + "\"" +
                ",\"SeedsKey\":\"" + info.SeedsKey + "\"" +
                ",\"DescribesError\":\"" + info.DescribesError + "\"" +
                ",\"Method\":\"" + info.Method + "\"" +//công việc
                 ",\"IsActive\":\"" + info.IsActive + "\"" +
            "}]";
            return cities;
        }
        #endregion
        #region [ SaveInfo ]
        [WebMethod]
        public static bool SaveCheckAssessment(string Key, string day, string SeedsKey, string DescribesError, string Method, string Active)
        {
            try
            {
                int memberID = HttpContext.Current.Session["MemberID"].ToInt();
                CheckAssessment_Info info = new CheckAssessment_Info(int.Parse(Key));
                info.Datetime = DateTime.ParseExact(day, "d/M/yyyy", CultureInfo.InvariantCulture);
                info.SeedsKey = SeedsKey.ToInt();
                info.DescribesError = DescribesError;
                info.Method = Method;                
                info.MemberKey = memberID;
                info.CreatedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.CreatedDateTime = DateTime.Now;
                info.ModifiedBy = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                info.ModifiedDateTime = DateTime.Now;
                info.IsActive = Active.ToBool();
                info.IsSync = true;
                info.Save();

                return true;
            }
            catch { return false; }
        }
        #endregion
        #endregion

        #region [ Load DropdownList ]
        #region [ Load Danh sách giống ]
        [WebMethod]
        public static string LoadSeeds()
        {
            DataTable nTable = Seed_Info.GetList(HttpContext.Current.Session["CooperativeKey"].ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách giống đã trồng chưa thu hoạch ]
        [WebMethod]
        public static string LoadSeedProcessDay(string dayclick)
        {
            DateTime dt = DateTime.ParseExact(dayclick, "d/M/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = Seed_Info.GetListSeedProcess(HttpContext.Current.Session["MemberID"].ToInt(), dt);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                string tday = Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "-" + tday + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["SeedsKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["SeedsName"].ToString() + "-" + tday + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách NSX Giống ]
        [WebMethod]
        public static string LoadSeedsCompanie()
        {
            DataTable nTable = Seeds_Companie_Info.GetList();
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["CompanyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["CompanyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách Phân hửu cơ ]
        [WebMethod]
        public static string LoadFertilizerOrganic()
        {
            DataTable nTable = Pesticide_Info.GetListFer_CoopPhanHuuCo(HttpContext.Current.Session["CooperativeKey"].ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizersKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizersKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách thuốc ]
        [WebMethod]
        public static string LoadPesticide()
        {
            DataTable nTable = Pesticide_Info.GetListPes_Coop(HttpContext.Current.Session["CooperativeKey"].ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["PesticideKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Trade_Name"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["PesticideKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Trade_Name"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách Phân ]
        [WebMethod]
        public static string LoadFertilizers()
        {
            DataTable nTable = Pesticide_Info.GetListFer_Coop(HttpContext.Current.Session["CooperativeKey"].ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizersKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["FertilizersKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["TradeName"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách NSX Phân thuốc ]
        [WebMethod]
        public static string LoadCompanie()
        {
            DataTable nTable = Companie_Info.GetList();
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["CompanyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["CompanyKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["CompanyName"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
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
        #region [ Load Danh sách trang thiết bị ]
        [WebMethod]
        public static string Equipment()
        {
            DataTable nTable = CheckEquipment_Data.GetList();
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["EquipmentKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["EquipmentName"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["EquipmentKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["EquipmentName"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
        #region [ Load Danh sách loại hàng(phân/thuốc) ]
        [WebMethod]
        public static string LoadInventory_Type()
        {
            DataTable nTable = Inventory_Type_Data.GetList();
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["InventoryTypeKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["InventoryTypeKey"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        [WebMethod]
        public static string LoadFertilizersORPesticides(string Type)
        {
            DataTable nTable = Inventory_Data.GetListFP(Type.ToInt(), HttpContext.Current.Session["CooperativeKey"].ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion

        
        #endregion

        #region [ Load Danh sách  NotCheck ]
        [WebMethod]
        public static string LoadListNotCheckAll(string num)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = All_Data.GetListNotActiveByMemberAllDay(memberID, DateTime.Now, num.ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                string tday = Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                string color = "black";
                DateTime DayNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if ((DateTime)(nTable.Rows[i]["Datetime"]) < DayNow) color = "Red";
                if ((DateTime)(nTable.Rows[i]["Datetime"]) > DayNow) color = "black";
                if ((DateTime)(nTable.Rows[i]["Datetime"]) == DayNow) color = "blue";
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Type"].ToString() + "_" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + tday + " - " + nTable.Rows[i]["Name"].ToString() + "\",\"Type\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"Color\":\"" + color + "\",\"Bookname\":\"" + nTable.Rows[i]["NameBook"].ToString() + "\",\"Datetime\":\"" + tday + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Type"].ToString() + "_" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + tday + " - " + nTable.Rows[i]["Name"].ToString() + "\",\"Type\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"Color\":\"" + color + "\",\"Bookname\":\"" + nTable.Rows[i]["NameBook"].ToString() + "\",\"Datetime\":\"" + tday + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        [WebMethod]
        public static string LoadListNotCheckday(string num)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = All_Data.GetListNotActiveByMemberDay(memberID, DateTime.Now, num.ToInt());
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                string tday = Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                string color = "black";
                DateTime DayNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if ((DateTime)(nTable.Rows[i]["Datetime"]) < DayNow) color = "Red";
                if ((DateTime)(nTable.Rows[i]["Datetime"]) > DayNow) color = "black";
                if ((DateTime)(nTable.Rows[i]["Datetime"]) == DayNow) color = "blue";
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Type"].ToString() + "_" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + tday + " - " + nTable.Rows[i]["Name"].ToString() + "\",\"Type\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"Color\":\"" + color + "\",\"Bookname\":\"" + nTable.Rows[i]["NameBook"].ToString() + "\",\"Datetime\":\"" + tday + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Type"].ToString() + "_" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + tday + " - " + nTable.Rows[i]["Name"].ToString() + "\",\"Type\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"Color\":\"" + color + "\",\"Bookname\":\"" + nTable.Rows[i]["NameBook"].ToString() + "\",\"Datetime\":\"" + tday + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        [WebMethod]
        public static string LoadListNotChecknow(string num)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = All_Data.GetListNotActiveByMemberNow(memberID, DateTime.Now);
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                string tday = Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                string color = "black";
                DateTime DayNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if ((DateTime)(nTable.Rows[i]["Datetime"]) < DayNow) color = "Red";
                if ((DateTime)(nTable.Rows[i]["Datetime"]) > DayNow) color = "black";
                if ((DateTime)(nTable.Rows[i]["Datetime"]) == DayNow) color = "blue";
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Type"].ToString() + "_" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"Type\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"Color\":\"" + color + "\",\"Bookname\":\"" + nTable.Rows[i]["NameBook"].ToString() + "\",\"Datetime\":\"" + tday + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Type"].ToString() + "_" + nTable.Rows[i]["KeyID"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Name"].ToString() + "\",\"Type\":\"" + nTable.Rows[i]["Type"].ToString() + "\",\"Color\":\"" + color + "\",\"Bookname\":\"" + nTable.Rows[i]["NameBook"].ToString() + "\",\"Datetime\":\"" + tday + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        [WebMethod]
        public static string LoadCountsNotCheckAll(string num)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = All_Data.GetCountsNotActiveByMemberAllDay(memberID, DateTime.Now, num.ToInt());
            string cities = "";
            cities = nTable.Rows[0]["Counts"].ToString();
            return cities;
        }
        [WebMethod]
        public static string LoadCountsNotCheckday(string num)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = All_Data.GetCountsNotActiveByMemberDay(memberID, DateTime.Now, num.ToInt());
            string cities = "";
            cities = nTable.Rows[0]["Counts"].ToInt()+"";
            return cities;
        }
        [WebMethod]
        public static string LoadCountsNotChecknow()
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            DataTable nTable = All_Data.GetCountsNotActiveByMemberNow(memberID, DateTime.Now);
            string cities = "";
            cities = nTable.Rows[0]["Counts"].ToString();
            return cities;
        }
        #endregion
        #region [ Save Danh sách  NotCheck ]
        [WebMethod]
        public static bool SaveListIsActive(string Key, string IsActive)
        {
            try
            {
                string[] _Module = Key.Split('_');
                string Type = _Module[0];
                int KeyID = _Module[1].ToInt();
                All_Data.UpdateListIsActive(KeyID, Type, IsActive.ToInt());
                return true;
            }
            catch { return false; }
        }

        #endregion
        #region [ Xóa ID ]
        [WebMethod]
        public static bool DelbyKey(string Key)
        {
            
            try
            {
                bool result;
                string[] _Module = Key.Split('|');
                string Type = _Module[0];
                int KeyID = _Module[1].ToInt();
                if (KeyID == 0)
                {
                    result = false;
                }
                else
                {
                    All_Data.DelbyKeyAll(KeyID, Type);
                    result = true;
                }

                return result;
            }
            catch { return false; }
        }

        #endregion
        #region [ load Danh sách Công việc Sẽ làm ]
        [WebMethod]
        public static string LoadNewMenu(string Menu, string Type, string num)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            string cities = "";
            DataTable nTable;
            if (Menu == "selam")
            {
                nTable = All_Data.GetListWilldo(Type, memberID, DateTime.Now, num.ToInt());
                for (int i = 0; i < nTable.Rows.Count; i++)
                {
                    string tday = Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                    cities += ",{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\"" +
                                ",\"Name\":\"" + tday + "-" + nTable.Rows[i]["Name"].ToString() + "\"" +
                                ",\"DayGet\":\"" + tday + "\"" +
                                ",\"IsActive\":\"" + "False" + "\"" +
                                "}";
                }
            }
            if (Menu == "chualam")
            {
                nTable = All_Data.GetListWilldoFuture(Type, memberID, DateTime.Now, num.ToInt());
                for (int i = 0; i < nTable.Rows.Count; i++)
                {
                    string tday = Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                    cities += ",{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\"" +
                                ",\"Name\":\"" + tday + "-" + nTable.Rows[i]["Name"].ToString() + "\"" +
                                ",\"DayGet\":\"" + tday + "\"" +
                                ",\"IsActive\":\"" + "False" + "\"" +
                                "}";
                }
            }
            if (Menu == "moi")
            {
                nTable = All_Data.GetListWilldoNow(Type, memberID, DateTime.Now, num.ToInt());
                for (int i = 0; i < nTable.Rows.Count; i++)
                {
                    string tday = Utils.DateTostring((DateTime)(nTable.Rows[i]["Datetime"]));
                    cities += ",{\"Key\":\"" + nTable.Rows[i]["KeyID"].ToString() + "\"" +
                                ",\"Name\":\"" + tday + "-" + nTable.Rows[i]["Name"].ToString() + "\"" +
                                ",\"DayGet\":\"" + tday + "\"" +
                                ",\"IsActive\":\"" + "False" + "\"" +
                                "}";
                }
            }
            if (cities != "") { cities = cities.Substring(1); }
            cities = "[" + cities + "]";
            return cities;
        }
        #endregion
    }
}