using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary;
using TNLibrary.Fields;
using System.IO;
using TNLibrary.WEB;

namespace Management.Fields
{
    public partial class Cooperative_Edit : System.Web.UI.Page
    {
        int Ckey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
                Response.Redirect("~/Login.aspx");
            string key = "0";
            if (this.Request["Key"] != null)
            {
                key = this.Request["Key"].ToString();
            }
            if (!IsPostBack)
            {
                //LoadData();

                if (int.TryParse(key, out Ckey))
                {
                    LoadInfo(Ckey);
                }
                LoadDataToToolboxWeb.DropDown_DDL(ddProvincesCitiesID, "SELECT ProvincesCities_Key,ProvincesCities_Name  FROM PUL_ProvincesCities", true);
                LoadDataToToolboxWeb.DropDown_DDL(ddCertifiedOrganization, "SELECT CertifiedOrganization_Key,CertifiedOrganization_Name  FROM PUL_CertifiedOrganization", true);
                LoadDataToToolboxWeb.DropDown_DDL(ddImage, "SELECT pc.Images as _key,pc.Images as _Name FROM   PUL_Cooperative AS pc GROUP BY pc.Images", true);
            }
        }


        protected void LoadInfo(int id)
        {
            Cooperative_Info cop = new Cooperative_Info(id);
        }
        protected void SaveInfo()
        {
            DateTime DateRange = new DateTime();
            DateTime DateExpiration = new DateTime();
            if (txtDateRange.Text != "")
            {
                DateRange = DateTime.ParseExact(txtDateRange.Text, "dd/MM/yyyy", null);
            }
            if (txtDateExpiration.Text != "")
            {
                DateExpiration = DateTime.ParseExact(txtDateExpiration.Text, "dd/MM/yyyy", null);
            }
            Cooperative_Info cop = new Cooperative_Info();
            cop.Cooperative_Key = Ckey;
            cop.Cooperative_ID = txtCooperative_ID.Text;
            cop.Cooperative_Name = txtCooperative_Name.Text;
            cop.ProvincesCities_ID = ddProvincesCitiesID.SelectedValue;
            cop.Address = txtAddress.Text;
            cop.VietGAPCode = txtVietGAPCode.Text;
            cop.Phone = txtPhone.Text;
            cop.Members = Convert.ToInt32(txtMembers.Text);
            cop.Area = Convert.ToInt32(txtArea.Text);
            cop.Quantity = Convert.ToInt32(txtQuantity.Text);
            cop.TreeType = txtTreeType.Text;
            cop.DateRange = DateRange;
            cop.DateExpiration = DateExpiration;
            cop.Owner = txtOwner.Text;
            cop.Description = txtDescription.Text;
            cop.CertifiedOrganization = ddCertifiedOrganization.SelectedValue;
            string[] arrListStr = txtlat.Text.Split(',');
            cop.Lat = arrListStr[0];
            cop.Lng = arrListStr[1];
            cop.Images = ddImage.SelectedValue;
            if (FileUploadControl.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    FileUploadControl.SaveAs(Server.MapPath("~/Img/Tree/") + filename);
                    cop.Images = filename;
                }
                catch (Exception ex)
                {

                }
            }
            cop.Save();
        }
        private void loadtxt(int id)
        {
            string DateRange = "";
            string DateExpiration = "";
            Cooperative_Info cop = new Cooperative_Info(id);
            if (cop.DateRange.ToString() != "")
            {
                DateRange = Convert.ToDateTime(cop.DateRange).ToString("dd/MM/yyyy");
            }
            if (cop.DateExpiration.ToString() != "")
            {
                DateExpiration = Convert.ToDateTime(cop.DateExpiration).ToString("dd/MM/yyyy");
            }

            txtCooperative_ID.Text = cop.Cooperative_ID;
            txtCooperative_Name.Text = cop.Cooperative_Name;
            ddProvincesCitiesID.SelectedValue = cop.ProvincesCities_ID;
            txtAddress.Text = cop.Address;
            txtVietGAPCode.Text = cop.VietGAPCode;
            txtPhone.Text = cop.Phone;
            txtMembers.Text = Convert.ToString(cop.Members);
            txtArea.Text = Convert.ToString(cop.Area);
            txtQuantity.Text = Convert.ToString(cop.Quantity);
            txtTreeType.Text = cop.TreeType;
            txtDateRange.Text = DateRange;
            txtDateExpiration.Text = DateExpiration;
            txtOwner.Text = cop.Owner;
            txtDescription.Text = cop.Description;
            ddCertifiedOrganization.SelectedValue = cop.CertifiedOrganization;
            ddImage.SelectedValue = cop.Images;
            txtlat.Text = cop.Lat+","+cop.Lng;
        }
        private void ShowMess(string mTextMsg, string mControlFocus)
        {
            LTShowMessage.Text = @"<script type='text/javascript'>

                                    document.forms[0].item('" +mControlFocus+@"').focus();
                                </script>";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            //sb.Append("<script language='JavaScript'>");
            //if (mTextMsg != "") 
            //    sb.Append("  alert('" + mTextMsg + "');");
            //if (mControlFocus != "") 
            //    sb.Append("document.forms[0].item('" + mControlFocus + "').focus();");
            //sb.Append("</script>");
            //if (!IsStartupScriptRegistered("setFocus")) RegisterStartupScript("setFocus", sb.ToString());
        }
        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }

        protected void cmdClosed_Click(object sender, ImageClickEventArgs e)
        {
            CloseForm();
        }
        
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            ShowMess("Không dc bo trống","txtCooperative_ID");
            SaveInfo();
            CloseForm();
        } 
    }
}