using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Categories;
using TNLibrary.WEB;
using TNLibrary.SYS;

namespace Management.Categories
{
    public partial class Companie_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0011");
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
                        LoadDataToToolboxWeb.DropDown_DDL(DLLCountry, "SELECT CountryKey,CountryName FROM PUL_Country", false);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                            txtAdddress.Enabled = false;
                            txtCity.Enabled = false;
                            txtCompanyName.Enabled = false;
                            DLLCountry.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                            txtAdddress.Enabled = false;
                            txtCity.Enabled = false;
                            txtCompanyName.Enabled = false;
                            DLLCountry.Enabled = false;
                        }
                    }
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            Companie_Info info = new Companie_Info(Key);
            txtCompanyName.Text = info.CompanyName;
            txtAdddress.Text = info.Address;
            txtCity.Text = info.City;
            DLLCountry.SelectedValue = info.Country.ToString();
        }
        protected void SaveInfo()
        {
            Companie_Info info = new Companie_Info(int.Parse(txtKey.Text));
            info.CompanyName = txtCompanyName.Text;
            info.Address = txtAdddress.Text;
            info.City = txtCity.Text;
            info.Country = int.Parse(DLLCountry.SelectedValue);
            info.Save();
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
    }
}