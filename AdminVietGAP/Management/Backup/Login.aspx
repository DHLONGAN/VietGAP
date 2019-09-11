<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Management.Login" %>
<%--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="JS/jquery-1.7.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            margin: 0;
            background-image: url(/Img/login-bg.png);
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            var nWidth = $(document).width();
            var nHeight = $(document).height();
            $("#Login").css("margin-top", (nHeight - 344) / 2);



        });

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div id="Login" style="background-image: url(/Img/Login.png); width: 560px; height: 344px;
            text-align: left;">
            <div style="height: 185px">
                <div style="padding: 145px 0 0 192px">
                    <asp:TextBox ID="txtUserName" runat="server" Font-Size="16px" Width="175px"></asp:TextBox></div>
                <div style="padding: 18px 0 0 192px">
                    <asp:TextBox ID="txtPassword" runat="server" Font-Size="16px" Width="175px" TextMode="Password"></asp:TextBox></div>
            </div>
            <div style="padding-left: 385px">
                <asp:ImageButton ID="cmdLogin" runat="server" ImageUrl="~/Img/bt-login.png" 
                   onclick="cmdLogin_Click"  />
            </div>
            <div style="font-style:italic; color:Red; padding:40px 0 0 160px"><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></div>
        </div>
        
    </center>
    </form>
</body>
</html>
--%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Đăng nhập hệ thống VietGAP</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no">
    <script src="JS/jquery-2.1.0.min.js"></script>
    <link href="/CSS/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="/Lib/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
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
            $("#htx").html(DDList("ListCooperative"));

        });
        function DDList(load) {
            var result = "";
            var ddlist = "";
            var ddl = JSON.parse(getList(load));
            for (var i = 0; i < ddl.length; i++) {
                var DS = ddl[i];
                ddlist += "<option value='" + DS.Key + "'>" + DS.Name + "</option>"
            }
            result ="<option value='0'>Chọn</option>" +
                                        ddlist;
            return result;
        }
        function getList(name) {
            var result = "";
            $.ajax({
                type: "POST",
                url: "Ajax.aspx/" + name,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    result = response.d;
                }
            });
            return result;
        }
        function dk(strURL) {
            var form = document.forms['f2'];
            var username = form.txtusername.value;
            var password = form.txtpassword.value;
            var repassword = form.txtrepassword.value;
            var email = form.txtemail.value;
            var name = form.txtname.value;
            var idmember = form.txtidmember.value;
            var Cooperative = $("#htx").val();
            var phone = form.txtphone.value;
            var address = form.txtaddress.value;
            var values = JSON.stringify({
                username : username,
                "password": password,
                "repassword": repassword,
                "email": email,
                "name": name,
                "idmember":idmember,
                "Cooperative": Cooperative,
                "phone": phone,
                "address":address
            });
            if (username == '' || password == '' || repassword == '' || email == '' || name == '' || idmember == '' || Cooperative == '' || phone == '' || address == '') {
                alert("Vui lòng điền đầy đủ thông tin");
                return;
            }
            $.ajax({
                type: "POST",
                url: "Ajax.aspx/dk", // + name,

                data: values,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    result = response.d;
                    $("#lbError2").html(result);
                    //alert(result);
                }
            });
        }
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
                        <a href="#confirmMessNull" data-toggle="modal" class=""><font color="white">Chưa có tài khoản? Đăng ký ngay!</font>
                                </a>
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
        <div class="modal fade" id="confirmMessNull" role="dialog" aria-labelledby="confirmDeleteLabel" aria-hidden="true">
        <div class="modal-dialog modal-sl">
            <div class="modal-content">
                <div class="modal-header modal-header-warning">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"> &times;</button>
                    <h2 class="modal-title">Đăng ký tài khoản mới</h2>
                </div>
                <div class="modal-body">
                     <br />
                <form id="f2">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Tài khoản</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtusername" name="txtusername"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Mật khẩu</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtpassword" name="txtpassword"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Nhập lại mật khẩu</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtrepassword" name="txtrepassword"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Email</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtemail" name="txtemail"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Tên xã viên</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtname" name="txtname"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Mã xã viên</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtidmember" name="txtidmember"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Số điện thoại</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtphone" name="txtphone"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Hợp tác xã</label>
                        <div class="col-sm-8">
                           <select id='htx'>
                                    
                          </select>
                                    <span id='spid'></span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-4 control-label">Địa chỉ</label>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" id="txtaddress" name="txtaddress"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" id="lbError2" class="col-sm-12 control-label text-center" style="color: Red;"></label>
                    </div>
                </form>
                </div>
                <div class="modal-footer">
                     <a class="btn btn-success center-block" href="javascript:dk('/Ajax.aspx/dk/')">Đồng ý</a>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
