using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using TNLibrary.WEB;
using TNLibrary.Categories;

namespace Management.Categories
{
    public partial class Ward_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0017");
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
                        LoadDataToToolboxWeb.DropDown_DDL(DDLDistrict, "Select ID,Name from PUL_District", false);
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
            Ward_Info info = new Ward_Info(Key);
            txtWardID.Text = info.WardID.ToString();
            txtName.Text = info.Name;
            DDLDistrict.SelectedValue = info.District_ID.ToString();
            txtDescription.Text = info.Description;
        }
        protected void SaveInfo()
        {
            Ward_Info info = new Ward_Info(int.Parse(txtKey.Text));
            info.District_ID = DDLDistrict.SelectedValue.ToInt();
            info.WardID = txtWardID.Text.ToInt();
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