using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Categories;
using TNLibrary.SYS;
using TNLibrary.WEB;

namespace Management.Categories
{
    public partial class District_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0016");
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
                        LoadDataToToolboxWeb.DropDown_DDL(DDLProvincesCities, "Select ProvincesCities_Key,ProvincesCities_Name from PUL_ProvincesCities", false);
                        LoadInfo(nEnvironmentsKey);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            //cmdSave.Visible = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            //cmdSave.Visible = false;
                        }
                    }
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            District_Info info = new District_Info(Key);
            txtDisID.Text = info.DisID.ToString();
            txtName.Text = info.Name;
            DDLProvincesCities.SelectedValue = info.ProvincesCities_ID.ToString();
            txtDescription.Text = info.Description;
        }
        protected void SaveInfo()
        {
            District_Info info = new District_Info(int.Parse(txtKey.Text));
            info.ProvincesCities_ID = DDLProvincesCities.SelectedValue.ToInt();
            info.DisID = txtDisID.Text.ToInt();
            info.Name = txtName.Text;
            info.Description = txtDescription.Text;
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