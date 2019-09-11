using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Fields;
using TNLibrary;
using TNLibrary.WEB;
using TNLibrary.SYS;
using System.Text.RegularExpressions;


namespace Management.Fields
{
    public partial class Member_Detail : System.Web.UI.Page
    {
        int keyHTX;
        int Ckey = 0;
        int cp_key = 0;
        string Cooperative_Key = "";
        int coopKey = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
                Response.Redirect("~/Login.aspx");
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            keyHTX = nUserLogin.GroupKey;
            if (nUserLogin.GroupKey < 1 | nUserLogin.GroupKey > 4)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
            }
            if (nUserLogin.GroupKey == 1)
            {
                cmdEdit.Visible = true;
                cmdDelete.Visible = true;
            }
            if (nUserLogin.GroupKey == 2)
            {
                cmdEdit.Visible = true;
                cmdDelete.Visible = true;
            }
            if (nUserLogin.GroupKey == 3)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
            }
            if (nUserLogin.GroupKey == 4)
            {
                cmdEdit.Visible = true;
                cmdDelete.Visible = true;
            }
            string key ="0";
            if(this.Request["Key"] !=null)
            {
                key = this.Request["Key"].ToString();
            }
            
            if (this.Request["coop"] != null)
            {
                Cooperative_Key = this.Request["coop"].ToString();
            }
            if (!IsPostBack)
            {
                //LoadData();
                if (int.TryParse(Cooperative_Key, out coopKey))
                {
                    if (!checkCooperative(coopKey))
                    {
                        if (int.TryParse(key, out Ckey))
                        {
                            LoadInfo(Ckey);
                        }
                    }
                }
                //Member_Info.DropDown_DDL(ddCooperative_Key, "SELECT Cooperative_Key,Cooperative_Name  FROM PUL_Cooperative", true);
                //LoadDataToToolboxWeb.DropDown_DDL(ddCertifiedOrganization, "SELECT CertifiedOrganization_Key,CertifiedOrganization_Name  FROM PUL_CertifiedOrganization", true);
                
            }
            
        }
        protected void LoadInfo(int id)
        {
            if (Ckey == 0)
            {
                cmdDelete.Visible = false;
                loadtxt(id);

            }
            else
            {
                loadlb(id);
            }
            
        }
        private void loadlb(int id)
        {
            Member_Info info = new Member_Info(id);
            Cooperative_Info coo = new Cooperative_Info(info.Cooperative_Key);
            check();
            lb(true);
            txt(false);
            lbkey.Text = Convert.ToString(info.Key);
            lbMemID.Text = info.MemID;
            lbName.Text = info.Name;
            lbCooperative_Key.Text = coo.Cooperative_Name;
            lbAddress.Text = info.Address;
            lbEmail.Text = info.Email;
            lbPhone.Text = info.Phone;
            lbArea.Text = Convert.ToString(info.Area);
            lbDescription.Text = info.Description;

        }
        private void loadtxt(int id)
        {
            
            check();
            LoadDataToToolboxWeb.DropDown_DDL(ddcolor, "Select Color as _Key, CategoryName _Name from PUL_Seeds_Categories as sc", true);
            Member_Info info = new Member_Info(id);
            coopKey = Convert.ToInt32(this.Request["coop"]);
            Cooperative_Info coo = new Cooperative_Info(coopKey);
            lb(false);
            txt(true);
            txtMemID.Text = info.MemID;
            txtName.Text = info.Name;
            ddCooperative_Key.Text = coo.Cooperative_Name;
            txtAddress.Text = info.Address;
            txtEmail.Text = info.Email;
            txtPhone.Text = info.Phone;
            txtArea.Text = Convert.ToString(info.Area);
            lbDescription.Text = info.Description;
            txtlat.Text = info.LatLng;

        }
        private void check()
        {
            Member_Info info = new Member_Info(Ckey);

            if (info.Cooperative_Key.ToString() != "")
            {
                Cooperative_Key = Convert.ToString(info.Cooperative_Key);
                cp_key = info.Cooperative_Key;
            }
            
        }
        private bool checkdetail()
        {
            if (txtMemID.Text == "")
            {
                txtMemID.Focus();
                return false;
            }
            if (txtName.Text == "")
            {
                txtName.Focus();
                return false;
            }
            if (ddCooperative_Key.Text == "")
            {
                ddCooperative_Key.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool checkCooperative(int coopKey)
        {
            Cooperative_Info coo = new Cooperative_Info(coopKey);
            if (coo.Cooperative_Key == 0)
            {
                cmdSave.Visible = false;
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
                MessageBox("Không tìm thấy hợp tác xã");
                return true;
            }
            else
            {
                return false;
            }

        }
        public void MessageBox( String Message)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>alert('" + Message + "');</script>"
            );
        }
        private void lb(bool check)
        {
            lbMemID.Visible = check;
            lbName.Visible = check;
            lbCooperative_Key.Visible = check;
            lbAddress.Visible = check;
            lbEmail.Visible = check;
            lbPhone.Visible = check;
            lbArea.Visible = check;
            lbDescription.Visible = check;
            cmdEdit.Visible = check;
            ddcolor.Visible = check;
            //ltdel.Text = "";
            if (keyHTX < 1 | keyHTX > 2)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
            }
            if (keyHTX == 2)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
                SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
                if (nUserLogin.CooperativeKey == this.Request["coop"].ToInt())
                {
                    cmdEdit.Visible = true;
                    cmdDelete.Visible = true;
                    if (this.Request["key"].ToInt() == 0)
                    {
                        cmdEdit.Visible = false;
                        cmdDelete.Visible = false;
                    }
                }
                
            }
            if (keyHTX == 1)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
                SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
                if (nUserLogin.EmployeeKey == this.Request["Key"].ToInt())
                {
                    cmdEdit.Visible = true;
                    cmdDelete.Visible = true;
                }
            }
            if (keyHTX == 4)
            {
                cmdEdit.Visible = true;
                cmdDelete.Visible = true;
                if (this.Request["key"].ToInt() == 0)
                {
                    cmdEdit.Visible = false;
                    cmdDelete.Visible = false;
                }
            }
            
        }
        private void txt(bool check)
        {
            txtMemID.Visible = check;
            txtName.Visible = check;
            ddCooperative_Key.Visible = check;
            ddCooperative_Key.ReadOnly = true;
            txtAddress.Visible = check;
            txtEmail.Visible = check;
            txtPhone.Visible = check;
            txtArea.Visible = check;
            txtDescription.Visible = check;
            cmdSave.Visible = check;
            ddcolor.Visible = check;
            
            if (check == true)
            {
                //                ltdel.Text = @"<input id='delete-button' type='button' value='Xóa' class='submitOn' style='position: fixed; top: 120px; right: 115px;' />
                //                                <input id='clear-button' type='button' value='Xóa tất cả' class='submitOn' style='position: fixed; top: 120px; right: 35px;' />";
            }
        }
        private void CloseForm()
        {
            string nUrl = "<script>CloseOnReload()</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "closeWindow", nUrl);
        }

        protected void cmdEdit_Click(object sender, ImageClickEventArgs e)
        {
            Ckey = Convert.ToInt32(lbkey.Text);
            loadtxt(Ckey);
            cmdEdit.Visible = false;
            cmdDelete.Visible = false;
        }

        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            if (checkdetail())
            {
                SaveInfo();
                CloseForm();
            }
        }
        protected void SaveInfo()
        {
            
            Member_Info info = new Member_Info();
            string keylb = lbkey.Text;
            if (keylb == "")
            {
                keylb = "0";
            }
            if (int.TryParse(Cooperative_Key, out coopKey))
            info.Key = Convert.ToInt32(keylb);
            info.MemID = txtMemID.Text;
            info.Name = txtName.Text;
            info.Cooperative_Key = coopKey;
            info.Address = txtAddress.Text;
            info.Email = txtEmail.Text;
            info.Phone = txtPhone.Text;
            info.Area = float.Parse(txtArea.Text);
            info.Description = txtDescription.Text;
            info.LatLng = txtlat.Text;
            info.Save();
        }
        protected string LoadLatlng(string id)
        {
            Member_Info info = new Member_Info(Ckey);
            String latlng = "";
            //string buf = '{"shapes":[';
            latlng = @"'shapes= {shapes:[" + info.LatLng + "]}; expires=Invalid Date';";
            return latlng;
        }
        protected string LoadCenter(string id){
            string latlng = "";
            Member_Info info = new Member_Info(Ckey);
            if (info.LatLng != "")
            {
                string A = info.LatLng.Replace("\"", "");
                Regex myRegex = new Regex("lat");
                string[] sKetQua = myRegex.Split(A);
                latlng = "new google.maps.LatLng(" + sKetQua[1].Substring(1).Replace("},{", "").Replace("lon:", "") + ");";

            }
            else
            {
                Cooperative_Info Coo = new Cooperative_Info(cp_key);
                if (cp_key != 0)
                {
                    latlng = "new google.maps.LatLng(" + Coo.Lat + "," + Coo.Lng + ");";
                }
                else
                {
                    latlng = "new google.maps.LatLng(10.656559161780331, 106.13067626953125);";
                }
            }
            return latlng;
        }

        protected void cmdDelete_Click(object sender, ImageClickEventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Có")
            {
                string keylb = this.Request["Key"].ToString();
                if (keylb == "")
                {
                    keylb = "0";
                }
                Member_Info info = new Member_Info(Convert.ToInt32(keylb));
                info.Delete();
                CloseForm();
            }
            else
            {
                
            }
        }
    }
}