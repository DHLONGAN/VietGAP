using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Categories;
using TNLibrary.SYS;

namespace Management.Categories
{
    public partial class ProvincesCities_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0015");
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
            ProvincesCitie_Info info = new ProvincesCitie_Info(Key);
            txtProvincesCities_ID.Text = info.ProvincesCities_ID;
            txtProvincesCities_Name.Text = info.ProvincesCities_Name;
            txtDescription.Text = info.Description;
        }
        protected void SaveInfo()
        {
            ProvincesCitie_Info info = new ProvincesCitie_Info(int.Parse(txtKey.Text));
            info.ProvincesCities_ID = txtProvincesCities_ID.Text;
            info.ProvincesCities_Name = txtProvincesCities_Name.Text;
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