using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using TNLibrary.Sys;
using System.Data;
using TNLibrary.SYS;
using TNLibrary.Categories;

namespace Management.Sys
{
    public partial class ListCooperative : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                string nKey = "";
                if (nKey != null)
                {
                    nUserLogin.CheckRole("CA0014");
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
            }
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();

        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);

        }
        protected void LoadInfo(int Key)
        {
            
        }
        protected void SaveInfo()
        {
            //if (txtDatetime.Text != "")
            //{
            //    string _currentdatetime = "";
            //    if (Session["CurrentDateTime"] != null)
            //    {
            //        _currentdatetime = Session["CurrentDateTime"].ToString();
            //    }
            //    //CooperativeVentures_Data.UpdateInfo(txtDatetime.Text, _currentdatetime);
            //    CloseForm();
            //    Session["created"] = false;
            //}
        }
        private void LoadData()
        {
            DataTable nTable = ListCooperative_Data.GetList();
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, "");
            }
            GV_CooperativeVentures.DataSource = nTable;
            GV_CooperativeVentures.DataBind();
            DateTime dt = DateTime.Now;
            
        }
        public string ConvertResult(string values)
        {
            return values == "True" ? "False" : "True";
        }
       
        int m_IsChange = 0;
        
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            //int nPageNumber = int.Parse(txtPageNumber.Text);
            //int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CA0014");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    Cooperative_Info info = new Cooperative_Info(mKey);
                    //if ((DateTime.Now - info.Datetime).TotalDays > 7)
                    //{
                    //    Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    //}
                    //else
                    {
                        info.Delete();
                        DataTable nTable = Cooperative_Data.GetList(0);
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add("", 0, "", "", "", "", 0, 0, null);
                        }
                        GV_CooperativeVentures.DataSource = nTable;
                        GV_CooperativeVentures.DataBind();
                        //LoadPages();
                    }
                }
                catch
                {
                    Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
                }
            }
        }
    }
}