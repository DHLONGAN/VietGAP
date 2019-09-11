using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using TNLibrary.WEB;
using System.Data;
using System.Globalization;

namespace Management.Culture
{
    public partial class Assessment_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int nAssessmentKey = 0;
            string nKey = "";
            if (nKey != null)
            {
                if (!IsPostBack)
                {
                    LoadData();
                    DateTime time = new DateTime();
                    if (this.Request["key"] != "0")
                    {
                        Session["CurrentDateTime"] = DateTime.ParseExact(this.Request["key"], "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        Session["CurrentDateTime"] = null;
                    }
                }
            }
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);

        }
        protected void LoadInfo(int Key)
        {
            //Assessment_Info info = new Assessment_Info(Key);
            ////txtAssessmentName.Text = info.AssessmentName;
            //if (0 < Key && Key < 6)
            //{
            //    txtAssessmentName.Text = "Đất";
            //}
            //else if (5 < Key && Key < 10)
            //{
            //    txtAssessmentName.Text = "Nước tưới";
            //}
            //else if (9 < Key && Key < 14)
            //{
            //    txtAssessmentName.Text = "Nước rửa sản phẩm";
            //}
            //else
            //{
            //    txtAssessmentName.Text = "Phân hữu cơ";
            //}
            //DLLPollutionKey.SelectedValue = info.PollutionKey.ToString();
            //rdbStatus.SelectedValue = info.Status ? "1" : "2";
            //txtSolution.Text = info.Solution;
        }
        protected void SaveInfo()
        {
            if (txtDatetime.Text != "")
            {
                string _currentdatetime = "";
                if (Session["CurrentDateTime"] != null)
                {
                    _currentdatetime = Session["CurrentDateTime"].ToString();
                }
                Assessment_Data.UpdateInfo(txtDatetime.Text, _currentdatetime);
                CloseForm();
                Session["created"] = false;
            }
        }
        private void LoadData()
        {
            string nKey = this.Request["key"];
            DateTime time = new DateTime();
            if (nKey != "0")
            {
                time = DateTime.ParseExact(nKey, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
            }
            if (Session["EmployeeKey"].ToInt() == 0)
            {
                Response.Write("<script >alert('Vui lòng chọn xã viên trước!')</script>");
                string nUrl = "<script>CloseOnReload()</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
                return;
            }
            DateTime currenttime = DateTime.Now;
            if (nKey == "0" && !(bool)Session["created"])            
            {
                
                nKey = currenttime.ToString("MM/dd/yyyy");
                Assessment_Data.CreatNewInfo("", int.Parse(Session["CooperativeKey"].ToString()), Convert.ToInt16(Session["EmployeeKey"]));
                txtDatetime.Text = "";
                DataTable nTable = Assessment_Data.GetListInfo("", Convert.ToInt16(Session["EmployeeKey"]));
                GV_Assessment.DataSource = nTable;
                GV_Assessment.DataBind();
                Session["created"] = true;
                Session["createddatetime"] = currenttime;
            }
            else
            {
                
                if (nKey == "0")
                {
                    //time = DateTime.ParseExact(Session["createddatetime"].ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture);
                    txtDatetime.Text = "";
                    nKey = "";
                }

                else
                {
                    txtDatetime.Text = time.ToString("MM/dd/yyyy");
                    //nKey = time.ToString("MM/dd/yyyy");
                    Session["created"] = false;
                }

                DataTable nTable = Assessment_Data.GetListInfo(txtDatetime.Text, Convert.ToInt16(Session["EmployeeKey"]));
                GV_Assessment.DataSource = nTable;
                GV_Assessment.DataBind();
                //Session["created"] = false;
            }
        }
        public string ConvertResult(string values)
        {
            return values == "True" ? "False" : "True";
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
        int m_IsChange = 0;
        protected void GV_Assessment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtAssessmentName = (Label)e.Row.FindControl("txtAssessmentName");
                if (txtAssessmentName.Text.Length > 0)
                {
                    //mCategoryName = txtAssessmentName.Text;
                    m_IsChange++;
                }
                if ((float)m_IsChange % 2 == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(255, 244, 201);
                }
                // Display the company name in italics.


            }


        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
    }
}