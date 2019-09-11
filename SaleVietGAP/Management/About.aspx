<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMains.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Management.About" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="f-footer" >
    <div class="container">
        <div class="row" style="min-height:400px">
            <div class="col-md-5 r-footer-about ">
                <span class="line">Về chúng tôi</span>
                <p>
                    Đơn vị chủ quản: 
                    <br>
                    Trụ sở chính: 
                    <br>
                    Chi nhánh Hồ Chí Minh: 
                    <br>
                    Website : <a href="http://vietgap.info">www.vietgap.info</a> - Email : <a href="mailto:info@vietgap.info">info@vietgap.info</a>
                    <br>
                </p>
            </div>
            <div class="col-md-5 r-footer-about ">
                <span class="line">Thống kê</span>
                    <p>
                    Đang truy cập: <asp:Label ID="LbCurrent" runat="server" Text="Label" style="font-size: 14px;display: inline;border-bottom: initial;color: white;"></asp:Label>
                    <br>
                    Tổng lượt truy cập: <asp:Label ID="LbTotal" runat="server" Text="Label" style="font-size: 14px;display: inline;border-bottom: initial;color: white;"></asp:Label> 
                </p>
            </div>
            <div class="col-md-3 r-footer-facebook">
                <style type="text/css">
                    .pluginBoxDividerGray
                    {
                        border-top-color: none !important;
                    }
                    .pluginBoxDivider
                    {
                        border-top: none !important;
                    }
                </style>
                <%--<span class="line">Facebook</span>--%>
            </div>
            <div class="clearfix">
            </div>
        </div>
        <div class="r-footer-bottom">
                <div class="row">
                    <div class="col-md-9">
                        <ul class="r-b-nav">
                            <li><a href="gioi-thieu.html" title="Nhà phát triển">Nhà phát triển</a></li>
                            <li><a href="dieu-khoan.html" title="Điều khoản">Điều khoản</a></li>
                            <li><a href="chinh-sach-bao-mat.html" title="Chính sách bảo mật">Bảo mật</a></li>
                            <li><a href="tro-giup.html" title="Video hướng dẫn sử dụng">Hướng dẫn sử dụng</a></li>
                            <li><a href="tin-tuc.html" title="Thông tin sự kiện">Thông tin sự kiện</a></li>
                            <li><a href="huong-dan-thanh-toan.html" title="Hướng dẫn thanh toán">Hướng dẫn thanh toán</a></li>
                            <li><a href="nhan-vien-kinh-doanh.html" title="Nhân viên kinh doanh">Nhân viên kinh doanh</a></li>
                        </ul>
                        <div class="clearfix">
                        </div>
                        <div class="r-copyright">
                            Copyright © 2014-2015. All rights reserved.</div>
                    </div>
                    <div class="col-md-3">
                    <!--LOGIN-->
                        <ul class="r-b-social">
                            <li><a href="#" title="facebook" class="fb"></a></li>
                            <li><a href="#" title="tweeter" class="tw"></a></li>
                            <li><a href="#" title="google+" class="gg"></a></li>
                            <li><a href="#" title="youtube" class="yb"></a></li>
                        </ul>
                    </div>
                </div>
            </div>
    </div>
    </div>
    

</asp:Content>
