<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Management.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập hệ thống VietGAP</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>
    <link href="/CSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="/Lib/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Lib/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            margin: 0;
            background-image: url(/Img/login-bg.png);
        }
        input[type=checkbox]
        {
            display: none;
        }
        .checkbox
        {
            width: 46px;
            height: 21px;
            background: transparent url("/Img/on-off.png") no-repeat 0 50%;
        }
        .checked
        {
            background: transparent url("/Img/on-off.png") no-repeat 100% 50%;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            var nWidth = $(document).width();
            var nHeight = $(document).height();
            $("#Login").css("margin-top", (nHeight - $("#loginbox").height()) / 2);
            $(".checkbox").click(function () {
                $(this).toggleClass('checked')
            });
            $("#loginbox").css("margin-top", (($(document).height() - $("#loginbox").height()) / 2));
            
        });
    </script>
</head>
<body>
<%--    <form id="form1" runat="server">
    <center>
        <div id="Login" class="BgLogin">
            <div class="frm_dangnhap">
                    <asp:TextBox ID="txtUserName" class="txt_box" runat="server" placeholder="Tên đăng nhập"></asp:TextBox>
                    <asp:TextBox ID="txtPassword" class="txt_box" runat="server" TextMode="Password" placeholder="Mật khẩu"></asp:TextBox>
            </div>
            <div style="padding-left: 221px; margin-top: -37px;">
                <asp:ImageButton ID="cmdLogin" runat="server" ImageUrl="~/Img/bt-login.png" onclick="cmdLogin_Click"  />
            </div>
            <div style="font-style:italic; color:Red; padding:40px 0 0 160px"><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></div>
        </div>
        
    </center>
    </form>--%>
    <form id="loginform" class="form-horizontal" role="form" runat="server">
    <div class="container">
        <div id="loginbox" class="mainbox col-md-4 col-md-offset-4 col-sm-offset-2">
            <div class="panel panel-info" style="border-color: #C4A37C; border-radius: 15px;background-image: url('/Img/login-bgf.png');">
                <div class="panel-heading" style="border-top-right-radius: 15px; border-top-left-radius: 15px;
                    color: #654f36; background-image: url('/Img/login-head.png'); padding-top: 20px;padding-left: 30px;
                    padding-bottom: 5px; border-color: #BE9C74; border-bottom: 0px solid transparent;">
                    <div class="panel-title">
                        <i class="fa fa-key fa-rotate-270 fa-2x"></i><strong style="font-family: tahoma;
                            font-size: 20px;">Đăng nhập</strong><br>
                    </div>
                </div>
                <div style="padding-top: 10px; padding-bottom: 45px;background-image: url('/Img/br2.png'); border-bottom-right-radius: 15px;
                    border-bottom-left-radius: 15px;  -webkit-box-shadow: 0px 7px 3px 0px #776249;  box-shadow: 0px 7px 3px 0px #776249;-moz-box-shadow: 0px 7px 3px 0px #776249;" class="panel-body">
                    <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                    </div>
                    <div class="col-sm-12" style="margin-bottom: 10px;">
                        <asp:TextBox ID="txtUserName" class="form-control" style="height: 40px;" runat="server" placeholder="Tên đăng nhập"></asp:TextBox>
                    </div>
                    <div class="col-sm-12">
                        <asp:TextBox ID="txtPassword" class="form-control" style="height: 40px;" runat="server" TextMode="Password"
                            placeholder="Mật khẩu"></asp:TextBox>
                    </div>
                    <div class="col-sm-12">
                        <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="White"></asp:Label>
                    </div>
                </div>
                <div class="panel-heading" style="color: #654f36; background-color: transparent; padding-top: 20px;
                    padding-bottom: 0px; border-color: #BE9C74; border-bottom: 0px solid transparent;">
                    <div class="">
                        <div class="col-xs-3" style="padding-top: 20px;  margin-top: -10px;">
                            <label class = "" style="padding: 0px;">
                                <asp:CheckBox ID="cbRemember" runat="server" /><div class="checkbox"></div>
                            </label>
                            <div class="col-xs-12" style="padding: 0px;  margin-top: -28px;  margin-left: 50px;color: #efdcc1;">
                            
                            </div>
                        </div>
                        <div style="padding-top: 15px;float: right;margin-top: -10px;" class="">
                            <div class="controls">
                                <asp:ImageButton ID="cmdLogin" runat="server" ImageUrl="~/Img/bt-login.png" OnClick="cmdLogin_Click" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <div class="col-xs-12 control" style="color: #BE9C74;  margin-left: 15px;">
                        <strong>Tự động đăng nhập</strong>
                        </div>
                </div>
            </div>
            
        </div>
        <div style="text-align: center;  padding-bottom: 40px;" hidden="true"><a href="#" style="color: #d1d1d1;font-family: tahoma;  font-size: 18px;  font-weight: bold;" >Quên mật khẩu</a><br></div>
    </div>
    </form>
</body>
</html>
