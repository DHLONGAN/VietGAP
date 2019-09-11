using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using TNLibrary.Culture;
using TNLibrary.SYS;
using TNLibrary.Categories;
using TNLibrary.WEB;
namespace Management.Culture
{
    public partial class SeedProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CU0001");
                 if (!nUserLogin.Role.Read)
                 {
                     //Response.Write("<script >alert('Bạn không có quyền xem danh mục này!')</script>");
                     Response.Write("<script >window.location.href='Error.aspx';</script>");
                 }
                 else
                 {
                     if (!IsPostBack)
                     {
                         LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "Select SeedsKey,SeedsName from PUL_Seeds where SeedsKey IN (Select SeedsKey FROM PUL_SeedProcess WHERE MemberKey = " + Convert.ToInt16(Session["EmployeeKey"]) + ")", false);
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
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = SeedProcess_Data.GetList(dtfrom, dtto, Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber, DDLSeeds.SelectedValue.ToInt());
            GV_SeedProcess.DataSource = nTable;
            GV_SeedProcess.DataBind();
            LoadPages();
        }
        private void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = SeedProcess_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber, DDLSeeds.SelectedValue.ToInt());
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, 0, "", 0, 0, 0, "", "", 0, "", "", "", 0, 0, null, null, 0, 0, 0, "", 0, 0, "", "", "", 0, "");
            }
            GV_SeedProcess.DataSource = nTable;
            GV_SeedProcess.DataBind();
            txtfromDatetime.Text = "01/" + DateTime.Now.ToString("MM/yyyy");
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CU0001");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    SeedProces_Info info = new SeedProces_Info(mKey);
                    info.Delete();
                    DataTable nTable = SeedProcess_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber, DDLSeeds.SelectedValue.ToInt());
                    if (nTable.Rows.Count == 0)
                    {
                        nTable.Rows.Add(0, 0, "", 0, 0, 0, "", "", 0, "", "", "", 0, 0, null, null, 0, 0, 0, "", 0, 0, "", "", "", 0, "");
                    }
                    GV_SeedProcess.DataSource = nTable;
                    GV_SeedProcess.DataBind();
                    LoadPages();
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

            int nTotalRecord = SeedProcess_Data.Count(Session["EmployeeKey"].ToInt(), DDLSeeds.SelectedValue.ToInt());
            
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

        protected void GV_SeedProcess_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ItemFrontPage")
            {
                int Key = int.Parse(e.CommandArgument.ToString());
                SeedProces_Info info = new SeedProces_Info(Key);
                if (info.Status)
                {
                    info.Status = false;
                }
                else
                {
                    info.Status = true;
                }
                info.Save();
                LoadData();
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

        protected void DDLSeeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}