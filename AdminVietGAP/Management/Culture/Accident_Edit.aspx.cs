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

namespace Management.Culture
{
    public partial class Accident_Edit : System.Web.UI.Page
    {
        public string PlaceOfBuy = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Accident_Data.GetAddressList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PlaceOfBuy += "\"" + dt.Rows[i]["PlaceOfBuy"].ToString() + "\",";
            }
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
                    LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "SELECT SeedsKey,SeedsName FROM PUL_Seeds", false);
                    LoadDataToToolboxWeb.DropDown_DDL(DDLEquipment, "SELECT EquipmentKey,EquipmentName FROM PUL_Equipment", false);
                    LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT EquipmentKey,EquipmentName FROM PUL_Equipment", false);
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
            Accident_Info info = new Accident_Info(Key);
            LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + info.CooperativeKey, false);
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
            }
            DDLSeeds.SelectedValue = info.SeedsKey.ToString();
            txtCode.Text = info.Code;
            DDLEquipment.SelectedValue = info.EquipmentKey.ToString();//.SelectedValue
            txtTreatments.Text = info.Treatments;
            txtNotice.Text = info.Notice;
            DDLCooperative.SelectedValue = info.CooperativeKey.ToString();
            DDLMember.SelectedValue = info.MemberKey.ToString();
        }
        protected void SaveInfo()
        {
            Accident_Info info = new Accident_Info(int.Parse(txtKey.Text));
            info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.SeedsKey = DDLSeeds.SelectedValue.ToInt();
            info.Code = txtCode.Text;
            info.EquipmentKey = int.Parse(DDLEquipment.SelectedValue);
            info.Treatments = txtTreatments.Text;
            info.Notice = txtNotice.Text;
            info.MemberKey = int.Parse(DDLMember.SelectedValue);
            info.CooperativeKey = int.Parse(DDLCooperative.SelectedValue);
            info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
    }
}