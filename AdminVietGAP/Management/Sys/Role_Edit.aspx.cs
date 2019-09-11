using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.WEB;
using TNLibrary.SYS;
using TNLibrary.Sys;

namespace Management.Sys
{
    public partial class Role_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nKey = this.Request["key"];
            txtKey.Text = "0";
            string key = "0";
            if (nKey != null)
            {
                if (nKey != "")
                {
                    key = txtKey.Text = nKey;
                }
                if (!IsPostBack)
                {
                    LoadInfo(key);
                    //if (key != "0")
                    {
                        LoadDataToToolboxWeb.DropDown_DDL(DDLWeb_Menu, "SELECT MenuKey,MenuName FROM SYS_Web_Menu", false);
                    }
                    //else
                    //{
                    //    LoadDataToToolboxWeb.DropDown_DDL(DDLWeb_Menu, "SELECT MenuKey,MenuName FROM SYS_Web_Menu where MenuKey NOT IN(Select RoleID from SYS_Roles)", false);
                    //}
                    SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
                    nUserLogin.CheckRole("SY0004");
                    if (!nUserLogin.Role.Edit)
                    {
                        cmdSave.Visible = false;
                        DDLWeb_Menu.Enabled = false;
                    }
                    //User_Role_Info nUser = new User_Role_Info(Session["UserLogin"].ToString(),
                }
            }
        }
        protected void LoadInfo(string Key)
        {
            Role_Info info = new Role_Info(Key);
            DDLWeb_Menu.SelectedValue = info.MenuKey.ToString();
            txtRoleName.Text = info.RoleName;
            txtRoleID.Text = info.RoleID;
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            CloseForm();
        }

        protected void SaveInfo()
        {
            Role_Info info;
            if (txtKey.Text == "0")
            {
                info = new Role_Info(new Guid().ToString());
            }
            else
            {
                info = new Role_Info(txtKey.Text);
            }

            info.RoleID = txtRoleID.Text;
            info.RoleName = txtRoleName.Text;
            info.MenuKey = int.Parse(DDLWeb_Menu.SelectedValue);
            info.Save();
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }
    }
}