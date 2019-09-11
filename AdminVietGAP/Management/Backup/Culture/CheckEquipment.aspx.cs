﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TNLibrary.Culture;
using System.Data;
using System.Globalization;
using TNLibrary.SYS;
using TNLibrary.WEB;

namespace Management.Culture
{
    public partial class CheckEquipment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
             if (nUserLogin != null)
             {
                 nUserLogin.CheckRole("CU0010");
                 if (!nUserLogin.Role.Read)
                 {
                     //Response.Write("<script >alert('Bạn không có quyền xem danh mục này!')</script>");
                     Response.Write("<script >window.location.href='Error.aspx';</script>");
                 }
                 else
                 {
                     if (!IsPostBack)
                     {
                         string SQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.SeedsName
                        FROM         dbo.PUL_SeedProcess INNER JOIN
                      dbo.PUL_CheckEquipment ON dbo.PUL_SeedProcess.SeedProcessKey = dbo.PUL_CheckEquipment.SeedsKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey WHERE dbo.PUL_SeedProcess.MemberKey = " + Convert.ToInt16(Session["EmployeeKey"]) + " GROUP BY dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.SeedsName";
                         LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, SQL, false);  
                         LoadData();
                         
                     }
                 }
             }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            txtPageNumber.Text = "1";
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = CheckEquipment_Data.GetList(dtfrom, dtto, Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber, DDLSeeds.SelectedValue);
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", 0, 0, "", "", null, 0,"");
            }
            GV_CheckEquipment.DataSource = nTable;
            GV_CheckEquipment.DataBind();
            LoadPages();
        }
        public void LoadData()
        {
            //LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "Select SeedsKey,SeedsName from PUL_Seeds where SeedsKey IN (Select SeedsKey FROM PUL_CheckEquipment WHERE MemberKey = " + Convert.ToInt16(Session["EmployeeKey"]) + ")", false);
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            DataTable nTable = CheckEquipment_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber, DDLSeeds.SelectedValue);
            GV_CheckEquipment.DataSource = nTable;
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add("", 0, 0, "", "", null, 0,"");
            }
            GV_CheckEquipment.DataBind();
            DateTime dt = DateTime.Now;
            txtfromDatetime.Text = "01/" + DateTime.Now.ToString("MM/yyyy");
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadPages();
        }
        protected void GrDelete(object sender, CommandEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
             SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            nUserLogin.CheckRole("CU0010");
            if (!nUserLogin.Role.Del)
            {
                Response.Write("<script >alert('Bạn không có quyền xóa danh mục này!')</script>");
            }
            else
            {
                try
                {
                    int mKey = int.Parse(e.CommandArgument.ToString());
                    CheckEquipment_Info info = new CheckEquipment_Info(mKey);
                    if ((DateTime.Now - info.Datetime).TotalDays > 7)
                    {
                        Response.Write("<script >alert('Dữ liệu đã quá 7 ngày, không được phép xóa!')</script>");
                    }
                    else
                    {
                        info.Delete();
                        DataTable nTable = CheckEquipment_Data.GetList(Convert.ToInt16(Session["EmployeeKey"]), nPageSize, nPageNumber, DDLSeeds.SelectedValue);
                        if (nTable.Rows.Count == 0)
                        {
                            nTable.Rows.Add("", 0, 0, "", "", null, 0,"");
                        }
                        GV_CheckEquipment.DataSource = nTable;
                        GV_CheckEquipment.DataBind();
                        LoadPages();
                    }
                }
                catch
                {
                    Response.Write("<script >alert('Có lỗi gì đó vui lòng thử lại!')</script>");
                }
            }
        }


        //protected void OnPaging(object sender, GridViewPageEventArgs e)
        //{

        //    GV_CheckEquipment.PageIndex = e.NewPageIndex;
        //    DataTable nTable = CheckEquipment_Data.GetList();
        //    GV_CheckEquipment.DataSource = nTable;
        //    GV_CheckEquipment.DataBind();
            

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
        protected void LoadPages()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);

            int nTotalRecord = CheckEquipment_Data.Count(Session["EmployeeKey"].ToInt(), DDLSeeds.SelectedValue);
            if (nTotalRecord > nPageSize)
            {
                PageNumbers.Visible = true;
                DataTable nTable = TNLibrary.WEB.LoadDataToToolboxWeb.LoadPageSize(nPageSize, nTotalRecord, nPageNumber);

                PageNumbers.SelectedIndex = (nPageNumber - 1) - (int.Parse(nTable.Rows[0][0].ToString()) - 1);
                PageNumbers.DataSource = nTable;
                PageNumbers.DataBind();
            }
            else
                PageNumbers.Visible = false;

        }
        protected void PageNumber_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string nPageNumberKey = PageNumbers.DataKeys[e.Item.ItemIndex].ToString();
            txtPageNumber.Text = nPageNumberKey;

            PageNumbers.SelectedIndex = e.Item.ItemIndex;
            LoadPages();
            LoadData();
        }

        protected int ShowNo(int index)
        {
            if (int.Parse(txtPageNumber.Text) > 1)
            {
                return ((int.Parse(txtPageNumber.Text) - 1) * int.Parse(txtPageSize.Text)) + index;
            }
            else
            {
                return index;
            }
        }

        protected void DDLSeeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}