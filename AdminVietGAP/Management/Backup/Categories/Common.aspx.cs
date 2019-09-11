using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using System.Data;
using TNLibrary.Categories;

namespace Management.Categories
{
    public partial class Common : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0018");
                if (!nUserLogin.Role.Read)
                {
                    //Response.Write("<script >alert('Bạn không có quyền xem danh mục này!')</script>");
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
        private void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            DataTable nTable = new DataTable();
            if (rdbType.SelectedValue == "1")
            {
                nTable = Fertilizer_Common_Data.GetList(nPageSize, nPageNumber, txtSearch.Text);
            }
            else
            {
                nTable = Pesticide_Common_Data.GetList(nPageSize, nPageNumber, txtSearch.Text);
            }
            GV_Common.DataSource = nTable;
            GV_Common.DataBind();
            LoadPages();
        }

        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            txtPageNumber.Text = "1";
            int nPageSize = int.Parse(txtPageSize.Text);
            DataTable nTable;
            if (rdbType.SelectedValue == "1")
            {
                nTable = Fertilizer_Common_Data.GetList(nPageSize, 1, txtSearch.Text);
            }
            else
            {
                nTable = Pesticide_Common_Data.GetList(nPageSize, 1, txtSearch.Text);
            }
            GV_Common.DataSource = nTable;
            GV_Common.DataBind();
            LoadPages();
        }

        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CA0018");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    Fertilizer_Info info = new Fertilizer_Info(mKey);
                    info.Delete();
                    DataTable nTable;
                    if (rdbType.SelectedValue == "1")
                    {
                        nTable = Fertilizer_Common_Data.GetList(nPageSize, 1, txtSearch.Text);
                    }
                    else
                    {
                        nTable = Pesticide_Common_Data.GetList(nPageSize, 1, txtSearch.Text);
                    }
                    GV_Common.DataSource = nTable;
                    GV_Common.DataBind();
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

            int nTotalRecord = 0;
            if (rdbType.SelectedValue == "1")
            {
                nTotalRecord = Fertilizer_Common_Data.Count(txtSearch.Text);
            }
            else
            {
                nTotalRecord = Pesticide_Common_Data.Count(txtSearch.Text);
            }
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
    }
}