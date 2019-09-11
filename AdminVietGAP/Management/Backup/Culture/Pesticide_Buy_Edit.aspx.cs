using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

using TNLibrary.Culture;
using TNLibrary.WEB;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class Pesticide_Buy_Edit : System.Web.UI.Page
    {
        public string address = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Pesticide_Buy_Data.GetAddressList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                address += "\"" + dt.Rows[i]["Address"].ToString() + "\",";
            }
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0004");
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
                            DDLPesticide.Enabled = false;
                            txtAddress.Enabled = false;
                            txtDatetime.Enabled = false;
                            //txtPrice.Enabled = false;
                            txtQuantity.Enabled = false;
                            DDLSeeds.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            //cmdSave.Visible = false;
                            DDLPesticide.Enabled = false;
                            txtAddress.Enabled = false;
                            txtDatetime.Enabled = false;
                            //txtPrice.Enabled = false;
                            txtQuantity.Enabled = false;
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
            Pesticide_Buy_Info info = new Pesticide_Buy_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
                
            }
            else
            {
                txtDatetime.Text = info.DatetimeBuy.ToString("dd/MM/yyyy");
            }
            DDLUnit.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.UnitKey.ToString());
            DDLPesticide.Text = LoadDataToToolboxWeb.GetName("SELECT Trade_Name FROM PUL_Pesticides where PesticideKey = " + info.PesticideKey.ToString());
            txtQuantity.Text = info.Quantity.ToString();
            //txtPrice.Text = info.Price;
            txtAddress.Text = info.Address;
            DDLSeeds.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = (Select SeedsKey from PUL_SeedProcess where SeedProcessKey  = (Select SeedsKey from PUL_Pesticide_Buy where PesticideBuyKey = " + info.PesticideBuyKey.ToString() + " ))");
        }
        protected void SaveInfo()
        {
            //Pesticide_Buy_Info info = new Pesticide_Buy_Info(int.Parse(txtKey.Text));
            //info.DatetimeBuy = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.PesticideKey = int.Parse(DDLPesticide.SelectedValue);
            //info.Quantity = float.Parse(txtQuantity.Text);
            ////info.Price = txtPrice.Text;
            //info.Address = txtAddress.Text;
            //info.CompanyKey = 1;
            //info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            //info.SeedsKey = DDLSeeds.SelectedValue.ToInt();
            //info.UnitKey = int.Parse(DDLUnit.SelectedValue);
            //info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}