using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using TNLibrary.Categories;
using System.Data;

namespace Management.Categories
{
    public partial class Fertilizers_Cooperative : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0008");
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
            DataTable nTable = Fertilizers_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, int.Parse(Session["CooperativeKey"].ToString()));
            GV_Fertilizers_Cooperative.DataSource = nTable;
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", 0, 0, 0, null, null, null , null);// "", "", 0);
            }
            GV_Fertilizers_Cooperative.DataBind();
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CA0008");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    Fertilizers_Cooperative_Info info = new Fertilizers_Cooperative_Info(mKey);
                    info.Delete();
                    DataTable nTable = Fertilizers_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, int.Parse(Session["CooperativeKey"].ToString()));
                    if (nTable.Rows.Count == 0)
                    {
                        nTable.Rows.Add("", 0, 0, 0, new Guid(), DateTime.Now, new Guid(), DateTime.Now);
                    }
                    GV_Fertilizers_Cooperative.DataSource = nTable;
                    GV_Fertilizers_Cooperative.DataBind();
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
            DataTable nTable = Fertilizers_Cooperative_Data.GetList(nPageSize, nPageNumber, txtSearch.Text, int.Parse(Session["CooperativeKey"].ToString()));
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", 0, 0, 0, new Guid(), DateTime.Now, new Guid(), DateTime.Now);
            }
            GV_Fertilizers_Cooperative.DataSource = nTable;
            GV_Fertilizers_Cooperative.DataBind();
            LoadPages();
        }

        protected void LoadPages()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);

            int nTotalRecord = Fertilizers_Cooperative_Data.Count(txtSearch.Text, int.Parse(Session["CooperativeKey"].ToString()));
            
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