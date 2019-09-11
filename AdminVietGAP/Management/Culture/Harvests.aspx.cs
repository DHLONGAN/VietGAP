using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using System.Data;
using System.Globalization;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class Harvests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0006");
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
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = Harvests_Data.GetList(dtfrom, dtto, Convert.ToInt16( Session["EmployeeKey"]), nPageSize, nPageNumber);
            GV_Harvests.DataSource = nTable;
            GV_Harvests.DataBind();
            LoadPages();
        }
        public void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = Harvests_Data.GetList(Convert.ToInt16( Session["EmployeeKey"]), nPageSize, nPageNumber);
            GV_Harvests.DataSource = nTable;
            GV_Harvests.DataBind();
            DateTime dt = DateTime.Now;
            txtfromDatetime.Text = "01/01/2013";
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             nUserLogin.CheckRole("CU0006");
             if (!nUserLogin.Role.Del)
             {
                 Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
             }
             else
             {
                 try
                 {
                     int mKey = int.Parse(e.CommandArgument.ToString());
                     Harvest_Info info = new Harvest_Info(mKey);
                     if ((DateTime.Now - info.DateOn).TotalDays > 7)
                     {
                         Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                     }
                     else
                     {
                         info.Delete();
                         DataTable nTable = Harvests_Data.GetList(Convert.ToInt16( Session["EmployeeKey"]), nPageSize, nPageNumber);
                         GV_Harvests.DataSource = nTable;
                         GV_Harvests.DataBind();
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

            int nTotalRecord = Harvests_Data.Count(Session["EmployeeKey"].ToInt());
            if (nTotalRecord > nPageSize * 5)
                nTotalRecord = nPageSize * 5;
            if (nTotalRecord > nPageSize)
            {
                PageNumbers.Visible = true;
                DataTable nTable = TNLibrary.WEB.LoadDataToToolboxWeb.LoadPageSize(nPageSize, nTotalRecord);

                PageNumbers.SelectedIndex = nPageNumber - 1;
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