using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using TNLibrary.WEB;

namespace Management.Culture
{
    public partial class Soiltreatment_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    //LoadDataToToolboxWeb.DropDown_DDL(DLLAdditives, "SELECT AdditivesKey,AdditivesName FROM PUL_Additives", false);
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
            Soiltreatment_Info info = new Soiltreatment_Info(Key);
            if (Key == 0)
            {
                DateTime dt = DateTime.Now;
                String.Format("dd/mm/yy", dt);
                txtDatetime.Text = dt.ToString();
            }
            else
            {
                DateTime dt = info.Datetime;
                String.Format("dd/mm/yyyy", dt);
                txtDatetime.Text = dt.ToString();
            }
            DLLAdditives.Text = LoadDataToToolboxWeb.GetName("SELECT AdditivesName FROM PUL_Additives where AdditivesKey = " + info.AdditivesKey.ToString());
            txtQuantity.Text = info.Quantity;
            txtHowtouse.Text = info.Howtouse;
            txtArea.Text = info.Area;
            txtWeather.Text = info.Weather;
        }
        protected void SaveInfo()
        {
            //Soiltreatment_Info info = new Soiltreatment_Info(int.Parse(txtKey.Text));
            //string[] cut = txtDatetime.Text.Split('-');
            //string ndate = cut[1] + "/" + cut[0] + "/" + cut[2];
            //info.Datetime = Convert.ToDateTime(ndate);
            //info.AdditivesKey = int.Parse(DLLAdditives.SelectedValue);
            //info.Quantity = txtQuantity.Text;
            //info.Howtouse = txtHowtouse.Text;
            //info.Area = txtArea.Text;
            //info.Weather = txtWeather.Text;
            //info.Save();
        }
    }
}