using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using System.Data;
using TNLibrary.WEB;
using System.Globalization;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class Harvests_Edit : System.Web.UI.Page
    {
        public string cmm = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0006");
                DataTable dt = Harvests_Data.GetAddressList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmm += "\"" + dt.Rows[i]["Address"].ToString() + "\",";
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
                        LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "SELECT SeedsKey,SeedsName FROM PUL_Seeds", false);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                            DDLSeeds.Enabled = false;
                            txtCode.Enabled = false;
                            txtDateOn.Enabled = false;
                            txtLoss.Enabled = false;
                            txtSlotAfter.Enabled = false;
                            txtSlotBefor.Enabled = false;
                            txtWeightAfter.Enabled = false;
                            txtWeightBefor.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                            DDLSeeds.Enabled = false;
                            txtCode.Enabled = false;
                            txtDateOn.Enabled = false;
                            txtLoss.Enabled = false;
                            txtSlotAfter.Enabled = false;
                            txtSlotBefor.Enabled = false;
                            txtWeightAfter.Enabled = false;
                            txtWeightBefor.Enabled = false;
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
            Harvest_Info info = new Harvest_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDateOn.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDateOn.Text = info.DateOn.ToString("dd/MM/yyyy");
            }
            DDLSeeds.SelectedValue = info.SeedsKey.ToString();
            txtCode.Text = info.Code;
            txtWeightBefor.Text = info.WeightBefor.ToString();
            txtSlotBefor.Text = info.SlotBefor;
            txtWeightAfter.Text = info.WeightAfter.ToString();
            txtSlotAfter.Text = info.SlotAfter;
            txtLoss.Text = info.Loss;
        }
        protected void SaveInfo()
        {
            Harvest_Info info = new Harvest_Info(int.Parse(txtKey.Text));
            info.DateOn = DateTime.ParseExact(txtDateOn.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.SeedsKey = DDLSeeds.SelectedValue.ToInt();
            info.Code = txtCode.Text;
            info.WeightBefor = int.Parse(txtWeightBefor.Text);
            info.SlotBefor = txtSlotBefor.Text;
            info.WeightAfter = int.Parse(txtWeightAfter.Text);
            info.SlotAfter = txtSlotAfter.Text;
            info.Loss = txtLoss.Text;
            info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}