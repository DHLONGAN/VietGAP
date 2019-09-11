using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS.Users;
using System.Web.Services;
using System.Data;
using TNLibrary.Categories;
using TNLibrary.Sys;


namespace Management
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = this.Request["Action"];
            string UserKey = this.Request["UserKey"];
            if (action == "UpdateInfo")
            {
                updateinfo(UserKey);
            }
        }

        [WebMethod]
        public void updateinfo(string UserKey)
        {
            User_Info userinfo = new User_Info(int.Parse(UserKey));
            Session["MemberName"] = userinfo.EmployeeName;
            Session["EmployeeKey"] = userinfo.EmployeeKey;
            Response.Write(userinfo.EmployeeName);
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
        #region [ Check Session ]
        [WebMethod]
        public static bool CheckSession()
        {
            try
            {
                if (HttpContext.Current.Session["UserLogin"] == null)
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
    }
}