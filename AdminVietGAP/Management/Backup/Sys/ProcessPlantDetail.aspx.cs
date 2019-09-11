using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.WEB;
using TNLibrary.SYS;
using TNLibrary.Sys;
using System.Data;

namespace Management.Sys
{
    public partial class ProcessPlantDetail : System.Web.UI.Page
    {
        int oderkey = 0;
        string mainkey = "0";
        int typekey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (Request["key"] == "" || Request["key"] == null || nUserLogin == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Vui lòng đăng nhập lại');", true);
                ClientScript.RegisterStartupScript(GetType(), "hwda", "window.location.href='Default.aspx'", true);
                return;
            }
            string[] nKey = this.Request["key"].Split('|');
            txtKey.Text = "0";
            if (nKey != null)
            {
                if (nKey[0] != "0")
                {
                    mainkey = txtKey.Text = nKey[0];
                   typekey = nKey[1].ToInt();
                }
                else
                {
                    if (DDLType.SelectedValue == "")
                    {
                        typekey = 1;
                    }
                    else
                    {
                        typekey = DDLType.SelectedValue.ToInt();
                    }
                }
               

                switch (nKey[1].ToInt())
                {
                    case 1: // sổ làm đất
                        PnLandUse.Visible = true;
                        PNSeed.Visible = false;
                        PNFerU.Visible = false;
                        PNPesU.Visible = false;
                        oderkey = LoadDataToToolboxWeb.GetID("Select LandUseKey from PUL_Process_LandUse where ProcessPlantDetailKey = " + nKey[0]);
                        break;
                    case 2: // sổ SD đất
                        PnLandUse.Visible = false;
                        PNSeed.Visible = true;
                        PNFerU.Visible = false;
                        PNPesU.Visible = false;
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLAreaUnit, "Select ID, Name from PUL_Unit", false);
                       // LoadDataToToolboxWeb.DropDown_DDL(DDLQuantityUnit, "Select ID, Name from PUL_Unit", false);
                        oderkey = LoadDataToToolboxWeb.GetID("Select SeedProcessKey from PUL_Process_SeedProcess where ProcessPlantDetailKey = " + nKey[0]);
                        break;
                    case 3: // sổ SD phân
                        PnLandUse.Visible = false;
                        PNSeed.Visible = false;
                        PNFerU.Visible = true;
                        PNPesU.Visible = false;
                        LoadDataToToolboxWeb.DropDown_DDL(DDLEquipment, "Select EquipmentKey, EquipmentName from PUL_Equipment", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLUnit, "Select ID, Name from PUL_Unit", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLFertilizer, "Select FertilizersKey, TradeName from PUL_Fertilizers where FertilizersKey IN(Select FertilizersKey from PUL_Fertilizers_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToString() + ")", false);                        
                        oderkey = LoadDataToToolboxWeb.GetID("Select FertilizerUseKey from PUL_Process_FertilizerUse where ProcessPlantDetailKey = " + nKey[0]);
                        DDLUnit.SelectedValue = "3";
                        DDLEquipment.SelectedValue = "5";
                        break;
                    case 4: // sổ SD thuốc
                        PnLandUse.Visible = false;
                        PNSeed.Visible = false;
                        PNFerU.Visible = false;
                        PNPesU.Visible = true;
                        LoadDataToToolboxWeb.DropDown_DDL(DDLEquip, "Select EquipmentKey, EquipmentName from PUL_Equipment", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLU, "Select ID, Name from PUL_Unit", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLPesticide, "Select PesticideKey, Trade_Name from PUL_Pesticides where PesticideKey IN(Select PesticideKey from PUL_Pesticide_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToString() + ")", false);
                        oderkey = LoadDataToToolboxWeb.GetID("Select PesticideUseKey from PUL_Process_PesticideUse where ProcessPlantDetailKey = " + nKey[0]);
                        DDLU.SelectedValue = "7";
                        break;
                }
                if (!IsPostBack)
                {
                    
                    if (mainkey == "0")
                    {
                        
                      //  LoadDataToToolboxWeb.DropDown_DDL(DDLSeed, "SELECT SeedsKey, SeedsName FROM PUL_Seeds WHERE SeedsKey IN(Select SeedKey from PUL_Seed_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToInt() + ")", false);
                    }
                    else
                    {
                        DDLType.Enabled = false;
                    }
                    
                    
                    
                    string strSQLType = "SELECT ProcessPlantDetai_TypeKey, Name FROM PUL_ProcessPlantDetai_Type";
                    if (Session["ProcessPlant_Type"].ToString() == "2")
                    {
                        strSQLType += " where ProcessPlantDetai_TypeKey <>2";
                    }
                    LoadDataToToolboxWeb.DropDown_DDL(DDLType, strSQLType, false);
                    //typekey = DDLType.SelectedValue.ToInt();
                    LoadInfo(typekey);
                    //nUserLogin.CheckRole("SY0004");
                    //if (!nUserLogin.Role.Edit)
                    //{
                    //    cmdSave.Visible = false;
                    //    DDLType.Enabled = false;
                    //}
                }
            }
        }
        protected void LoadInfo(int Key)
        {
            ProcessPlantDetail_Info minfo = new ProcessPlantDetail_Info(mainkey.ToInt());
            DDLType.SelectedValue = minfo.ProcessPlantDetai_Type.ToString();
            txtDescription.Text = minfo.Description;
            switch (Key)
            {
                case 1:
                    {
                        Process_LandUse_Info info = new Process_LandUse_Info(oderkey);
                        txtAction.Text = info.Action;
                        txtReason.Text = info.Reason;
                        txtSolution.Text = info.Solution;
                        txtDateNum.Text = info.Datetime_Num.ToString();
                        break;
                    }
                case 2:
                    {
                        Process_SeedProces_Info info = new Process_SeedProces_Info(oderkey);
                        txtDateBuy.Text = info.DateBuy_Num.ToString();
                        txtDateOfManufacture.Text = info.DateOfManufacture_Num.ToString();
                        txtDateSowing.Text = info.DateSowing_Num.ToString();
                        txtCompany.Text = info.CompanyName;
                        //txtParcel.Text = info.Parcel;
                        //txtArea.Text = info.Area.ToString();
                       // DDLAreaUnit.SelectedValue = info.AreaUnit.ToString();
                       // txtQuantity.Text = info.Quantity.ToString();
                       // DDLQuantityUnit.SelectedValue = info.QuantityUnit.ToString();
                        txtReason.Text = info.Reasons;
                        txtEndNum.Text = info.EndTime_Num.ToString();
                        break;
                    }
                case 3:
                    Process_FertilizerUse_Info info3 = new Process_FertilizerUse_Info(oderkey);
                    txtDateNum2.Text = info3.DateTimeUse_Num.ToString();
                    txtParcel2.Text = info3.Parcel;
                    txtArea2.Text = info3.Area;
                    txtHowtouse.Text = info3.Howtouse;
                    
                    DDLFertilizer.SelectedValue = info3.FertilizerKey.ToString();
                    txtFormulaUsed.Text = info3.FormulaUsed.ToString();
                    if (oderkey != 0)
                    {
                        DDLUnit.SelectedValue = info3.UnitKey.ToString();
                        DDLEquipment.SelectedValue = info3.CooperativeKey.ToString();
                    }
                    else
                    {
                        DDLUnit.SelectedValue = "3";
                        DDLEquipment.SelectedValue = "5";
                    }
                    //txtQuantity2.Text = info3.Quantity;
                    //txtQuarantinePeriod.Text = info3.QuarantinePeriod;
                    break;
                case 4:
                    Process_PesticideUse_Info info4 = new Process_PesticideUse_Info(oderkey);
                    txtDatetime.Text = info4.DateTimeUse_Num.ToString();
                    txtArea3.Text = info4.Area;
                    txtPestName.Text = info4.PestName;
                    txtSolution2.Text = info4.Solution;
                    DDLEquip.SelectedValue = info4.EquipmentKey.ToString();
                    DDLPesticide.SelectedValue = info4.PesticideKey.ToString();
                    txtDose.Text = info4.Dose.ToString();
                    DDLU.SelectedValue = info4.UnitKey.ToString();
                    //txtDosage.Text = info4.Dosage;
                    txtGT.Text = info4.QuarantinePeriod;
                    break;
            }
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)            //ProcessPlant_Info info = new ProcessPlant_Info(Key);
        {
            SaveInfo();
           
        }

        protected void SaveInfo()
        {
            switch (typekey)
            {
                case 1:
                    if (txtAction.Text == "" || txtReason.Text == "" || txtSolution.Text == "" || txtDescription.Text == "" || txtDateNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                  
                    break;
                case 2:
                    if (txtDateBuy.Text == "" || txtDateOfManufacture.Text == "" || txtDateSowing.Text == "" || txtCompany.Text == "" || txtEndNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                   
                    break;
                case 3:
                    if (txtDateNum2.Text == "" || txtParcel2.Text == "" || txtHowtouse.Text == "" || txtFormulaUsed.Text == "")// || txtQuantity.Text == "" || txtReason.Text == "" || txtEndNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                    
                    break;
                case 4:
                    if (txtDatetime.Text == "" || txtArea3.Text == "" || txtPestName.Text == "" || txtSolution2.Text == "" || txtDose.Text == "" || txtGT.Text == "")// txtReason.Text == "" || txtEndNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                   
                    break;
            }
            ProcessPlantDetail_Info minfo = new ProcessPlantDetail_Info(mainkey.ToInt());
            int newkey = 0;
            if (mainkey == "0")
            {
                minfo.ProcessPlantDetai_Type = DDLType.SelectedValue.ToInt();
                minfo.Description = txtDescription.Text;
                minfo.DateNum = txtDateNum.Text.ToInt();
                minfo.ProcessPlantKey = Session["ProcessPlantKey"].ToInt();
                newkey = minfo.Create().ToInt();
            }

            else
            {
                minfo.ProcessPlantDetai_Type = DDLType.SelectedValue.ToInt();
                minfo.Description = txtDescription.Text;
                if (txtDateNum.Text != "")
                {
                    minfo.DateNum = txtDateNum.Text.ToInt();
                }
                else
                {
                    if (txtDateNum2.Text != "")
                    {
                        minfo.DateNum = txtDateNum2.Text.ToInt();
                    }
                    else
                    {
                        if (txtDateOfManufacture.Text != "")
                        {
                            minfo.DateNum = txtDateOfManufacture.Text.ToInt();
                        }
                        else
                        {
                            minfo.DateNum = txtDatetime.Text.ToInt();
                        }
                    }
                }
                minfo.Update();
                newkey = minfo.ProcessPlantDetailKey;
            }
            int SeedKey = LoadDataToToolboxWeb.GetID("Select SeedsKey from PUL_ProcessPlant WHERE ProcessPlantKey = " + LoadDataToToolboxWeb.GetID("Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = " + newkey.ToString()));
            switch (typekey)
            {
                case 1:
                    if (txtAction.Text == "" || txtReason.Text == "" || txtSolution.Text == "" || txtDescription.Text == "" || txtDateNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                    Process_LandUse_Info info = new Process_LandUse_Info(oderkey);
                    info.Action = txtAction.Text;
                    info.Reason = txtReason.Text;
                    info.Solution = txtSolution.Text;
                    info.Note = txtDescription.Text;
                    info.ProcessPlantDetailKey = newkey;
                    info.SeedKey = SeedKey;
                    info.Datetime_Num = txtDateNum.Text.ToInt();
                    info.Save();
                    break;
                case 2:
                    if (txtDateBuy.Text == "" || txtDateOfManufacture.Text == "" || txtDateSowing.Text == "" || txtCompany.Text == "" || txtEndNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                    Process_SeedProces_Info info2 = new Process_SeedProces_Info(oderkey);
                    info2.DateBuy_Num = txtDateBuy.Text.ToInt();
                    info2.DateOfManufacture_Num = txtDateOfManufacture.Text.ToInt();
                    info2.DateSowing_Num = txtDateSowing.Text.ToInt();
                    info2.CompanyName = txtCompany.Text;
                    info2.Parcel = "";//txtParcel.Text;
                    info2.Area = 0;//txtArea.Text.ToInt();
                    info2.AreaUnit = 0;//DDLAreaUnit.SelectedValue.ToInt();
                    info2.Quantity = 0;//txtQuantity.Text.ToInt();
                    info2.QuantityUnit = 0;//DDLQuantityUnit.SelectedValue.ToInt();
                    info2.ProcessPlantDetailKey = newkey;
                    info2.SeedsKey = SeedKey.ToString();
                    info2.Reasons = txtReason.Text;
                    info2.EndTime_Num = txtEndNum.Text.ToInt();
                    info2.Save();
                    break;
                case 3:
                    if (txtDateNum2.Text == "" || txtParcel2.Text == "" || txtHowtouse.Text == "" || txtFormulaUsed.Text == "")// || txtQuantity.Text == "" || txtReason.Text == "" || txtEndNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                    Process_FertilizerUse_Info info3 = new Process_FertilizerUse_Info(oderkey);
                    info3.DateTimeUse_Num = txtDateNum2.Text.ToInt();
                    info3.Parcel = txtParcel2.Text;
                    info3.Area = txtArea2.Text;
                    info3.Howtouse = txtHowtouse.Text;
                    info3.CooperativeKey = DDLEquipment.SelectedValue.ToInt();
                    info3.FertilizerKey = DDLFertilizer.SelectedValue.ToInt();
                    info3.FormulaUsed = float.Parse(txtFormulaUsed.Text);
                    info3.UnitKey = DDLUnit.SelectedValue.ToInt();
                    info3.Quantity = "0";//txtQuantity2.Text;
                    info3.QuarantinePeriod = "0";//txtQuarantinePeriod.Text;
                    info3.SeedKey = SeedKey;
                    info3.ProcessPlantDetailKey = newkey;
                    info3.Save();
                    break;
                case 4:
                    if (txtDatetime.Text == "" || txtArea3.Text == "" || txtPestName.Text == "" || txtSolution2.Text == "" || txtDose.Text == "" || txtGT.Text == "")// txtReason.Text == "" || txtEndNum.Text == "")
                    {
                        Err.Text = "Vui lòng nhập đầy đủ thông tin";
                        return;
                    }
                    Process_PesticideUse_Info info4 = new Process_PesticideUse_Info(oderkey);
                    info4.DateTimeUse_Num = txtDatetime.Text.ToInt();
                    info4.Area = txtArea3.Text;
                    info4.PestName = txtPestName.Text;
                    info4.Solution = txtSolution2.Text;
                    info4.EquipmentKey = DDLEquip.SelectedValue.ToInt();
                    info4.PesticideKey = DDLPesticide.SelectedValue.ToInt();
                    info4.Dose = float.Parse(txtDose.Text);
                    info4.Dosage = "Chưa có dữ liệu";//txtDosage.Text;
                    info4.QuarantinePeriod = txtGT.Text;
                    info4.SeedKey = SeedKey;
                    info4.ProcessPlantDetailKey = newkey;
                    info4.UnitKey = DDLU.SelectedValue.ToInt();
                    info4.Save();
                    break;
            }
            CloseForm();
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }

        protected void DDLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DDLType.SelectedValue.ToInt())
            {
                case 1: // sổ làm đất
                    PnLandUse.Visible = true;
                    PNSeed.Visible = false;
                    PNFerU.Visible = false;
                    PNPesU.Visible = false;
                    oderkey = LoadDataToToolboxWeb.GetID("Select LandUseKey from PUL_Process_LandUse where ProcessPlantDetailKey = " + mainkey);
                    typekey = 1;
                    break;
                case 2: // sổ SD đất
                    PnLandUse.Visible = false;
                    PNSeed.Visible = true;
                    PNFerU.Visible = false;
                    PNPesU.Visible = false;
                    //LoadDataToToolboxWeb.DropDown_DDL(DDLAreaUnit, "Select ID, Name from PUL_Unit", false);
                    //LoadDataToToolboxWeb.DropDown_DDL(DDLQuantityUnit, "Select ID, Name from PUL_Unit", false);
                    oderkey = LoadDataToToolboxWeb.GetID("Select FertilizerUseKey from PUL_Process_FertilizerUse where ProcessPlantDetailKey = " + mainkey);
                    typekey = 2;
                    break;
                case 3: // sổ SD phân
                    PnLandUse.Visible = false;
                    PNSeed.Visible = false;
                    PNFerU.Visible = true;
                    PNPesU.Visible = false;
                    LoadDataToToolboxWeb.DropDown_DDL(DDLEquipment, "Select EquipmentKey, EquipmentName from PUL_Equipment", false);
                    LoadDataToToolboxWeb.DropDown_DDL(DDLUnit, "Select ID, Name from PUL_Unit", false);
                    LoadDataToToolboxWeb.DropDown_DDL(DDLFertilizer, "Select FertilizersKey, TradeName from PUL_Fertilizers where FertilizersKey IN(Select FertilizersKey from PUL_Fertilizers_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToString() + ")", false);
                    oderkey = LoadDataToToolboxWeb.GetID("Select SeedProcessKey from PUL_Process_SeedProcess where ProcessPlantDetailKey = " + mainkey);
                    typekey = 3;
                    DDLUnit.SelectedValue = "3";
                        DDLEquipment.SelectedValue = "5";
                    break;
                case 4: // sổ SD thuốc
                    PnLandUse.Visible = false;
                    PNSeed.Visible = false;
                    PNFerU.Visible = false;
                    PNPesU.Visible = true;
                    LoadDataToToolboxWeb.DropDown_DDL(DDLEquip, "Select EquipmentKey, EquipmentName from PUL_Equipment", false);
                    LoadDataToToolboxWeb.DropDown_DDL(DDLU, "Select ID, Name from PUL_Unit", false);
                    LoadDataToToolboxWeb.DropDown_DDL(DDLPesticide, "Select PesticideKey, Trade_Name from PUL_Pesticides where PesticideKey IN(Select PesticideKey from PUL_Pesticide_Cooperative where CooperativeKey = " + Session["CooperativeKey"].ToString() + ")", false);
                    oderkey = LoadDataToToolboxWeb.GetID("Select PesticideUseKey from PUL_Process_PesticideUse where ProcessPlantDetailKey = " + mainkey);
                    typekey = 4;
                    DDLU.SelectedValue = "7";
                        break;
            }
            Err.Text = "";
        }
    }
}