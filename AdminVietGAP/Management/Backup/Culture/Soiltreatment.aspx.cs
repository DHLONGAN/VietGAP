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
    public partial class Soiltreatment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                txtfromDatetime.Text = "01/" + DateTime.Now.ToString("MM/yyyy");
                txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = Soiltreatment_Data.GetList(dtfrom, dtto);
            GV_Soiltreatment.DataSource = nTable;
            GV_Soiltreatment.DataBind();
            //txtfromDatetime.Text = Session["fromDatetime"].ToString();
            //txttoDatetime.Text = Session["todatetime"].ToString(); 
        }
        protected void cmdView_Click2(object sender, ImageClickEventArgs e)
        {
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = Soiltreatment_Data.GetList(dtfrom, dtto);
            GV_Soiltreatment.DataSource = nTable;
            GV_Soiltreatment.DataBind();
            //txtfromDatetime.Text = Session["fromDatetime"].ToString();
            //txttoDatetime.Text = Session["todatetime"].ToString(); 
        }
        public void LoadData()
        {
            DataTable nTable = Soiltreatment_Data.GetList();
            GV_Soiltreatment.DataSource = nTable;
            GV_Soiltreatment.DataBind();
            DateTime dt = DateTime.Now;
            Session["fromdatetime"] = txtfromDatetime.Text = dt.ToString();//"dd-MM-yyyy");
            Session["todatetime"] = txttoDatetime.Text = dt.ToString();//"dd-MM-yyyy");
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            try
            {
                int mKey = int.Parse(e.CommandArgument.ToString());
                Soiltreatment_Info info = new Soiltreatment_Info(mKey);
                info.Delete();
                DataTable nTable = Assessment_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]));
                GV_Soiltreatment.DataSource = nTable;
                GV_Soiltreatment.DataBind();
            }
            catch
            {
                Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
            }
        }


        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            GV_Soiltreatment.PageIndex = e.NewPageIndex;
            DataTable nTable = Soiltreatment_Data.GetList();
            GV_Soiltreatment.DataSource = nTable;
            GV_Soiltreatment.DataBind();

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