using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using TNLibrary.SYS.Users;
using TNLibrary.SYS;

namespace Management.Sys
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("SY0003");
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
                     //LoadData();
                 }
             }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = Users_Data.GetList(txtSearch.Text, int.Parse(Session["CooperativeKey"].ToString()), nUserLogin.GroupKey);
            GV_User.DataSource = nTable;
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(new Guid(), 0, "", "", "", 0, DateTime.Now, DateTime.Now, 0);
            }
            GV_User.DataBind();
        }
        public void LoadData()
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = new DataTable();
            if (nUserLogin.GroupKey == 4)
            {
                nTable = Users_Data.GetList("", Session["CooperativeKey"].ToInt());
            }
            else if (nUserLogin.GroupKey == 3)
            {
                nTable = Users_Data.GetList(Session["CooperativeKey"].ToInt(), 3, "");
            }
            else if (nUserLogin.GroupKey == 2)
            {
                nTable = Users_Data.GetList("", nUserLogin.CooperativeKey, nUserLogin.GroupKey);
            }
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(null, 0, "", "", "", 0, DateTime.Now, DateTime.Now, 0);
            }
            
            GV_User.DataSource = nTable;
            
            GV_User.DataBind();
            DateTime dt = DateTime.Now;
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CU0010");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    string mKey = e.CommandArgument.ToString();
                    User_Info info = new User_Info(mKey);
                    if ((DateTime.Now - info.CreatedDateTime).TotalDays > 7)
                    {
                        Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    }
                    else
                    {
                        info.Delete();
                        DataTable nTable = Users_Data.GetList(txtSearch.Text, int.Parse(Session["CooperativeKey"].ToString()),nUserLogin.GroupKey);
                        GV_User.DataSource = nTable;
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add(null, 0, "", "", "", 0, DateTime.Now, DateTime.Now, 0);
                        }
                        GV_User.DataBind();
                    }
                }
                catch
                {
                    Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
                }
            }
        }


        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            GV_User.PageIndex = e.NewPageIndex;
            DataTable nTable = Users_Data.GetList(txtSearch.Text, int.Parse(Session["CooperativeKey"].ToString()),nUserLogin.GroupKey);
            GV_User.DataSource = nTable;
            GV_User.DataBind();

        }
        public string ConvertDate(string dt)
        {
            if (dt != "")
            {
                DateTime dt2 = Convert.ToDateTime(dt);
                return dt2.Day.ToString() + "/" + dt2.Month.ToString() + "/" + dt2.Year.ToString();
            }
            return "";
        }
        protected void GV_User_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ItemFrontPage")
            {
                string mKey = e.CommandArgument.ToString();
                User_Info info = new User_Info(mKey);
                if (info.Activate)
                {
                    info.Activate = false;
                }
                else
                {
                    info.Activate = true;
                }
                info.Save();
                LoadData();
            }
        }
        public string ConvertResult(string values)
        {
            return values == "True" ? "True" : "False";
        }


        protected void LoadPages()
        {
            //int nPageNumber = int.Parse(txtPageNumber.Text);
            //int nPageSize = int.Parse(txtPageSize.Text);

            //int nTotalRecord = CheckEquipment_Data.Count();
            //if (nTotalRecord > nPageSize * 5)
            //    nTotalRecord = nPageSize * 5;
            //if (nTotalRecord > nPageSize)
            //{
            //    PageNumbers.Visible = true;
            //    DataTable nTable = TNLibrary.WEB.LoadDataToToolboxWeb.LoadPageSize(nPageSize, nTotalRecord);

            //    PageNumbers.SelectedIndex = nPageNumber - 1;
            //    PageNumbers.DataSource = nTable;
            //    PageNumbers.DataBind();
            //}
            //else
            //    PageNumbers.Visible = false;

        }
        protected void PageNumber_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string nPageNumberKey = PageNumbers.DataKeys[e.Item.ItemIndex].ToString();
            txtPageNumber.Text = nPageNumberKey;

            PageNumbers.SelectedIndex = e.Item.ItemIndex;
            LoadPages();
            LoadData();
        }

        protected int ShowNo(int index)
        {
            if (int.Parse(txtPageNumber.Text) > 1)
            {
                return ((int.Parse(txtPageNumber.Text) - 1) * int.Parse(txtPageSize.Text)) + index;
            }
            else
            {
                return index;
            }
        }
    }
}