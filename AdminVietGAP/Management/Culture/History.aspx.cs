﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TNLibrary.Culture;
using TNLibrary.SYS;
using System.Globalization;
using TNLibrary.WEB;

namespace Management.Culture
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            if (nUserLogin != null)
            {
                //nUserLogin.CheckRole("CU0002");
                //if (!nUserLogin.Role.Read)
                //{
                //    //Response.Write("<script >alert('Bạn không có quyền xem danh mục này!')</script>");
                //    Response.Write("<script >window.location.href='Error.aspx';</script>");
                //}
                //else
                {
                    if (!IsPostBack)
                    {
                        int nPageNumber = int.Parse(txtPageNumber.Text);
                        int nPageSize = int.Parse(txtPageSize.Text);
                        DataTable nTable = History_Data.GetListofMember(nPageSize, nPageNumber, Session["EmployeeKey"].ToInt());
                        string listseeds = "";
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            if (i < nTable.Rows.Count - 1)
                            {
                                listseeds += nTable.Rows[i]["Name"].ToString() + ",";
                            }
                            else
                            {
                                listseeds += nTable.Rows[i]["Name"].ToString();
                            }
                        }
                        //LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "Select SeedsName,SeedsName from PUL_Seeds where SeedsKey IN (" + listseeds + ")", false);
                        LoadDataToToolboxWeb.DropDown_DDL(DDLSeeds, "Select SeedsName,SeedsName from PUL_Seeds where SeedsKey IN(Select SeedsKey from PUL_SeedProcess  where dbo.PUL_SeedProcess.MemberKey = " + Convert.ToInt16(Session["EmployeeKey"]).ToString() + ")", false);
                        LoadData();
                    }
                }
            }
        }
        protected void cmdView_Click1(object sender, ImageClickEventArgs e)
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DateTime dtfrom = DateTime.ParseExact(txtfromDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtto = DateTime.ParseExact(txttoDatetime.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable nTable = History_Data.GetList(dtfrom, dtto, Convert.ToInt16(Session["CooperativeKey"]), nPageSize, nPageNumber, Session["EmployeeKey"].ToInt(), DDLSeeds.SelectedValue);
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(null, "", 0);
            }
            GV_History.DataSource = nTable;
            GV_History.DataBind();
            LoadPages();
        }
        public void LoadData()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);
            SessionUserLogin nUserLogin = (SessionUserLogin)Session["UserLogin"];
            DataTable nTable = History_Data.GetList(Convert.ToInt16(Session["CooperativeKey"]), nPageSize, nPageNumber, Session["EmployeeKey"].ToInt(), DDLSeeds.SelectedValue);
            if (nTable.Rows.Count == 0)
            {
                nTable.Rows.Add(null,"",0);
            }
            nTable.Columns.Add();
            GV_History.DataSource = nTable;
            GV_History.DataBind();
            DateTime dt = DateTime.Now;
            txtfromDatetime.Text = "01/01/2013";
            txttoDatetime.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadPages();
        }
        


        protected void LoadPages()
        {
            int nPageNumber = int.Parse(txtPageNumber.Text);
            int nPageSize = int.Parse(txtPageSize.Text);

            int nTotalRecord = History_Data.Count(Session["EmployeeKey"].ToInt(), DDLSeeds.SelectedValue);

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
        public string ConvertDate(string dt)
        {
            if (dt != "")
            {
                DateTime dt2 = Convert.ToDateTime(dt);
                return dt2.Day.ToString() + "/" + dt2.Month.ToString() + "/" + dt2.Year.ToString();
            }
            return "";
        }

        protected void DDLSeeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}