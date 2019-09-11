using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using TNLibrary.WEB;
using TNLibrary.Categories;
using TNLibrary.SYS;
namespace Management.Categories
{
    public partial class Seeds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CA0001");
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

        public int page = 0;
        private void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            DataTable nTable = Seeds_Data.GetList(nPageSize, nPageNumber,txtSearch.Text);
            GV_Seeds.DataSource = nTable;
            GV_Seeds.DataBind();
            LoadPages();
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            txtPageNumber.Text = "1";
            int nPageSize = int.Parse(txtPageSize.Text);
            DataTable nTable = Seeds_Data.GetList(nPageSize, 1, txtSearch.Text);
            GV_Seeds.DataSource = nTable;
            GV_Seeds.DataBind();
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CA0001");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    Seed_Info info = new Seed_Info(mKey);
                    info.Delete();
                    DataTable nTable = Seeds_Data.GetList(0, 1, txtSearch.Text);
                    GV_Seeds.DataSource = nTable;
                    GV_Seeds.DataBind();
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

            int nTotalRecord = Seeds_Data.Count(txtSearch.Text);
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
            page = int.Parse(nPageNumberKey);
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