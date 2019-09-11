using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.WEB;
using TNLibrary.SYS;
using TNLibrary.SYS.Users;

namespace Management.Sys
{
    public partial class Users_Roles_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
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
                    LoadDataToToolboxWeb.DropDown_DDL(DDLUsers, "SELECT UserKey,UserName FROM SYS_Users WHERE UserKey = '" + Session["ukey"].ToString() + "'", false);
                    if (key != "0")
                    {
                        LoadDataToToolboxWeb.DropDown_DDL(DDLRoles, "SELECT RoleKey,RoleName FROM SYS_Roles  order by RoleName", false);                        
                    }
                    else
                    {
                        LoadDataToToolboxWeb.DropDown_DDL(DDLRoles, "SELECT RoleKey,RoleName FROM SYS_Roles where RoleKey NOT IN(Select RoleKey from SYS_Users_Roles where UserKey ='" + Session["ukey"].ToString() + "')  order by RoleName", false);
                    }

                    nUserLogin.CheckRole("SY0005");
                    if (!nUserLogin.Role.Edit)
                    {
                        cmdSave.Visible = false;
                        //DDLWeb_Menu.Enabled = false;
                    }
                    //User_User_Role_Info nUser = new User_User_Role_Info(Session["UserLogin"].ToString(),
                }
            }
        }
        protected void LoadInfo(string Key)
        {
            User_Role_Info info;
            if (Key == "0")
            {
                info = new User_Role_Info();
            }
            else
            {
                string[] sl = Key.Split(' ');
                info = new User_Role_Info(sl[0], sl[1]);
            }
            DDLRoles.SelectedValue = info.Key;
            DDLUsers.SelectedValue = info.UserKey;
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            CloseForm();
        }

        protected void SaveInfo()
        {
            User_Role_Info info;
            if (txtKey.Text != "0")
            {
                info = new User_Role_Info(new Guid().ToString());
                info.UserKey = DDLUsers.SelectedValue;
                info.Key = DDLRoles.SelectedValue;
                info.Update();
            }
            else
            {
                info = new User_Role_Info(txtKey.Text);
                info.UserKey = DDLUsers.SelectedValue;
                info.Key = DDLRoles.SelectedValue;
                info.Create();
            }            
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }
    }
}