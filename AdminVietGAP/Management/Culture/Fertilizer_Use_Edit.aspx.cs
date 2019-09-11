using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using TNLibrary.Culture;
using TNLibrary.WEB;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class Fertilizer_Use_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CU0003");
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
                             DDLFertilizer.Enabled = false;
                             txtQuantity.Enabled = false;
                             //txtArea.Enabled = false;
                             DDLEquipment.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtFormulaUsed.Enabled = false;
                             txtHowtouse.Enabled = false;
                             txtParcel.Enabled = false;
                             DDLSeeds.Enabled = false;
                         }
                         else if (!nUserLogin.Role.Add && nKey == "0")
                         {
                             //cmdSave.Visible = false;
                             DDLFertilizer.Enabled = false;
                             txtQuantity.Enabled = false;
                             //txtArea.Enabled = false;
                             DDLEquipment.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtFormulaUsed.Enabled = false;
                             txtHowtouse.Enabled = false;
                             txtParcel.Enabled = false;
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
            Fertilizer_Use_Info info = new Fertilizer_Use_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.DateTimeUse.ToString("dd/MM/yyyy");
            }
            DDLUnit.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.UnitKey.ToString());
            //txtArea.Text = info.Area;
            txtParcel.Text = info.Parcel.ToString(); ;
            txtFormulaUsed.Text = info.FormulaUsed.ToString();
            DDLEquipment.Text = LoadDataToToolboxWeb.GetName("SELECT EquipmentName FROM PUL_Equipment where EquipmentKey = " + info.CooperativeKey.ToString());
            DDLSeeds.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = (Select SeedsKey from PUL_SeedProcess where SeedProcessKey  = (Select SeedKey from PUL_Fertilizer_Use where FertilizerUseKey = " + info.FertilizerUseKey.ToString() + " ))");
            txtQuantity.Text = info.Quantity;
            txtHowtouse.Text = info.Howtouse;
            txtArea.Text = info.Area;
            txtQuarantinePeriod.Text = info.QuarantinePeriod;
            DDLUnit.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.UnitKey.ToString());
        }
        protected void SaveInfo()
        {
            //Fertilizer_Use_Info info = new Fertilizer_Use_Info(int.Parse(txtKey.Text));
            //info.DateTimeUse = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.FertilizerKey = int.Parse(DDLFertilizer.SelectedValue);
            //info.SeedKey = DDLSeeds.SelectedValue.ToInt();
            //info.CooperativeKey = int.Parse(DDLEquipment.SelectedValue);
            //info.Quantity = txtQuantity.Text;
            //info.FormulaUsed = float.Parse(txtFormulaUsed.Text);
            //info.Howtouse = txtHowtouse.Text;
            ////info.Area = txtArea.Text;
            //info.Parcel = txtParcel.Text;
            //info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            //info.QuarantinePeriod = txtQuarantinePeriod.Text;
            //info.Area = txtArea.Text;
            //info.UnitKey = int.Parse(DDLUnit.SelectedValue);
            //info.Save();
        }
        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}