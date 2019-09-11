using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using TNLibrary.WEB;
using System.Globalization;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class LandUse_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0001");
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
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "SELECT SeedsKey,SeedsName FROM PUL_Seeds", false);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            //cmdSave.Visible = false;
                            txtAction.Enabled = false;
                            txtDatetime.Enabled = false;
                            txtSolution.Enabled = false;
                            txtNotice.Enabled = false;
                            txtReason.Enabled = false;
                            DDLSeeds.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            //cmdSave.Visible = false;
                            txtAction.Enabled = false;
                            txtDatetime.Enabled = false;
                            txtSolution.Enabled = false;
                            txtNotice.Enabled = false;
                            txtReason.Enabled = false;
                            DDLSeeds.Enabled = false;
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
            LandUse_Info info = new LandUse_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }
            txtAction.Text = info.Action;
            txtReason.Text = info.Reason;
            txtSolution.Text = info.Solution;
            txtNotice.Text = info.Note;
            DDLSeeds.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = " + info.SeedKey.ToString());
        }
        protected void SaveInfo()
        {
            //LandUse_Info info = new LandUse_Info(int.Parse(txtKey.Text));
            //info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.SeedKey = DDLSeeds.SelectedValue.ToInt();
            //info.Action = txtAction.Text;
            //info.Reason = txtReason.Text;
            //info.Solution = txtSolution.Text;
            //info.Note = txtNotice.Text;
            //info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            //info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}