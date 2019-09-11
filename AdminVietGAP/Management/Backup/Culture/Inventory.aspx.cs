using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using System.Data;
using TNLibrary.Book;
using System.Globalization;
using TNLibrary.WEB;

namespace Management.Culture
{
    public partial class Inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0002");
                //if (!nUserLogin.Role.Read)
                //{
                //    //Response.Write("<script >alert('Bạn không có quyền xem danh mục này!')</script>");
                //    Response.Write("<script >window.location.href='Error.aspx';</script>");
                //}
                //else
                {
                    if (!IsPostBack)
                    {
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "Select FertilizerOrganicKey,Name from PUL_FertilizerOrganic where FertilizerOrganicKey IN (Select SeedsKey FROM PUL_Fertilizer_Buy WHERE MemberKey = " + Convert.ToInt16(Session["EmployeeKey"]) + ")", false);
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
            DataTable nTable = Inventory_Data.GetList(dtfrom, dtto, Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber);
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, 0, null, 0, "", "", null, 0);
            }
            GV_CompostingOrganic.DataSource = nTable;
            GV_CompostingOrganic.DataBind();
            LoadPages();
        }
        public void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = Inventory_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber);
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, 0, null, 0, "", "", null, 0);
            }
            nTable.Columns.Add();
            GV_CompostingOrganic.DataSource = nTable;
            GV_CompostingOrganic.DataBind();
            DateTime dt = DateTime.Now;
            txtfromDatetime.Text = "01/" + DateTime.Now.ToString("MM/yyyy");
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CU0002");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    Inventory_Info info = new Inventory_Info(mKey);
                    if ((DateTime.Now - info.Datetime).TotalDays > 7)
                    {
                        Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    }
                    else
                    {
                        info.Delete();
                        DataTable nTable = Inventory_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber);
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add(0, 0, null, 0, "", "", null, 0);
                        }
                        GV_CompostingOrganic.DataSource = nTable;
                        GV_CompostingOrganic.DataBind();
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

            int nTotalRecord = Inventory_Data.Count(Session["EmployeeKey"].ToInt());
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

        public string ConvertName(string type, string ID)
        {
            if (type == "0")
            {
                return "";
            }
            if (type == "1")
            {
                return LoadDataToToolboxWeb.GetName("SELECT TradeName FROM PUL_Fertilizers where FertilizersKey = " + ID);
            }
            else
            {
                return LoadDataToToolboxWeb.GetName("SELECT Trade_Name FROM PUL_Pesticides where PesticideKey = " + ID);
            }
        }
    }
}