using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

using TNLibrary.Culture;
using TNLibrary.WEB;
using TNLibrary.SYS;
namespace Management.Culture
{
    public partial class Training_Edit : System.Web.UI.Page
    {
        public string Job = "";
        public string TrainingContent = "";
        public string Trainer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CU0009");
                DataTable dt = Training_Data.GetJobList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Job += "\"" + dt.Rows[i]["Job"].ToString() + "\",";
                }
                dt = Training_Data.GetTrainingContentList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TrainingContent += "\"" + dt.Rows[i]["TrainingContent"].ToString() + "\",";
                }
                dt = Training_Data.GetTrainerList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Trainer += "\"" + dt.Rows[i]["Trainer"].ToString() + "\",";
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
                        if (!nUserLogin.Role.Edit && nKey != "0")
                        {
                            cmdSave.Visible = false;
                            txtDatetime.Enabled = false;
                            txtJob.Enabled = false;
                            txtTrainer.Enabled = false;
                            txtTrainingContent.Enabled = false;
                            txtTrainingTime.Enabled = false;
                        }
                        else if (!nUserLogin.Role.Add && nKey == "0")
                        {
                            cmdSave.Visible = false;
                            txtDatetime.Enabled = false;
                            txtJob.Enabled = false;
                            txtTrainer.Enabled = false;
                            txtTrainingContent.Enabled = false;
                            txtTrainingTime.Enabled = false;
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
            Training_Info info = new Training_Info(Key);
           
            if (Key == 0)
            {
                DateTime time = DateTime.Now;
                txtDatetime.Text = time.ToString("dd/MM/yyyy");
                txtTrainingTime.Text = time.ToString("dd/MM/yyyy");
            }
            else
            {
                txtDatetime.Text = info.Datetime.ToString("dd/MM/yyyy");
                txtTrainingTime.Text = info.TrainingTime.ToString("dd/MM/yyyy");
            }
            txtJob.Text = info.Job;
            txtTrainingContent.Text = info.TrainingContent;
            txtTrainer.Text = info.Trainer;
        }
        protected void SaveInfo()
        {
            Training_Info info = new Training_Info(int.Parse(txtKey.Text));
            info.Datetime = DateTime.ParseExact(txtDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.TrainingTime = DateTime.ParseExact(txtTrainingTime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            info.Job = txtJob.Text;
            info.TrainingContent = txtTrainingContent.Text;
            info.Trainer = txtTrainer.Text;
            info.MemberKey = Session["EmployeeKey"].ToInt();
            info.Save();
        }

        protected void DDLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}