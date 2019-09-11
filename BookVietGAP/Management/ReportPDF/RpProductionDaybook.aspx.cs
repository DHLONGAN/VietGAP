using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using TNLibrary.Book;
using System.Globalization;
using System.IO;

namespace BookVietGAP.ReportPDF
{
    public partial class RpProductionDaybook : System.Web.UI.Page
    {
        int seedkey = 0;
        DateTime FromDate, ToDate = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["seedkey"] != null)
            {
                seedkey = this.Request["seedkey"].ToInt();
            }
            if (this.Request["from"] != null)
            {
                FromDate = DateTime.ParseExact(this.Request["from"], "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            if (this.Request["to"] != null)
            {
                ToDate = DateTime.ParseExact(this.Request["to"], "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            if (memberID != 0)
            {
                string NameSeed = "";
                TNLibrary.Book.Member_Info minfo = new TNLibrary.Book.Member_Info(memberID);
                
                MemoryStream ms = new MemoryStream();
                Document document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 66, 36, 54, 54);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                BaseFont bfTimes = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, false);

                Font fontHeader = new Font(bfTimes, 12, Font.BOLD);
                Font fontNormal = new Font(bfTimes, 11);
                Font fontozel = new Font(bfTimes, 12, Font.NORMAL);
                writer.PageEvent = new PDFFooter();
                
                
                document.Open();
                if (seedkey.ToInt() == 0)
                {
                    NameSeed = "_Tatca";
                    DataTable sTable = Report_Data.SeedProcessListReport(memberID, FromDate, ToDate);
                    if (sTable != null)
                    {
                        for (int j = 0; j < sTable.Rows.Count; j++)
                        {
                            seedkey = sTable.Rows[j]["SeedsKey"].ToInt();
                            Seed_Info sinfo = new Seed_Info(seedkey.ToInt());
                            Paragraph header = new Paragraph(@"SỔ NHẬT KÝ SẢN XUẤT", new Font(bfTimes, 40, Font.BOLD));
                            header.Alignment = Element.ALIGN_CENTER;
                            header.SpacingAfter = 20;
                            document.Add(header);
                            Paragraph header1 = new Paragraph(@"Năm............", new Font(bfTimes, 20, Font.NORMAL));
                            header1.Alignment = Element.ALIGN_CENTER;
                            header1.SpacingAfter = 170;
                            document.Add(header1);
                            document.Add(new Paragraph(@"NÔNG HỘ: " + minfo.Name, new Font(bfTimes, 20, Font.BOLD)));
                            document.Add(new Paragraph(@"MÃ SỐ: " + minfo.MemID, new Font(bfTimes, 20, Font.BOLD)));
                            document.NewPage();
                            Paragraph Text1 = new Paragraph(@"PHẦN SẢN XUẤT", new Font(bfTimes, 30, Font.BOLD));
                            Text1.Alignment = Element.ALIGN_CENTER;
                            document.Add(Text1);
                            Paragraph Text2 = new Paragraph(@"Cây trồng: " + sinfo.SeedsName, new Font(bfTimes, 25, Font.NORMAL));
                            Text2.Alignment = Element.ALIGN_CENTER;
                            document.Add(Text2);
                            Paragraph Text3 = new Paragraph(@"Từ " + this.Request["from"] + " đến " + this.Request["to"], new Font(bfTimes, 25, Font.NORMAL));
                            Text3.Alignment = Element.ALIGN_CENTER;
                            document.Add(Text3);
                            document.NewPage();
                            #region [ Quản lý giống ]
                            if (1 == 1)
                            {

                                Paragraph para = new Paragraph(@"QUẢN LÝ GIỐNG", new Font(bfTimes, 30, Font.BOLD));
                                para.Alignment = Element.ALIGN_CENTER;
                                para.SpacingAfter = 20;
                                document.Add(para);

                                PdfPTable mainTable = new PdfPTable(6);
                                mainTable.WidthPercentage = 100;

                                mainTable.SetWidths(new int[] { 15, 20, 15, 15, 15, 20 });
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên Giống", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nhà sản xuất/Phân phối (Nguồn gốc giống)", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày trồng", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Mã số lô", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Diện tích", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng lượng giống", fontHeader), 1));
                                DataTable nTable = Report_Data.SeedProcessReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                                if (nTable != null)
                                {
                                    for (int i = 0; i < nTable.Rows.Count; i++)
                                    {
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["CompanyName"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Parcel"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Area"].ToString() + " " + nTable.Rows[i]["UnitArea"].ToString(), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["UnitQuantity"].ToString(), fontozel), 1));
                                    }
                                }

                                document.Add(mainTable);
                            }
                            #endregion
                            #region [ Quản lý ủ phân hữu cơ ]
                            if (1 == 1)
                            {

                                Paragraph para = new Paragraph(@"QUẢN LÝ Ủ PHÂN HỮU CƠ", new Font(bfTimes, 30, Font.BOLD));
                                para.Alignment = Element.ALIGN_CENTER;
                                para.SpacingAfter = 20;
                                document.Add(para);

                                PdfPTable mainTable = new PdfPTable(5);
                                mainTable.WidthPercentage = 100;

                                mainTable.SetWidths(new int[] { 15, 10, 45, 15, 15 });
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Loại phân", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Khối lượng", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Phương pháp ủ", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thời gian ủ", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Người thực hiện", fontHeader), 1));
                                DataTable nTable = Report_Data.CompostingOrganicReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                                if (nTable != null)
                                {
                                    for (int i = 0; i < nTable.Rows.Count; i++)
                                    {
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Method"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["CompostingDates"].ToString() + " ngày", fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Member_Name"].ToString(), fontozel), 0));

                                    }
                                }

                                document.Add(mainTable);
                                document.NewPage();
                            }
                            #endregion
                            #region [ Quản lý Mua phân/hóa chất ]
                            if (1 == 1)
                            {

                                Paragraph para = new Paragraph(@"QUẢN LÝ MUA PHÂN BÓN, HÓA CHẤT", new Font(bfTimes, 30, Font.BOLD));
                                para.Alignment = Element.ALIGN_CENTER;
                                para.SpacingAfter = 20;
                                document.Add(para);

                                PdfPTable mainTable = new PdfPTable(6);
                                mainTable.WidthPercentage = 100;

                                mainTable.SetWidths(new int[] { 10, 15, 25, 25, 15, 10 });
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày mua", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên thương mại", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thành phần/ Hoạt chất", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nhà sản xuất/ Phân phối", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nhà cung cấp", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng", fontHeader), 1));
                                DataTable nTable = Report_Data.FertilizerPesticideBuyReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                                if (nTable != null)
                                {
                                    for (int i = 0; i < nTable.Rows.Count; i++)
                                    {
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Common"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Company"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Address"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                    }
                                }
                                document.Add(mainTable);
                                document.NewPage();
                            }
                            #endregion
                            #region [ Quản lý Sử dụng phân/hóa chất ]
                            if (1 == 1)
                            {

                                Paragraph para = new Paragraph(@"QUẢN LÝ SỬ DỤNG PHÂN BÓN, HÓA CHẤT", new Font(bfTimes, 30, Font.BOLD));
                                para.Alignment = Element.ALIGN_CENTER;
                                para.SpacingAfter = 20;
                                document.Add(para);

                                PdfPTable mainTable = new PdfPTable(9);
                                mainTable.WidthPercentage = 100;

                                mainTable.SetWidths(new int[] { 9, 11, 14, 15, 10, 15, 10, 10, 6 });
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Cộng việc", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Lý do áp dụng/ Mật độ", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Phương pháp", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thiết bị sử dụng", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nguyên vật liệu (tên phân bón/ thuốc BVTV/ hóa chất)", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Liều lượng (Nồng độ)", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng lượng/ lít nước", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("TGCL", fontHeader), 1));
                                DataTable nTable = Report_Data.FertilizerPesticideUseReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                                if (nTable != null)
                                {
                                    for (int i = 0; i < nTable.Rows.Count; i++)
                                    {
                                        string TGCL = "";
                                        if (nTable.Rows[i]["TGCL"].ToString() == "")
                                        {
                                            TGCL = "";
                                        }
                                        else
                                        {
                                            TGCL = nTable.Rows[i]["TGCL"].ToString() + " ngày";
                                        }
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Parcel"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Area"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Solution"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["EquipmentName"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Dose"].ToString(), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Dosage"].ToString(), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(TGCL, fontozel), 0));
                                    }
                                }
                                document.Add(mainTable);
                                document.NewPage();
                            }
                            #endregion
                            #region [ KIỂM TRA, BẢO TRÌ, VỆ SINH THIẾT BỊ DỤNG CỤ ]
                            if (1 == 1)
                            {

                                Paragraph para = new Paragraph(@"KIỂM TRA, BẢO TRÌ, VỆ SINH THIẾT BỊ DỤNG CỤ", new Font(bfTimes, 30, Font.BOLD));
                                para.Alignment = Element.ALIGN_CENTER;
                                para.SpacingAfter = 20;
                                document.Add(para);

                                PdfPTable mainTable = new PdfPTable(5);
                                mainTable.WidthPercentage = 100;

                                mainTable.SetWidths(new int[] { 15, 15, 15, 40, 15 });
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày thực hiện", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thiết bị, dụng cụ", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Hoạt động", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Mô tả phương pháp thực hiện", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Người thực hiện", fontHeader), 1));
                                DataTable nTable = Report_Data.CheckEquipmentReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                                if (nTable != null)
                                {
                                    for (int i = 0; i < nTable.Rows.Count; i++)
                                    {
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Action"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Info"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["MemberName"].ToString(), fontozel), 0));
                                    }
                                }
                                document.Add(mainTable);
                                document.NewPage();
                            }
                            #endregion
                            #region [ QUẢN LÝ THU HOẠCH, XUẤT BÁN ]
                            if (1 == 1)
                            {

                                Paragraph para = new Paragraph(@"QUẢN LÝ THU HOẠCH, XUẤT BÁN", new Font(bfTimes, 30, Font.BOLD));
                                para.Alignment = Element.ALIGN_CENTER;
                                para.SpacingAfter = 20;
                                document.Add(para);

                                PdfPTable mainTable = new PdfPTable(6);
                                mainTable.WidthPercentage = 100;

                                mainTable.SetWidths(new int[] { 15, 20, 15, 20, 15, 15 });
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày thu hoạch/ Xuất bán", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Mã số truy vết", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng thu hoạch", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Người thu hoạch", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng bán (Kg)", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nơi thu mua", fontHeader), 1));
                                DataTable nTable = Report_Data.HarvestedForSaleReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                                if (nTable != null)
                                {
                                    for (int i = 0; i < nTable.Rows.Count; i++)
                                    {
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Code"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["QuantityHarvested"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["MemberName"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["QuantitySale"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["WhereToBuy"].ToString(), fontozel), 0));
                                    }
                                }
                                document.Add(mainTable);
                                document.NewPage();
                            }
                            #endregion
                            #region [ KIỂM KÊ TỒN KHO ]
                            if (1 == 1)
                            {

                                Paragraph para = new Paragraph(@"KIỂM KÊ TỒN KHO", new Font(bfTimes, 30, Font.BOLD));
                                para.Alignment = Element.ALIGN_CENTER;
                                para.SpacingAfter = 20;
                                document.Add(para);

                                PdfPTable mainTable = new PdfPTable(4);
                                mainTable.WidthPercentage = 100;

                                mainTable.SetWidths(new int[] { 15, 25, 25, 25 });
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày kiểm kê", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("phân bón/ Hóa chất", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng", fontHeader), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Hạn dùng", fontHeader), 1));
                                DataTable nTable = Report_Data.InventoryReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                                if (nTable != null)
                                {
                                    for (int i = 0; i < nTable.Rows.Count; i++)
                                    {
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["ExpireDate"]), fontozel), 1));
                                    }
                                }
                                document.Add(mainTable);
                                document.NewPage();
                            }
                            #endregion
                        }
                    }
                }
                else 
                {
                    Seed_Info sinfo = new Seed_Info(seedkey.ToInt());
                    NameSeed = "_" + sinfo.SeedsName;
                    Paragraph header = new Paragraph(@"SỔ NHẬT KÝ SẢN XUẤT", new Font(bfTimes, 40, Font.BOLD));
                    header.Alignment = Element.ALIGN_CENTER;
                    header.SpacingAfter = 20;
                    document.Add(header);
                    Paragraph header1 = new Paragraph(@"Năm............", new Font(bfTimes, 20, Font.NORMAL));
                    header1.Alignment = Element.ALIGN_CENTER;
                    header1.SpacingAfter = 170;
                    document.Add(header1);
                    document.Add(new Paragraph(@"NÔNG HỘ: " + minfo.Name, new Font(bfTimes, 20, Font.BOLD)));
                    document.Add(new Paragraph(@"MÃ SỐ: " + minfo.MemID, new Font(bfTimes, 20, Font.BOLD)));
                    document.NewPage();
                    Paragraph Text1 = new Paragraph(@"PHẦN SẢN XUẤT", new Font(bfTimes, 30, Font.BOLD));
                    Text1.Alignment = Element.ALIGN_CENTER;
                    document.Add(Text1);
                    Paragraph Text2 = new Paragraph(@"Cây trồng: " + sinfo.SeedsName, new Font(bfTimes, 25, Font.NORMAL));
                    Text2.Alignment = Element.ALIGN_CENTER;
                    document.Add(Text2);
                    Paragraph Text3 = new Paragraph(@"Từ " + this.Request["from"] + " đến " + this.Request["to"], new Font(bfTimes, 25, Font.NORMAL));
                    Text3.Alignment = Element.ALIGN_CENTER;
                    document.Add(Text3);
                    document.NewPage();
                    #region [ Quản lý giống ]
                    if (1 == 1)
                    {

                        Paragraph para = new Paragraph(@"QUẢN LÝ GIỐNG", new Font(bfTimes, 30, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        para.SpacingAfter = 20;
                        document.Add(para);

                        PdfPTable mainTable = new PdfPTable(6);
                        mainTable.WidthPercentage = 100;

                        mainTable.SetWidths(new int[] { 15, 20, 15, 15, 15, 20 });
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên Giống", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nhà sản xuất/Phân phối (Nguồn gốc giống)", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày trồng", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Mã số lô", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Diện tích", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng lượng giống", fontHeader), 1));
                        DataTable nTable = Report_Data.SeedProcessReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                        if (nTable != null)
                        {
                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["CompanyName"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Parcel"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Area"].ToString() + " " + nTable.Rows[i]["UnitArea"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["UnitQuantity"].ToString(), fontozel), 1));
                            }
                        }

                        document.Add(mainTable);
                    }
                    #endregion
                    #region [ Quản lý ủ phân hữu cơ ]
                    if (1 == 1)
                    {

                        Paragraph para = new Paragraph(@"QUẢN LÝ Ủ PHÂN HỮU CƠ", new Font(bfTimes, 30, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        para.SpacingAfter = 20;
                        document.Add(para);

                        PdfPTable mainTable = new PdfPTable(5);
                        mainTable.WidthPercentage = 100;

                        mainTable.SetWidths(new int[] { 15, 10, 45, 15, 15 });
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Loại phân", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Khối lượng", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Phương pháp ủ", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thời gian ủ", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Người thực hiện", fontHeader), 1));
                        DataTable nTable = Report_Data.CompostingOrganicReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                        if (nTable != null)
                        {
                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Method"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["CompostingDates"].ToString() + " ngày", fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Member_Name"].ToString(), fontozel), 0));

                            }
                        }

                        document.Add(mainTable);
                        document.NewPage();
                    }
                    #endregion
                    #region [ Quản lý Mua phân/hóa chất ]
                    if (1 == 1)
                    {

                        Paragraph para = new Paragraph(@"QUẢN LÝ MUA PHÂN BÓN, HÓA CHẤT", new Font(bfTimes, 30, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        para.SpacingAfter = 20;
                        document.Add(para);

                        PdfPTable mainTable = new PdfPTable(6);
                        mainTable.WidthPercentage = 100;

                        mainTable.SetWidths(new int[] { 10, 15, 25, 25, 15, 10 });
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày mua", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên thương mại", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thành phần/ Hoạt chất", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nhà sản xuất/ Phân phối", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nhà cung cấp", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng", fontHeader), 1));
                        DataTable nTable = Report_Data.FertilizerPesticideBuyReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                        if (nTable != null)
                        {
                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Common"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Company"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Address"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                            }
                        }
                        document.Add(mainTable);
                        document.NewPage();
                    }
                    #endregion
                    #region [ Quản lý Sử dụng phân/hóa chất ]
                    if (1 == 1)
                    {

                        Paragraph para = new Paragraph(@"QUẢN LÝ SỬ DỤNG PHÂN BÓN, HÓA CHẤT", new Font(bfTimes, 30, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        para.SpacingAfter = 20;
                        document.Add(para);

                        PdfPTable mainTable = new PdfPTable(9);
                        mainTable.WidthPercentage = 100;

                        mainTable.SetWidths(new int[] { 9, 11, 14, 15, 10, 15, 10, 10, 6 });
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Cộng việc", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Lý do áp dụng/ Mật độ", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Phương pháp", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thiết bị sử dụng", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nguyên vật liệu (tên phân bón/ thuốc BVTV/ hóa chất)", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Liều lượng (Nồng độ)", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng lượng/ lít nước", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("TGCL", fontHeader), 1));
                        DataTable nTable = Report_Data.FertilizerPesticideUseReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                        if (nTable != null)
                        {
                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                string TGCL = "";
                                if (nTable.Rows[i]["TGCL"].ToString() == "")
                                {
                                    TGCL = "";
                                }
                                else
                                {
                                    TGCL = nTable.Rows[i]["TGCL"].ToString() + " ngày";
                                }
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Parcel"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Area"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Solution"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["EquipmentName"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Dose"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Dosage"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(TGCL, fontozel), 0));
                            }
                        }
                        document.Add(mainTable);
                        document.NewPage();
                    }
                    #endregion
                    #region [ KIỂM TRA, BẢO TRÌ, VỆ SINH THIẾT BỊ DỤNG CỤ ]
                    if (1 == 1)
                    {

                        Paragraph para = new Paragraph(@"KIỂM TRA, BẢO TRÌ, VỆ SINH THIẾT BỊ DỤNG CỤ", new Font(bfTimes, 30, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        para.SpacingAfter = 20;
                        document.Add(para);

                        PdfPTable mainTable = new PdfPTable(5);
                        mainTable.WidthPercentage = 100;

                        mainTable.SetWidths(new int[] { 15, 15, 15, 40, 15 });
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày thực hiện", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Thiết bị, dụng cụ", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Hoạt động", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Mô tả phương pháp thực hiện", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Người thực hiện", fontHeader), 1));
                        DataTable nTable = Report_Data.CheckEquipmentReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                        if (nTable != null)
                        {
                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Action"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Info"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["MemberName"].ToString(), fontozel), 0));
                            }
                        }
                        document.Add(mainTable);
                        document.NewPage();
                    }
                    #endregion
                    #region [ QUẢN LÝ THU HOẠCH, XUẤT BÁN ]
                    if (1 == 1)
                    {

                        Paragraph para = new Paragraph(@"QUẢN LÝ THU HOẠCH, XUẤT BÁN", new Font(bfTimes, 30, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        para.SpacingAfter = 20;
                        document.Add(para);

                        PdfPTable mainTable = new PdfPTable(6);
                        mainTable.WidthPercentage = 100;

                        mainTable.SetWidths(new int[] { 15, 20, 15, 20, 15, 15 });
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày thu hoạch/ Xuất bán", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Mã số truy vết", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng thu hoạch", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Người thu hoạch", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng bán (Kg)", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Nơi thu mua", fontHeader), 1));
                        DataTable nTable = Report_Data.HarvestedForSaleReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                        if (nTable != null)
                        {
                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Code"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["QuantityHarvested"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["MemberName"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["QuantitySale"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["WhereToBuy"].ToString(), fontozel), 0));
                            }
                        }
                        document.Add(mainTable);
                        document.NewPage();
                    }
                    #endregion
                    #region [ KIỂM KÊ TỒN KHO ]
                    if (1 == 1)
                    {

                        Paragraph para = new Paragraph(@"KIỂM KÊ TỒN KHO", new Font(bfTimes, 30, Font.BOLD));
                        para.Alignment = Element.ALIGN_CENTER;
                        para.SpacingAfter = 20;
                        document.Add(para);

                        PdfPTable mainTable = new PdfPTable(4);
                        mainTable.WidthPercentage = 100;

                        mainTable.SetWidths(new int[] { 15, 25, 25, 25 });
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày kiểm kê", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("phân bón/ Hóa chất", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng", fontHeader), 1));
                        mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Hạn dùng", fontHeader), 1));
                        DataTable nTable = Report_Data.InventoryReport(memberID, seedkey.ToInt(), FromDate, ToDate);
                        if (nTable != null)
                        {
                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["ExpireDate"]), fontozel), 1));
                            }
                        }
                        document.Add(mainTable);
                        document.NewPage();
                    }
                    #endregion
                }
                document.Close();
                writer.Close();
                Response.ContentType = "application/pdf;";
                Response.AddHeader("Content-Disposition", "attachment; filename=SoNhatKySanXuat_" + TNLibrary.Book.Utils.RemoveUnicode(minfo.Name.Replace(" ", "")) + TNLibrary.Book.Utils.RemoveUnicode(NameSeed.Replace(" ", "")) + "_" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm") + ".pdf");
                byte[] pdf = ms.ToArray();
                Response.OutputStream.Write(pdf, 0, pdf.Length);
            }
        }
        private class PDFFooter : PdfPageEventHelper
        {
            int page = 1;
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                tabFot.SpacingAfter = 10F;
                PdfPCell cell;
                tabFot.TotalWidth = 300F;
                cell = new PdfPCell(new Phrase(""));
                cell.BorderWidth = 0;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
                BaseFont bfTimes = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, false);
                Font fontHeader = new Font(bfTimes, 10, Font.ITALIC);

                PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                PdfPCell cell;
                tabFot.TotalWidth = 600F;
                cell = new PdfPCell(new Phrase("Lần ban hành: 02     Ngày ban hành: 1/4/2014     Số hiệu: BM01/THVIETGAP     Trang: " + page++, fontHeader));
                cell.BorderWidth = 0;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }
        private static PdfPCell GetCellForBorderlessTable(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 5f;
            cell.BorderWidth = 1;
            cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            return cell;
        }
    }
}