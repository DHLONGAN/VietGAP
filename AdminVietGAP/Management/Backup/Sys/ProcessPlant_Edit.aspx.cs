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
    public partial class ProcessPlant_Edit1 : System.Web.UI.Page
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
                    LoadInfo(key.ToInt());
                    //if (key != "0")
                    {
                        LoadDataToToolboxWeb.DropDown_DDL(DDLType, "SELECT ProcessPlant_TypeKey, Name FROM PUL_ProcessPlant_Type", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLSeed, "SELECT SeedsKey, SeedsName FROM PUL_Seeds WHERE SeedsKey IN(Select SeedKey from PUL_Seed_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToInt() + ")", false);
                    }
                    //else
                    //{
                    //    LoadDataToToolboxWeb.DropDown_DDL(DDLWeb_Menu, "SELECT MenuKey,MenuName FROM SYS_Web_Menu where MenuKey NOT IN(Select RoleID from SYS_Roles)", false);
                    //}
                    SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
                    //nUserLogin.CheckRole("SY0004");
                    //if (!nUserLogin.Role.Edit)
                    //{
                    //    cmdSave.Visible = false;
                    //    DDLType.Enabled = false;
                    //}
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            ProcessPlant_Info info = new ProcessPlant_Info(Key);
            if (Key != 0)
            {
                DDLSeed.SelectedValue = info.SeedsKey.ToString();
                DDLType.SelectedValue = info.ProcessPlant_Type.ToString();
                txtName.Text = info.ProcessPlantName;
                txtDescription.Text = info.Description;
            }
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            CloseForm();
        }

        protected void SaveInfo()
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            ProcessPlant_Info info = new ProcessPlant_Info(txtKey.Text.ToInt());
            info.SeedsKey = DDLSeed.SelectedValue.ToInt();
            info.ProcessPlant_Type = DDLType.SelectedValue.ToInt();
            info.ProcessPlantName = txtName.Text;
            info.Description = txtDescription.Text;
            info.Cooperative_Key = Session["CooperativeKey"].ToInt();
            if (txtKey.Text.ToInt() != 0)
            {
                info.ModifiedBy = new Guid(nUserLogin.Key);
                info.ModifiedDateTime = DateTime.Now;
            }
            else
            {
                info.CreatedBy = new Guid(nUserLogin.Key);
                info.CreatedDateTime = DateTime.Now;
            }
            info.Save();
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }
    }
}