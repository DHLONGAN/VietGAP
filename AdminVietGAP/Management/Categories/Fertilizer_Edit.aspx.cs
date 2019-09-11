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
    public partial class Fertilizer_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CA0002");
                 string nKey = this.Request["key"];
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
                         LoadDataToToolboxWeb.DropDown_DDL(DLLCommon, "SELECT Common_Key,Common_Name FROM PUL_Fertilizer_Common order by Common_Name", true);
                         LoadDataToToolboxWeb.DropDown_DDL(DLLUnit, "SELECT Fertilizer_Unit_Key,Fertilizer_Unit_Name FROM PUL_Fertilizer_Unit order by Fertilizer_Unit_Name", true);
                         LoadDataToToolboxWeb.DropDown_DDL(DLLCompany, "SELECT CompanyKey,CompanyName FROM PUL_Companies order by CompanyName", true);
                         LoadDataToToolboxWeb.DropDown_DDL(DLLCategoryID, "SELECT CategoryKey,CategoryName FROM PUL_Fertilizer_Categories order by CategoryName", true);
                         if (!nUserLogin.Role.Edit && nKey != "0")
                         {
                             cmdSave.Visible = false;
                             DLLCompany.Enabled = false;
                             DLLCategoryID.Enabled = false;
                             DLLCommon.Enabled = false;
                             DLLUnit.Enabled = false;
                         }
                         else if (!nUserLogin.Role.Add && nKey == "0")
                         {
                             cmdSave.Visible = false;
                             DLLCompany.Enabled = false;
                             DLLCategoryID.Enabled = false;
                             DLLCommon.Enabled = false;
                             DLLUnit.Enabled = false;
                         }
                     }
                 }
             }
            
        }
        protected void LoadInfo(int Key)
        {
            Fertilizer_Info info = new Fertilizer_Info(Key);
            txtTrade.Text = info.TradeName;
            if (Key == 0)
            {
                DLLCommon.SelectedValue = "218";
                DLLCompany.SelectedValue = "234";
                DLLUnit.SelectedValue = "1";
                DLLCategoryID.SelectedValue = "2";
            }
            else
            {
                DLLCommon.SelectedValue = info.CommonKey.ToString();
                DLLUnit.SelectedValue = info.UnitKey.ToString();
                DLLCompany.SelectedValue = info.CompanyKey.ToString();
                DLLCategoryID.SelectedValue = info.CategoryKey.ToString();
            }
           
            imgLink.ImageUrl = info.Images;
        }
        protected void SaveInfo()
        {
            Fertilizer_Info info = new Fertilizer_Info(int.Parse(txtKey.Text));
            info.TradeName = txtTrade.Text;
            
            if (int.Parse(DLLUnit.SelectedValue) == 0)
            {
                info.UnitKey = 1;
            }
            else
            {
                info.UnitKey = int.Parse(DLLUnit.SelectedValue);
            }
            if (int.Parse(DLLCommon.SelectedValue) == 0)
            {
                info.CommonKey = 217;
            }
            else
            {
                info.CommonKey = int.Parse(DLLCommon.SelectedValue);
            }

            info.CompanyKey = int.Parse(DLLCompany.SelectedValue);
            info.CategoryKey = int.Parse(DLLCategoryID.SelectedValue);
            if (FileUploadKML.HasFile)
            {
                try
                {
                    FileUploadKML.SaveAs(Server.MapPath("~/Img/Fertilizer/") + info.FertilizersKey.ToString() + ".png");
                    info.Images = "../Img/Fertilizer/" + info.FertilizersKey.ToString() + ".png";
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                info.Images = "../Img/Fertilizer/Default.png";
            }
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