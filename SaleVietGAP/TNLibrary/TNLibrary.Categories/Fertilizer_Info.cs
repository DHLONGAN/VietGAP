﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
    public class Fertilizer_Info
    {

        #region [ Field Name ]
        private int _FertilizersKey = 0;
        private string _TradeName = "";
        private int _UnitKey = 0;
        private int _CommonKey = 0;
        private int _CompanyKey = 0;
        private int _CategoryKey = 0;
        private int _UsingStatus = 0;
        private string _Images = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Fertilizer_Info()
        {
        }
        public Fertilizer_Info(int FertilizersKey)
        {
            string zSQL = "SELECT * FROM PUL_Fertilizers WHERE FertilizersKey = @FertilizersKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@FertilizersKey", SqlDbType.Int).Value = FertilizersKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _FertilizersKey = int.Parse(zReader["FertilizersKey"].ToString());
                    _TradeName = zReader["TradeName"].ToString();
                    _UnitKey = int.Parse(zReader["UnitKey"].ToString());
                    _CommonKey = int.Parse(zReader["CommonKey"].ToString());
                    _CompanyKey = int.Parse(zReader["CompanyKey"].ToString());
                    _CategoryKey = int.Parse(zReader["CategoryKey"].ToString());
                    _UsingStatus = int.Parse(zReader["UsingStatus"].ToString());
                    _Images = zReader["Images"].ToString();
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int FertilizersKey
        {
            get { return _FertilizersKey; }
            set { _FertilizersKey = value; }
        }
        public string TradeName
        {
            get { return _TradeName; }
            set { _TradeName = value; }
        }
        public int UnitKey
        {
            get { return _UnitKey; }
            set { _UnitKey = value; }
        }
        public int CommonKey
        {
            get { return _CommonKey; }
            set { _CommonKey = value; }
        }
        public int CompanyKey
        {
            get { return _CompanyKey; }
            set { _CompanyKey = value; }
        }
        public int CategoryKey
        {
            get { return _CategoryKey; }
            set { _CategoryKey = value; }
        }
        public int UsingStatus
        {
            get { return _UsingStatus; }
            set { _UsingStatus = value; }
        }
        public string Images
        {
            get { return _Images; }
            set { _Images = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        #endregion

        #region [ Constructor Update Information ]

        public string Create()
        {
            //---------- String SQL Access Database ---------------
            string zSQL = "INSERT INTO PUL_Fertilizers ("
        + " TradeName ,UnitKey ,CommonKey ,CompanyKey ,CategoryKey ,UsingStatus ,Images ) "
         + " VALUES ( "
         + "@TradeName ,@UnitKey ,@CommonKey ,@CompanyKey ,@CategoryKey ,@UsingStatus ,@Images ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@FertilizersKey", SqlDbType.Int).Value = _FertilizersKey;
                zCommand.Parameters.Add("@TradeName", SqlDbType.NVarChar).Value = _TradeName;
                zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
                zCommand.Parameters.Add("@CommonKey", SqlDbType.Int).Value = _CommonKey;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = _CategoryKey;
                zCommand.Parameters.Add("@UsingStatus", SqlDbType.Int).Value = _UsingStatus;
                zCommand.Parameters.Add("@Images", SqlDbType.NVarChar).Value = _Images;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
            }
            catch (Exception Err)
            {
                _Message = Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }


        public string Update()
        {
            string zSQL = "UPDATE PUL_Fertilizers SET "
                        + " TradeName = @TradeName,"
                        + " UnitKey = @UnitKey,"
                        + " CommonKey = @CommonKey,"
                        + " CompanyKey = @CompanyKey,"
                        + " CategoryKey = @CategoryKey,"
                        + " UsingStatus = @UsingStatus,"
                        + " Images = @Images"
                       + " WHERE FertilizersKey = @FertilizersKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@FertilizersKey", SqlDbType.Int).Value = _FertilizersKey;
                zCommand.Parameters.Add("@TradeName", SqlDbType.NVarChar).Value = _TradeName;
                zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
                zCommand.Parameters.Add("@CommonKey", SqlDbType.Int).Value = _CommonKey;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = _CategoryKey;
                zCommand.Parameters.Add("@UsingStatus", SqlDbType.Int).Value = _UsingStatus;
                zCommand.Parameters.Add("@Images", SqlDbType.NVarChar).Value = _Images;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
            }
            catch (Exception Err)
            {
                _Message = Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }


        public string Save()
        {
            string zResult;
            if (_FertilizersKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Fertilizers WHERE FertilizersKey = @FertilizersKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@FertilizersKey", SqlDbType.Int).Value = _FertilizersKey;
                zResult = zCommand.ExecuteNonQuery().ToString();
                zCommand.Dispose();
            }
            catch (Exception Err)
            {
                _Message = Err.ToString();
            }
            finally
            {
                zConnect.Close();
            }
            return zResult;
        }
        #endregion
    }
}
