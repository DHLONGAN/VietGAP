using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Sys;
using TNLibrary.SYS;
using TNLibrary.WEB;
using TNLibrary.Categories;

namespace Management.Categories
{
    public partial class Seed_Cooperative_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0007");
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
                        if (nKey == "0")
                        {
                            LoadDataToToolboxWeb.DropDown_DDL(DLLSeeds, "SELECT SeedsKey,SeedsName FROM PUL_Seeds WHERE SeedsKey NOT IN(Select SeedKey FROM PUL_Seed_Cooperative WHERE CooperativeKey = " + nUserLogin.CooperativeKey + ") order by SeedsName", false);
                        }
                        else
                        {
                            LoadDataToToolboxWeb.DropDown_DDL(DLLSeeds, "SELECT SeedsKey,SeedsName FROM PUL_Seeds order by SeedsName", false);
                        }
                        LoadInfo(nEnvironmentsKey);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                        }
                    }
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            Seed_Cooperative_Info info = new Seed_Cooperative_Info(Key);
            DLLSeeds.SelectedValue = info.SeedKey.ToString();
            txtPrice.Text = String.Format("{0:#,0}", info.Price);
        }
        protected void SaveInfo()
        {
            Seed_Cooperative_Info info = new Seed_Cooperative_Info(int.Parse(txtKey.Text));
            info.SeedKey = int.Parse(DLLSeeds.SelectedValue);
            info.CooperativeKey = Convert.ToInt16(Session["CooperativeKey"]);
            info.Price = txtPrice.Text.Tofloat();
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (txtKey.Text.ToInt() == 0)
            {
                info.CreatedBy = info.ModifiedBy = new Guid(nUserLogin.Key);
                info.CreatedDateTime = info.ModifiedDateTime = DateTime.Now;
            }
            else
            {
                info.ModifiedBy = new Guid(nUserLogin.Key);
                info.ModifiedDateTime = DateTime.Now;
            }
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