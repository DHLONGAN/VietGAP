using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using System.Globalization;
using TNLibrary.SYS;
using TNLibrary.WEB;

namespace Management.Culture
{
    public partial class HarvestedForSale_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CU0011");
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
                             txtCode.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtQuantityHarvested.Enabled = false;
                             txtQuantitySale.Enabled = false;
                             txtWhereToBuy.Enabled = false;
                             DDLSeeds.Enabled = false;
                         }
                         else if (!nUserLogin.Role.Add && nKey == "0")
                         {
                             cmdSave.Visible = false;
                             txtCode.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtQuantityHarvested.Enabled = false;
                             txtQuantitySale.Enabled = false;
                             txtWhereToBuy.Enabled = false;
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
            HarvestedForSale_Info info = new HarvestedForSale_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
                           }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }
            txtCode.Text = info.Code;
            txtQuantityHarvested.Text = info.QuantityHarvested.ToString();
            txtQuantitySale.Text = info.QuantitySale.ToString();
            txtWhereToBuy.Text = info.WhereToBuy;
            DDLUnit.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.UnitKey.ToString());
            DDLSeeds.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = (Select SeedsKey from PUL_SeedProcess where SeedProcessKey  = (Select SeedsKey from PUL_HarvestedForSale where HarvestedForSaleKey = " + info.HarvestedForSaleKey.ToString() + "))");
        }
        protected void SaveInfo()
        {
            //HarvestedForSale_Info info = new HarvestedForSale_Info(int.Parse(txtKey.Text));
            //info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.Code = txtCode.Text;
            //info.QuantityHarvested = float.Parse(txtQuantityHarvested.Text);
            //info.QuantitySale = float.Parse(txtQuantitySale.Text);
            //info.WhereToBuy = txtWhereToBuy.Text;
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