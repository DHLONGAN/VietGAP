using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Sys;
using TNLibrary.SYS;

namespace Management.Sys
{
    public partial class CooperativeVentures_Edit : System.Web.UI.Page
    {
        public string Job = "";
        public string CooperativeVentureContent = "";
        public string Trainer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0013");
                string nKey = this.Request["key"];
                txtKey.Text = "0";
                int _nKey = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out _nKey))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(_nKey);
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
        protected void LoadInfo(int Key)
        {
            CooperativeVenture_Info info = new CooperativeVenture_Info(Key);

            txtCooperativeVentures.Text = info.CooperativeVenturesName;
        }
        protected void SaveInfo()
        {
            CooperativeVenture_Info info = new CooperativeVenture_Info(int.Parse(txtKey.Text));
            info.CooperativeVenturesName = txtCooperativeVentures.Text;
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            info.CreatedBy = info.ModifiedBy = new Guid(nUserLogin.Key);
            info.CreatedDateTime = info.ModifiedDateTime = DateTime.Now;
            info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}