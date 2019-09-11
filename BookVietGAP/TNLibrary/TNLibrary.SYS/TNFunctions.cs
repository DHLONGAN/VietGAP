using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TNLibrary.SYS
{
    public class TNFunctions
    {
        public static DateTime TransferToDateBeginMonth(string TranferDate)
        {
            string nStrFinancialMonth = TranferDate;
            int nMonth = int.Parse(nStrFinancialMonth.Substring(0, 2));
            int nYear = int.Parse(nStrFinancialMonth.Substring(3, 4));
            return new DateTime(nYear, nMonth, 1, 0, 0, 0);
        }

        public static DateTime TransferToDateEndMonth(string TranferDate)
        {
            string nStrFinancialMonth = TranferDate;
            int nMonth = int.Parse(nStrFinancialMonth.Substring(0, 2));
            int nYear = int.Parse(nStrFinancialMonth.Substring(3, 4));
            DateTime nDate = new DateTime(nYear, nMonth, 1, 23, 59, 0);
            nDate = nDate.AddMonths(1);
            nDate = nDate.AddDays(-1);
            return nDate;
        }

        public static int DayEndMonth(string MonthCurrency)
        {
            DateTime nDay = new DateTime(int.Parse(MonthCurrency.Substring(3, 4)), int.Parse(MonthCurrency.Substring(0, 2)), 01);
            nDay = nDay.AddMonths(1);
            nDay = nDay.AddDays(-1);
            return nDay.Day;
        }
        public static int DayEndMonth(DateTime DateCurrency)
        {
            DateTime nDay = new DateTime(DateCurrency.Year, DateCurrency.Month, 01);
            nDay = nDay.AddMonths(1);
            nDay = nDay.AddDays(-1);
            return nDay.Day;
        }

        public static string GetMonthYear(DateTime DateCurrency)
        {
            string strDate = DateCurrency.Month.ToString() + "/" + DateCurrency.Year.ToString();
            if (strDate.Length < 7)
                strDate = "0" + strDate;
            return strDate;
        }

        public static ArrayList ListOne(ArrayList ListMember, string Member)
        {

            foreach (string nMember in ListMember)
            {
                if (nMember == Member)
                {
                    return ListMember;
                }
            }
            ListMember.Add(Member);
            return ListMember;
        }

        public static bool SearchString(string StrSource, string StrSearch)
        {
            string[] nStrSource = StrSource.Split(',');
            string[] nStrSearch = StrSearch.Split(',');
            foreach (string nItemSearch in nStrSearch)
            {
                foreach (string nItemSource in nStrSource)
                {
                    if (nItemSearch == nItemSource)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void MOD(long Number, long Divisor, out long a, out long b)
        {
            Math.DivRem(Number, Divisor, out b);
            a = (Number - b) / Divisor;
        }
        public static string NumberHundredsWords(long Number)
        {
            long a, b;
            bool IsHaveTens = false;
            string nNumberWords = "";
            string[] WordsVN = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            if (Number >= 100)
            {
                MOD(Number, 100, out a, out b);
                nNumberWords = WordsVN[a] + " trăm ";
                Number = b;
            }

            if (Number >= 10)
            {
                MOD(Number, 10, out a, out b);
                if (a < 2)
                    nNumberWords += "mười ";
                else
                    nNumberWords += WordsVN[a] + " mươi ";

                Number = b;

                IsHaveTens = true;
            }

            if (Number > 0)
            {
                if (!IsHaveTens)
                    nNumberWords += " lẽ ";
                nNumberWords += WordsVN[Number];
            }
            return nNumberWords;
        }
        public static string NumberWords(long Number)
        {
            long a, b;
            string nNumberWords = "";
            if (Number == 0)// 4/1/2011
            {
                return nNumberWords = "không";
            }
            if (Number >= 1000000000)
            {
                MOD(Number, 1000000000, out a, out b);
                nNumberWords = NumberHundredsWords(a) + " tỷ, ";
                Number = b;
            }

            if (Number >= 1000000)
            {
                MOD(Number, 1000000, out a, out b);
                nNumberWords += NumberHundredsWords(a) + " triệu, ";
                Number = b;
            }

            if (Number >= 1000)
            {
                MOD(Number, 1000, out a, out b);
                nNumberWords += NumberHundredsWords(a) + " ngàn, ";
                Number = b;
            }
            if (Number > 0)
            {
                nNumberWords += NumberHundredsWords(Number);
            }

            nNumberWords = nNumberWords.Trim();

            if (nNumberWords.Substring(0, 1) == "l")
                nNumberWords = nNumberWords.Substring(3, nNumberWords.Length - 3);

            if (nNumberWords.Substring(nNumberWords.Length - 1) == ",")
                nNumberWords = nNumberWords.Substring(0, nNumberWords.Length - 1);

            nNumberWords = nNumberWords.Substring(0, 1).ToUpper() + nNumberWords.Substring(1);

            return nNumberWords;
        }
        public static string NumberWords(double Number)
        {
            long a = 0, b = 0;
            string nNumberWords = "";
            string[] nSplit = Number.ToString().Split('.');
            a = long.Parse(nSplit[0]);
            nNumberWords = NumberWords(a);

            if (nSplit.Length == 2)
            {
                b = long.Parse(nSplit[1]);
            }
            if (b > 0)
            {
                string nNumberWordsAfter = NumberHundredsWords(b).Trim();
                if (nNumberWordsAfter.Substring(0, 1) == "l")
                    nNumberWordsAfter = nNumberWordsAfter.Substring(3, nNumberWordsAfter.Length - 3);

                nNumberWords += " phẩy " + nNumberWordsAfter;
            }

            return nNumberWords;
        }
        public static DateTime ParseDateVNToEN(string DateVN)
        {
            string[] split = DateVN.Split('/');

            string DateChanged = split[1] + "/" + split[0] + "/" + split[2];

            return DateTime.Parse(DateChanged);
        }

        public static int PageNumber(int PageSize, int Record)
        {
            int SoNguyen = Record / PageSize;
            int SoDu = Record % PageSize;

            if (SoDu > 0)
            {
                SoNguyen = SoNguyen + 1;
            }
            return SoNguyen;
        }

        public static int NumberDaysWorkingInMonth(int Month, int Year)
        {
            int nResult = 0;
            DateTime nDay = new DateTime(Year, Month, 01);
            int nNextMonth = nDay.AddMonths(1).Month;
            while (nDay.Month != nNextMonth)
            {
                string strDayOfWeek = nDay.DayOfWeek.ToString();
                if (strDayOfWeek != "Saturday" && strDayOfWeek != "Sunday")
                    nResult++;
                nDay = nDay.AddDays(1);
            }
            return nResult;
        }
    }
}
