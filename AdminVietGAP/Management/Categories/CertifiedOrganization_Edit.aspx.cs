using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.WEB;
using System.Data;
using TNLibrary.Categories;
using TNLibrary.SYS;

namespace Management.Categories
{
    public partial class CertifiedOrganization_edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CA0010");
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
                         LoadInfo(nEnvironmentsKey);
                         LoadDataToToolboxWeb.DropDown_DDL(DDLCategories, "SELECT CertificationType_Key,CertificationType_Name FROM PUL_CertificationType", false);
                         if (!nUserLogin.Role.Edit && nKey != "0")
                         {
                             cmdSave.Visible = false;
                             DDLCategories.Enabled = false;
                             txtCertifiedOrganization_Name.Enabled = false;
                             txtAddress.Enabled = false;
                             txtPhone.Enabled = false;
                             txtWebsite.Enabled = false;
                             txtInfrastructure.Enabled = false;
                             txtExamination_Process.Enabled = false;
                             txtEmail.Enabled = false;
                             txtFax.Enabled = false;
                             txtCertifiedOrganization_ID.Enabled = false;
                         }
                         else if (!nUserLogin.Role.Add && nKey == "0")
                         {
                             cmdSave.Visible = false;
                             DDLCategories.Enabled = false;
                             txtCertifiedOrganization_Name.Enabled = false;
                             txtAddress.Enabled = false;
                             txtPhone.Enabled = false;
                             txtWebsite.Enabled = false;
                             txtInfrastructure.Enabled = false;
                             txtExamination_Process.Enabled = false;
                             txtEmail.Enabled = false;
                             txtFax.Enabled = false;
                             txtCertifiedOrganization_ID.Enabled = false;
                         }
                     }
                 }
             }            
        }
        private void LoadInfo(int key)
        {
            CertifiedOrganization_Info info = new CertifiedOrganization_Info(key);
            txtCertifiedOrganization_ID.Text = info.CertifiedOrganization_ID;
            txtCertifiedOrganization_Name.Text = info.CertifiedOrganization_Name;
            DDLCategories.SelectedValue = info.CertificationType_Key.ToString();
            txtAddress.Text = info.Address;
            txtPhone.Text = info.Phone;
            txtFax.Text = info.Fax;
            txtEmail.Text = info.Email;
            txtWebsite.Text = info.Website;
            txtInfrastructure.Text = info.Infrastructure;
            txtExamination_Process.Text = info.Examination_Process;
        }
        private void SaveInfo()
        {
            CertifiedOrganization_Info info = new CertifiedOrganization_Info(int.Parse(txtKey.Text));
            info.CertifiedOrganization_ID = txtCertifiedOrganization_ID.Text;
            info.CertifiedOrganization_Name = txtCertifiedOrganization_Name.Text;
            info.CertificationType_Key = int.Parse(DDLCategories.SelectedValue);
            info.Address = txtAddress.Text;
            info.Phone = txtPhone.Text;
            info.Fax = txtFax.Text;
            info.Email = txtEmail.Text;
            info.Website = txtWebsite.Text;
            info.Infrastructure = txtInfrastructure.Text;
            info.Examination_Process = txtExamination_Process.Text;
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