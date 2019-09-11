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
using TNLibrary.Sys;
using TNLibrary.Categories;

namespace Management.Culture
{
    public partial class SeedProcess_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0001");
                string nKey = this.Request["key"];
                txtKey.Text = "0";
                int nAssessmentKey = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out nAssessmentKey))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(nAssessmentKey);
                      
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            //cmdSave.Visible = false;
                            //txtDateBuy.Enabled = false;
                            txtDateOfManufacture.Enabled = false;
                            txtQuantity.Enabled = false;
                            //txtReasons.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            //cmdSave.Visible = false;
                            //.Enabled = false;
                            txtDateOfManufacture.Enabled = false;
                            txtQuantity.Enabled = false;
                            //txtReasons.Enabled = false;
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
            SeedProces_Info info = new SeedProces_Info(Key);
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + info.CooperativeKey, false);
            if (Key == 0)
            {
                txtDateOfManufacture.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDateBuy.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDateOfManufacture.Text = info.DateOfManufacture.ToString("dd/MM/yyyy");
                txtDateBuy.Text = info.DateBuy.ToString("dd/MM/yyyy");
            }
            DDLSeedsName.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = " + info.SeedsKey.ToString());
            DDLQuantityUnit.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.QuantityUnit.ToString());
            DDLAreaUnit.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.AreaUnit.ToString());
            txtQuantity.Text = info.Quantity.ToString();
            txtArea.Text = info.Area.ToString();
            txtParcel.Text = info.Parcel;
            
        }
        protected void SaveInfo()
        {
            //SeedProces_Info info = new SeedProces_Info(int.Parse(txtKey.Text));
            //info.DateOfManufacture = DateTime.ParseExact(txtDateOfManufacture.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.DateBuy = DateTime.ParseExact(txtDateBuy.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.SeedsKey = DDLSeedsName.SelectedValue;
            ////info.PesticideKey = int.Parse(DDLPesticideName.SelectedValue);
            ////info.Status = rdbStatus.SelectedValue == "1" ? true : false;
            ////info.Reasons = txtReasons.Text;
            //info.Quantity = float.Parse(txtQuantity.Text);
            //info.Parcel = txtParcel.Text;
            //info.Area = float.Parse(txtArea.Text);
            //info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            //info.AreaUnit = int.Parse(DDLAreaUnit.SelectedValue);
            //info.QuantityUnit = int.Parse(DDLQuantityUnit.SelectedValue);
            ////info.MemberKey = int.Parse(DDLMember.SelectedValue);
            ////info.CooperativeKey = int.Parse(DDLCooperative.SelectedValue);
            //info.Save();
        }
        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}