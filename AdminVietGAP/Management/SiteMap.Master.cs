using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using TNLibrary.WEB;
using TNLibrary.SYS;
using TNLibrary.Fields;

namespace Management
{
    public partial class SiteMap : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["UserLogin"] == null)
                    Response.Redirect("~/Login.aspx");
                SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
                Session["GroupKey"] = nUserLogin.GroupKey;
                if (nUserLogin.GroupKey == 1)
                {
                    lbGroup.Text = "Thành viên hợp tác xã";
                }
                if (nUserLogin.GroupKey == 2)
                {
                    lbGroup.Text = "Giám đốc hợp tác xã";
                }
                if (nUserLogin.GroupKey == 3)
                {
                    lbGroup.Text = "Liên hiệp hợp tác xã";
                }
                if (nUserLogin.GroupKey == 4)
                {
                    lbGroup.Text = "Nhà Nước";
                }
                txtUserLogin.Text = nUserLogin.Name;
                if (Session["EmployeeKey"] == null)
                {
                    Session["EmployeeKey"] = nUserLogin.EmployeeKey;
                }
                LoadMenu();
            }
        }
        private void LoadMenu()
        {
            string nURL = Request.Url.AbsolutePath;
            string[] _MenuModule = nURL.Split('/');
            int nKey = 0;
            string _Title = "";
            DataTable _Table = Web_Menu_Data.List(0);

            StringBuilder nMenuTop = new StringBuilder();
            nMenuTop.AppendLine("<ul>");
            nMenuTop.AppendLine("<li class='space'></li>");
            foreach (DataRow nRow in _Table.Rows)
            {
                if (_MenuModule[1].ToUpper() == nRow["Module"].ToString().ToUpper())
                {
                    nMenuTop.AppendLine("<li class='selected'><img src='/Img/Menu/" + nRow["Icon"] + "_selected.png'/></li>");
                    nKey = int.Parse(nRow["MenuKey"].ToString());
                    _Title = nRow["MenuName"].ToString();
                }
                else
                    nMenuTop.AppendLine("<li><a href='" + nRow["MenuLink"] + "'><img src='/Img/Menu/" + nRow["Icon"] + ".png'/></a></li>");
                nMenuTop.AppendLine("<li class='space'></li>");
            }
            nMenuTop.AppendLine("</ul>");
           
            LiteMenuTop.Text = nMenuTop.ToString();
           
        }
    }
}