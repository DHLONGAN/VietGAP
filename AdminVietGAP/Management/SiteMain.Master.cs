using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using TNLibrary.WEB;
using TNLibrary.SYS;
using TNLibrary.Fields;

namespace Management
{
    public partial class SiteMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin == null)
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Vui lòng đăng nhập lại');", true);
                Page.Response.Redirect("/Login.aspx");
                //Page.ClientScript.RegisterStartupScript(GetType(), "hwda", "window.location.href='Default.aspx'", true);
                //return;
            }
            if (!IsPostBack)
            {
                if (Session["UserLogin"] == null)
                    Response.Redirect("~/Login.aspx");
                
                txtUserLogin.Text = nUserLogin.Name;
                txtMemberName.Text = Session["MemberName"].ToString();
                string _SQL = "SELECT Common_Key,Common_Name FROM PUL_Fertilizer_Common";
                if (Session["EmployeeKey"] == null)
                {
                    Session["EmployeeKey"] = nUserLogin.EmployeeKey;
                }
                LoadMenu();

                string zSQL = "SELECT Cooperative_Key,Cooperative_Name FROM PUL_Cooperative";
                //LoadDataToToolboxWeb.DropDown_DDL(DLLCooperative, "SELECT Common_Key,Common_Name FROM PUL_Fertilizer_Common", true);
                //txtCooperative_Name.Text = nUserLogin.BranchName;
                //Session["CooperativeKey"] = nUserLogin.CooperativeKey;
                Session["created"] = false;

                if (nUserLogin.GroupKey == 1 || nUserLogin.GroupKey == 2)
                {
                    zSQL += " WHERE Cooperative_Key ='" + nUserLogin.CooperativeKey + "'";
                }
                else if (nUserLogin.GroupKey == 3)
                {
                    zSQL += " WHERE Cooperative_Key IN (Select Cooperative_Key FROM PUL_ListCooperative WHERE  CooperativeVenturesKey = " + nUserLogin.CooperativeVenturesKey + ")";
                }
                //DLLCooperative.DataSource = null;
                //DLLCooperative.SelectedValue = null;
                LoadDataToToolboxWeb.DropDown_DDL(DLLCooperative, zSQL, false);
                if (Session["CooperativeKey"] != null)
                {
                    DLLCooperative.SelectedValue = Session["CooperativeKey"].ToString();
                }
            }
            if (nUserLogin.GroupKey < 2)
            {
                pnlChose.Visible = false;
            }
            if (Session["IsFist"] == null || !(bool)Session["IsFist"])
            {
                Session["IsFist"] = true;
                Session["GroupKey"] = nUserLogin.GroupKey;
            }
            if (DLLCooperative.SelectedValue != "")
            {
                Session["CooperativeKey"] = DLLCooperative.SelectedValue;
            }
        }

        private void LoadMenu()
        {
            
            string nURL = Request.Url.AbsolutePath;
            string[] _MenuModule = nURL.Split('/');
            int nKey = 0;
            string _Title = "";
            DataTable _Table = Web_Menu_Data.List(0);

            StringBuilder nMenuTop = new StringBuilder();
            nMenuTop.AppendLine("<ul>");
            nMenuTop.AppendLine("<li class='space'></li>");
            foreach (DataRow nRow in _Table.Rows)
            {
                if (_MenuModule[1].ToUpper() == nRow["Module"].ToString().ToUpper())
                {
                    nMenuTop.AppendLine("<li class='selected'><img src='/Img/Menu/" + nRow["Icon"] + "_selected.png'/></li>");
                    nKey = int.Parse(nRow["MenuKey"].ToString());
                    _Title = nRow["MenuName"].ToString();
                }
                else
                    nMenuTop.AppendLine("<li><a href='" + nRow["MenuLink"] + "'><img src='/Img/Menu/" + nRow["Icon"] + ".png'/></a></li>");
                nMenuTop.AppendLine("<li class='space'></li>");
            }
            nMenuTop.AppendLine("</ul>");
            txtTitleCategory.Text = _Title;
            LiteMenuTop.Text = nMenuTop.ToString();
            LoadMenuSub(nKey);
        }
        private void LoadMenuSub(int Parent)
        {
            StringBuilder nMenuSub = new StringBuilder();
            DataTable _Table = Web_Menu_Data.List(Parent);
            string MenuIDActive = "";
            foreach (DataRow nRow in _Table.Rows)
            {
                int Key = Convert.ToInt32(nRow["MenuKey"]);

                DataTable _TableSub = Web_Menu_Data.List(Key);

                if (_TableSub.Rows.Count > 0)
                {

                    nMenuSub.AppendLine("<div id='Show" + nRow["MenuID"].ToString() + "' class='ListItemParent' onclick='SetMenuParent(this.id)'>" + nRow["MenuName"].ToString() + "</div>");
                    nMenuSub.AppendLine("<div class='Line2' style='clear: both'></div>");
                    nMenuSub.AppendLine("<div id='ViewShow" + nRow["MenuID"].ToString() + "' class='Categories' style='display:none'>");
                    nMenuSub.AppendLine("<ul>");
                    foreach (DataRow nRowSub in _TableSub.Rows)
                    {
                        if (nRowSub["MenuLink"].ToString() == Request.Url.AbsolutePath)
                        {
                            nMenuSub.AppendLine("<li class='ItemSubActivate' id='MenuKey_" + nRowSub["MenuKey"].ToString() + "' onclick='SetStatusLayer(this.id," + nRowSub["MenuKey"].ToString() + ")' >" + nRowSub["MenuName"].ToString() + "</li>");
                            MenuIDActive = "Show" + nRow["MenuID"].ToString();
                        }
                        else
                            nMenuSub.AppendLine("<li class='ItemSub' id='MenuKey_" + nRowSub["MenuKey"].ToString() + "' onclick='SetStatusLayer(this.id," + nRowSub["MenuKey"].ToString() + ")' ><a href='" + nRowSub["MenuLink"].ToString() + "'>" + nRowSub["MenuName"].ToString() + "<a></li>");

                    }
                    nMenuSub.AppendLine("</ul>");
                    nMenuSub.AppendLine("<div class='Line2' style='clear: both'></div>");
                    nMenuSub.AppendLine("</div>");

                }

            }
            StringBuilder nMenuActivate = new StringBuilder();
            nMenuActivate.AppendLine("<script type='text/javascript'>");
            nMenuActivate.AppendLine("$(document).ready(function () {");
            nMenuActivate.AppendLine("SetMenuParent('" + MenuIDActive + "')");
            nMenuActivate.AppendLine(" }); </script>");
            LiteMenuSub.Text = nMenuSub.ToString();
            LiteSetMenuActive.Text = nMenuActivate.ToString();
            // txtTitleCategory.Text = Request.Url.AbsolutePath;
        }

        protected string listMember(string id)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin == null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "hwa", "alert('Vui lòng đăng nhập lại');", true);
                Page.ClientScript.RegisterStartupScript(GetType(), "hwda", "window.location.href='Default.aspx'", true);
                return "";
            }
            if (nUserLogin.GroupKey >= 2)
            {
                int CooperativeKey = int.Parse(DLLCooperative.SelectedValue);
                DataTable nMember = Member_Info.GetList(CooperativeKey);
                string listMember = "";
                for (int j = 0; j < nMember.Rows.Count; j++)
                {
                    //listMember += nMember.Rows[j]["Cooperative_Key"] + " " + nMember.Rows[j]["Name"] + "</p>";
                    listMember += "{Cooperative_Key:'" + nMember.Rows[j]["Cooperative_Key"] + "',Member_Key:'" + nMember.Rows[j]["Key"] + "',Member_Name:'" + nMember.Rows[j]["Name"] + "'},";
                }
                return listMember;
            }
            else
            {
                return "";
            }
        }

        protected int Cooperative_Key()
        {
            //SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            //return nUserLogin.CooperativeKey;
            return int.Parse(DLLCooperative.SelectedValue);
        }

        protected void DLLCooperative_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CooperativeKey"] = DLLCooperative.SelectedValue;
            Response.Redirect(Request.Url.AbsoluteUri);
            //string nUrl = "<script>CloseOnReload()</script>";

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", nUrl);
        }
    }
}