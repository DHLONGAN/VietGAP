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

namespace Management
{
    public partial class SiteMains : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenu();
                CountOnline();
            }
        }
        private void LoadMenu()
        {
            string nURL = Request.Url.AbsolutePath;
            string[] _MenuModule = nURL.Split('/');
            DataTable _Table = Web_Menu_SAL.List(0);
            StringBuilder nMenuTop = new StringBuilder();
            nMenuTop.AppendLine("<li class='space'></li>");
            foreach (DataRow nRow in _Table.Rows)
            {
                if (_MenuModule[1].ToUpper() == nRow["MenuLink"].ToString().Substring(1).ToUpper())
                {
                    nMenuTop.AppendLine("<li class='active'><a href='" + nRow["MenuLink"] + "'>"+ nRow["MenuName"]+"</a></li>");
                }
                else
                    nMenuTop.AppendLine("<li><a href='" + nRow["MenuLink"] + "'>" + nRow["MenuName"] + "</a></li>");
            }
            LiteMenuTop.Text = nMenuTop.ToString();
        }
        private void CountOnline()
        {
            int count_visit = 0;
            //Kiểm tra file count_visit.txt nếu không tồn  tại thì
            if (System.IO.File.Exists(Server.MapPath("~/count_visit.txt")) == false)
            {
                count_visit = 1;
            }
            // Ngược lại thì
            else
            {
                // Đọc dử liều từ file count_visit.txt
                System.IO.StreamReader read = new System.IO.StreamReader(Server.MapPath("~/count_visit.txt"));
                count_visit = int.Parse(read.ReadLine());
                read.Close();
                // Tăng biến count_visit thêm 1
                count_visit++;
            }
            // khóa website
            Application.Lock();
            // gán biến Application count_visit
            Application["count_visit"] = count_visit;
            // Mở khóa website
            Application.UnLock();
            // Lưu dử liệu vào file  count_visit.txt
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("~/count_visit.txt"));
            writer.WriteLine(count_visit);
            writer.Close();
            if (Session["online"] == null)
            {
                Session["online"] = 1;
            }
            else
            {
                Session["online"] = int.Parse(Session["online"].ToString()) + 1;
            }
            //LbTotal.Text = count_visit.ToString();
            //LbCurrent.Text = OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.UsersCount.ToString();
        }
    }
}