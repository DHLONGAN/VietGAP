﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Management
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CountOnline();
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
            LbTotal.Text = count_visit.ToString();
            LbCurrent.Text = OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.UsersCount.ToString();
        }


    }
}