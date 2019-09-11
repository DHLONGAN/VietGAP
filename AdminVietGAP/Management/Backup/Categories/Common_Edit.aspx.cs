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
    public partial class Common_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nKey = this.Request["key"];
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0018");
                int Key = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out Key))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(nKey);

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
        protected void LoadInfo(string Key)
        {
            if (Key != "0")
            {
                string[] keys = Key.Split('-');

                if (keys[1].ToInt() == 1)
                {
                    Fertilizer_Common_Info info = new Fertilizer_Common_Info(keys[0].ToInt());
                    txtCommon_Name.Text = info.Common_Name;
                    rdbType.SelectedValue = "1";
                }
                else
                {
                    Pesticide_Common_Info info2 = new Pesticide_Common_Info(keys[0].ToInt());
                    txtCommon_Name.Text = info2.Common_Name;
                    rdbType.SelectedValue = "2";
                }
                txtKey.Text = keys[0];
            }
        }
        protected void SaveInfo()
        {
            if (rdbType.SelectedValue == "1")
            {
                Fertilizer_Common_Info info = new Fertilizer_Common_Info(int.Parse(txtKey.Text));
                info.Common_Name = txtCommon_Name.Text;
                info.Save();
            }
            else
            {
                Pesticide_Common_Info info = new Pesticide_Common_Info(int.Parse(txtKey.Text));
                info.Common_Name = txtCommon_Name.Text;
                info.Save();
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
    }
}