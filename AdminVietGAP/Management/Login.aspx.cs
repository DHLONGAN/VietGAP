using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TNLibrary.SYS.Users;
using TNLibrary.SYS;
using TNLibrary.WEB;

namespace Management
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                        lblStatus.Text = "Vui lòng kiểm tra Username và Password";
                        break;
                    case "CheckUser_Error02":
                        lblStatus.Text = "User này đã lock, vui lòng liên hệ Administrator";
                        break;
                    //case "CheckUser_Error03":
                    //    lblStatus.Text = "User này đã hết hạn, vui lòng liên hệ Administrator";
                    //    break;
                    case "CantLogin":
                        lblStatus.Text = "Tài khoản của bạn không đủ quyền truy cập!";
                        break;
                }
            }
            else
            {
                SessionUserLogin nLogin = new SessionUserLogin();
                nLogin.Name = txtUserName.Text;
                nLogin.Key = nResult[1];
                Session["EmployeeKey"] = nLogin.EmployeeKey = int.Parse(nResult[2]);
                nLogin.GroupKey = int.Parse(nResult[3]);
                nLogin.CooperativeKey = int.Parse(nResult[4]);
                nLogin.EmployeeName = nResult[5];
                nLogin.BranchName = nResult[6];
                nLogin.CooperativeVenturesKey = int.Parse(nResult[7]);
                Session["UserLogin"] = nLogin;
                if (int.Parse(nResult[2]) != 0)
                {
                    Session["MemberName"] = LoadDataToToolboxWeb.GetName("Select Name FROM PUL_Member WHERE [Key] =" + nResult[2]);
                }
                else
                {
                    Session["MemberName"] = "";
                }
                //   
                FormsAuthentication.RedirectFromLoginPage(nResult[1], false);
                Response.Redirect("~/Culture/Default.aspx");

            }
        }
    }
}