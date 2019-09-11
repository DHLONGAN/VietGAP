using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using TNLibrary.Book;
using System.Data;

namespace BookVietGAP.ReportPDF
{
    public partial class RpCost : System.Web.UI.Page
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
                    Paragraph header = new Paragraph(@"BÁO CÁO CHI PHÍ TỔNG HỢP", new Font(bfTimes, 40, Font.BOLD));
                    header.Alignment = Element.ALIGN_CENTER;
                    header.SpacingAfter = 20;
                    document.Add(header);
                    Paragraph Text3 = new Paragraph(@"Từ " + this.Request["from"] + " đến " + this.Request["to"], new Font(bfTimes, 25, Font.NORMAL));
                    Text3.Alignment = Element.ALIGN_CENTER;
                    Text3.SpacingAfter = 150;
                    document.Add(Text3);
                }
                else
                {
                    Paragraph header = new Paragraph(@"BÁO CÁO CHI PHÍ", new Font(bfTimes, 40, Font.BOLD));
                    header.Alignment = Element.ALIGN_CENTER;
                    header.SpacingAfter = 20;
                    document.Add(header);
                    Paragraph Text3 = new Paragraph(@"Từ " + this.Request["from"] + " đến " + this.Request["to"], new Font(bfTimes, 25, Font.NORMAL));
                    Text3.Alignment = Element.ALIGN_CENTER;
                    Text3.SpacingAfter = 20;
                    document.Add(Text3);
                    Seed_Info sinfo = new Seed_Info(seedkey.ToInt());
                    Paragraph headerseed = new Paragraph(sinfo.SeedsName, new Font(bfTimes, 40, Font.BOLD));
                    headerseed.Alignment = Element.ALIGN_CENTER;
                    headerseed.SpacingAfter = 90;
                    document.Add(headerseed);
                }
                document.Add(new Paragraph(@"NÔNG HỘ: " + minfo.Name, new Font(bfTimes, 20, Font.BOLD)));
                document.Add(new Paragraph(@"MÃ SỐ: " + minfo.MemID, new Font(bfTimes, 20, Font.BOLD)));
                document.NewPage();

                if (seedkey.ToInt() == 0)
                {
                    NameSeed = "_Tatca";
                    if (1 == 1)
                    {
                        DataTable mTable = Report_Data.SumMoneyReport(memberID, seedkey, FromDate, ToDate);
                        Paragraph headerTH = new Paragraph(@"Bảng tổng hợp thu chi", new Font(bfTimes, 30, Font.BOLD));
                        headerTH.Alignment = Element.ALIGN_CENTER;
                        headerTH.SpacingAfter = 20;
                        document.Add(headerTH);
                        PdfPTable THTable = new PdfPTable(2);
                        THTable.WidthPercentage = 100;
                        THTable.SetWidths(new int[] { 50, 50 });
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng thu", fontHeader), 1));
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Compute("Sum(TotalBuy)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalBuy)", "")), fontHeader), 1));
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Compute("Sum(TotalSale)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalSale)", "")), fontHeader), 1));
                        document.Add(THTable);
                        document.Add(new Paragraph("Ghi chú: Tổng chi = Tổng chi giống + Tổng chi phân bón + Tổng chi Thuốc(hóa chất) ", new Font(bfTimes, 15, Font.NORMAL)));
                        document.Add(new Paragraph("Tổng thu = Tổng thu hoạch xuất bán ", new Font(bfTimes, 15, Font.NORMAL)));
                        document.NewPage();
                        Paragraph headerTP = new Paragraph(@"Thống kê theo thành phần", new Font(bfTimes, 30, Font.BOLD));
                        headerTP.Alignment = Element.ALIGN_CENTER;
                        headerTP.SpacingAfter = 20;
                        document.Add(headerTP);

                        document.Add(new Paragraph("1. Thống kê theo giống ", new Font(bfTimes, 13, Font.BOLD)));
                        PdfPTable SeedTable = new PdfPTable(2);
                        SeedTable.SpacingBefore = 10;
                        SeedTable.WidthPercentage = 100;
                        SeedTable.SetWidths(new int[] { 50, 50 });
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên giống", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        double TotalSeedProcess = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Mua giống")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                TotalSeedProcess += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi giống : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalSeedProcess), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.Add(new Paragraph("2. Thống kê theo phần bón ", new Font(bfTimes, 13, Font.BOLD)));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên phân bón", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        double TotalFertilizers = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Mua phân bón")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                TotalFertilizers += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi phân bón : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalFertilizers), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.Add(new Paragraph("3. Thống kê theo Thuốc(hóa chất) ", new Font(bfTimes, 13, Font.BOLD)));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên thuốc", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        double TotalPesticides = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Mua thuốc BVTV")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                TotalPesticides += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi Thuốc(hóa chất) : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalPesticides), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.Add(new Paragraph("4. Thống kê theo thu hoạch xuất bán ", new Font(bfTimes, 13, Font.BOLD)));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên giống", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng thu", fontHeader), 1));
                        double TotalHarvestedForSale = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Thu hoạch xuất bán")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]), fontozel), 2));
                                TotalHarvestedForSale += String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng thu hoạch xuất bán : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalHarvestedForSale), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.NewPage();
                        DataTable nTable = Report_Data.MoneyReport(memberID, seedkey, FromDate, ToDate);
                        if (nTable != null)
                        {
                            Paragraph headerDate = new Paragraph(@"Bảng tổng hợp thu chi theo ngày", new Font(bfTimes, 30, Font.BOLD));
                            headerDate.Alignment = Element.ALIGN_CENTER;
                            headerDate.SpacingAfter = 20;
                            document.Add(headerDate);
                            PdfPTable mainTable = new PdfPTable(8);
                            mainTable.WidthPercentage = 100;
                            mainTable.SetWidths(new int[] { 5, 10, 20, 20, 10, 10, 10, 15 });
                            double TotalSale = 0, TotalBuy = 0;
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("STT", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Loại thu chi", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên Hàng", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng Chi", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng Thu", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Cung Cấp", fontHeader), 1));

                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase((i + 1).ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Type"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", nTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", nTable.Rows[i]["TotalSale"]), fontozel), 2));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Address"].ToString(), fontozel), 0));
                                TotalSale += nTable.Rows[i]["TotalSale"].ToDouble();
                                TotalBuy += nTable.Rows[i]["TotalBuy"].ToDouble();
                            }
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalBuy), fontHeader), 2));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalSale), fontHeader), 2));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            document.Add(mainTable);
                        }
                    }
                }
                else
                {
                    Seed_Info sinfo = new Seed_Info(seedkey.ToInt());
                    NameSeed = "_" + sinfo.SeedsName;
                    if (1 == 1)
                    {
                        DataTable mTable = Report_Data.SumMoneyReport(memberID, seedkey, FromDate, ToDate);
                        Paragraph headerTH = new Paragraph(@"Bảng tổng hợp thu chi", new Font(bfTimes, 30, Font.BOLD));
                        headerTH.Alignment = Element.ALIGN_CENTER;
                        headerTH.SpacingAfter = 20;
                        document.Add(headerTH);
                        PdfPTable THTable = new PdfPTable(2);
                        THTable.WidthPercentage = 100;
                        THTable.SetWidths(new int[] { 50, 50 });
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng thu", fontHeader), 1));
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Compute("Sum(TotalBuy)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalBuy)", "")), fontHeader), 1));
                        THTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Compute("Sum(TotalSale)", "").ToString() == "" ? "0" : mTable.Compute("Sum(TotalSale)", "")), fontHeader), 1));
                        document.Add(THTable);
                        document.Add(new Paragraph("Ghi chú: Tổng chi = Tổng chi giống + Tổng chi phân bón + Tổng chi Thuốc(hóa chất) ", new Font(bfTimes, 15, Font.NORMAL)));
                        document.Add(new Paragraph("Tổng thu = Tổng thu hoạch xuất bán ", new Font(bfTimes, 15, Font.NORMAL)));
                        document.NewPage();
                        document.Add(new Paragraph("Thống kê theo thành phần ", new Font(bfTimes, 15, Font.BOLD)));
                        document.Add(new Paragraph("1. Thống kê theo giống ", new Font(bfTimes, 13, Font.BOLD)));
                        PdfPTable SeedTable = new PdfPTable(2);
                        SeedTable.SpacingBefore = 10;
                        SeedTable.WidthPercentage = 100;
                        SeedTable.SetWidths(new int[] { 50, 50 });
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên giống", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        double TotalSeedProcess = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Mua giống")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                TotalSeedProcess += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi giống : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalSeedProcess), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.Add(new Paragraph("2. Thống kê theo phần bón ", new Font(bfTimes, 13, Font.BOLD)));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên phân bón", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        double TotalFertilizers = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Mua phân bón")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                TotalFertilizers += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi phân bón : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalFertilizers), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.Add(new Paragraph("3. Thống kê theo Thuốc(hóa chất) ", new Font(bfTimes, 13, Font.BOLD)));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên thuốc", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi", fontHeader), 1));
                        double TotalPesticides = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Mua thuốc BVTV")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                TotalPesticides += String.Format("{0:#,0}", mTable.Rows[i]["TotalBuy"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng chi Thuốc(hóa chất) : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalPesticides), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.Add(new Paragraph("4. Thống kê theo thu hoạch xuất bán ", new Font(bfTimes, 13, Font.BOLD)));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên giống", fontHeader), 1));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng thu", fontHeader), 1));
                        double TotalHarvestedForSale = 0;
                        for (int i = 0; i < mTable.Rows.Count; i++)
                        {
                            if (mTable.Rows[i]["Type"].ToString() == "Thu hoạch xuất bán")
                            {
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(mTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]), fontozel), 2));
                                TotalHarvestedForSale += String.Format("{0:#,0}", mTable.Rows[i]["TotalSale"]).ToDouble();
                            }
                        }
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng thu hoạch xuất bán : ", fontHeader), 2));
                        SeedTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalHarvestedForSale), fontozel), 2));
                        document.Add(SeedTable);
                        SeedTable.DeleteBodyRows();
                        document.NewPage();
                        DataTable nTable = Report_Data.MoneyReport(memberID, seedkey, FromDate, ToDate);
                        if (nTable != null)
                        {
                            Paragraph headerDate = new Paragraph(@"Bảng tổng hợp thu chi theo ngày", new Font(bfTimes, 30, Font.BOLD));
                            headerDate.Alignment = Element.ALIGN_CENTER;
                            headerDate.SpacingAfter = 20;
                            document.Add(headerDate);
                            PdfPTable mainTable = new PdfPTable(8);
                            mainTable.WidthPercentage = 100;
                            mainTable.SetWidths(new int[] { 5, 10, 20, 20, 10, 10, 10, 15 });
                            double TotalSale = 0, TotalBuy = 0;
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("STT", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Ngày", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Loại thu chi", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tên Hàng", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Số lượng", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng Chi", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng Thu", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Cung Cấp", fontHeader), 1));

                            for (int i = 0; i < nTable.Rows.Count; i++)
                            {
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase((i + 1).ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(Utils.DateTostring((DateTime)nTable.Rows[i]["DateTime"]), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Type"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Name"].ToString(), fontozel), 0));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Quantity"].ToString() + " " + nTable.Rows[i]["Unit"].ToString(), fontozel), 1));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", nTable.Rows[i]["TotalBuy"]), fontozel), 2));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", nTable.Rows[i]["TotalSale"]), fontozel), 2));
                                mainTable.AddCell(GetCellForBorderlessTable(new Phrase(nTable.Rows[i]["Address"].ToString(), fontozel), 0));
                                TotalSale += nTable.Rows[i]["TotalSale"].ToDouble();
                                TotalBuy += nTable.Rows[i]["TotalBuy"].ToDouble();
                            }
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("Tổng", fontHeader), 1));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalBuy), fontHeader), 2));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase(String.Format("{0:#,0}", TotalSale), fontHeader), 2));
                            mainTable.AddCell(GetCellForBorderlessTable(new Phrase("", fontHeader), 1));
                            document.Add(mainTable);
                        }
                    }
                }
                
                document.Close();
                writer.Close();
                Response.ContentType = "application/pdf;";
                Response.AddHeader("Content-Disposition", "attachment; filename=BaoCaoChiPhi_" + TNLibrary.Book.Utils.RemoveUnicode(minfo.Name.Replace(" ", "")) + TNLibrary.Book.Utils.RemoveUnicode(NameSeed.Replace(" ", "")) + "_" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm") + ".pdf");
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
                tabFot.TotalWidth = 50F;
                cell = new PdfPCell(new Phrase("Trang: " + page++, fontHeader));
                cell.BorderWidth = 0;
                tabFot.AddCell(cell);
                tabFot.WriteSelectedRows(0, -1, 650, document.Bottom, writer.DirectContent);
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