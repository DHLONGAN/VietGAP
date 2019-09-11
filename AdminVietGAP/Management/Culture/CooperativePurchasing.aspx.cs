using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using System.Data;
using TNLibrary.SYS;
using System.Globalization;

namespace Management.Culture
{
    public partial class CooperativePurchasing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0014");
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
            //txtPageNumber.Text = "1";
            //int nPageNumber = int.Parse(txtPageNumber.Text);
            //int nPageSize = int.Parse(txtPageSize.Text);
            //DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DataTable nTable = CooperativePurchasing_Data.GetList(dtfrom, dtto, Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber, DDLSeeds.SelectedValue);
            //if (nTable.Rows.Count == 0)
            //{
            //    nTable.Rows.Add(0, null, "", 0, 0, "", 0, "", 0, "");
            //}
            //GV_HarvestedForSale.DataSource = nTable;
            //GV_HarvestedForSale.DataBind();
            //LoadPages();
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            Session["dtfrom"] = txtfromDatetime.Text;
            Session["dtto"] = txttoDatetime.Text;
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = CooperativePurchasing_Data.GetList(nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
            DataTable nTable2 = CooperativePurchasing_Data.GetListSale(dtfrom, dtto, nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", DateTime.Now, "", 0, 0, 0, "", DateTime.Now, "", 0, 0, 0, "");

            }
            if (nTable2.Rows.Count == 0)
            {
                nTable2.Rows.Add(0, DateTime.Now, "", 0, 0, "", 0, 0, 0, "");
            }
            GV_HarvestedForSale.DataSource = nTable;
            ListSale.DataSource = nTable2;
            ListSale.DataBind();
            GV_HarvestedForSale.DataBind();
            DateTime dt = DateTime.Now;
            txtfromDatetime.Text = "01/01/2013";
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadPages();
            LoadPages2();
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
            DataTable nTable = CooperativePurchasing_Data.GetList(nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
            DataTable nTable2 = CooperativePurchasing_Data.GetListSale(dtfrom, dtto, nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", DateTime.Now, "", 0, 0, 0, "", DateTime.Now, "", 0, 0, 0, "");
                
            }
            if (nTable2.Rows.Count == 0)
            {
                nTable2.Rows.Add(0, DateTime.Now, "", 0, 0, "", 0, 0, 0, "");
            }
            GV_HarvestedForSale.DataSource = nTable;
            ListSale.DataSource = nTable2;
            ListSale.DataBind();
            
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
            nUserLogin.CheckRole("CU0014");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    CooperativePurchasing_Info info = new CooperativePurchasing_Info(mKey);
                    //if ((DateTime.Now - info.Datetime).TotalDays > 7)
                    //{
                    //    Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    //}
                    //else
                    {
                        info.Delete();
                        DataTable nTable = CooperativePurchasing_Data.GetList(nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
                        DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DataTable nTable2 = CooperativePurchasing_Data.GetListSale(dtfrom, dtto, nPageSize, nPageNumber, Session["CooperativeKey"].ToInt());
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add("", DateTime.Now, "", 0, 0, 0, "", DateTime.Now, "", 0, 0, 0, "");

                        }
                        if (nTable2.Rows.Count == 0)
                        {
                            nTable2.Rows.Add(0, DateTime.Now, "", 0, 0, "", 0, 0, 0, "");
                        }
                        GV_HarvestedForSale.DataSource = nTable;
                        GV_HarvestedForSale.DataBind();
                        ListSale.DataSource = nTable2;
                        ListSale.DataBind();
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
             int nTotalRecord = CooperativePurchasing_Data.Count(dtfrom, dtto, Session["CooperativeKey"].ToInt());
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
        protected void LoadPages2()
        {
            int nPageNumber = int.Parse(txtPageNumber2.Text);
            int nPageSize = int.Parse(txtPageSize2.Text);
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
            int nTotalRecord = CooperativePurchasing_Data.Count2(dtfrom, dtto, Session["CooperativeKey"].ToInt());
            if (nTotalRecord > nPageSize)
            {
                PageNumbers2.Visible = true;
                DataTable nTable = TNLibrary.WEB.LoadDataToToolboxWeb.LoadPageSize(nPageSize, nTotalRecord, nPageNumber);

                PageNumbers2.SelectedIndex = (nPageNumber - 1) - (int.Parse(nTable.Rows[0][0].ToString()) - 1);
                PageNumbers2.DataSource = nTable;
                PageNumbers2.DataBind();
            }
            else
                PageNumbers2.Visible = false;

        }
        protected void PageNumber_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string nPageNumberKey = PageNumbers.DataKeys[e.Item.ItemIndex].ToString();
            txtPageNumber.Text = nPageNumberKey;

            PageNumbers.SelectedIndex = e.Item.ItemIndex;
            LoadPages();
            LoadData();
        }
        protected void PageNumber2_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string nPageNumberKey = PageNumbers2.DataKeys[e.Item.ItemIndex].ToString();
            txtPageNumber2.Text = nPageNumberKey;

            PageNumbers2.SelectedIndex = e.Item.ItemIndex;
            LoadPages2();
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