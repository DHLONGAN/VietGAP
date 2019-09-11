using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using TNLibrary.Search;
using TNLibrary.SYS;
using TNLibrary.SYS.Users;
using TNLibrary.Book;
using System.Web.Services;

namespace BookVietGAP
{
    public partial class SiteBook : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadMenu();
                LoadAccounts();
            }
            
        }
        
        
        private void LoadMenu()
        {
            string nURL = Request.Url.AbsolutePath;
            string[] _MenuModule = nURL.Split('/');
            //DataTable _Table = Web_Menu_SAL.List(0);
            string list = "MenuName:Bản Đồ,MenuLink:/Map.aspx;MenuName:Nhật Ký,MenuLink:/Book.aspx;MenuName:Báo Cáo,MenuLink:/Report.aspx;MenuName:Tra Cứu,MenuLink:/Search.aspx";

            DataTable _Table = new DataTable();
            _Table = convertStringToDataTable(list);
            StringBuilder nMenuTop = new StringBuilder();
            //nMenuTop.AppendLine("<li class='space'></li>");
            foreach (DataRow nRow in _Table.Rows)
            {
                if (_MenuModule[1].ToUpper() == nRow["MenuLink"].ToString().Substring(1).ToUpper())
                {
                    if (_MenuModule[1].ToString() == "Map.aspx")
                    {
                        ltmenuright.Text = @"<li><a href='#confirmSave' data-toggle='modal'><i class='fa fa-save fa-fw'></i>Lưu</a> </li>
                                            <li><a href='#confirmDelete' data-toggle='modal'><i class='fa fa-times-circle fa-fw'></i>Xóa</a> </li>
                                            <li class='divider'></li>
                                            <li><a href='#' onclick='weatherhide()'><i class='fa fa-cloud fa-fw'></i>Thời tiết</a> </li></li>
                                            <li><a href='#' onclick='MyGarden()'><i class='fa fa-map-marker fa-fw'></i>Ruộng của tôi</a> </li></li>
                                            <li class='divider'></li>";
                    }
                    nMenuTop.AppendLine("<li class=''><a href='" + nRow["MenuLink"] + "'><img class='img_menu' src='/Img/Menu/Menu_" + nRow["MenuName"] + "_active.png'></a></li>");
                        
                    
                }
                else
                {
                    nMenuTop.AppendLine("<li><a href='" + nRow["MenuLink"] + "'><img class='img_menu' src='/Img/Menu/Menu_" + nRow["MenuName"] + ".png'></a></li>");
                }
            }
            LiteMenuTop.Text =  nMenuTop.ToString();
        }
        private void LoadAccounts()
        {
            Member_Info info = new Member_Info(Session["MemberID"].ToInt());
            Cooperative_Info minfo = new Cooperative_Info(info.Cooperative_Key);
            ltAccounts.Text = @"<label class='col-sm-3'>Tài khoản :</label><p> " + Session["UserName"] + @"<br></p>
                                <label class='col-sm-3'>Mật khẩu :</label><p><label class='col-sm-2' style='margin-left: -15px;'>******</label><a href='#ChangePassword' data-toggle='modal'>Đổi mật khẩu</a><br></p>
                                <label class='col-sm-3'>Mã Xã viên :</label><p> " + info.MemID + @"<br></p>
                                <label class='col-sm-3'>Hộ sản xuất :</label><p> " + info.Name + @"<br></p>
                                <label class='col-sm-3'>Tên hợp tác xã :</label><p> " + minfo.Cooperative_Name + @"<br></p>
                                <label class='col-sm-3'>Địa chỉ :</label><p> " + info.Address + @"<br></p>
                                <label class='col-sm-3'>Liên hệ(sđt) :</label><p> " + info.Phone + @"<br></p>
                                <label class='col-sm-3'>Diện tích (m2) :</label><p> " + info.Area + @"<br></p>
                                <label class='col-sm-3'>Ghi chú :</label><p> " + info.Description + @"<br></p>";
        }
        public static DataTable convertStringToDataTable(string data)
        {
            DataTable dataTable = new DataTable();
            bool columnsAdded = false;
            foreach (string row in data.Split(';'))
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (string cell in row.Split(','))
                {
                    string[] keyValue = cell.Split(':');
                    if (!columnsAdded)
                    {
                        DataColumn dataColumn = new DataColumn(keyValue[0]);
                        dataTable.Columns.Add(dataColumn);
                    }
                    dataRow[keyValue[0]] = keyValue[1];
                }
                columnsAdded = true;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }
        public static string convertDataTableToString(DataTable dataTable)
        {
            string data = string.Empty;
            int rowsCount = dataTable.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                DataRow row = dataTable.Rows[i];
                int columnsCount = dataTable.Columns.Count;
                for (int j = 0; j < columnsCount; j++)
                {
                    data += dataTable.Columns[j].ColumnName + ":" + row[j];
                    if (j == columnsCount - 1)
                    {
                        if (i != (rowsCount - 1))
                            data += ";";
                    }
                    else
                        data += ",";
                }
            }
            return data;
        }
        public string Urldomain(string id)
        {
            Domain_Info info = new Domain_Info(1);
            string urldomain = "'" + info.Name + "'";
            urldomain.Substring(2);
            return urldomain;
        }
        public string TimeLimit(string id)
        {
            TimeLimit_Cooperative_Info info = new TimeLimit_Cooperative_Info(HttpContext.Current.Session["CooperativeKey"].ToInt(), true);
            string TimeL = "'" + info.TimeLimit.ToInt() + "'";
            return TimeL;
        }
        public string GetDateTimeNow(string id)
        {
            string data ;
            data = "new Date('"+DateTime.Now.Year+"','"+(DateTime.Now.Month -1)+"','"+DateTime.Now.Day+"');";
            return data;
        }
    }
}