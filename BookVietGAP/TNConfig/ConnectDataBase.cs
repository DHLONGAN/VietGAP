using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace TNConfig
{
    public class ConnectDataBase
    {
        private string _Message = "";
        private static SqlConnection _SQLConnect;
        //static string _ConnectionString = @"Data Source=.\sql2008;DataBase=Vietgap;User=sa;Password=123456;Pooling=False";
        private static string _ConnectionString = @"Data Source=45.125.239.35;DataBase=VietGap;User=sa;Password=vietgap1!;Pooling=False";

        public ConnectDataBase()
        {
            _SQLConnect = new SqlConnection();
        }
        public ConnectDataBase(string StrConnect)
        {
            try
            {
                _SQLConnect = new SqlConnection();
                _SQLConnect.ConnectionString = StrConnect;
                _SQLConnect.Open();
                _ConnectionString = StrConnect;
            }
            catch (Exception Err)
            {
                _Message = Err.ToString();
            }
            finally
            {
                _SQLConnect.Close();
            }

        }
        public void CloseConnect()
        {
            _SQLConnect.Close();
        }
        public string Message
        {
            get
            {
                return _Message;
            }
        }
        public static string ConnectionString
        {
            set
            {
                _ConnectionString = value;
            }
            get
            {
                return _ConnectionString;
            }

        }

    }
}
