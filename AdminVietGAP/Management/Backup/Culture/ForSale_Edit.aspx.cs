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
    public partial class ForSale_Edit : System.Web.UI.Page
    {
        public string PlaceOfBuy = "";
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CU0007");
                 DataTable dt = ForSale_Data.GetAddressList();
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     PlaceOfBuy += "\"" + dt.Rows[i]["PlaceOfBuy"].ToString() + "\",";
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
                             txtBill.Enabled = false;
                             txtCode.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtLoss.Enabled = false;
                             txtPlaceOfBuy.Enabled = false;
                             txtSlot.Enabled = false;
                             txtWeight.Enabled = false;
                         }
                         else if (!nUserLogin.Role.Add && nKey == "0")
                         {
                             cmdSave.Visible = false;
                             DDLSeeds.Enabled = false;
                             txtBill.Enabled = false;
                             txtCode.Enabled = false;
                             txtDatetime.Enabled = false;
                             txtLoss.Enabled = false;
                             txtPlaceOfBuy.Enabled = false;
                             txtSlot.Enabled = false;
                             txtWeight.Enabled = false;
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
            ForSale_Info info = new ForSale_Info(Key);
            
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }
            DDLSeeds.SelectedValue = info.SeedsKey.ToString();
            txtCode.Text = info.Code;
            txtWeight.Text = info.Weight.ToString();
            txtSlot.Text = info.Slot;
            txtPlaceOfBuy.Text = info.PlaceOfBuy;
            txtBill.Text = info.Bill;
        }
        protected void SaveInfo()
        {
            ForSale_Info info = new ForSale_Info(int.Parse(txtKey.Text));
            info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.SeedsKey = DDLSeeds.SelectedValue.ToInt();
            info.Code = txtCode.Text;
            info.Weight = int.Parse(txtWeight.Text);
            info.Slot = txtSlot.Text;
            info.PlaceOfBuy = info.PlaceOfBuy;
            info.Bill = txtBill.Text;
            info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}