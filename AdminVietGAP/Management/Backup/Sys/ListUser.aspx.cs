using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TNLibrary.Sys;
using TNLibrary.SYS.Users;
using TNLibrary.SYS;

namespace Management.Sys
{
    public partial class ListUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                if (!IsPostBack)
                {
                    LoadData();
                }
            }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = new DataTable();
            nTable = Users_Roles_Data.GetList(nUserLogin.CooperativeVenturesKey, nUserLogin.GroupKey, txtSearch.Text, nUserLogin.CooperativeVenturesKey);
            //if (nUserLogin.GroupKey == 4)
            //{
            //    nTable = Users_Roles_Data.GetList(txtSearch.Text);
            //}
            //if(nUserLogin.GroupKey==3)
            //{
            //    nTable = Users_Roles_Data.GetList(nUserLogin.CooperativeVenturesKey, 3, txtSearch.Text);
            //}
            //else  if(nUserLogin.GroupKey==2)
            //{
            //    nTable = Users_Roles_Data.GetList(nUserLogin.CooperativeKey, 2, txtSearch.Text);
            //}

            GV_Users_Roles.DataSource = nTable;
            GV_Users_Roles.DataBind();
            LoadPages();
        }
        public void LoadData()
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = new DataTable();
            nTable = Users_Roles_Data.GetList(nUserLogin.CooperativeKey, nUserLogin.GroupKey, txtSearch.Text, nUserLogin.CooperativeVenturesKey);
            //if (nUserLogin.GroupKey == 4)
            //{
            //    nTable = Users_Roles_Data.GetList(0, 0, txtSearch.Text);
            //}
            //if (nUserLogin.GroupKey == 3)
            //{
            //    nTable = Users_Roles_Data.GetList(nUserLogin.CooperativeVenturesKey, 3, txtSearch.Text);
            //}
            //else if (nUserLogin.GroupKey == 2)
            //{
            //    nTable = Users_Roles_Data.GetList(nUserLogin.CooperativeKey, 2, txtSearch.Text);
            //}
            GV_Users_Roles.DataSource = nTable;
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(null, "");
            }
            GV_Users_Roles.DataBind();
            DateTime dt = DateTime.Now;
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            try
            {
                string[] mKey = e.CommandArgument.ToString().Split('+');
                User_Role_Info info = new User_Role_Info(mKey[0], mKey[1]);
                info.Delete();
                DataTable nTable = Users_Roles_Data.GetList(txtSearch.Text);
                GV_Users_Roles.DataSource = nTable;
                GV_Users_Roles.DataBind();
                LoadPages();
            }
            catch
            {
                Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
            }
        }


        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            GV_Users_Roles.PageIndex = e.NewPageIndex;
            DataTable nTable = Users_Roles_Data.GetList(txtSearch.Text);
            GV_Users_Roles.DataSource = nTable;
            GV_Users_Roles.DataBind();

        }
        protected void GV_Users_Roles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] mKey = e.CommandArgument.ToString().Split('+');
            User_Role_Info info = new User_Role_Info(mKey[0], mKey[1]);
            if (e.CommandName == "Read")
            {
                if (info.Read)
                {
                    info.Read = false;
                }
                else
                {
                    info.Read = true;
                }
                info.Update();
                LoadData();
            }
            if (e.CommandName == "Add")
            {
                if (info.Add)
                {
                    info.Add = false;
                }
                else
                {
                    info.Add = true;
                }
                info.Update();
                LoadData();
            }
            if (e.CommandName == "Del")
            {
                if (info.Del)
                {
                    info.Del = false;
                }
                else
                {
                    info.Del = true;
                }
                info.Update();
                LoadData();
            }
            if (e.CommandName == "Sua")
            {
                if (info.Edit)
                {
                    info.Edit = false;
                }
                else
                {
                    info.Edit = true;
                }
                info.Update();
                LoadData();
            }
        }
        protected void LoadPages()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            int nTotalRecord = Users_Roles_Data.Count(nUserLogin.CooperativeVenturesKey, nUserLogin.GroupKey, txtSearch.Text);
            if (nTotalRecord > nPageSize)
            {
                PageNumbers.Visible = true;
                DataTable nTable = TNLibrary.WEB.LoadDataToToolboxWeb.LoadPageSize(nPageSize, nTotalRecord, nPageNumber);

                PageNumbers.SelectedIndex = (nPageNumber - 1) - (int.Parse(nTable.Rows[0][0].ToString()) - 1);
                PageNumbers.DataSource = nTable;
                PageNumbers.DataBind();
            }
            else
                PageNumbers.Visible = false;

        }
        protected void PageNumber_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string nPageNumberKey = PageNumbers.DataKeys[e.Item.ItemIndex].ToString();
            txtPageNumber.Text = nPageNumberKey;

            PageNumbers.SelectedIndex = e.Item.ItemIndex;
            LoadPages();
            LoadData();
        }

        protected string Rank(string index)
        {
            string result = "";
            switch (index)
            {
                case "1":
                    {
                        result = "Xã viên";
                        break;
                    }
                case "2":
                    {
                        result = "Chủ nhiệm HTX";
                        break;
                    }
                case "3":
                    {
                        result = "Liên hiệp tác xã";
                        break;
                    }
                case "4":
                    {
                        result = "Quản trị viên";
                        break;
                    }
            }
            return result;
            //if (int.Parse(txtPageNumber.Text) > 1)
            //{
            //    return ((int.Parse(txtPageNumber.Text) - 1) * int.Parse(txtPageSize.Text)) + index;
            //}
            //else
            //{
            //    return index;
            //}
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
        public string ConvertResult(string values)
        {
            return values == "True" ? "True" : "False";
        }
    }
}