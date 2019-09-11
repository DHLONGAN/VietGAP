﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Soiltreatment_Info
    {

        #region [ Field Name ]
        private int _SoiltreatmentKey = 0;
        private DateTime _Datetime;
        private int _AdditivesKey = 0;
        private string _Quantity = "";
        private string _Howtouse = "";
        private string _Area = "";
        private string _Weather = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Soiltreatment_Info()
        {
        }
        public Soiltreatment_Info(int SoiltreatmentKey)
        {
            string zSQL = "SELECT * FROM PUL_Soiltreatment WHERE SoiltreatmentKey = @SoiltreatmentKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SoiltreatmentKey", SqlDbType.Int).Value = SoiltreatmentKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _SoiltreatmentKey = int.Parse(zReader["SoiltreatmentKey"].ToString());
                    if (zReader["Datetime"] != DBNull.Value)
                        _Datetime = (DateTime)zReader["Datetime"];
                    _AdditivesKey = int.Parse(zReader["AdditivesKey"].ToString());
                    _Quantity = zReader["Quantity"].ToString();
                    _Howtouse = zReader["Howtouse"].ToString();
                    _Area = zReader["Area"].ToString();
                    _Weather = zReader["Weather"].ToString();
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int SoiltreatmentKey
        {
            get { return _SoiltreatmentKey; }
            set { _SoiltreatmentKey = value; }
        }
        public DateTime Datetime
        {
            get { return _Datetime; }
            set { _Datetime = value; }
        }
        public int AdditivesKey
        {
            get { return _AdditivesKey; }
            set { _AdditivesKey = value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public string Howtouse
        {
            get { return _Howtouse; }
            set { _Howtouse = value; }
        }
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public string Weather
        {
            get { return _Weather; }
            set { _Weather = value; }
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
            string zSQL = "INSERT INTO PUL_Soiltreatment ("
        + " Datetime ,AdditivesKey ,Quantity ,Howtouse ,Area ,Weather ) "
         + " VALUES ( "
         + "@Datetime ,@AdditivesKey ,@Quantity ,@Howtouse ,@Area ,@Weather ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SoiltreatmentKey", SqlDbType.Int).Value = _SoiltreatmentKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@AdditivesKey", SqlDbType.Int).Value = _AdditivesKey;
                zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
                zCommand.Parameters.Add("@Howtouse", SqlDbType.NVarChar).Value = _Howtouse;
                zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
                zCommand.Parameters.Add("@Weather", SqlDbType.NVarChar).Value = _Weather;
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
            string zSQL = "UPDATE PUL_Soiltreatment SET "
                        + " Datetime = @Datetime,"
                        + " AdditivesKey = @AdditivesKey,"
                        + " Quantity = @Quantity,"
                        + " Howtouse = @Howtouse,"
                        + " Area = @Area,"
                        + " Weather = @Weather"
                       + " WHERE SoiltreatmentKey = @SoiltreatmentKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SoiltreatmentKey", SqlDbType.Int).Value = _SoiltreatmentKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@AdditivesKey", SqlDbType.Int).Value = _AdditivesKey;
                zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
                zCommand.Parameters.Add("@Howtouse", SqlDbType.NVarChar).Value = _Howtouse;
                zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
                zCommand.Parameters.Add("@Weather", SqlDbType.NVarChar).Value = _Weather;
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
            if (_SoiltreatmentKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Soiltreatment WHERE SoiltreatmentKey = @SoiltreatmentKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@SoiltreatmentKey", SqlDbType.Int).Value = _SoiltreatmentKey;
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
