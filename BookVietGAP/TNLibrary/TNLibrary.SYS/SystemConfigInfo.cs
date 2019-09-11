using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TNConfig;

namespace TNLibrary.SYS
{
    public class SystemConfig_Info
    {
        //Field 
        private string mCurrencyMain = "";                  //1
        private string mFormatProviderCurrency = "";          //3
        private string mFormatCurrencyMain = "";             //2
        private string mFormatCurrencyForeign = "";           //4    
        private string mFormatDecimal = "";                 //5
        private string mFormatProviderDecimal = "";           //6
        private string mFormatDate;                     //7
        private string mMethorInventory = "";               //8

        private string mAccountProduct = "152"; //9
        private string mAccountVendor = "3311";//10
        private string mAccountCustomer = "1311";//11

        private string mVATOutput = "333111";
        private string mVATDeduction = "133111";

        private string mCashOnHand = "111";
        private string mCashOnBank = "112";
        private string mLanguage = "VN";

        private string m_Message = "";

        public SystemConfig_Info()
        {
            string nSQL = "SELECT * FROM SYS_SystemConfig";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

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
                            mFormatProviderCurrency = nReader["UseValue"].ToString();
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
                            mFormatProviderDecimal = nReader["UseValue"].ToString();
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

            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
            
        }

        #region [ Properties]
        public string CurrencyMain
        {
            get
            {
                return mCurrencyMain;
            }
            set
            {
                mCurrencyMain = value;
            }
        }
        public string FormatProviderCurrency
        {
            get
            {
                return mFormatProviderCurrency;
            }
            set
            {
                mFormatProviderCurrency = value;
            }
        }
        public string FormatCurrencyMain
        {
            get
            {
                return mFormatCurrencyMain;
            }
            set
            {
                mFormatCurrencyMain = value;
            }
        }
        public string FormatCurrencyForeign
        {
            get
            {
                return mFormatCurrencyForeign;
            }
            set
            {
                mFormatCurrencyForeign = value;
            }
        }
        public string FormatDecimal
        {
            get
            {
                return mFormatDecimal;
            }
            set
            {
                mFormatDecimal = value;
            }
        }
        public string FormatProviderDecimal
        {
            get
            {
                return mFormatProviderDecimal;
            }
            set
            {
                mFormatProviderDecimal = value;
            }
        }

        public string FormatDate
        {
            get
            {
                return mFormatDate;
            }
            set
            {
                mFormatDate = value;
            }
        }
        public string MethorInventory
        {
            get
            {
                return mMethorInventory;
            }
            set
            {
                mMethorInventory = value;
            }
        }

        public string AccountProduct
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
        public string AccountVendor
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
        public string AccountCustomer
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
        public string VATOutput
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
        public string VATDeduction
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
        public string CashOnHand
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
        public string CashOnBank
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

        public string Language
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
        #endregion

        #region [ Method ]

        public string UpdateAll()
        {
            Update(1, mCurrencyMain, "CurrencyMain");
            Update(2, mFormatProviderCurrency, "FormatProviderCurrency");
            Update(3, mFormatCurrencyMain, "FormatCurrencyMain");
            Update(4, mFormatCurrencyForeign, "FormatCurrencyForeign");
            Update(5, mFormatDecimal, "FormatDecimal");
            Update(6, mFormatProviderDecimal, "FormatProviderDecimal");
            Update(7, mFormatDate, "FormatDate");
            Update(8, mMethorInventory, "MethorInventory");
            Update(9, mAccountProduct, "AccountProduct");
            Update(10, mAccountVendor, "AccountVendor");
            Update(11, mAccountCustomer, "AccountCustomer");
            Update(12, mVATOutput, "VATOutput");
            Update(13, mVATDeduction, "VATDeduction");
            Update(14, mCashOnHand, "CashOnHand");
            Update(15, mCashOnBank, "CashOnBank");
            Update(16, mLanguage, "Language");
            return "";
        }
        public string Update(int ParameterKey, string UseValue, string ParameterName)
        {
            string nResult = "";
            string nSQL = "UPDATE SYS_SystemConfig SET UseValue = @UseValue WHERE (ParameterKey = @ParameterKey)";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@ParameterKey", SqlDbType.Int).Value = ParameterKey;
                nCommand.Parameters.Add("@UseValue", SqlDbType.NVarChar).Value = UseValue;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nSQL = "INSERT INTO SYS_SystemConfig (ParameterKey,ParameterName,UseValue)VALUES(@ParameterKey,@ParameterName,@UseValue)";
                nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.Parameters.Add("@ParameterKey", SqlDbType.Int).Value = ParameterKey;
                nCommand.Parameters.Add("@ParameterName", SqlDbType.NVarChar).Value = ParameterName;
                nCommand.Parameters.Add("@UseValue", SqlDbType.NVarChar).Value = UseValue;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();

            }
            catch (Exception err)
            {
                m_Message = err.Message.ToString();
            }

            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        #endregion
    }
}
