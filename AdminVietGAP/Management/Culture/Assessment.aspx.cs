using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using TNLibrary.Categories;
using TNLibrary.Culture;
using System.Globalization;

namespace Management.Culture
{
    public partial class Assessment : System.Web.UI.Page
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
            DataTable nTable = Assessment_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]), dtfrom, dtto);
            GV_Assessment.DataSource = nTable;
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(default(DateTime));//	Đất	Kim loại nặng
            }
            GV_Assessment.DataBind();
        }
        private void LoadData()
        {
            txtfromDatetime.Text = "01/01/2013";
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DataTable nTable = Assessment_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]));
            GV_Assessment.DataSource = nTable;
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(default(DateTime));//	Đất	Kim loại nặng
            }
            GV_Assessment.DataBind();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            try
            {
                int mKey = int.Parse(e.CommandArgument.ToString());
                Assessment_Info info = new Assessment_Info(mKey);
                info.Delete();
                DataTable nTable = Assessment_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]));
                GV_Assessment.DataSource = nTable;
                GV_Assessment.DataBind();
            }
            catch
            {
                Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
            }
        }


        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {

            GV_Assessment.PageIndex = e.NewPageIndex;
            DataTable nTable = Assessment_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]));
            GV_Assessment.DataSource = nTable;
            GV_Assessment.DataBind();

        }
        public string YesNo(string Status)
        {
            return Status == "True" ? "Đạt" : "Không đạt";
        }
        private string mCategoryName = "";
        int m_IsChange = 0;
        protected void GV_Assessment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           //  if (e.Row.RowType == DataControlRowType.DataRow)
           // {
           //     Label txtAssessmentName = (Label)e.Row.FindControl("txtAssessmentName");
           //     if (txtAssessmentName.Text.Length >0)
           //     {
           //         //mCategoryName = txtAssessmentName.Text;
           //         m_IsChange++;
           //     }
           //     if ((float)m_IsChange % 2 == 0)
           //     {
           //         e.Row.BackColor = System.Drawing.Color.FromArgb(255,244,201);
           //     }
           //     // Display the company name in italics.
               
                
           //}

           
        }

        protected void GV_Assessment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ItemFrontPage")
            {
                int Key = int.Parse(e.CommandArgument.ToString());
                ProcessEnvironmental_Info info = new ProcessEnvironmental_Info(Key);
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


        public string ConvertResult(string values)
        {
            return values == "True" ? "False" : "True";
        }
        public string ConvertDate(string dt)
        {
            if (dt != "" && dt!="01/01/0001 00:00:00")
            {
                DateTime dt2 = Convert.ToDateTime(dt);
                return dt2.Day.ToString() + "/" + dt2.Month.ToString() + "/" + dt2.Year.ToString();
            }
            return "";
        }
    }
}