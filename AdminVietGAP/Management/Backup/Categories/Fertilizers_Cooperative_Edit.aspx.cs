using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Categories;
using TNLibrary.WEB;
using TNLibrary.SYS;

namespace Management.Categories
{
    public partial class Fertilizers_Cooperative_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0008");
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
                            LoadDataToToolboxWeb.DropDown_DDL(DDLFertilizers, "SELECT FertilizersKey,TradeName FROM PUL_Fertilizers WHERE FertilizersKey NOT IN(Select FertilizersKey FROM PUL_Fertilizers_Cooperative WHERE CooperativeKey = " + nUserLogin.CooperativeKey + ")  order by TradeName", false);
                        }
                        else
                        {
                            LoadDataToToolboxWeb.DropDown_DDL(DDLFertilizers, "SELECT FertilizersKey,TradeName FROM PUL_Fertilizers order by TradeName", false);
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
            Fertilizers_Cooperative_Info info = new Fertilizers_Cooperative_Info(Key);
            DDLFertilizers.SelectedValue = info.FertilizersKey.ToString();
        }
        protected void SaveInfo()
        {
            Fertilizers_Cooperative_Info info = new Fertilizers_Cooperative_Info(int.Parse(txtKey.Text));
            info.FertilizersKey = int.Parse(DDLFertilizers.SelectedValue);
            info.CooperativeKey = Convert.ToInt16(Session["CooperativeKey"]);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            info.CreatedBy = info.ModifiedBy = new Guid(nUserLogin.Key);
            info.CreatedDateTime = info.ModifiedDateTime = DateTime.Now;
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