using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using System.Data;
using TNLibrary.System;

namespace Management.Sys
{
    public partial class TimeLimit_Cooperative : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                //nUserLogin.CheckRole("CU0002");
                //if (!nUserLogin.Role.Read)
                //{
                //    Response.Write("<script >window.location.href='Error.aspx';</script>");
                //}
                //else
                {
                    if (!IsPostBack)
                    {
                        LoadData();
                    }
                }
            }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            txtPageNumber.Text = "1";
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = new DataTable();
            switch (nUserLogin.GroupKey.ToInt())
            {
                case 2:
                    {
                        nTable = TimeLimit_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, nUserLogin.CooperativeKey, nUserLogin.GroupKey);
                        break;
                    }
                case 3:
                    {
                        nTable = TimeLimit_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, nUserLogin.CooperativeVenturesKey, nUserLogin.GroupKey);
                        break;
                    }
                case 4:
                    {
                        goto case 3;
                    }
            }
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, 0, "", 0);
            }
            GV_List.DataSource = nTable;
            GV_List.DataBind();
            LoadPages();
        }
        public void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = new DataTable();
            switch (nUserLogin.GroupKey.ToInt())
            {
                case 2:
                    {
                        nTable = TimeLimit_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, nUserLogin.CooperativeKey, nUserLogin.GroupKey);
                        break;
                    }
                case 3:
                    {
                        nTable = TimeLimit_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, nUserLogin.CooperativeVenturesKey, nUserLogin.GroupKey);
                        break;
                    }
                case 4:
                    {
                        goto case 3;
                    }
            }
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, 0, "", 0);
            }
            nTable.Columns.Add();
            GV_List.DataSource = nTable;
            GV_List.DataBind();
            DateTime dt = DateTime.Now;
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CU0002");
            //if (!nUserLogin.Role.Del)W
            //{
            //    Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            //}
            //else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    TimeLimit_Cooperative_Info info = new TimeLimit_Cooperative_Info(mKey);
                    //if ((DateTime.Now - info.CreatedDateTime).TotalDays > 7)
                    //{
                    //    Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    //}
                    //else
                    {
                        info.Delete();
                        DataTable nTable = new DataTable();
                        switch (nUserLogin.GroupKey.ToInt())
                        {
                            case 2:
                                {
                                    nTable = TimeLimit_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, nUserLogin.CooperativeKey, nUserLogin.GroupKey);
                                    break;
                                }
                            case 3:
                                {
                                    nTable = TimeLimit_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, nUserLogin.CooperativeVenturesKey, nUserLogin.GroupKey);
                                    break;
                                }
                            case 4:
                                {
                                    goto case 3;
                                }
                        }
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add(0, 0, "", 0);
                        }
                        GV_List.DataSource = nTable;
                        GV_List.DataBind();
                        LoadPages();
                    }
                }
                catch
                {
                    Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
                }
            }
        }


        protected void LoadPages()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             int nTotalRecord = TimeLimit_Cooperative_Data.Count(txtSearch.Text, nUserLogin.CooperativeKey, nUserLogin.GroupKey);
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
        public string ConvertDate(string dt)
        {
            if (dt != "")
            {
                DateTime dt2 = Convert.ToDateTime(dt);
                return dt2.Day.ToString() + "/" + dt2.Month.ToString() + "/" + dt2.Year.ToString();
            }
            return "";
        }
    }
}