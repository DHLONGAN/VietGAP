using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using TNLibrary.SYS;
using TNLibrary.WEB;
using System.Globalization;

namespace Management.Culture
{
    public partial class EditSale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0006");
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
            if (Key != 0)
            {
                LoadDataToToolboxWeb.DropDown_DDL(DDLUnit, "Select ID, Name from PUL_Unit", false);
                CooperativePurchasing_Info info = new CooperativePurchasing_Info(Key);
                HarvestedForSale_Info info_ = new HarvestedForSale_Info(info.HarvestedForSaleKey);
                txtSeedName.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = (Select SeedsKey from PUL_SeedProcess where SeedProcessKey  = (Select SeedsKey from PUL_HarvestedForSale where HarvestedForSaleKey = " + info.HarvestedForSaleKey.ToString() + "))");
                txtQuantity.Text = info.Quantity.ToString();
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
                txtBaskets.Text = info.Baskets.ToString();
                DDLUnit.SelectedValue = info.UnitKey.ToString();
                txtcode.Text = info_.Code;
                txtPrice.Text = info.Price.ToString();
                txtSolution.Text = info.Solution;
                DDLEvaluate.SelectedValue = info.Evaluate.ToString();
                txtMember.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Member where [Key] = " + info_.MemberKey.ToString());
            }
        }
        protected void SaveInfo()
        {
            if (txtKey.Text != "0")
            {
                CooperativePurchasing_Info info = new CooperativePurchasing_Info(txtKey.Text.ToInt());
                HarvestedForSale_Info info_ = new HarvestedForSale_Info(info.HarvestedForSaleKey);
                if (info_.HarvestedForSaleKey != 0)// mua hàng của xã viên HTX
                {
                    if (txtQuantity.Text.ToInt() > info_.QuantityHarvested.ToInt())
                    {
                        lberr.Text = "Số lượng thu mua không được lớn hơn số lượng xuất bán";
                        return;
                    }
                    //info.Type = 1;
                }
                //else
                //{
                //    info.Type = 1;
                //}
                if (txtQuantity.Text == "" || (txtQuantity.Text.ToInt() < 0))
                {
                    lberr.Text = "Số lượng thu mua không được bé hơn 0";
                    return;
                }
                if (txtBaskets.Text == "" || (txtBaskets.Text.ToInt() < 0))
                {
                    lberr.Text = "Số lượng sọt rổ không được bé hơn 0";
                    return;
                }
                if (txtPrice.Text == "" || (txtPrice.Text.ToInt() < 0))
                {
                    lberr.Text = "Tổng tiền không được bé hơn 0";
                    return;
                }
                
                if (txtPrice.Text == "")
                {
                    lberr.Text = "Vui lòng kiểm tra giá mua";
                    return;
                }
                info.Baskets = txtBaskets.Text.ToInt();
                info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                info.Quantity = txtQuantity.Text.ToInt();
                info.UnitKey = DDLUnit.SelectedValue.ToInt();
                info.HarvestedForSaleKey = info_.HarvestedForSaleKey;
                info.SeedProcessKey = info_.SeedsKey;
                info.Price = txtPrice.Text.ToInt();
                info.Solution = txtSolution.Text;
                info.Evaluate = DDLEvaluate.SelectedValue.ToInt();
                info.Save();
                CloseForm();
            }

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