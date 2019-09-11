using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using TNLibrary.Book;
using System.Globalization;
using System.Data;
using System.Web.Script.Services;

namespace BookVietGAP
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region [ Report ]
        #region [ ReportSeed ]
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadReportSeed(string seedkey, string fromdate, string todate)
        {

            DateTime FromDate = DateTime.ParseExact(fromdate, "d/M/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(todate, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            TNLibrary.Book.Member_Info minfo = new TNLibrary.Book.Member_Info(memberID);
            string cities ="";
            if (seedkey.ToInt() == 0)
            {
                DataTable sTable = Report_Data.SeedProcessListReport(memberID, FromDate, ToDate);
                if (sTable != null)
                {
                    for (int j = 0; j < sTable.Rows.Count; j++)
                    {
                        int SeedKey = sTable.Rows[j]["SeedsKey"].ToInt();
                        Seed_Info sinfo = new Seed_Info(SeedKey);
                        cities += @"<div style=' text-align: center;padding-top: 100px;'>
                                <div style='font-size: 40px;font-weight: bold;'>SỔ NHẬT KÝ SẢN XUẤT</div></br>
                                <div style='font-size: 20px;'>Năm ...........</div>
                              </div>
                              <div style='page-break-before:always;padding-top: 200px;padding-left: 50px;'>
                                <strong style='font-size: 20px;'>NÔNG HỘ: " + HttpContext.Current.Session["MemberName"] + @"</strong></br>
                                <strong style='font-size: 20px;'>MÃ SỐ: " + minfo.MemID + @"</strong>
                             </div>
                              <div style=' text-align: center;padding-top: 200px;padding-bottom: 200px;'>
                                <div style='font-size: 40px;font-weight: bold;'>PHẦN SẢN XUẤT</div></br>
                                <h2>Cây trồng: " + sinfo.SeedsName + @"</h2></br>
                              <h2>Từ  " + fromdate + " đến " + todate + @"</h2>
                              </div>";
                        #region [ Quản lý giống ]
                        if (1 == 1)
                        {
                            cities += @"<h2 style='text-align: center;'>QUẢN LÝ GIỐNG</h2><table border='1' style='border: 2px solid #000;width: 100%;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th>Tên Giống</th>
                                <th>Nhà sản xuất/Phân phối <br>(Nguồn gốc giống)</th>
                                <th>Ngày trồng</th>
                                <th>Mã số lô</th>
                                <th>Diện tích</th>
                                <th>Tổng lượng giống</th>
                            </tr>
                        </thead>
                        <tbody>";
                            DataTable nTable = Report_Data.SeedProcessReport(memberID, SeedKey, FromDate, ToDate);
                            if (nTable != null)
                            {
                                for (int i = 0; i < nTable.Rows.Count; i++)
                                {
                                    cities += "<tr>" +
                                                    "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["CompanyName"].ToString() + "</td>" +
                                                    "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Parcel"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Area"].ToString() + " " + nTable.Rows[i]["UnitArea"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["UnitQuantity"].ToString() + "</td>" +
                                              "</tr>";
                                }
                            }
                            cities += "</tbody></table>";
                        }
                        #endregion
                        #region [ Quản lý ủ phân hữu cơ ]
                        if (1 == 1)
                        {
                            cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ Ủ PHÂN hữu CƠ</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th>Loại phân</th>
                                <th>Khối lượng</th>
                                <th>Phương pháp ủ</th>
                                <th>Thời gian ủ</th>
                                <th>Người thực hiện</th>
                            </tr>
                        </thead>
                        <tbody>";
                            DataTable nTable = Report_Data.CompostingOrganicReport(memberID, SeedKey, FromDate, ToDate);
                            if (nTable != null)
                            {
                                for (int i = 0; i < nTable.Rows.Count; i++)
                                {
                                    cities += "<tr>" +
                                                    "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Method"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["CompostingDates"].ToString() + " ngày" + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Member_Name"].ToString() + "</td>" +
                                              "</tr>";
                                }
                                cities += "</tbody></table></div>";
                            }
                        }
                        #endregion
                        #region [ Quản lý Mua phân/hóa chất ]
                        if (1 == 1)
                        {
                            cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ MUA PHÂN BÓN, HÓA CHẤT</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 0px;'>Ngày mua</th>
                                <th style='min-width: 150px;'>Tên thương mại</th>
                                <th style='min-width: 150px;'>Thành phần/ Hoạt chất</th>
                                <th style='min-width: 200px;'>Nhà sản xuất/ Phân phối</th>
                                <th style='min-width: 150px;'>Nhà cung cấp</th>
                                <th style='min-width: 90px;'>Số lượng</th>
                            </tr>
                        </thead>
                        <tbody>";
                            DataTable nTable = Report_Data.FertilizerPesticideBuyReport(memberID, SeedKey, FromDate, ToDate);
                            if (nTable != null)
                            {
                                for (int i = 0; i < nTable.Rows.Count; i++)
                                {
                                    cities += "<tr>" +
                                                    "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Common"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Company"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Address"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                              "</tr>";
                                }
                                cities += "</tbody></table></div>";
                            }
                        }
                        #endregion
                        #region [ Quản lý Sử dụng phân/hóa chất ]
                        if (1 == 1)
                        {
                            cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ SỬ DỤNG PHÂN BÓN, HÓA CHẤT</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 0px;'>Ngày</th>
                                <th style='min-width: 100px;'>Cộng việc</th>
                                <th style='min-width: 120px;'>Lý do áp dụng/ Mật độ</th>
                                <th style='min-width: 100px;'>Phương pháp</th>
                                <th style='min-width: 100px;'>Thiết bị sử dụng</th>
                                <th style='min-width: 100px;'>Nguyên vật liệu (tên phân bón/ thuốc BVTV/ hóa chất)</th>
                                <th style='min-width: 90px;'>Liều lượng (Nồng độ)</th>
                                <th style='min-width: 90px;'>Tổng lượng/lít nước</th>
                                <th style='min-width: 70px;'>TGCL</th>
                            </tr>
                        </thead>
                        <tbody>";
                            DataTable nTable = Report_Data.FertilizerPesticideUseReport(memberID, SeedKey, FromDate, ToDate);
                            if (nTable != null)
                            {
                                for (int i = 0; i < nTable.Rows.Count; i++)
                                {
                                    string TGCL = "";
                                    if (nTable.Rows[i]["TGCL"].ToString() == "")
                                    {
                                        TGCL = "<td></td>";
                                    }
                                    else
                                    {
                                        TGCL = "<td>" + nTable.Rows[i]["TGCL"].ToString() + " ngày" + "</td>";
                                    }
                                    cities += "<tr>" +
                                                    "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Parcel"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Area"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Solution"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["EquipmentName"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Dose"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Dosage"].ToString() + "</td>" +
                                                    TGCL +
                                              "</tr>";
                                }
                                cities += "</tbody></table></div>";
                            }
                        }
                        #endregion
                        #region [ Kiểm tra, bảo trì, vệ sinh thiết bị dụng cụ ]
                        if (1 == 1)
                        {
                            cities += @"<div class='page-break'><h2 style=' text-align: center;'>KIỂM TRA, BẢO TRÌ, VỆ SINH THIẾT BỊ DỤNG CỤ</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 50px;'>Ngày thục hiện</th>
                                <th style='min-width: 150px;'>Thiết bị dung cụ</th>
                                <th style='min-width: 150px;'>Hoạt động</th>
                                <th style='min-width: 200px;'>Mô tả phương pháp thực hiện</th>
                                <th style='min-width: 150px;'>Người thực hiện</th>
                            </tr>
                        </thead>
                        <tbody>";
                            DataTable nTable = Report_Data.CheckEquipmentReport(memberID, SeedKey, FromDate, ToDate);
                            if (nTable != null)
                            {
                                for (int i = 0; i < nTable.Rows.Count; i++)
                                {
                                    cities += "<tr>" +
                                                    "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Action"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Info"].ToString() + " ngày" + "</td>" +
                                                    "<td>" + nTable.Rows[i]["MemberName"].ToString() + "</td>" +
                                              "</tr>";
                                }
                                cities += "</tbody></table></div>";
                            }
                        }
                        #endregion
                        #region [ Quản lý thu hoạch xuất bán ]
                        if (1 == 1)
                        {
                            cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ THU HOẠCH, XUẤT BÁN</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 50px;'>Ngày thu hoạch/ Xuất bán</th>
                                <th style='min-width: 150px;'>Mã số truy vết</th>
                                <th style='min-width: 100px;'>Số lượng thu hoạch</th>
                                <th style='min-width: 200px;'>Người thu hoạch</th>
                                <th style='min-width: 150px;'>Số lượng bán (Kg)</th>
                                <th style='min-width: 150px;'>Nơi thu mua</th>
                            </tr>
                        </thead>
                        <tbody>";
                            DataTable nTable = Report_Data.HarvestedForSaleReport(memberID, SeedKey, FromDate, ToDate);
                            if (nTable != null)
                            {
                                for (int i = 0; i < nTable.Rows.Count; i++)
                                {
                                    cities += "<tr>" +
                                                    "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Code"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["QuantityHarvested"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["MemberName"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["QuantitySale"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["WhereToBuy"].ToString() + "</td>" +
                                              "</tr>";
                                }
                                cities += "</tbody></table></div>";
                            }
                        }
                        #endregion
                        #region [ Kiểm kê tồn kho ]
                        if (1 == 1)
                        {
                            cities += @"<h2 style=' text-align: center;'>KIỂM KÊ TỒN KHO</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 50px;'>Ngày kiểm kê</th>
                                <th style='min-width: 150px;'>phân bón/ Hóa chất</th>
                                <th style='min-width: 100px;'>Số lượng</th>
                                <th style='min-width: 200px;'>Hạn dùng</th>
                            </tr>
                        </thead>
                        <tbody>";
                            DataTable nTable = Report_Data.InventoryReport(memberID, SeedKey, FromDate, ToDate);
                            if (nTable != null)
                            {
                                for (int i = 0; i < nTable.Rows.Count; i++)
                                {
                                    cities += "<tr>" +
                                                    "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                                    "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                                    "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["ExpireDate"]) + "</td>" +
                                              "</tr>";
                                }
                                cities += "</tbody></table>";
                            }
                        }
                        #endregion
                    }
                }
            }
            else
            {
                Seed_Info sinfo = new Seed_Info(seedkey.ToInt());
                cities += @"<div style=' text-align: center;padding-top: 100px;'>
                                <div style='font-size: 40px;font-weight: bold;'>SỔ NHẬT KÝ SẢN XUẤT</div></br>
                                <div style='font-size: 20px;'>Năm ...........</div>
                              </div>
                              <div style='page-break-before:always;padding-top: 200px;padding-left: 50px;'>
                                <strong style='font-size: 20px;'>NÔNG HỘ: " + HttpContext.Current.Session["MemberName"] + @"</strong></br>
                                <strong style='font-size: 20px;'>MÃ SỐ: " + minfo.MemID + @"</strong>
                             </div>
                              <div style=' text-align: center;padding-top: 200px;padding-bottom: 200px;'>
                                <div style='font-size: 40px;font-weight: bold;'>PHẦN SẢN XUẤT</div></br>
                                <h2>Cây trồng: " + sinfo.SeedsName + @"</h2></br>
                              <h2>Từ  " + fromdate + " đến " + todate + @"</h2>
                              </div>";

                #region [ Quản lý giống ]
                if (1 == 1)
                {
                    cities += @"<h2 style='text-align: center;'>QUẢN LÝ GIỐNG</h2><table border='1' style='border: 2px solid #000;width: 100%;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th>Tên Giống</th>
                                <th>Nhà sản xuất/Phân phối <br>(Nguồn gốc giống)</th>
                                <th>Ngày trồng</th>
                                <th>Mã số lô</th>
                                <th>Diện tích</th>
                                <th>Tổng lượng giống</th>
                            </tr>
                        </thead>
                        <tbody>";
                    DataTable nTable = Report_Data.SeedProcessReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                    if (nTable != null)
                    {
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            cities += "<tr>" +
                                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["CompanyName"].ToString() + "</td>" +
                                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                            "<td>" + nTable.Rows[i]["Parcel"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Area"].ToString() + " " + nTable.Rows[i]["UnitArea"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["UnitQuantity"].ToString() + "</td>" +
                                      "</tr>";
                        }
                    }
                    cities += "</tbody></table>";
                }
                #endregion
                #region [ Quản lý ủ phân hữu cơ ]
                if (1 == 1)
                {
                    cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ Ủ PHÂN hữu CƠ</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th>Loại phân</th>
                                <th>Khối lượng</th>
                                <th>Phương pháp ủ</th>
                                <th>Thời gian ủ</th>
                                <th>Người thực hiện</th>
                            </tr>
                        </thead>
                        <tbody>";
                    DataTable nTable = Report_Data.CompostingOrganicReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                    if (nTable != null)
                    {
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            cities += "<tr>" +
                                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Method"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["CompostingDates"].ToString() + " ngày" + "</td>" +
                                            "<td>" + nTable.Rows[i]["Member_Name"].ToString() + "</td>" +
                                      "</tr>";
                        }
                        cities += "</tbody></table></div>";
                    }
                }
                #endregion
                #region [ Quản lý Mua phân/hóa chất ]
                if (1 == 1)
                {
                    cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ MUA PHÂN BÓN, HÓA CHẤT</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 0px;'>Ngày mua</th>
                                <th style='min-width: 150px;'>Tên thương mại</th>
                                <th style='min-width: 150px;'>Thành phần/ Hoạt chất</th>
                                <th style='min-width: 200px;'>Nhà sản xuất/ Phân phối</th>
                                <th style='min-width: 150px;'>Nhà cung cấp</th>
                                <th style='min-width: 90px;'>Số lượng</th>
                            </tr>
                        </thead>
                        <tbody>";
                    DataTable nTable = Report_Data.FertilizerPesticideBuyReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                    if (nTable != null)
                    {
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            cities += "<tr>" +
                                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Common"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Company"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Address"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                      "</tr>";
                        }
                        cities += "</tbody></table></div>";
                    }
                }
                #endregion
                #region [ Quản lý Sử dụng phân/hóa chất ]
                if (1 == 1)
                {
                    cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ SỬ DỤNG PHÂN BÓN, HÓA CHẤT</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 0px;'>Ngày</th>
                                <th style='min-width: 100px;'>Cộng việc</th>
                                <th style='min-width: 120px;'>Lý do áp dụng/ Mật độ</th>
                                <th style='min-width: 100px;'>Phương pháp</th>
                                <th style='min-width: 100px;'>Thiết bị sử dụng</th>
                                <th style='min-width: 100px;'>Nguyên vật liệu (tên phân bón/ thuốc BVTV/ hóa chất)</th>
                                <th style='min-width: 90px;'>Liều lượng (Nồng độ)</th>
                                <th style='min-width: 90px;'>Tổng lượng/lít nước</th>
                                <th style='min-width: 70px;'>TGCL</th>
                            </tr>
                        </thead>
                        <tbody>";
                    DataTable nTable = Report_Data.FertilizerPesticideUseReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                    if (nTable != null)
                    {
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            string TGCL = "";
                            if (nTable.Rows[i]["TGCL"].ToString() == "")
                            {
                                TGCL = "<td></td>";
                            }
                            else
                            {
                                TGCL = "<td>" + nTable.Rows[i]["TGCL"].ToString() + " ngày" + "</td>";
                            }
                            cities += "<tr>" +
                                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                            "<td>" + nTable.Rows[i]["Parcel"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Area"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Solution"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["EquipmentName"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Dose"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Dosage"].ToString() + "</td>" +
                                            TGCL +
                                      "</tr>";
                        }
                        cities += "</tbody></table></div>";
                    }
                }
                #endregion
                #region [ Kiểm tra, bảo trì, vệ sinh thiết bị dụng cụ ]
                if (1 == 1)
                {
                    cities += @"<div class='page-break'><h2 style=' text-align: center;'>KIỂM TRA, BẢO TRÌ, VỆ SINH THIẾT BỊ DỤNG CỤ</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 50px;'>Ngày thục hiện</th>
                                <th style='min-width: 150px;'>Thiết bị dung cụ</th>
                                <th style='min-width: 150px;'>Hoạt động</th>
                                <th style='min-width: 200px;'>Mô tả phương pháp thực hiện</th>
                                <th style='min-width: 150px;'>Người thực hiện</th>
                            </tr>
                        </thead>
                        <tbody>";
                    DataTable nTable = Report_Data.CheckEquipmentReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                    if (nTable != null)
                    {
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            cities += "<tr>" +
                                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Action"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Info"].ToString() + " ngày" + "</td>" +
                                            "<td>" + nTable.Rows[i]["MemberName"].ToString() + "</td>" +
                                      "</tr>";
                        }
                        cities += "</tbody></table></div>";
                    }
                }
                #endregion
                #region [ Quản lý thu hoạch xuất bán ]
                if (1 == 1)
                {
                    cities += @"<div class='page-break'><h2 style=' text-align: center;'>QUẢN LÝ THU HOẠCH, XUẤT BÁN</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 50px;'>Ngày thu hoạch/ Xuất bán</th>
                                <th style='min-width: 150px;'>Mã số truy vết</th>
                                <th style='min-width: 100px;'>Số lượng thu hoạch</th>
                                <th style='min-width: 200px;'>Người thu hoạch</th>
                                <th style='min-width: 150px;'>Số lượng bán (Kg)</th>
                                <th style='min-width: 150px;'>Nơi thu mua</th>
                            </tr>
                        </thead>
                        <tbody>";
                    DataTable nTable = Report_Data.HarvestedForSaleReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                    if (nTable != null)
                    {
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            cities += "<tr>" +
                                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                            "<td>" + nTable.Rows[i]["Code"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["QuantityHarvested"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["MemberName"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["QuantitySale"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["WhereToBuy"].ToString() + "</td>" +
                                      "</tr>";
                        }
                        cities += "</tbody></table></div>";
                    }
                }
                #endregion
                #region [ Kiểm kê tồn kho ]
                if (1 == 1)
                {
                    cities += @"<h2 style=' text-align: center;'>KIỂM KÊ TỒN KHO</h2><table border='1' style='border: 2px solid #000;' class='table table-bordered'>
                         <thead>
                            <tr>
                                <th style='min-width: 50px;'>Ngày kiểm kê</th>
                                <th style='min-width: 150px;'>phân bón/ Hóa chất</th>
                                <th style='min-width: 100px;'>Số lượng</th>
                                <th style='min-width: 200px;'>Hạn dùng</th>
                            </tr>
                        </thead>
                        <tbody>";
                    DataTable nTable = Report_Data.InventoryReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                    if (nTable != null)
                    {
                        for (int i = 0; i < nTable.Rows.Count; i++)
                        {
                            cities += "<tr>" +
                                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                                            "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["ExpireDate"]) + "</td>" +
                                      "</tr>";
                        }
                        cities += "</tbody></table>";
                    }
                }
                #endregion
            }
            HttpContext.Current.Session["ReportSeeds"] = cities;
            return cities;
        }
        #endregion
        #region [ ReportMoney ]
        [WebMethod]
        public static string LoadReportCost(string seedkey,string fromdate, string todate)
        {

            DateTime FromDate = DateTime.ParseExact(fromdate, "d/M/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(todate, "d/M/yyyy", CultureInfo.InvariantCulture);
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            TNLibrary.Book.Member_Info minfo = new TNLibrary.Book.Member_Info(memberID);
            string cities = "";
            if (seedkey.ToInt() == 0)
            {
                DataTable mTable = Report_Data.SumMoneyReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                cities = @"<div style=' text-align: center;padding-top: 100px;'>
                                <div style='font-size: 40px;font-weight: bold;'>BÁO CÁO CHI PHÍ TỔNG HỢP</div></br>
                                <div style='font-size: 20px;'>Từ  " + fromdate + " đến " + todate + @"</div>
                              </div>
                              <div style='padding-top: 200px;padding-left: 50px;padding-bottom: 100px;'>
                                <strong style='font-size: 20px;'>NÔNG HỘ: " + HttpContext.Current.Session["MemberName"] + @"</strong></br>
                                <strong style='font-size: 20px;'>MÃ SỐ: " + minfo.MemID + @"</strong>
                             </div>
                             <div style='padding-top: 30px; text-align: center;'>
                                <div style='font-size: 40px;font-weight: bold;'>Bảng tổng hợp thu chi</div>
                             </div>
                            <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tổng chi</th>
                                    <th>Tổng thu</th>
                                </tr>
                            </thead>
                            <tbody>
                            <tr>
                                <td style='text-align: center;'>" + String.Format("{0:#,0}", mTable.Compute("Sum(TotalBuy)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalBuy)", "")) + @"</td>
                                <td style='text-align: center;'>" + String.Format("{0:#,0}", mTable.Compute("Sum(TotalSale)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalSale)", "")) + @"</td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <label style='font-size: 15px;'>Ghi chú: Tổng chi = Tổng chi giống + Tổng chi phân bón + Tổng chi Thuốc(hóa chất) </label></br>
                        <label style='font-size: 15px;'>Tổng thu = Tổng thu hoạch xuất bán </label>
                    </div>
                    <div style='text-align: center;padding-top: 70px;'>
                        <strong style=' font-size: 30px;'>Thống kê theo thành phần </strong></br>
                    </div>";
                #region [ Giống ]
                cities += @"<strong style='font-size: 15px;'>1. Thống kê theo giống </strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên giống</th>
                                    <th>Tổng chi</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalSeedProcess = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Mua giống")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]) + @"</td></tr>";
                        TotalSeedProcess += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng chi giống : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalSeedProcess) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Phân bón ]
                cities += @"<strong style='font-size: 15px;'>2. Thống kê theo phần bón</strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên phân bón</th>
                                    <th>Tổng chi</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalFertilizers = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Mua phân bón")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]) + @"</td></tr>";
                        TotalFertilizers += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng chi phân bón : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalFertilizers) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Hóa chất ]
                cities += @"<strong style='font-size: 15px;'>3. Thống kê theo Thuốc(hóa chất)</strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên thuốc</th>
                                    <th>Tổng chi</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalPesticides = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Mua thuốc BVTV")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]) + @"</td></tr>";
                        TotalPesticides += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng chi Thuốc(hóa chất) : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalPesticides) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Thu hoạch ]
                cities += @"<strong style='font-size: 15px;'>4. Thống kê theo thu hoạch xuất bán</strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên giống</th>
                                    <th>Tổng thu</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalHarvestedForSale = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Thu hoạch xuất bán")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]) + @"</td></tr>";
                        TotalHarvestedForSale += String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng thu hoạch xuất bán : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalHarvestedForSale) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Theo Ngày ]
                cities += @"<div style='text-align: center;padding-top: 70px;'>
                                <strong style='font-size: 30px;'>Bảng tổng hợp thu chi theo ngày</strong>
                            </div>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Ngày</th>
                                    <th>Loại thu chi</th>
                                    <th>Tên Hàng</th>
                                    <th>Số lượng</th>
                                    <th>Tổng Chi</th>
                                    <th>Tổng Thu</th>
                                    <th>Cung Cấp</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalSale = 0, TotalBuy = 0;
                DataTable nTable = Report_Data.MoneyReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                for (int i = 0; i < nTable.Rows.Count; i++)
                {
                    cities += "<tr>" +
                            "<td style='text-align: center;vertical-align: inherit;'>" + (i + 1) + "</td>" +
                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                            "<td>" + nTable.Rows[i]["Type"].ToString() + "</td>" +
                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                            "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", nTable.Rows[i]["TotalBuy"]) + "</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", nTable.Rows[i]["TotalSale"]) + "</td>" +
                            "<td>" + nTable.Rows[i]["Address"].ToString() + "</td>" +
                      "</tr>";
                    TotalBuy += nTable.Rows[i]["TotalBuy"].ToDouble();
                    TotalSale += nTable.Rows[i]["TotalSale"].ToDouble();
                }
                cities += @"<tr>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td style='text-align: right;vertical-align: inherit;font-weight: bold;'>Tổng</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", TotalBuy) + "</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", TotalSale) + "</td>" +
                            "<td></td>" +
                          @"</tr>
                        </tbody>
                    </table>";
                #endregion
            }
            else
            {
                Seed_Info sinfo = new Seed_Info(seedkey.ToInt());
                DataTable mTable = Report_Data.SumMoneyReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                cities = @"<div style=' text-align: center;padding-top: 100px;'>
                                <div style='font-size: 40px;font-weight: bold;'>BÁO CÁO CHI PHÍ </div></br>
                                <div style='font-size: 20px;'>Từ  " + fromdate + " đến " + todate + @"</div>
                              </div>
                              <div style='padding-top: 20px; text-align: center;'>
                                <div style='font-size: 40px;font-weight: bold;'>" + sinfo.SeedsName + @"</div>
                             </div>
                              <div style='padding-top: 200px;padding-left: 50px;padding-bottom: 100px;'>
                                <strong style='font-size: 20px;'>NÔNG HỘ: " + HttpContext.Current.Session["MemberName"] + @"</strong></br>
                                <strong style='font-size: 20px;'>MÃ SỐ: " + minfo.MemID + @"</strong>
                             </div>
                             <div style='padding-top: 30px; text-align: center;'>
                                <div style='font-size: 40px;font-weight: bold;'>Bảng tổng hợp thu chi</div>
                             </div>
                            <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tổng chi</th>
                                    <th>Tổng thu</th>
                                </tr>
                            </thead>
                            <tbody>
                            <tr>
                                <td style='text-align: center;'>" + String.Format("{0:#,0}", mTable.Compute("Sum(TotalBuy)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalBuy)", "")) + @"</td>
                                <td style='text-align: center;'>" + String.Format("{0:#,0}", mTable.Compute("Sum(TotalSale)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalSale)", "")) + @"</td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                        <label style='font-size: 15px;'>Ghi chú: Tổng chi = Tổng chi giống + Tổng chi phân bón + Tổng chi Thuốc(hóa chất) </label></br>
                        <label style='font-size: 15px;'>Tổng thu = Tổng thu hoạch xuất bán </label>
                    </div>
                    <div style='text-align: center;padding-top: 70px;'>
                        <strong style=' font-size: 30px;'>Thống kê theo thành phần </strong></br>
                    </div>";
                #region [ Giống ]
                cities += @"<strong style='font-size: 15px;'>1. Thống kê theo giống </strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên giống</th>
                                    <th>Tổng chi</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalSeedProcess = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Mua giống")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]) + @"</td></tr>";
                        TotalSeedProcess += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng chi giống : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalSeedProcess) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Phân bón ]
                cities += @"<strong style='font-size: 15px;'>2. Thống kê theo phần bón</strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên phân bón</th>
                                    <th>Tổng chi</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalFertilizers = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Mua phân bón")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]) + @"</td></tr>";
                        TotalFertilizers += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng chi phân bón : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalFertilizers) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Hóa chất ]
                cities += @"<strong style='font-size: 15px;'>3. Thống kê theo Thuốc(hóa chất)</strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên thuốc</th>
                                    <th>Tổng chi</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalPesticides = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Mua thuốc BVTV")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]) + @"</td></tr>";
                        TotalPesticides += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng chi Thuốc(hóa chất) : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalPesticides) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Thu hoạch ]
                cities += @"<strong style='font-size: 15px;'>4. Thống kê theo thu hoạch xuất bán</strong></br>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>Tên giống</th>
                                    <th>Tổng thu</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalHarvestedForSale = 0;
                for (int i = 0; i < mTable.Rows.Count; i++)
                {
                    if (mTable.Rows[i]["Type"].ToString() == "Thu hoạch xuất bán")
                    {
                        cities += @"<tr><td style='text-align: left;'>" + mTable.Rows[i]["Name"].ToString() + @"</td>
                                <td style='text-align: right;'>" + String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]) + @"</td></tr>";
                        TotalHarvestedForSale += String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]).ToDouble();
                    }
                }
                cities += @"</tr><td style='text-align: right;  font-weight: bold;'>Tổng thu hoạch xuất bán : </td>
                            <td style='text-align: right;'>" + String.Format("{0:#,0}", TotalHarvestedForSale) + @"</td></tr>
                    </tbody>
                </table>";
                #endregion
                #region [ Theo Ngày ]
                cities += @"<div style='text-align: center;padding-top: 70px;'>
                                <strong style='font-size: 30px;'>Bảng tổng hợp thu chi theo ngày</strong>
                            </div>
                             <table style='border: 2px solid #000;' class='table table-bordered'>
                             <thead>
                                <tr>
                                    <th>STT</th>
                                    <th>Ngày</th>
                                    <th>Loại thu chi</th>
                                    <th>Tên Hàng</th>
                                    <th>Số lượng</th>
                                    <th>Tổng Chi</th>
                                    <th>Tổng Thu</th>
                                    <th>Cung Cấp</th>
                                </tr>
                            </thead>
                            <tbody>";
                double TotalSale = 0, TotalBuy = 0;
                DataTable nTable = Report_Data.MoneyReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                for (int i = 0; i < nTable.Rows.Count; i++)
                {
                    cities += "<tr>" +
                            "<td style='text-align: center;vertical-align: inherit;'>" + (i + 1) + "</td>" +
                            "<td>" + Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]) + "</td>" +
                            "<td>" + nTable.Rows[i]["Type"].ToString() + "</td>" +
                            "<td>" + nTable.Rows[i]["Name"].ToString() + "</td>" +
                            "<td>" + nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString() + "</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", nTable.Rows[i]["TotalBuy"]) + "</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", nTable.Rows[i]["TotalSale"]) + "</td>" +
                            "<td>" + nTable.Rows[i]["Address"].ToString() + "</td>" +
                      "</tr>";
                    TotalBuy += nTable.Rows[i]["TotalBuy"].ToDouble();
                    TotalSale += nTable.Rows[i]["TotalSale"].ToDouble();
                }
                cities += @"<tr>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td></td>" +
                            "<td style='text-align: right;vertical-align: inherit;font-weight: bold;'>Tổng</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", TotalBuy) + "</td>" +
                            "<td style='text-align: right;vertical-align: inherit;'>" + String.Format("{0:#,0}", TotalSale) + "</td>" +
                            "<td></td>" +
                          @"</tr>
                        </tbody>
                    </table>";
                #endregion
            }
            return cities;
        }
        #endregion
        #endregion
    }
}