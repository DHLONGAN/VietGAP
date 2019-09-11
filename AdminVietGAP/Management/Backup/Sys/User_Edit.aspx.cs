using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.WEB;
using TNLibrary.SYS;
using TNLibrary.SYS.Users;
using System.Globalization;

namespace Management.Sys
{
    public partial class User_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nKey = this.Request["key"];
            txtKey.Text = "0";
            string key = "0";
            if (nKey != "0")
            {
                Panel5.Visible = true;
                lbpassword.Text = "Mật khẩu mới";
                
            }
            if (nKey != null)
            {
                if (nKey != "")
                {
                    key = txtKey.Text = nKey;
                }
                if (!IsPostBack)
                {
                    LoadInfo(key);
                    SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
                    
                    if (nUserLogin.GroupKey == 4) //Nhà nước
                    {
                        Panel1.Visible = false;
                        Panel2.Visible = false;//Ẩn HTX
                        Panel3.Visible = false;//Ẩn ds member
                        Panel4.Visible = true;
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLCooperativeVentures, "SELECT CooperativeVenturesKey,CooperativeVenturesName FROM SYS_CooperativeVentures", false);
                        Session["CooperativeKey"] = 0;
                        Session["GroupKey"] = 3;
                    }
                    else if (nUserLogin.GroupKey == 3)//liên HTX
                    {
                        Panel1.Visible = false;//Ẩn ds liên hợp tác xã
                        //Panel3.Visible = false;//Ẩn ds member
                        Session["GroupKey"] = 2;
                        LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative WHERE Cooperative_Key IN(Select Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = " + nUserLogin.CooperativeVenturesKey + ")", false);
                        Session["CooperativeKey"] = nUserLogin.CooperativeKey;
                        Session["CooperativeVenturesKey"] = nUserLogin.CooperativeVenturesKey;
                    }
                    else if (nUserLogin.GroupKey == 2)
                    {
                        Panel1.Visible = false;//Ẩn ds liên hợp tác xã
                        Panel2.Visible = false;//Ẩn HTX
                        Session["GroupKey"] = 1;
                        Session["CooperativeVenturesKey"] = nUserLogin.CooperativeVenturesKey;
                    }
                    if (DDLCooperative.SelectedValue.ToString() != "")
                    {

                        LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
                    }
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLCooperativeVentures, "SELECT CooperativeVenturesKey,CooperativeVenturesName FROM SYS_CooperativeVentures", false);                    
                    ////
                    //string zSQL = "SELECT GroupID,GroupName FROM Sys_Group";
                    //string zSQL2 = "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative ";
                    //
                    //if (nUserLogin.GroupKey == 2)//HTX
                    //{
                    //    zSQL += " WHERE GroupID = 1";
                    //    zSQL2 += " WHERE Cooperative_Key = " + nUserLogin.CooperativeKey;
                    //}
                    //else if (nUserLogin.GroupKey == 3)//Liên HTX
                    //{
                    //    zSQL += " WHERE GroupID =2";
                    //    zSQL2 += " WHERE Cooperative_Key IN (SELECT Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey =" + nUserLogin.CooperativeVenturesKey + ")";
                    //}
                    ////if (nUserLogin.GroupKey > 1)
                    ////{
                    ////    LoadDataToToolboxWeb.DropDown_DDL(DDLGroup, zSQL, false);
                    ////}
                    //LoadDataToToolboxWeb.DropDown_DDL(DDLCooperativeVentures, "SELECT CooperativeVenturesKey,CooperativeVenturesName FROM SYS_CooperativeVentures WHERE CooperativeVenturesKey = " + nUserLogin.CooperativeVenturesKey, false);
                    //LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, zSQL2, false);
                    nUserLogin.CheckRole("SY0003");
                    if (!nUserLogin.Role.Edit)
                    {
                        cmdSave.Visible = false;
                        DDLCooperative.Enabled = false;
                        DDLMember.Enabled = false;
                        txtUserName.Enabled = false;
                        txtPassword.Enabled = false;
                        txtDescription.Enabled = false;
                        //txtDatetime.Enabled = false;
                        //DDLGroup.Enabled = false;
                    }
                    //User_Role_Info nUser = new User_Role_Info(Session["UserLogin"].ToString(),
                }
            }
        }
        protected void LoadInfo(string Key)
        {
            User_Info info = new User_Info(Key);
            if (Key == "0")
            {
                LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + Session["CooperativeKey"].ToString() + " and [Key] NOT IN(Select EmployeeKey from SYS_Users)", false);
            }
            else
            {
                LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + Session["CooperativeKey"].ToString(), false);
            }
            if (Key == "0")
            {
                DateTime time = DateTime.Now;
                //txtDatetime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                //txtDatetime.Text = info.ExpireDate.ToString("dd/MM/yyyy");
            }
            txtUserName.Text = info.Name;
            txtDescription.Text = info.Description;
            rdbActivate.SelectedValue = info.Activate ? "1" : "2";
            DDLCooperative.SelectedValue = info.CooperativeKey.ToString();
            DDLMember.SelectedValue = info.EmployeeKey.ToString();
            DDLCooperativeVentures.SelectedValue = info.CooperativeVenturesKey.ToString();
            txtPassword.Attributes.Add("value", "yourPassword");
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            
        }

        protected void SaveInfo()
        {
            //string msg = "Lỗi";
            //bool CanSave = false;
            User_Info info = new User_Info(txtKey.Text);
            info.ExpireDate = DateTime.Now;// ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.Name = txtUserName.Text;
            if (txtpasswordnew.Text != "")
            {
                if (txtpasswordnew.Text.IndexOf(" ") > -1)
                {
                    lberr.Text = "Mật khẩu không được có dấu cách";
                    return;
                }
                else if (txtpasswordnew.Text.Length < 8)
                {
                    lberr.Text = "Mật khẩu phải lớn hơn 8 ký tự";
                    return;
                }
                bool IsOK = false;
                //MessageBox.Show(hoten[0].ToString());
                for (int i = 0; i < txtpasswordnew.Text.Length; i++)
                {
                    if (char.IsUpper(txtpasswordnew.Text[i]) == true)
                    {
                        IsOK = true;
                        break;
                    }

                }
                if (!IsOK)
                {
                    lberr.Text = "Mật khẩu phải có ít nhất 1 chữ hoa";
                    return;
                }
                IsOK = false;
                for (int i = 0; i < txtpasswordnew.Text.Length; i++)
                {
                    if (char.IsNumber(txtpasswordnew.Text[i]) == true)
                    {
                        IsOK = true;
                        break;
                    }

                }
                if (!IsOK)
                {
                    lberr.Text = "Mật khẩu phải có ít nhất 1 số";
                    return;
                }
                info.Password = txtPassword.Text;
            }
            info.Description = txtDescription.Text;
            info.Activate = rdbActivate.SelectedValue == "1" ? true : false;
            if (Panel4.Visible)
            {
                switch (DDLGroup.SelectedValue)
                {
                    case "4":
                        {
                            info.CooperativeKey = 0;
                            info.CooperativeVenturesKey = 0;
                            info.EmployeeKey = 0;
                            break;
                        }
                    case "3":
                        {
                            if (DDLCooperativeVentures.SelectedValue.ToString() == "")
                            {
                                lberr.Text = "Liên hợp tác xã không thể để trống";
                                return;
                            }
                            info.CooperativeKey = 0;
                            if (DDLCooperative.SelectedValue != "")
                            {
                                info.CooperativeVenturesKey = DDLCooperative.SelectedValue.ToInt();
                            }
                            info.EmployeeKey = 0;
                            break;
                        }
                    case "2":
                    case "1":
                        {
                            if (DDLCooperativeVentures.SelectedValue.ToString() == "")
                            {
                                lberr.Text = "Liên hợp tác xã không thể để trống";
                                return;
                            }
                            if (DDLCooperative.SelectedValue.ToString() == "")
                            {
                                lberr.Text = "Hợp tác xã không thể để trống";
                                return;
                            }
                            info.CooperativeKey = DDLCooperative.SelectedValue.ToInt();
                            info.CooperativeVenturesKey = DDLCooperativeVentures.SelectedValue.ToInt();
                            info.EmployeeKey = DDLMember.SelectedValue.ToInt();
                            break;
                        }
                }
                info.GroupKey = DDLGroup.SelectedValue.ToInt();
            }
            else
            {

                if (DDLCooperative.SelectedValue != "")
                {
                    info.CooperativeKey = int.Parse(DDLCooperative.SelectedValue);
                }
                else
                {
                    info.CooperativeKey = int.Parse(Session["CooperativeKey"].ToString());
                }
                if (DDLMember.SelectedValue != "")// && Session["CooperativeKey"].ToString()!="0")
                {
                    info.EmployeeKey = int.Parse(DDLMember.SelectedValue);
                }
                else
                {
                    info.EmployeeKey = 0;
                }
                if (DDLCooperativeVentures.SelectedValue != "")
                {
                    info.CooperativeVenturesKey = int.Parse(DDLCooperativeVentures.SelectedValue);
                }
                else
                {
                    info.CooperativeVenturesKey = int.Parse(Session["CooperativeVenturesKey"].ToString());
                }
                int Groupkey = Session["GroupKey"].ToInt();
                if (Groupkey == 0)
                {
                    Groupkey = 1;
                }
                info.GroupKey = Groupkey;


            }
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            info.CreatedBy = info.ModifiedBy = nUserLogin.Key;
            info.CreatedDateTime = info.ModifiedDateTime = DateTime.Now;
            
            info.Save();
            CloseForm();
        }

        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }
        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToToolboxWeb.DropDown_DDL(DDLMember, "SELECT [Key],Name FROM PUL_Member WHERE Cooperative_Key=" + DDLCooperative.SelectedValue.ToString(), false);
        }
        protected void DDLCooperativeVentures_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative WHERE Cooperative_Key IN(Select Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = " + DDLCooperativeVentures.SelectedValue.ToString() + ")", false);
        }
        protected void DDLGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DDLGroup.SelectedValue)
            {
                case "4":
                    {
                        Panel1.Visible = false;
                        Panel2.Visible = false;
                        Panel3.Visible = false;
                        break;
                    }
                case "3":
                    {
                        Panel1.Visible = true;
                        Panel2.Visible = false;
                        Panel3.Visible = false;
                        LoadDataToToolboxWeb.DropDown_DDL(DDLCooperativeVentures, "SELECT CooperativeVenturesKey,CooperativeVenturesName FROM SYS_CooperativeVentures", false);
                        break;
                    }
                case "2":
                    {
                        LoadDataToToolboxWeb.DropDown_DDL(DDLCooperativeVentures, "SELECT CooperativeVenturesKey,CooperativeVenturesName FROM SYS_CooperativeVentures", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative WHERE Cooperative_Key IN(Select Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = " + DDLCooperativeVentures.SelectedValue.ToString() + ")", false);
                        Panel1.Visible = true;
                        Panel2.Visible = true;
                        Panel3.Visible = false;
                        break;
                    }
                case "1":
                    {
                        LoadDataToToolboxWeb.DropDown_DDL(DDLCooperativeVentures, "SELECT CooperativeVenturesKey,CooperativeVenturesName FROM SYS_CooperativeVentures", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLCooperative, "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative WHERE Cooperative_Key IN(Select Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = " + DDLCooperativeVentures.SelectedValue.ToString() + ")", false);
                        Panel1.Visible = true;
                        Panel2.Visible = true;
                        Panel3.Visible = true;
                        break;
                    }
            }
        }
    }
}