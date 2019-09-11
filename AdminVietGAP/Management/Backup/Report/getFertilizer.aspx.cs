using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Management.Report
{
    public partial class getFertilizer : System.Web.UI.Page
    {
        string dataRpt = "";
        TNLibrary.SYS.SessionUserLogin nUserLogin;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["dataRpt"] = dataRpt;
            if (!Page.IsPostBack)
            {
                nUserLogin = (TNLibrary.SYS.SessionUserLogin)Session["UserLogin"];
                if (nUserLogin != null)
                {
                    for (int i = 2000; i <= DateTime.Now.Year; i++)
                    {
                        DropDownListFromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        DropDownListToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    switch (nUserLogin.GroupKey)
                    {
                        case 1://Xa vien
                            Permit(false, false, false, false);
                            break;
                        case 2://Hop tac xa
                            Permit(false, false, false, true);
                            FillDropDownList(DropDownListMembers, ReportPUL2.ReportPUL.getMember(nUserLogin.CooperativeKey), "Name", "Key", "--- Chọn xã viên ---");
                            break;
                        case 3://Lien hop tac xa
                            Permit(false, false, true, true);
                            FillDropDownList(DropDownListCooperatives, ReportPUL2.ReportPUL.getCooperativeVenture(nUserLogin.CooperativeKey), "CooperativeVenturesName", "CooperativeVenturesKey", "--- Chọn hợp tác xã ---");
                            break;
                        case 4://Admin
                            Permit(true, true, true, true);
                            FillDropDownList(DropDownListProvinces, ReportPUL2.ReportPUL.getProvincesCities(), "ProvincesCities_Name", "ProvincesCities_Key");
                            break;
                    }
                }
            }
        }
        protected void DropDownListProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListCooperativeVentures.Items.Clear();
            DropDownListCooperatives.Items.Clear();
            DropDownListMembers.Items.Clear();
            FillDropDownList(DropDownListCooperativeVentures, ReportPUL2.ReportPUL.getCooperativeVenture(int.Parse(DropDownListProvinces.SelectedValue)), "CooperativeVenturesName", "CooperativeVenturesKey", "--- Chọn liên HTX ---");
        }

        protected void DropDownListCooperativeVentures_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListCooperatives.Items.Clear();
            DropDownListMembers.Items.Clear();
            if (DropDownListCooperativeVentures.SelectedIndex > 0)
            {
                FillDropDownList(DropDownListCooperatives, ReportPUL2.ReportPUL.getCooperative(int.Parse(DropDownListCooperativeVentures.SelectedValue)), "Cooperative_Name", "Cooperative_Key", "--- Chọn hợp tác xã ---");
            }
        }

        protected void DropDownListCooperatives_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListMembers.Items.Clear();
            if (DropDownListCooperatives.SelectedIndex > 0)
            {
                FillDropDownList(DropDownListMembers, ReportPUL2.ReportPUL.getMember(int.Parse(DropDownListCooperatives.SelectedValue)), "Name", "Key", "--- Chọn xã viên ---");
            }
        }

        protected void ButtonPreview_Click(object sender, EventArgs e)
        {
            dataRpt = "<p style='color: #1408C5; font-weight: bold;'>THỐNG KÊ KẾT QUẢ</p>";
            //Print
            nUserLogin = (TNLibrary.SYS.SessionUserLogin)Session["UserLogin"];
            if (nUserLogin.GroupKey == 1 || DropDownListMembers.SelectedIndex > 0)
            {//Print a member                
                dataRpt += "<div style='margin-left: 20px;'><p>+ Xã viên: " + (DropDownListMembers.SelectedIndex > 0 ? DropDownListMembers.SelectedItem.Text : nUserLogin.Name) + "</p>";
                dataRpt += PrintMember(DropDownListMembers.SelectedIndex > 0 ? DropDownListMembers.SelectedValue : nUserLogin.EmployeeKey.ToString(), int.Parse(DropDownListFromYear.SelectedValue), int.Parse(DropDownListToYear.SelectedValue));
                dataRpt += "</div>";
            }
            else if ((nUserLogin.GroupKey == 2 && DropDownListMembers.SelectedIndex == 0) || DropDownListCooperatives.SelectedIndex > 0)
            {//Print a cooperative
                System.Data.DataTable users = ReportPUL2.ReportPUL.getMember(DropDownListCooperatives.SelectedIndex > 0 ? int.Parse(DropDownListCooperatives.SelectedValue) : nUserLogin.CooperativeKey);
                dataRpt += "<div><p style='font-weight:bold; color: red;'>+ Hợp tác xã: " + ReportPUL2.ReportPUL.getCooperativeInfo(DropDownListCooperatives.SelectedIndex > 0 ? int.Parse(DropDownListCooperatives.SelectedValue) : nUserLogin.CooperativeKey).Rows[0]["Cooperative_Name"].ToString() + "</p>";
                //Print cooperative total
                dataRpt += "<div>";
                dataRpt += PrintMember("SELECT [Key] FROM Pul_Member WHERE Cooperative_Key=" + (DropDownListCooperatives.SelectedIndex > 0 ? int.Parse(DropDownListCooperatives.SelectedValue) : nUserLogin.CooperativeKey), int.Parse(DropDownListFromYear.SelectedValue), int.Parse(DropDownListToYear.SelectedValue));
                dataRpt += "</div>";
                for (int i = 0; i < users.Rows.Count; i++)
                {
                    dataRpt += "<div style='margin-left: 20px;'><p style='text-decoration: underline; font-weight: bold;'>" + (i + 1) + ". Xã viên: " + users.Rows[i]["Name"].ToString() + "</p>";
                    dataRpt += PrintMember(users.Rows[i]["Key"].ToString(), int.Parse(DropDownListFromYear.SelectedValue), int.Parse(DropDownListToYear.SelectedValue));
                    dataRpt += "</div>";
                }
                dataRpt += "</div>";
            }
            else if ((nUserLogin.GroupKey == 3 && DropDownListCooperatives.SelectedIndex == 0) || DropDownListCooperativeVentures.SelectedIndex > 0)
            {//Print cooperativeventure
                System.Data.DataTable coopeartives = ReportPUL2.ReportPUL.getCooperative(DropDownListCooperativeVentures.SelectedIndex > 0 ? int.Parse(DropDownListCooperativeVentures.SelectedValue) : nUserLogin.CooperativeVenturesKey);
                dataRpt += "<div><p style='font-weight: bold;'>+ Liên hợp tác xã: " + ReportPUL2.ReportPUL.getCooperativeVentureInfo(DropDownListCooperativeVentures.SelectedIndex > 0 ? int.Parse(DropDownListCooperativeVentures.SelectedValue) : nUserLogin.CooperativeVenturesKey).Rows[0]["CooperativeVenturesName"].ToString() + "</p>";
                //Print cooperative total
                dataRpt += "<div>";
                dataRpt += PrintMember("SELECT [Key] FROM Pul_Member WHERE Cooperative_Key in (select cooperative_key from pul_listcooperative where CooperativeVenturesKey=" + (DropDownListCooperativeVentures.SelectedIndex > 0 ? int.Parse(DropDownListCooperativeVentures.SelectedValue) : nUserLogin.CooperativeVenturesKey) + ")", int.Parse(DropDownListFromYear.SelectedValue), int.Parse(DropDownListToYear.SelectedValue));
                dataRpt += "</div>";
                for (int h = 0; h < coopeartives.Rows.Count; h++)
                {
                    System.Data.DataTable users = ReportPUL2.ReportPUL.getMember(int.Parse(coopeartives.Rows[h]["Cooperative_Key"].ToString()));
                    dataRpt += "<div style='margin-left: 10px;'><p style='font-weight:bold; color: red;'>" + (h + 1) + ". Hợp tác xã: " + coopeartives.Rows[h]["Cooperative_Name"].ToString() + "</p>";
                    //Print cooperative total
                    dataRpt += "<div>";
                    dataRpt += PrintMember("SELECT [Key] FROM Pul_Member WHERE Cooperative_Key=" + (int.Parse(coopeartives.Rows[h]["Cooperative_Key"].ToString())), int.Parse(DropDownListFromYear.SelectedValue), int.Parse(DropDownListToYear.SelectedValue));
                    dataRpt += "</div>";
                    for (int i = 0; i < users.Rows.Count; i++)
                    {
                        dataRpt += "<div style='margin-left: 20px;'><p style='text-decoration: underline; font-weight: bold;'>" + (i + 1) + ". Xã viên: " + users.Rows[i]["Name"].ToString() + "</p>";
                        dataRpt += PrintMember(users.Rows[i]["Key"].ToString(), int.Parse(DropDownListFromYear.SelectedValue), int.Parse(DropDownListToYear.SelectedValue));
                        dataRpt += "</div>";
                    }
                    dataRpt += "</div>";
                }
                dataRpt += "</div>";
            }
            Session["dataRpt"] = dataRpt;
        }

        private string PrintMember(string listUser, int fromYear, int toYear)
        {
            string st = "Không có dữ liệu!";
            System.Data.DataTable dataTotal = ReportPUL2.ReportPUL.getFertilizer(listUser, fromYear, toYear);
            System.Collections.ArrayList a = new System.Collections.ArrayList();
            System.Collections.ArrayList b = new System.Collections.ArrayList();
            getParamsArray(dataTotal, a, b);
            a.Sort(); b.Sort();
            string codeNam = "", codeTitle = "", codeValue = "";
            for (int i = 0; i < a.Count; i++)
            {
                codeNam += "<td colspan='2'>" + (int.Parse(a[i].ToString())) + "</td>";
                codeTitle += "<td>Số hộ SD</td><td>Số lượng</td>";
            }

            for (int i = 0; i < b.Count; i++)
            {
                codeValue += "<tr><td>" + (i + 1) + "</td><td>" + b[i].ToString() + "</td>";
                for (int j = 0; j < a.Count; j++)
                {
                    codeValue += SearchValue(dataTotal, a[j].ToString(), b[i].ToString());
                }
                codeValue += "</tr>";
            }
            if (a.Count > 0 || b.Count > 0)
                st = @"<table id='tbl_linh'>
                                            <tr id='tbl_linh_header'>
                                                <td rowspan='2'>STT</td>
                                                <td rowspan='2'>Tên sản phẩm</td>" +
                                      codeNam
                                    + @"
                                            </tr>
                                            <tr id='tbl_linh_header'>" +
                                     codeTitle
                                + @"</tr>" +
                                    codeValue
                                + @"
                                         </table>";
            return st;
        }
        protected string SearchValue(System.Data.DataTable dataTotal, string year, string seedsname)
        {
            string st = "";
            for (int i = 0; i < dataTotal.Rows.Count; i++)
            {
                if (dataTotal.Rows[i]["TradeName"].ToString() == seedsname && dataTotal.Rows[i]["UsedYear"].ToString() == year)
                {
                    string soluong = dataTotal.Rows[i]["SumFormulaUsed"].ToString();
                    if (soluong.Length > 0)
                        soluong += "kg";
                    st = "<td>" + dataTotal.Rows[i]["CountMember"].ToString() + "</td><td>" + soluong + "</td>";
                }
            }
            if (st.Length <= 0)
                st = "<td></td><td></td>";
            return st;
        }
        private void getParamsArray(System.Data.DataTable dataTotal, System.Collections.ArrayList a, System.Collections.ArrayList b)
        {
            for (int i = 0; i < dataTotal.Rows.Count; i++)
            {
                if (!a.Contains(dataTotal.Rows[i]["UsedYear"].ToString()))
                    a.Add(dataTotal.Rows[i]["UsedYear"].ToString());
                if (!b.Contains(dataTotal.Rows[i]["TradeName"].ToString()))
                    b.Add(dataTotal.Rows[i]["TradeName"].ToString());
            }
        }
        private void FillDropDownList(DropDownList ddl, System.Data.DataTable DataSource, string DataTextField, string DataValueField, string headerText = "")
        {
            ddl.DataSource = DataSource;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            if (headerText != "")
                ddl.Items.Insert(0, headerText);
        }
        private void Permit(bool Provinces, bool CooperativeVentures, bool Cooperatives, bool Members)
        {
            DropDownListProvinces.Visible = Provinces;
            DropDownListCooperativeVentures.Visible = CooperativeVentures;
            DropDownListCooperatives.Visible = CooperativeVentures;
            DropDownListMembers.Visible = Members;
        }
    }
}