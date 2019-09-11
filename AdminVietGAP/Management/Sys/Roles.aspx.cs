using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TNLibrary.Sys;
using TNLibrary.SYS;

namespace Management.Sys
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("SY0004");
            if (!nUserLogin.Role.Read)
            {
                Response.Write("<script >window.location.href='Error.aspx';</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    LoadData();
                }
            }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            DataTable nTable = Roles_Data.GetList(txtSearch.Text);
            GV_Roles.DataSource = nTable;
            GV_Roles.DataBind();
        }
        public void LoadData()
        {
            DataTable nTable = Roles_Data.GetList("");
            GV_Roles.DataSource = nTable;
            GV_Roles.DataBind();
            DateTime dt = DateTime.Now;
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            try
            {
                string mKey = e.CommandArgument.ToString();
                Role_Info info = new Role_Info(mKey);
                info.Delete();
                DataTable nTable = Roles_Data.GetList(txtSearch.Text);
                GV_Roles.DataSource = nTable;
                GV_Roles.DataBind();
            }
            catch
            {
                Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
            }
        }


        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            GV_Roles.PageIndex = e.NewPageIndex;
            DataTable nTable = Roles_Data.GetList(txtSearch.Text);
            GV_Roles.DataSource = nTable;
            GV_Roles.DataBind();

        }
    }
}