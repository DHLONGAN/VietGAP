using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using TNLibrary.WEB;
using TNLibrary.Culture;
using System.Globalization;

namespace Management.Culture
{
    public partial class PostharvestHandling_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0013");
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
                        LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "Select SeedsKey,SeedsName from PUL_Seeds WHERE SeedsKey IN(Select SeedKey from PUL_Seed_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToString()+")", false);
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
            PostharvestHandling_Info info = new PostharvestHandling_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }            
            txtMethod.Text = info.Method;
            DDLSeeds.SelectedValue = info.SeedsKey.ToString();
        }
        protected void SaveInfo()
        {
            PostharvestHandling_Info info = new PostharvestHandling_Info(int.Parse(txtKey.Text));
            info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.SeedsKey = DDLSeeds.SelectedValue.ToInt();
            info.Method = txtMethod.Text;
            info.MemberKey = Session["EmployeeKey"].ToInt();
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