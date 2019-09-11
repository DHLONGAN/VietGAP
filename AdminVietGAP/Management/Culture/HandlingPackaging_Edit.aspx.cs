using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TNLibrary.WEB;
using TNLibrary.Culture;
using System.Globalization;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class HandlingPackaging_Edit : System.Web.UI.Page
    {
        public string Treatment = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0008");
                DataTable dt = HandlingPackaging_Data.GetTreatmentList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Treatment += "\"" + dt.Rows[i]["Treatment"].ToString() + "\",";
                }
                string nKey = this.Request["key"];
                txtKey.Text = "0";
                int _nKey = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out _nKey))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(_nKey);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            //cmdSave.Visible = false;
                            txtDatetime.Enabled = false;
                            txtPlace.Enabled = false;
                            txtTreatment.Enabled = false;
                            txtType.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            //cmdSave.Visible = false;
                            txtDatetime.Enabled = false;
                            txtPlace.Enabled = false;
                            txtTreatment.Enabled = false;
                            txtType.Enabled = false;
                        }
                    }
                }
            }
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            CloseForm();
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);

        }
        protected void LoadInfo(int Key)
        {
            HandlingPackaging_Info info = new HandlingPackaging_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }
            txtType.Text = info.Type;
            txtPlace.Text = info.Place;
            txtTreatment.Text = info.Treatment;
        }
        protected void SaveInfo()
        {
            HandlingPackaging_Info info = new HandlingPackaging_Info(int.Parse(txtKey.Text));
            info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.Type = txtType.Text;
            info.Place = txtPlace.Text;
            info.Treatment = txtTreatment.Text;
            info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}