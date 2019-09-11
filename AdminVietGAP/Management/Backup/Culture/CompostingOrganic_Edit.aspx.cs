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
    public partial class CompostingOrganic_Edit : System.Web.UI.Page
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
            CompostingOrganic_Info info = new CompostingOrganic_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.StartDate.ToString("dd/MM/yyyy");

            }
            txtName.Text = LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_FertilizerOrganic where FertilizerOrganicKey = " + info.FertilizerOrganicKey.ToString());
            txtQuantity.Text = info.Quantity + " " + LoadDataToToolboxWeb.GetName("SELECT Name FROM PUL_Unit where ID = " + info.UnitKey.ToString());
            txtMethod.Text = info.Method;
            txtCompostingDates.Text = info.CompostingDates.ToString() + " ngày";
        }
        protected void SaveInfo()
        {
            //CompostingOrganic_Info info = new CompostingOrganic_Info(int.Parse(txtKey.Text));
            //info.DatetimeBuy = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.FertilizerKey = int.Parse(DDLFertilizer.SelectedValue);
            //info.Quantity = float.Parse(txtQuantity.Text);
            //info.CompanyKey = 1;
            //info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            ////info.Price = txtPrice.Text;
            //info.Address = txtAddress.Text;
            //DDLSeeds.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = " + info.SeedsKey.ToString());
            //info.UnitKey = int.Parse(DDLUnit.SelectedValue);
            //info.Save();
        }
    }
}