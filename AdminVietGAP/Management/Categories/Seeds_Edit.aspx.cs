using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using TNLibrary.Categories;
using TNLibrary.WEB;
using TNLibrary.SYS.Users;
using TNLibrary.SYS;

namespace Management.Categories
{
    public partial class Seeds_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nKey = this.Request["key"];
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0001");
                txtKey.Text = "0";
                int nFertilizerKey = 0;
                if (nKey != null)
                {
                    if (int.TryParse(nKey, out nFertilizerKey))
                    {
                        txtKey.Text = nKey;
                    }
                    if (!IsPostBack)
                    {
                        LoadInfo(nFertilizerKey);
                        LoadDataToToolboxWeb.DropDown_DDL(DLLSeason, "SELECT SeasonKey,SeasonName FROM PUL_Season_Categories", true);
                        LoadDataToToolboxWeb.DropDown_DDL(DLLCompany, "SELECT CompanyKey,CompanyName FROM PUL_Seeds_Companies", true);
                        //LoadDataToToolboxWeb.DropDown_DDL(DLLCategoryID, "SELECT CategoryKey,CategoryName FROM PUL_Seeds_Categories", true);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLType, "SELECT TypeKey,TypeName FROM PUL_SeedTypes", true);

                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                            DLLSeason.Enabled = false;
                            DLLCompany.Enabled = false;
                            //DLLCategoryID.Enabled = false;
                            DDLType.Enabled = false;
                            txtDetail.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                            DLLSeason.Enabled = false;
                            DLLCompany.Enabled = false;
                            //DLLCategoryID.Enabled = false;
                            DDLType.Enabled = false;
                            txtDetail.Enabled = false;
                        }
                        //User_Role_Info nUser = new User_Role_Info(Session["UserLogin"].ToString(),
                    }
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            Seed_Info info = new Seed_Info(Key);
            txtSeedsName.Text = info.SeedsName;
            if (Key == 0)
            {
                DDLType.SelectedValue = "1";
                DLLCompany.SelectedValue = "241";
                DLLSeason.SelectedValue = "240";
            }
            else
            {
                DLLCompany.SelectedValue = info.CompanyKey.ToString();
                DDLType.SelectedValue = info.TypeKey.ToString();
                DLLSeason.SelectedValue = info.SeasonKey.ToString();
            }
            //DLLCategoryID.SelectedValue = info.CategoryKey.ToString();

            rdbStatus.SelectedValue = info.StatusKey.ToString();
            txtDetail.Text = info.Detail;
            imgLink.ImageUrl = info.Images;
        }
        protected void SaveInfo()
        {
            Seed_Info info = new Seed_Info(int.Parse(txtKey.Text));
            info.SeedsName = txtSeedsName.Text;
            info.CategoryKey = 1;// int.Parse(DLLCategoryID.SelectedValue);
            info.CompanyKey = int.Parse(DLLCompany.SelectedValue);
            info.StatusKey = int.Parse(rdbStatus.SelectedValue);
            info.SeasonKey = int.Parse(DLLSeason.SelectedValue);
            if (FileUploadKML.HasFile)
            {
                try
                {
                    FileUploadKML.SaveAs(Server.MapPath("~/Img/Seeds/") + info.SeedsKey.ToString() + ".png");
                    info.Images = "../Img/Seeds/" + info.SeedsKey.ToString() + ".png";
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                info.Images = "../Img/Seeds/Default.png";
            }
            info.TypeKey = int.Parse(DDLType.SelectedValue);
            info.Detail = txtDetail.Text;
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