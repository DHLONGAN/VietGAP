using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Sys;
using TNLibrary.Categories;
using TNLibrary.WEB;
using TNLibrary.SYS;

namespace Management.Sys
{
    public partial class CooperativeVenturesAdd : System.Web.UI.Page
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
                    //LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT UserKey,UserName FROM SYS_Users WHERE UserKey = '" + Session["ukey"].ToString() + "'", false);
                    LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative", false);
                    //if (key != "0")
                    //{
                    //    LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative", false);
                    //}
                    //else
                    //{
                    //    LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative where Cooperative_Key NOT IN(Select Cooperative_Key from SYS_Users_Roles where UserKey ='" + Session["ukey"].ToString() + "')", false);
                    //}

                    nUserLogin.CheckRole("CA0001");
                    if (!nUserLogin.Role.Edit)
                    {
                        //cmdSave.Visible = false;
                        //DDLWeb_Menu.Enabled = false;
                    }
                    //User_ListCooperative_Info nUser = new User_ListCooperative_Info(Session["UserLogin"].ToString(),
                }
            }
        }
        protected void LoadInfo(string Key)
        {
            ListCooperative_Info info;
            if (Key == "0")
            {
                info = new ListCooperative_Info();
            }
            else
            {
                info = new ListCooperative_Info(Session["CooperativeVenturesKey"].ToString(), Key);
            }
            DDLCooperative.SelectedValue = info.Cooperative_Key.ToString();
            //DDLListCooperative.SelectedValue = info.UserKey;
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            CloseForm();
        }

        protected void SaveInfo()
        {

            ListCooperative_Info info = new ListCooperative_Info(Session["CooperativeVenturesKey"].ToString(), (txtKey.Text));
            info.Cooperative_Key = int.Parse(DDLCooperative.SelectedValue);
            info.CooperativeVenturesKey = int.Parse(Session["CooperativeVenturesKey"].ToString());
            info.Save();
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }
    }
}