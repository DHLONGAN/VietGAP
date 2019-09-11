using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using TNConfig;

namespace TNLibrary.SYS
{
    public class GlobalSystemConfig
    {
        private static string mCurrencyMain = "";                  //1
        private static IFormatProvider mFormatProviderCurrency;         //2
        private static string mFormatCurrencyMain = "#,###,###,###";             //3
        private static string mFormatCurrencyForeign = "#,###,###,###";           //4    
        private static string mFormatDecimal = "";                 //5
        private static IFormatProvider mFormatProviderDecimal;          //6
        private static string mFormatDate = "";                     //7
        private static string mMethorInventory = "";               //8

        private static string mAccountProduct = "152"; //9
        private static string mAccountVendor = "3311";//10
        private static string mAccountCustomer = "1311";//11

        private static string mVATOutput = "333111";
        private static string mVATDeduction = "133111";

        private static string mCashOnHand = "111";
        private static string mCashOnBank = "112";

        private static string mLanguage = "VN";
        private static string m_Message = "";
        public GlobalSystemConfig()
        {

        }
        public void LoadGlobalSystemConfig()
        {
            string nSQL = "SELECT * FROM SYS_SystemConfig";

            string nConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                SqlDataReader nReader = nCommand.ExecuteReader();
                int mParameterKey;
                while (nReader.Read())
                {
                    mParameterKey = int.Parse(nReader["ParameterKey"].ToString());
                    switch (mParameterKey)
                    {
                        case 1:
                            mCurrencyMain = nReader["UseValue"].ToString();
                            break;
                        case 2:
                            mFormatProviderCurrency = new CultureInfo(nReader["UseValue"].ToString(), true);
                            break;
                        case 3:
                            mFormatCurrencyMain = nReader["UseValue"].ToString();
                            break;
                        case 4:
                            mFormatCurrencyForeign = nReader["UseValue"].ToString();
                            break;
                        case 5:
                            mFormatDecimal = nReader["UseValue"].ToString();
                            break;
                        case 6:
                            mFormatProviderDecimal = new CultureInfo(nReader["UseValue"].ToString(), true);
                            break;
                        case 7:
                            mFormatDate = nReader["UseValue"].ToString();
                            break;
                        case 8:
                            mMethorInventory = nReader["UseValue"].ToString();
                            break;
                        case 9:
                            mAccountProduct = nReader["UseValue"].ToString();
                            break;
                        case 10:
                            mAccountVendor = nReader["UseValue"].ToString();
                            break;
                        case 11:
                            mAccountCustomer = nReader["UseValue"].ToString();
                            break;
                        case 12:
                            mVATOutput = nReader["UseValue"].ToString();
                            break;
                        case 13:
                            mVATDeduction = nReader["UseValue"].ToString();
                            break;
                        case 14:
                            mCashOnHand = nReader["UseValue"].ToString();
                            break;
                        case 15:
                            mCashOnBank = nReader["UseValue"].ToString();
                            break;
                        case 16:
                            mLanguage = nReader["UseValue"].ToString();
                            break;
                    }
                }
                //---- Close Connect SQL ----
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();
                
            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
        }
        public static string CurrencyMain
        {
            get
            {
                return mCurrencyMain;
            }

        }

        public static IFormatProvider FormatProviderCurrency
        {
            get
            {
                return mFormatProviderCurrency;
            }
        }
        public static string FormatCurrencyMain
        {
            get
            {
                return mFormatCurrencyMain;
            }
        }
        public static string FormatCurrencyMainTwoPoint
        {
            get
            {
                return mFormatCurrencyMain.Substring(0, 9) + ".00";
            }
        }

        public static int DigitsAfterCurrencyMain
        {
            get
            {
                string[] nList = mFormatCurrencyMain.Split('.');
                if (nList.Length > 1)
                    return nList[1].Length;
                else
                    return 0;
            }
        }
        public static string FormatCurrencyForeign
        {
            get
            {
                return mFormatCurrencyForeign;
            }
        }
        public static int DigitsAfterCurrencyForeign
        {
            get
            {
                string[] nList = mFormatCurrencyForeign.Split('.');
                if (nList.Length > 1)
                    return nList[1].Length;
                else
                    return 0;
            }
        }

        public static string FormatDecimal
        {
            get
            {
                return mFormatDecimal;
            }
        }
        public static IFormatProvider FormatProviderDecimal
        {
            get
            {
                return mFormatProviderDecimal;
            }
        }

        public static IFormatProvider FormatProviderDate
        {
            get
            {
                CultureInfo nCultureInfo;
                if (mFormatDate.Substring(0, 1) == "M")
                    nCultureInfo = new CultureInfo("en-US");
                else
                    nCultureInfo = new CultureInfo("es-ES");

                return mFormatProviderDecimal;
            }
        }
        public static string FormatDate
        {
            get
            {
                return mFormatDate;
            }
        }

        public static string MethorInventory
        {
            get
            {
                return mMethorInventory;
            }
        }

        public static string AccountProduct
        {
            get
            {
                return mAccountProduct;
            }
            set
            {
                mAccountProduct = value;
            }
        }
        public static string AccountVendor
        {
            get
            {
                return mAccountVendor;
            }
            set
            {
                mAccountVendor = value;
            }
        }
        public static string AccountCustomer
        {
            get
            {
                return mAccountCustomer;
            }
            set
            {
                mAccountCustomer = value;
            }
        }
        public static string VATOutput
        {
            get
            {
                return mVATOutput;
            }
            set
            {
                mVATOutput = value;
            }
        }
        public static string VATDeduction
        {
            get
            {
                return mVATDeduction;
            }
            set
            {
                mVATDeduction = value;
            }
        }

        public static string CashOnHand
        {
            get
            {
                return mCashOnHand;
            }
            set
            {
                mCashOnHand = value;
            }
        }
        public static string CashOnBank
        {
            get
            {
                return mCashOnBank;
            }
            set
            {
                mCashOnBank = value;
            }
        }
        public static string Language
        {
            get
            {
                return mLanguage;
            }
            set
            {
                mLanguage = value;
            }
        }
    }
}
