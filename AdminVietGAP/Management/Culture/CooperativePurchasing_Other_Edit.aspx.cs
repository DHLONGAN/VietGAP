using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.WEB;
using System.Globalization;
using TNLibrary.Culture;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class CooperativePurchasing_Other_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0016");
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
                        LoadDataToToolboxWeb.DropDown_DDL(DDLUnit, "Select ID, Name from PUL_Unit", false);
                        string SQL = @"SELECT     dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds.SeedsName 
                        FROM         dbo.PUL_Seeds WHERE dbo.PUL_Seeds.SeedsKey IN (Select SeedKey from PUL_Seed_Cooperative where CooperativeKey =  " + Convert.ToInt16(Session["CooperativeKey"]) + ")";
                        LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, SQL, false);
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
            CooperativePurchasing_Other_Info info = new CooperativePurchasing_Other_Info(Key);
            if (Key != 0)
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            DDLSeeds.SelectedValue = info.SeedKey.ToString();
            txtcode.Text = info.Code;
            txtQuantity.Text = info.Quantity.ToString();
            txtBaskets.Text = info.Baskets.ToString();
            DDLUnit.SelectedValue = info.UnitKey.ToString();
            txtMember.Text = info.Name;
            txtSolution.Text = info.Solution;
            DDLEvaluate.SelectedValue = info.Evaluate.ToString();
            txtPrice.Text = info.Price.ToString();
        }
        protected void SaveInfo()
        {

            if (txtQuantity.Text == "" || (txtQuantity.Text.ToInt() < 1))
            {
                lberr.Text = "Số lượng thu mua không được bé hơn 1";
                return;
            }
            if (txtPrice.Text == "")
            {
                lberr.Text = "Vui lòng kiểm tra giá mua";
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
            CooperativePurchasing_Other_Info info = new CooperativePurchasing_Other_Info(txtKey.Text.ToInt());
            info.Baskets = txtBaskets.Text.ToInt();
            info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.Quantity = txtQuantity.Text.ToInt();
            info.UnitKey = DDLUnit.SelectedValue.ToInt();
            info.CooperativeKey = Session["CooperativeKey"].ToInt();
            info.Price = txtPrice.Text.ToInt();
            info.Name = txtMember.Text;
            info.SeedKey = DDLSeeds.SelectedValue.ToInt();
            info.Code = txtcode.Text;
            info.Solution = txtSolution.Text;
            info.Evaluate = DDLEvaluate.SelectedValue.ToInt();
            info.Save();
            CloseForm();

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