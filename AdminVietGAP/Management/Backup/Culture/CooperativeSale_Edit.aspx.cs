using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.WEB;
using TNLibrary.Culture;
using TNLibrary.SYS;
using System.Globalization;

namespace Management.Culture
{
    public partial class CooperativeSale_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0015");
                string nKey = this.Request["key"];
                txtKey.Text = "0";
                int nEnvironmentsKey = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out nEnvironmentsKey))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(nEnvironmentsKey);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                        }
                    }
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            LoadDataToToolboxWeb.DropDown_DDL(DDLUnit, "Select ID, Name from PUL_Unit", false);
            LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "Select SeedsKey, SeedsName from PUL_Seeds where SeedsKey IN(Select SeedKey from PUL_Seed_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToString() + ")", false);
            CooperativeSale_Info info = new CooperativeSale_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
                DDLUnit.SelectedValue = "3";
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
                DDLUnit.SelectedValue = info.UnitKey.ToString();

            }
            
            txtQuantity.Text = info.Quantity.ToString();

            DDLSeeds.SelectedValue = info.SeedKey.ToString();
            txtAddress.Text = info.Address;
            txtcode.Text = info.Code;
            txtPrice.Text = info.Price.ToString();


        }
        protected void SaveInfo()
        {
            if (txtPrice.Text == "" || txtPrice.Text.ToInt() < 0)
            {
                lberr.Text = "Vui lòng kiểm tra lại giá tiền";
                return;
            }
            if (txtQuantity.Text == "" || txtQuantity.Text.ToInt() < 1)
            {
                lberr.Text = "Vui lòng kiểm tra lại khối lượng";
                return;
            }
            CooperativeSale_Info info = new CooperativeSale_Info(txtKey.Text.ToInt());

            info.SeedKey = DDLSeeds.SelectedValue.ToInt();
            info.Quantity = txtQuantity.Text.ToInt();
            info.Address = txtAddress.Text;
            info.Code = txtcode.Text;
            info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.Quantity = txtQuantity.Text.ToInt();
            info.UnitKey = DDLUnit.SelectedValue.ToInt();
            //info.HarvestedForSaleKey = info_.HarvestedForSaleKey;
            info.CooperativeKey = Session["CooperativeKey"].ToInt();
            //info.SeedProcessKey = info_.SeedsKey;
            info.Price = txtPrice.Text.ToInt();
            info.Save();

            CloseForm();
            //Equipment_Info info = new Equipment_Info(int.Parse(txtKey.Text));
            //info.EquipmentName = txtEquipmentName.Text;
            //info.Notice = txtNotice.Text;
            //info.Save();
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
    }
}