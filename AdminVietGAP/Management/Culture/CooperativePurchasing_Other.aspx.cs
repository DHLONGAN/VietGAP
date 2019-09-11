using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using System.Data;
using TNLibrary.Culture;
using System.Globalization;

namespace Management.Culture
{
    public partial class CooperativePurchasing_Other : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0016");
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
            Session["dtfrom"] = txtfromDatetime.Text;
            Session["dtto"] = txttoDatetime.Text;
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = CooperativePurchasing_Other_Data.GetList(nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", 0, "", 0, 0, 0, 0, DateTime.Now, "", "", 0);

            }
            GV_HarvestedForSale.DataSource = nTable;
            GV_HarvestedForSale.DataBind();
            DateTime dt = DateTime.Now;
            txtfromDatetime.Text = "01/01/2013";
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadPages();
        }
        public void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            txtfromDatetime.Text = "01/01/2013";
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime dtfrom;
            DateTime dtto;
            if (Session["dtfrom"] != null)
            {
                dtfrom = DateTime.ParseExact(Session["dtfrom"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dtto = DateTime.ParseExact(Session["dtto"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            DataTable nTable = CooperativePurchasing_Other_Data.GetList(nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
             if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", 0, "", 0, 0, 0, 0, DateTime.Now, "", "", 0);

            }
            GV_HarvestedForSale.DataSource = nTable;
            GV_HarvestedForSale.DataBind();
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
            nUserLogin.CheckRole("CU0016");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    CooperativePurchasing_Other_Info info = new CooperativePurchasing_Other_Info(mKey);
                    //if ((DateTime.Now - info.Datetime).TotalDays > 7)
                    //{
                    //    Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    //}
                    //else
                    {
                        info.Delete();
                        DataTable nTable = CooperativePurchasing_Other_Data.GetList(nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
                        DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add("", 0, "", 0, 0, 0, 0, DateTime.Now, "", "", 0);

                        }
                        GV_HarvestedForSale.DataSource = nTable;
                        GV_HarvestedForSale.DataBind();
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
            DateTime dtfrom;
            DateTime dtto;
            if (Session["dtfrom"] != null)
            {
                dtfrom = DateTime.ParseExact(Session["dtfrom"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dtto = DateTime.ParseExact(Session["dtto"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            int nTotalRecord = CooperativePurchasing_Other_Data.Count(dtfrom, dtto, Session["CooperativeKey"].ToInt());
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