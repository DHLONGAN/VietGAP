using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TNLibrary.WEB;
using TNLibrary.Categories;
using TNLibrary.SYS;

namespace Management.Categories
{
    public partial class Pesticide_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0003");
                string nKey = this.Request["key"];
                txtKey.Text = "0";
                int nPesticideKey = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out nPesticideKey))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(nPesticideKey);
                        LoadDataToToolboxWeb.DropDown_DDL(DLLCommon, "SELECT Common_Key,Common_Name FROM PUL_Pesticide_Common order by Common_Name", true);
                        LoadDataToToolboxWeb.DropDown_DDL(DLLCompany, "SELECT CompanyKey,CompanyName FROM PUL_Companies order by CompanyName", true);
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                            DLLCompany.Enabled = false;
                            DLLCommon.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                            DLLCompany.Enabled = false;
                            DLLCommon.Enabled = false;
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
            Pesticide_Info info = new Pesticide_Info(Key);
            txtTradeName.Text = info.Trade_Name;
            txtCropName.Text = info.Crop_Name;
            if (Key == 0)
            {
                DLLCommon.SelectedValue = "108";
                DLLCompany.SelectedValue = "234";
            }
            else
            {
                DLLCommon.SelectedValue = info.Common_Key.ToString();
                DLLCompany.SelectedValue = info.CompanyKey.ToString();
            }
            rdbStatus.SelectedValue = info.UsingStatus.ToString();
            imgLink.ImageUrl = info.Images;
        }
        protected void SaveInfo()
        {
            Pesticide_Info info = new Pesticide_Info(int.Parse(txtKey.Text));
            info.Trade_Name = txtTradeName.Text;
            info.Crop_Name = txtCropName.Text;
            info.Common_Key = int.Parse(DLLCommon.SelectedValue);
            info.CompanyKey = int.Parse(DLLCompany.SelectedValue);
            info.UsingStatus = int.Parse(rdbStatus.SelectedValue);
            info.CategoryKey = 1;
            if (FileUploadKML.HasFile)
            {
                try
                {
                    FileUploadKML.SaveAs(Server.MapPath("~/Img/Pesticides/") + info.PesticideKey.ToString() + ".png");
                    info.Images = "../Img/Pesticides/" + info.PesticideKey.ToString() + ".png";
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                info.Images = "../Img/Pesticides/Default.png";
            }
            info.Save();
        }
    }
}