using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.System;
using TNLibrary.SYS;

namespace Management.Sys
{
    public partial class TimeLimit_Cooperative_Edit : System.Web.UI.Page
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

                        LoadInfo(nKey.ToInt());
                        //if (!nUserLogin.Role.Edit && nKey != "0")
                        //{
                        //    cmdSave.Visible = false;
                        //}
                        //else if (!nUserLogin.Role.Add && nKey == "0")
                        //{
                        //    cmdSave.Visible = false;
                        //}
                    }
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            TimeLimit_Cooperative_Info info = new TimeLimit_Cooperative_Info(Key);
            txtDateLimit.Text = info.TimeLimit.ToString();
        }
        protected void SaveInfo()
        {
            TimeLimit_Cooperative_Info info = new TimeLimit_Cooperative_Info(int.Parse(txtKey.Text));
            info.TimeLimit = txtDateLimit.Text.ToInt();
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