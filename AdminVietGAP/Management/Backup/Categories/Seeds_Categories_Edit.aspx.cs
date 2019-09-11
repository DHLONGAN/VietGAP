using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using TNLibrary.Categories;

namespace Management.Categories
{
    public partial class Seeds_Categories_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nKey = this.Request["key"];
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0001");
                txtKey.Text = "0";
                int Key = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out Key))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(Key);

                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                        }
                        //User_Role_Info nUser = new User_Role_Info(Session["UserLogin"].ToString(),
                    }
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            Seeds_Categorie_Info info = new Seeds_Categorie_Info(Key);
            txtCategoryName.Text = info.CategoryName;
            txtColor.Text = info.Color;
        }
        protected void SaveInfo()
        {
            Seeds_Categorie_Info info = new Seeds_Categorie_Info(int.Parse(txtKey.Text));
            info.CategoryName = txtCategoryName.Text;
            info.Color = txtColor.Text;
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