using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Categories;
using System.Data;
using TNLibrary.SYS;

namespace Management.Categories
{
    public partial class Equipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CA0006");
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
            DataTable nTable = Equipment_Data.GetList(nPageSize, nPageNumber, txtSearch.Text);
            GV_Equipment.DataSource = nTable;
            GV_Equipment.DataBind();
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CA0006");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    Equipment_Info info = new Equipment_Info(mKey);
                    info.Delete();
                    DataTable nTable = Equipment_Data.GetList(nPageSize, nPageNumber, txtSearch.Text);
                    GV_Equipment.DataSource = nTable;
                    GV_Equipment.DataBind();
                    LoadPages();
                }
                catch
                {
                    Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
                }
            }
        }

        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            txtPageNumber.Text = "1";
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            DataTable nTable = Equipment_Data.GetList(nPageSize, 1, txtSearch.Text);
            GV_Equipment.DataSource = nTable;
            GV_Equipment.DataBind();
            LoadPages();
        }

        protected void LoadPages()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);

            int nTotalRecord = Equipment_Data.Count(txtSearch.Text);
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