using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TNLibrary.SYS.Users;
using TNLibrary.SYS;
using System.Text;

namespace Management
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //cookie
            //HttpCookie cm = new HttpCookie("Code");
            //cm.Value = "abdasdasd";
            //this.Response.Cookies.Add(cm);
            if (Session["MemberID"] != null)
            {
                Response.Redirect("~/Book.aspx");
            }
            if (this.Request.Cookies["Code"] != null)
            {
                User_Info info = new User_Info(this.Request.Cookies["Code"].Value, "Ac");
                if (info.Key != "")
                {
                    SessionUserLogin nLogin = new SessionUserLogin();
                    nLogin.Name = txtUserName.Text;
                    nLogin.Key =info.Key;
                    Session["EmployeeKey"] = Session["MemberID"] = nLogin.EmployeeKey = info.EmployeeKey;
                    nLogin.GroupKey = info.GroupKey;
                    nLogin.CooperativeKey = info.CooperativeKey;
                    nLogin.EmployeeName = info.EmployeeName;
                    nLogin.BranchName = info.BranchName;
                    Session["UserLogin"] = nLogin;
                    Session["MemberName"] = info.BranchName;
                    Session["CooperativeKey"] = info.CooperativeKey;
                    Session["UserName"] = txtUserName.Text;
                    Session["SysUserKey"] = info.Key;
                    Response.Redirect("~/Book.aspx");
                    return;
                }
                
            }
             Session["UserLogin"] = null;
            FormsAuthentication.SignOut();
        }

        protected void cmdLogin_Click(object sender, ImageClickEventArgs e)
        {
            string[] nResult = Users_Data.CheckUser(txtUserName.Text, txtPassword.Text);
            if (nResult[0] == "ERR")
            {
                switch (nResult[1])
                {
                    case "CheckUser_Error01":
                        lblStatus.Text = "Vui lòng kiểm tra tài khoản và mật khẩu";
                        break;
                    case "CheckUser_Error02":
                        lblStatus.Text = "User này đã lock, vui lòng liên hệ Administrator";
                        break;
                    case "CheckUser_Error03":
                        lblStatus.Text = "User này đã hết hạn, vui lòng liên hệ Administrator";
                        break;
                }
            }
            else
            {
                if (cbRemember.Checked)
                {
                    User_Info info = new User_Info(int.Parse(nResult[2]));
                    string cookies = RandomString(50);
                    info.Cookies = cookies;
                    HttpCookie ck = new HttpCookie("Code");
                    ck.Value = cookies;
                    this.Response.Cookies.Add(ck);
                    info.Cookies = cookies;
                    info.Save();
                }
                SessionUserLogin nLogin = new SessionUserLogin();
                nLogin.Name = txtUserName.Text;
                nLogin.Key = nResult[1];
                Session["EmployeeKey"] = Session["MemberID"] = nLogin.EmployeeKey = int.Parse(nResult[2]);
                nLogin.GroupKey = int.Parse(nResult[3]);
                nLogin.CooperativeKey = int.Parse(nResult[4]);
                nLogin.EmployeeName = nResult[5];
                nLogin.BranchName = nResult[6];
                Session["UserLogin"] = nLogin;
                Session["MemberName"] = nResult[5];
                Session["CooperativeKey"] = nLogin.CooperativeKey;
                Session["UserName"] = txtUserName.Text;
                Session["SysUserKey"] = nResult[1];
                //   
                FormsAuthentication.RedirectFromLoginPage(nResult[1], false);
                Response.Redirect("~/Book.aspx");

            }
        }
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}