using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
    public class Pesticide_Info
    {

        #region [ Field Name ]
        private int _PesticideKey = 0;
        private string _Trade_Name = "";
        private string _Crop_Name = "";
        private int _Common_Key = 0;
        private int _CompanyKey = 0;
        private int _CategoryKey = 0;
        private int _UsingStatus = 0;
        private string _Images = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Pesticide_Info()
        {
        }
        public Pesticide_Info(int PesticideKey)
        {
            string zSQL = "SELECT * FROM PUL_Pesticides WHERE PesticideKey = @PesticideKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = PesticideKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _PesticideKey = int.Parse(zReader["PesticideKey"].ToString());
                    _Trade_Name = zReader["Trade_Name"].ToString();
                    _Crop_Name = zReader["Crop_Name"].ToString();
                    _Common_Key = int.Parse(zReader["Common_Key"].ToString());
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
        public int PesticideKey
        {
            get { return _PesticideKey; }
            set { _PesticideKey = value; }
        }
        public string Trade_Name
        {
            get { return _Trade_Name; }
            set { _Trade_Name = value; }
        }
        public string Crop_Name
        {
            get { return _Crop_Name; }
            set { _Crop_Name = value; }
        }
        public int Common_Key
        {
            get { return _Common_Key; }
            set { _Common_Key = value; }
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
            string zSQL = "INSERT INTO PUL_Pesticides ("
        + " Trade_Name ,Crop_Name ,Common_Key ,CompanyKey ,CategoryKey ,UsingStatus ,Images ) "
         + " VALUES ( "
         + "@Trade_Name ,@Crop_Name ,@Common_Key ,@CompanyKey ,@CategoryKey ,@UsingStatus ,@Images ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Trade_Name", SqlDbType.NVarChar).Value = _Trade_Name;
                zCommand.Parameters.Add("@Crop_Name", SqlDbType.NText).Value = _Crop_Name;
                zCommand.Parameters.Add("@Common_Key", SqlDbType.Int).Value = _Common_Key;
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
            string zSQL = "UPDATE PUL_Pesticides SET "
                        + " Trade_Name = @Trade_Name,"
                        + " Crop_Name = @Crop_Name,"
                        + " Common_Key = @Common_Key,"
                        + " CompanyKey = @CompanyKey,"
                        + " CategoryKey = @CategoryKey,"
                        + " UsingStatus = @UsingStatus,"
                        + " Images = @Images"
                       + " WHERE PesticideKey = @PesticideKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Trade_Name", SqlDbType.NVarChar).Value = _Trade_Name;
                zCommand.Parameters.Add("@Crop_Name", SqlDbType.NText).Value = _Crop_Name;
                zCommand.Parameters.Add("@Common_Key", SqlDbType.Int).Value = _Common_Key;
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
            if (_PesticideKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Pesticides WHERE PesticideKey = @PesticideKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
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
