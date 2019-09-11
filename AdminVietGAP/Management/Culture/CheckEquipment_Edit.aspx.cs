using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using TNLibrary.WEB;
using System.Globalization;
using System.Data;
using TNLibrary.SYS;

namespace Management.Culture
{
    public partial class CheckEquipment_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0010");
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
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLEquipment, "SELECT EquipmentKey,EquipmentName FROM PUL_Equipment", false);
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "SELECT SeedsKey,SeedsName FROM PUL_Seeds", false);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            //cmdSave.Visible = false;
                            txtAction.Enabled = false;
                            txtDatetime.Enabled = false;
                            txtInfo.Enabled = false;
                            DDLSeeds.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            //cmdSave.Visible = false;
                            txtAction.Enabled = false;
                            txtDatetime.Enabled = false;
                            txtInfo.Enabled = false;
                            DDLSeeds.Enabled = false;
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
            CheckEquipment_Info info = new CheckEquipment_Info(Key);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }
            txtAction.Text = info.Action;
            txtInfo.Text = info.Info;
            DDLSeeds.Text = LoadDataToToolboxWeb.GetName("SELECT SeedsName FROM PUL_Seeds where SeedsKey = (Select SeedsKey from PUL_SeedProcess where SeedProcessKey  = (Select SeedsKey from PUL_CheckEquipment where CheckEquipmentKey = " + info.CheckEquipmentKey.ToString() + "))");
            DDLEquipment.Text = LoadDataToToolboxWeb.GetName("SELECT EquipmentName FROM PUL_Equipment where EquipmentKey = " + info.EquipmentKey.ToString());
        }
        protected void SaveInfo()
        {
            //CheckEquipment_Info info = new CheckEquipment_Info(int.Parse(txtKey.Text));
            //info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //info.EquipmentKey = int.Parse(DDLEquipment.SelectedValue);
            //info.Action = txtAction.Text;
            //info.Info = txtInfo.Text;
            //info.MemberKey = Convert.ToInt16(Session["EmployeeKey"]);
            //info.SeedsKey = DDLSeeds.SelectedValue.ToInt();
            //info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}