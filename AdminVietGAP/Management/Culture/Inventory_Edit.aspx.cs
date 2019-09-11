using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Book;
using TNLibrary.WEB;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class Inventory_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0002");
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
            Inventory_Info info = new Inventory_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
                txtExpireDate.Text = info.ExpireDate.ToString("dd/MM/yyyy");// +" ngày";
            }
            if (info.Type == 1)
            {
                txtName.Text =  LoadDataToToolboxWeb.GetName("SELECT TradeName FROM PUL_Fertilizers where FertilizersKey = " + info.FertilizersPesticidesKey);
            }
            else
            {
                txtName.Text = LoadDataToToolboxWeb.GetName("SELECT Trade_Name FROM PUL_Pesticides where PesticideKey = " + info.FertilizersPesticidesKey);
            }
            //txtName.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_FertilizerOrganic where FertilizerOrganicKey = " + info.FertilizerOrganicKey.ToString());
            txtQuantity.Text = info.Quantity + " " + LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.UnitKey.ToString());
            
        }
        protected void SaveInfo()
        {
        }
    }
}