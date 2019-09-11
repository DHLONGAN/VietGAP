using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using TNLibrary.WEB;
using TNLibrary.Culture;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class Pesticide_Use_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CU0005");
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
                             cmdSave.Visible = false;
                             DDLPesticide.Enabled = false;
                             DDLEquipment.Enabled = false;
                             DDLSeeds.Enabled = false;
                             txtArea.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtDosage.Enabled = false;
                             txtDose.Enabled = false;
                             txtPestName.Enabled = false;
                         }
                         else if (!nUserLogin.Role.Add && nKey == "0")
                         {
                             cmdSave.Visible = false;
                             DDLPesticide.Enabled = false;
                             DDLEquipment.Enabled = false;
                             DDLSeeds.Enabled = false;
                             txtArea.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtDosage.Enabled = false;
                             txtDose.Enabled = false;
                             txtPestName.Enabled = false;   
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
            Pesticide_Use_Info info = new Pesticide_Use_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.DateTimeUse.ToString("dd/MM/yyyy");
            }
            DDLPesticide.Text = LoadDataToToolboxWeb.GetName("SELECT Trade_Name FROM PUL_Pesticides where PesticideKey = " + info.PesticideKey.ToString());
            txtArea.Text = info.Area;
            txtDosage.Text = info.Dosage;
            txtDose.Text = info.Dose.ToString();
            txtPestName.Text = info.PestName;
            txtSolution.Text = info.Solution;
            DDLSeeds.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = (Select SeedsKey from PUL_SeedProcess where SeedProcessKey  = (Select SeedKey from PUL_Pesticide_Use where PesticideUseKey = " + info.PesticideUseKey.ToString() + "))");
            DDLEquipment.Text = LoadDataToToolboxWeb.GetName("SELECT EquipmentName FROM PUL_Equipment where EquipmentKey = " + info.EquipmentKey.ToString());
            txtQuarantinePeriod.Text = info.QuarantinePeriod;
            DDLUnit.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.UnitKey.ToString());
        }
        protected void SaveInfo()
        {
            //Pesticide_Use_Info info = new Pesticide_Use_Info(int.Parse(txtKey.Text));
            //info.DateTimeUse = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.PesticideKey = int.Parse(DDLPesticide.SelectedValue);
            ////info.SeedKey = int.Parse(DDLSeed.SelectedValue);
            //info.Dose = float.Parse(txtDose.Text);
            //info.Dosage = txtDosage.Text;
            //info.Area = txtArea.Text;
            //info.EquipmentKey = int.Parse(DDLEquipment.SelectedValue);
            //info.SeedKey = DDLSeeds.SelectedValue.ToInt();
            //info.PestName = txtPestName.Text;
            //info.Solution = txtSolution.Text;
            //info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            //info.QuarantinePeriod = txtQuarantinePeriod.Text;
            //info.UnitKey = int.Parse(DDLUnit.SelectedValue);
            //info.Save();
        }
        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}