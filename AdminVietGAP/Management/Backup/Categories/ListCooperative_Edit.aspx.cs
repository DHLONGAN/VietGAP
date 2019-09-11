using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.SYS;
using System.Data;
using TNLibrary.Sys;
using TNLibrary.Categories;

namespace Management.Sys
{
    public partial class ListCooperative_Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            Session["CooperativeVenturesKey"] = this.Request["key"];
            txtKey.Text = this.Request["key"];
            if (nUserLogin != null)
            {
                nUserLogin.CheckRole("CA0014");
                if (!nUserLogin.Role.Read)
                {
                    //Response.Write("<script >alert('Bạn không có quyền xem danh mục này!')</script>");
                    Response.Write("<script >window.location.href='Error.aspx';</script>");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        LoadData();
                    }
                }
            }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = Cooperative_Data.GetList(int.Parse(txtKey.Text));
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, "");
            }
            GV_Cooperative.DataSource = nTable;
            GV_Cooperative.DataBind();
            //LoadPages();
        }
        public void LoadData()
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = Cooperative_Data.GetList(int.Parse(txtKey.Text));
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(0, "");//, null, "", 0, 0, 0, "", "", "", "", 0);
            }
            nTable.Columns.Add();
            GV_Cooperative.DataSource = nTable;
            GV_Cooperative.DataBind();
            DateTime dt = DateTime.Now;
            
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CA0014");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    string mKey = (e.CommandArgument.ToString());
                    ListCooperative_Info info = new ListCooperative_Info(txtKey.Text, mKey);
                    //if ((DateTime.Now - info.DatetimeBuy).TotalDays > 7)
                    //{
                    //    Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    //}
                    //else
                    {
                        info.Delete();
                        DataTable nTable = Cooperative_Data.GetList(int.Parse(txtKey.Text.ToString()));//.GetLits();''xtKey.Text));
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add(0, "");
                        }
                        GV_Cooperative.DataSource = nTable;
                        GV_Cooperative.DataBind();
                        //LoadPages();
                    }
                }
                catch
                {
                    Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
                }
            }
        }


        //protected void LoadPages()
        //{
        //    int nPageNumber = int.Parse(txtPageNumber.Text);
        //    int nPageSize = int.Parse(txtPageSize.Text);

        //    int nTotalRecord = SeedProcess_Data.Count();
        //    if (nTotalRecord > nPageSize * 5)
        //        nTotalRecord = nPageSize * 5;
        //    if (nTotalRecord > nPageSize)
        //    {
        //        PageNumbers.Visible = true;
        //        DataTable nTable = TNLibrary.WEB.LoadDataToToolboxWeb.LoadPageSize(nPageSize, nTotalRecord);

        //        PageNumbers.SelectedIndex = nPageNumber - 1;
        //        PageNumbers.DataSource = nTable;
        //        PageNumbers.DataBind();
        //    }
        //    else
        //        PageNumbers.Visible = false;

        //}
        //protected void PageNumber_ItemCommand(object source, DataListCommandEventArgs e)
        //{
        //    string nPageNumberKey = PageNumbers.DataKeys[e.Item.ItemIndex].ToString();
        //    txtPageNumber.Text = nPageNumberKey;

        //    PageNumbers.SelectedIndex = e.Item.ItemIndex;
        //    LoadPages();
        //    LoadData();
        //}

        //protected int ShowNo(int index)
        //{
        //    if (int.Parse(txtPageNumber.Text) > 1)
        //    {
        //        return ((int.Parse(txtPageNumber.Text) - 1) * int.Parse(txtPageSize.Text)) + index;
        //    }
        //    else
        //    {
        //        return index;
        //    }
        //}
        public string ConvertDate(string dt)
        {
            if (dt != "")
            {
                DateTime dt2 = Convert.ToDateTime(dt);
                return dt2.Day.ToString() + "/" + dt2.Month.ToString() + "/" + dt2.Year.ToString();
            }
            return "";
        }
    }
}