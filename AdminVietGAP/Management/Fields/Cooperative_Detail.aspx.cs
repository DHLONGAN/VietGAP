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
using System.Drawing;
using TNLibrary.SYS;

namespace Management.Fields
{
    public partial class Cooperative_Detail : System.Web.UI.Page
    {
        int keyHTX;
        int Ckey = 0;
        string DateRange = "";
        string DateExpiration = "";
        string CertifiedOrganization = "0";
        string ProvincesCities_ID = "0";
        string Images = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogin"] == null)
                Response.Redirect("~/Login.aspx");
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            keyHTX = nUserLogin.GroupKey;
            if (nUserLogin.GroupKey < 2 | nUserLogin.GroupKey > 3)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
            }
            if (nUserLogin.GroupKey == 2)
            {
                cmdEdit.Visible = true;
                cmdDelete.Visible = false;
            }
            if (nUserLogin.GroupKey == 3)
            {
                cmdEdit.Visible = true;
                cmdDelete.Visible = true;
            }
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
            loadlb(id);
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
            cop.Cooperative_Key = Convert.ToInt32(lbkey.Text);
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
                catch (Exception ex){}
            }
            cop.Save();
            Cooperative_Image_Info cooImg = new Cooperative_Image_Info();
            if (FileUpload1.HasFile)
            {
                try{
                    string Name = lbkey.Text + "_1";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img1.png";
                    FileUpload1.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/"+filename;
                    cooImg.Save();
                }catch (Exception ex) { }
            }
            if (FileUpload2.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_2";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img2.png";
                    FileUpload2.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload3.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_3";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img3.png";
                    FileUpload3.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload4.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_4";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img4.png";
                    FileUpload4.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload5.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_5";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img5.png";
                    FileUpload5.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload6.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_6";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img6.png";
                    FileUpload6.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload7.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_7";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img7.png";
                    FileUpload7.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload8.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_8";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img8.png";
                    FileUpload8.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload9.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_9";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img9.png";
                    FileUpload9.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
            if (FileUpload10.HasFile)
            {
                try
                {
                    string Name = lbkey.Text + "_10";
                    Cooperative_Image_Info cooImgName = new Cooperative_Image_Info(Name);
                    cooImg.Cooperative_Image_Key = cooImgName.Cooperative_Image_Key;
                    cooImg.Name = Name;
                    cooImg.Cooperative_Key = Convert.ToInt32(lbkey.Text);
                    cooImg.DateTime = DateTime.Now;
                    cooImg.Description = txtCooperative_Name.Text;
                    string filename = lbkey.Text + "_img10.png";
                    FileUpload10.SaveAs(Server.MapPath("~/Img/coop/") + filename);
                    cooImg.Images = "../Img/coop/" + filename;
                    cooImg.Save();
                }
                catch (Exception ex) { }
            }
        }
        private void loadlb(int id)
        {
            Cooperative_Info cop = new Cooperative_Info(id);
            check();
            lbkey.Text = Convert.ToString(cop.Cooperative_Key);
            lbCooperative_ID.Text = cop.Cooperative_ID;
            lbCooperative_Name.Text = cop.Cooperative_Name;
            lbProvincesCitiesID.Text = cop.ProvincesCities_Name;
            lbAddress.Text = cop.Address;
            lbVietGAPCode.Text = cop.VietGAPCode;
            lbPhone.Text = cop.Phone;
            lbMembers.Text = Convert.ToString(cop.Members);
            lbArea.Text = Convert.ToString(cop.Area);
            lbQuantity.Text = Convert.ToString(cop.Quantity);
            lbTreeType.Text = cop.TreeType;
            lbDateRange.Text = DateRange;
            lbDateExpiration.Text = DateExpiration;
            lbOwner.Text = cop.Owner;
            lbDescription.Text = cop.Description;
            lbCertifiedOrganization.Text = cop.CertifiedOrganization_Name;
            Image1.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img1.png";
            Image2.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img2.png";
            Image3.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img3.png";
            Image4.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img4.png";
            Image5.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img5.png";
            Image6.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img6.png";
            Image7.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img7.png";
            Image8.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img8.png";
            Image9.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img9.png";
            Image10.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img10.png";
            ltmap.Text = @"<script type='text/javascript'>
                            function initialize() {
                                var myLatlng = new google.maps.LatLng(" + cop.Lat + "," + cop.Lng + @");
                                var mapOptions = {
                                    zoom: 13,
                                    mapTypeId: google.maps.MapTypeId.SATELLITE,
                                    center: myLatlng
                                }
                                var icon = { 
                                    url:'/Img/Tree/" + cop.Images + @"',
                                    scaledSize: new google.maps.Size(40, 40)
                                };
                                var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
                                var marker = new google.maps.Marker({
                                    position: myLatlng,
                                    map: map,
                                    icon: icon,
                                    title:'" + cop.Cooperative_Name + @"'
                                });
                            }
                            google.maps.event.addDomListener(window, 'load', initialize);
                        </script>";
            lb(true);
            txt(false);
        }
        private void loadtxt(int id)
        {
            
            Cooperative_Info cop = new Cooperative_Info(id);
            check();
            lb(false);
            txt(true);

            txtCooperative_ID.Text = cop.Cooperative_ID;
            txtCooperative_Name.Text = cop.Cooperative_Name;
            ddProvincesCitiesID.SelectedValue = ProvincesCities_ID;
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
            ddCertifiedOrganization.SelectedValue = CertifiedOrganization;
            ddImage.SelectedValue = Images;
            txtlat.Text = cop.Lat+","+cop.Lng;
            Image1.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img1.png";
            Image2.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img2.png";
            Image3.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img3.png";
            Image4.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img4.png";
            Image5.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img5.png";
            Image6.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img6.png";
            Image7.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img7.png";
            Image8.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img8.png";
            Image9.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img9.png";
            Image10.ImageUrl = "../Img/coop/" + cop.Cooperative_Key + "_img10.png";
            ltmap.Text = @"<script type='text/javascript'>
                            function initialize() {
                                var myLatlng = new google.maps.LatLng(" + cop.Lat + "," + cop.Lng + @");
                                var mapOptions = {
                                    zoom: 13,
                                    mapTypeId: google.maps.MapTypeId.SATELLITE,
                                    center: myLatlng
                                }
                                var icon = { 
                                    url:'/Img/Tree/" + cop.Images + @"',
                                    scaledSize: new google.maps.Size(40, 40)
                                };
                                var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
                                var marker = new google.maps.Marker({
                                    position: myLatlng,
                                    map: map,
                                    icon: icon,
                                    draggable: true,
                                    title:'" + cop.Cooperative_Name + @"'
                                });
                                google.maps.event.addListener(map, 'click', function (e) {
                                    marker.setMap(null);
                                    marker = new google.maps.Marker({
                                        position: e.latLng,
                                        map: map,
                                        icon: icon,
                                        draggable: true,
                                        title:'" + cop.Cooperative_Name + @"'
                                    });
                                    document.getElementById('txtlat').value = e.latLng.lat().toString() + ', ' + e.latLng.lng().toString();
                                });
                            }
                            google.maps.event.addDomListener(window, 'load', initialize);
                        </script>";

        }
        private void check()
        {
            Cooperative_Info cop = new Cooperative_Info(Ckey);
            if (cop.DateRange.ToString() != "1/1/0001 12:00:00 AM")
            {
                DateRange = Convert.ToDateTime(cop.DateRange).ToString("dd/MM/yyyy");
            }
            if (cop.DateExpiration.ToString() != "1/1/0001 12:00:00 AM")
            {
                DateExpiration = Convert.ToDateTime(cop.DateExpiration).ToString("dd/MM/yyyy");
            }
            if (cop.CertifiedOrganization.ToString() != "")
            {
                CertifiedOrganization = cop.CertifiedOrganization;
            }
            if (cop.ProvincesCities_ID.ToString() != "")
            {
                ProvincesCities_ID = cop.ProvincesCities_ID;
            }
            if (cop.Images.ToString() != "")
            {
                Images = cop.Images;
            }
        }
        private void lb(bool check)
        {
            lbCooperative_ID.Visible = check;
            lbCooperative_Name.Visible = check;
            lbProvincesCitiesID.Visible = check;
            lbAddress.Visible = check;
            lbVietGAPCode.Visible = check;
            lbPhone.Visible = check;
            lbMembers.Visible = check;
            lbArea.Visible = check;
            lbQuantity.Visible = check;
            lbTreeType.Visible = check;
            lbDateRange.Visible = check;
            lbDateExpiration.Visible = check;
            lbOwner.Visible = check;
            lbDescription.Visible = check;
            lbCertifiedOrganization.Visible = check;
            cmdEdit.Visible = check;
            
            if (keyHTX < 2 | keyHTX > 3)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
            }
            if (keyHTX == 2)
            {
                cmdEdit.Visible = false;
                cmdDelete.Visible = false;
                SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
                if (nUserLogin.CooperativeKey == this.Request["Key"].ToInt())
                {
                    cmdEdit.Visible = true;
                    cmdDelete.Visible = true;
                }
            }
        }
        private void txt(bool check)
        {
            txtCooperative_ID.Visible = check;
            txtCooperative_Name.Visible = check;
            ddProvincesCitiesID.Visible = check;
            txtAddress.Visible = check;
            txtVietGAPCode.Visible = check;
            txtPhone.Visible = check;
            txtMembers.Visible = check;
            txtArea.Visible = check;
            txtQuantity.Visible = check;
            txtTreeType.Visible = check;
            txtDateRange.Visible = check;
            txtDateExpiration.Visible = check;
            txtOwner.Visible = check;
            txtDescription.Visible = check;
            ddCertifiedOrganization.Visible = check;
            ddImage.Visible = check;
            FileUploadControl.Visible = check;
            lbimage.Visible = check;
            lbupimage.Visible = check;
            txtlat.Visible = check;
            cmdSave.Visible = check;
            FileUpload1.Visible = check;
            FileUpload2.Visible = check;
            FileUpload3.Visible = check;
            FileUpload4.Visible = check;
            FileUpload5.Visible = check;
            FileUpload6.Visible = check;
            FileUpload7.Visible = check;
            FileUpload8.Visible = check;
            FileUpload9.Visible = check;
            FileUpload10.Visible = check;
            ImageButton1.Visible = check;
            ImageButton2.Visible = check;
            ImageButton3.Visible = check;
            ImageButton4.Visible = check;
            ImageButton5.Visible = check;
            ImageButton6.Visible = check;
            ImageButton7.Visible = check;
            ImageButton8.Visible = check;
            ImageButton9.Visible = check;
            ImageButton10.Visible = check;
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
        protected void cmdEdit_Click(object sender, ImageClickEventArgs e)
        {
            Ckey = Convert.ToInt32(lbkey.Text);
            loadtxt(Ckey);
            cmdEdit.Visible = false;
            cmdDelete.Visible = false;
        }
        protected void cmdSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveInfo();
            CloseForm();
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
                Cooperative_Info info = new Cooperative_Info(Convert.ToInt32(keylb));
                info.Delete();
                CloseForm();
            }
            else
            {

            }
        }
        private void DeleteImage(string Name)
        {
            string filename = lbkey.Text + "_img"+Name+".png";
            string fullPath = Request.MapPath("~/Img/coop/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            Cooperative_Image_Data.DeleteImg(lbkey.Text+"_"+Name);
            CloseForm();
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("1");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("2");
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("3");
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("4");
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("5");
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("6");
        }

        protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("7");
        }

        protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("8");
        }

        protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("9");
        }

        protected void ImageButton10_Click(object sender, ImageClickEventArgs e)
        {
            DeleteImage("10");
        }        
    }
}