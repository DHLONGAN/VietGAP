using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TNLibrary.Culture;
using System.Globalization;

namespace Management.Culture
{
    public partial class Accident : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = Accident_Data.GetList(dtfrom, dtto);
            GV_Accident.DataSource = nTable;
            GV_Accident.DataBind();
        }
        public void LoadData()
        {
            DataTable nTable = Accident_Data.GetList();
            GV_Accident.DataSource = nTable;
            GV_Accident.DataBind();
            DateTime dt = DateTime.Now;
            txtfromDatetime.Text = "01/01/2013";
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void GrDelete(object sender, CommandEventArgs e)
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
                    DataTable nTable = Accident_Data.GetList();
                    GV_Accident.DataSource = nTable;
                    GV_Accident.DataBind();
                }
            }
            catch
            {
                Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
            }
        }


        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            GV_Accident.PageIndex = e.NewPageIndex;
            DataTable nTable = Accident_Data.GetList();
            GV_Accident.DataSource = nTable;
            GV_Accident.DataBind();

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