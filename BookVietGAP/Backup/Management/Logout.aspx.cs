﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Management
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserLogin"] = null;
            Session["MemberID"] = null;
            Response.Cookies["Code"].Expires = DateTime.Now.AddDays(-1); 
            Response.Redirect("~/Login.aspx");
        }
    }
}