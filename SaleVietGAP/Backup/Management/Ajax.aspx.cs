using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using TNLibrary.Fields;
using TNLibrary.SYS.Users;

namespace Management
{
    public partial class Ajax : System.Web.UI.Page
    {
        int Ckey = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = "0";
            if (this.Request["id"] != null)
            {
                key = this.Request["id"].ToString();
            }
            if (!IsPostBack)
            {
                if (int.TryParse(key, out Ckey))
                {
                    Ckey = Ckey;
                }
            }
        }
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
            else
            {
                cities = "";
            }
            return cities;
        }
        #endregion
        #region [ Load List ]
        
        [WebMethod]
        public static List<string[]> GetLoadSeedCooperative()
        {
            DataTable nTable = Seed_Cooperative_Info.GetListSeed();
            List<string[]> li_str = ConvertTableToSting(nTable);
            return li_str;
        }
        [WebMethod]
        public static List<string[]> GetLoadSeedCooperativeNew()
        {
            DataTable nTable = Seed_Cooperative_Info.GetListSeedNew();
            List<string[]> li_str = ConvertTableToSting(nTable);
            return li_str;
        }
        [WebMethod]
        public static List<string[]> GetLoadSeedCooperativePrice()
        {
            DataTable nTable = Seed_Cooperative_Info.GetListSeedPrice();
            List<string[]> li_str = ConvertTableToSting(nTable);
            return li_str;
        }
        #endregion

        #region [ Load List TypeKey ]
        [WebMethod]
        public static List<string[]> GetLoadSeedCooperativeTypeKey(int TypeKey)
        {
            //int test = Convert.ToInt32(TypeKey);
            DataTable nTable = Seed_Cooperative_Info.GetListSeed(TypeKey);
            List<string[]> li_str = ConvertTableToSting(nTable);
            return li_str;
        }
        [WebMethod]
        public static List<string[]> GetLoadSeedCooperativeNewTypeKey(int TypeKey)
        {
            DataTable nTable = Seed_Cooperative_Info.GetListSeedNew(TypeKey);
            List<string[]> li_str = ConvertTableToSting(nTable);
            return li_str;
        }
        [WebMethod]
        public static List<string[]> GetLoadSeedCooperativePriceTypeKey(int TypeKey)
        {
            DataTable nTable = Seed_Cooperative_Info.GetListSeedPrice(TypeKey);
            List<string[]> li_str = ConvertTableToSting(nTable);
            return li_str;
        }
        #endregion
        static List<string[]> ConvertTableToSting(DataTable table)
        {
            return table.Rows.Cast<DataRow>()
               .Select(row => table.Columns.Cast<DataColumn>().Select(col => Convert.ToString(row[col])).ToArray()).ToList();
        }

        [WebMethod]
        public static string ListCooperative()
        {

            DataTable nTable = Cooperative_Data.GetList();
            string cities = "";
            for (int i = 0; i < nTable.Rows.Count; i++)
            {
                if (i + 1 < nTable.Rows.Count)
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Cooperative_Key"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Cooperative_Name"].ToString() + "\"},";
                }
                else
                {
                    cities += "{\"Key\":\"" + nTable.Rows[i]["Cooperative_Key"].ToString() + "\",\"Name\":\"" + nTable.Rows[i]["Cooperative_Name"].ToString() + "\"}";
                }
            }
            cities = "[" + cities + "]";
            return cities;
        }
        [WebMethod]
        public static string dk(string username, string password, string repassword, string email , string name , string idmember, string Cooperative, string phone, string address)
        {
            string result = "Đăng ký thành công! Tuy nhiên bạn cần xác chờ xác thực từ chủ nhiệm HTX mới có thể sử dụng tài khoản này!";
            if(password.Length<6)
            {
                result = "Mật khẩu phải từ 6 ký tự trở lên";
            }
            else if (password != repassword)
            {
                result = "Mật khẩu nhập lại không khớp";
            }
            else if (email.IndexOf("@") <= -1)
            {
                result = "Email không đúng định dạng";
            }
            else if (phone.Length <10 || phone.ToInt() < 1)
            {
                result = "Số điện thoại không đúng định dạng";
            }
            else
            {
                Member_Info meminfo = new Member_Info(name,Cooperative.ToInt());
                if (meminfo.Key != 0)
                {
                    return "Xã viên đã tồn tại!";
                }
                else
                {
                    meminfo.MemID = idmember;
                    meminfo.Cooperative_Key = Cooperative.ToInt();
                    meminfo.Address = address;
                    meminfo.Email = email;
                    meminfo.Phone = phone;
                    meminfo.Name = name;
                    meminfo.Save();
                    Member_Info meminfo2 = new Member_Info(name, Cooperative.ToInt());
                    User_Info user = new User_Info(username,true);
                    if (user.Key != null)
                    {
                        user.Name = username;
                        user.Password = password;
                        user.Activate = false;
                        user.ExpireDate = DateTime.Now;
                        user.CooperativeKey = meminfo2.Cooperative_Key;
                        user.EmployeeKey = meminfo2.Key;
                        user.Save();
                    }
                    else
                    {
                        return "Tên đăng nhập đã tồn tại!";
                    }
                }
            }
            //User_Info 
            return result;
        }
    }
}