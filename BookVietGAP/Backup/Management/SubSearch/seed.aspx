<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBook.Master" AutoEventWireup="true"
    CodeBehind="seed.aspx.cs" Inherits="BookVietGAP.SubSearch.seed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Lib/bootstrap/css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />
    <script src="/Lib/bootstrap/js/bootstrap-select.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    var ddlAll;
    var name = "";
    var idsearch = <% = getsearchid("") %>;
        $(document).ready(function () {
            if(idsearch=='giong'){
                $('#ra_seed').trigger("click");
            }
            if(idsearch=='phanbon'){
                $('#ra_Fertilizers').trigger("click");
            }
            if(idsearch=='saubenh'){
                $('#ra_Pesticides').trigger("click");
            }
            $("#btn_search").click(function () {
                checkclick();
            });
            $('#txt_search').bind("keyup keypress", function (e) {
            var code = e.keyCode || e.which;
            if (code == 13) {
                checkclick();
                return false;
            }
        });
        });
        function checkclick(){
            if ($('#ra_seed').is(':checked')) {
                loadSearch('LoadSeedProces');
                name = 'Seed';
            }
            if ($('#ra_Fertilizers').is(':checked')) {
                loadSearch('LoadFertilizersProces');
                name = 'Fertilizers';
            }
            if ($('#ra_Pesticides').is(':checked')) {
                loadSearch('LoadPesticidesProces');
                name = 'Pesticides';
            }
        }
        function LoadDetailSearch(stt){
            var nheader = "";
            var contact = "";
            var DS = ddlAll[stt];
            var zName,zTyper,zNxs,zVu,zTinhTrang;
            if(name == 'Seed'){
                nheader = "THÔNG TIN GIỐNG CÂY TRỒNG";
                zName = 'Tên giống';
                zTyper = 'Cây trồng'
                zNxs = 'Cơ quan tác giả';
                zVu = 'Vụ/Vùng công nhận';
                zTinhTrang = 'Mức độ công nhận';
            }
            if(name == 'Fertilizers'){
                nheader = "THÔNG TIN PHÂN BÓN";
                zName = 'Tên thương phẩm';
                zTyper = 'Loại phân'
                zNxs = 'Tổ chức xin đăng ký';
                zVu = 'Đơn vị';
                zTinhTrang = 'Thành phần';
            }
            if(name == 'Pesticides'){
                nheader = "THÔNG TIN THUỐC BẢO VỆ THỰC VẬT";
                zName = 'Tên thương phẩm';
                zTyper = 'Đối tượng phòng trừ'
                zNxs = 'Tổ chức xin đăng ký';
                zVu = 'Tình trạng';
                zTinhTrang = 'Thành phần';
            }
            contact =   "<div class='col-md-3'>"+
                            "<div class='thumbnail'>"+
                                "<img style='height: 200px;' src='" + urldomain + DS.Images + "'>"+
                            "</div>"+
                        "</div>"+
                        "<div class='col-md-9'>"+
                            "<div class='col-md-4'><h4>"+zName+" :</h4></div>"+
                            "<div class='col-md-8'><h4> " + DS.Name + "</h4></div>"+
                        "</div>"+
                        "<div class='col-md-9'>"+
                            "<div class='col-md-4'><h4>"+zTyper+" :</h4></div>"+
                            "<div class='col-md-8'><h5> " + DS.CategoryName + "</h5></div>"+
                        "</div>"+
                        "<div class='col-md-9'>"+
                            "<div class='col-md-4'><h4>"+zNxs+" : </h4></div>"+
                            "<div class='col-md-8'><h5> " + DS.CompanyName + "</h5></div>"+
                        "</div>"+
                        "<div class='col-md-9'>"+
                            "<div class='col-md-4'><h4>"+zVu+" :</h4></div>"+
                            "<div class='col-md-8'><h5> " + DS.SeasonName + "</h5></div>"+
                        "</div>"+
                        "<div class='col-md-9'>"+
                            "<div class='col-md-4'><h4>"+zTinhTrang+" :</h4></div>"+
                            "<div class='col-md-8'><h5> " + DS.StatusName + "</h5></div>"+
                        "</div>";
                        
            $('#nheader').html(nheader);
            $('#DivcontactDetai').html(contact);
            $('#pro').modal('show');
        }
        function loadSearch(name) {
            var ddl = JSON.parse(getDataSearch(name, JSON.stringify({ Search: $("#txt_search").val() })));
            ddlAll = ddl;
            loadListHtml(ddl,1);
        }
        function loadListHtml(ddl,num) {
            var numcount = getDataSearch("LoadCount", JSON.stringify({ Search : "" }));
            var count = "<div class='col-md-12' style ='padding-top: 0px;'><h4>" + numcount + " kết quả được tìm thấy</h4></div>"
            document.getElementById('Divcount').innerHTML = count;
            $('#Divcontact').html('');
            var contact = "";
            for (var i = 0; i < ddl.length; i++) {
                var DS = ddl[i];
                contact += "<div class='col-md-12 alert alert-default' style ='margin-top: 0px;margin-bottom: 0px;padding-top: 0px;'>" +
                                "<div class='col-xs-2 col-sm-1 col-md-1 col-lg-1' style='margin-top: 10px;padding: 0px;'>" +
                                        "<a href='#' onclick='LoadDetailSearch("+i+")' data-toggle='modal' class=''>" +
                                            "<img style='width: 60px;height: 80px;  border-radius: 10px;' src='" + urldomain + DS.Images + "' alt='" + DS.Name + "'>" +
                                        "</a>" +
                                    "</div>" +
                                "<div class='col-xs-10 col-sm-11 col-md-11 col-lg-11' style='padding-top: 10px;'>" +
                                  "<a href='#' onclick='LoadDetailSearch("+i+")'><h4>" + DS.Name + "</h4></a>" +
                                  "<p>Loại : " + DS.CategoryName + "</p>" +
                                  "<p>Nhà sản xuất : " + DS.CompanyName + "</p>" +
                                "</div></div><hr style='width: 100%; color: black; height: 1px; background-color:#eee;  margin: 0;'/>";
            }
            
            $('#Divcontact').html(contact);

            var pagination = "";
            var numpage = Math.ceil((numcount/10)+1);
            if(num==""){num=1;}
            if(num-5>0){
                if(num+5 < numpage){
                    for (var i = num-5; i < numpage & i <num + 5; i++) {
                        if(i==num){
                            pagination += "<li class='active'><a href='#' onclick='LoadPageSearch("+i+")'>"+i+"</a></li>";
                        }
                        else{
                            pagination += "<li><a href='#' onclick='LoadPageSearch("+i+")'>"+i+"</a></li>";
                        }
                    }
                }
                else{
                    for (var i = numpage-10; i < numpage; i++) {
                        if(i==num){
                            pagination += "<li class='active'><a href='#' onclick='LoadPageSearch("+i+")'>"+i+"</a></li>";
                        }
                        else{
                            pagination += "<li><a href='#' onclick='LoadPageSearch("+i+")'>"+i+"</a></li>";
                        }
                    }
                }
            }
            if(num<6){
            for (var i = 1; i < numpage & i < 11; i++) {
                    if(i==num){
                        pagination += "<li class='active'><a href='#' onclick='LoadPageSearch("+i+")'>"+i+"</a></li>";
                    }
                    else{
                        pagination += "<li><a href='#' onclick='LoadPageSearch("+i+")'>"+i+"</a></li>";
                    }
                }
            }
            document.getElementById('Pagi').innerHTML ="<ul class='pagination'><li><a href='#'onclick='LoadPageSearch(1)'>&laquo;</a></li>"+ pagination +"<li><a href='#' onclick='LoadPageSearch("+(numpage-1)+")'>&raquo;</a></li></ul>";
        }
        function LoadPageSearch(num){
        
            if ($('#ra_seed').is(':checked')) {
                name = 'Seed';
            }
            if ($('#ra_Fertilizers').is(':checked')) {
                name = 'Fertilizers';
            }
            if ($('#ra_Pesticides').is(':checked')) {
                name = 'Pesticides';
            }
            var ddl = JSON.parse(getDataSearch('PageSearch', JSON.stringify({ Num: num ,Name : name})));
            ddlAll = ddl;
            loadListHtml(ddl,num);
        }
        function getDataSearch(obj, data) {
            var result = "";
            $.ajax(
                {
                    type: "POST",
                    url: "seed.aspx/" + obj,
                    data: data,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        result = response.d;
                    }
                });
            return result;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-12 well" style=" margin-bottom: 5px;padding: 10px;">
        <div class="col-lg-6 col-lg-offset-1">
            <div id="custom-search-input">
                <div class="input-group col-md-12">
                    <input id="txt_search" type="text" class="form-control input-lg" placeholder="Tìm kiếm" />
                    <span class="input-group-btn">
                        <button id="btn_search" class="btn btn-info btn-lg" type="button">
                            <i class="glyphicon glyphicon-search"></i>
                        </button>
                    </span>
                </div>
            </div>
        </div>
        <div class="" style="padding-top: 15px;">
            <input type="radio" name="optradio" id='ra_seed'>Giống</label>
            <input type="radio" name="optradio" id ='ra_Fertilizers'>Phân bón</label>
            <input type="radio" name="optradio" id ="ra_Pesticides">Thuốc & Sâu bệnh</label>
        </div>
    </div>
    <div class="container">
        <div id="Divcount"></div>
        <div id="Divcontact"></div>
        <div id="Pagi">
        </div>
    </div>
    <div class="modal fade" id="confirmSearch" role="dialog" aria-labelledby="" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header modal-header-info">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h2 class="modal-title">
                        Chi tiết</h2>
                </div>
                <div class="modal-body">
                    <p>
                        Bạn có chắc muốn thoát không ?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Đóng</button>
                </div>
            </div>
        </div>
    </div>
    <div id="pro" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="gridModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header modal-header-info">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                    <h1 class="modal-title" style="text-align: center;"><div id="nheader"></div><a class="anchorjs-link" href="#pro16"><span class="anchorjs-icon"></span></a></h1>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div id="DivcontactDetai">
                            
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal"> Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
