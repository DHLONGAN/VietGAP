using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
    public class Pollution_Info
    {

        #region [ Field Name ]
        private int _PollutionKey = 0;
        private string _PollutionName = "";
        private string _Notice = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Pollution_Info()
        {
        }
        public Pollution_Info(int PollutionKey)
        {
            string zSQL = "SELECT * FROM PUL_Pollution WHERE PollutionKey = @PollutionKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = PollutionKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _PollutionKey = int.Parse(zReader["PollutionKey"].ToString());
                    _PollutionName = zReader["PollutionName"].ToString();
                    _Notice = zReader["Notice"].ToString();
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int PollutionKey
        {
            get { return _PollutionKey; }
            set { _PollutionKey = value; }
        }
        public string PollutionName
        {
            get { return _PollutionName; }
            set { _PollutionName = value; }
        }
        public string Notice
        {
            get { return _Notice; }
            set { _Notice = value; }
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
            string zSQL = "INSERT INTO PUL_Pollution ("
        + " PollutionName ,Notice ) "
         + " VALUES ( "
         + "@PollutionName ,@Notice ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = _PollutionKey;
                zCommand.Parameters.Add("@PollutionName", SqlDbType.NVarChar).Value = _PollutionName;
                zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
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
            string zSQL = "UPDATE PUL_Pollution SET "
                        + " PollutionName = @PollutionName,"
                        + " Notice = @Notice"
                       + " WHERE PollutionKey = @PollutionKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = _PollutionKey;
                zCommand.Parameters.Add("@PollutionName", SqlDbType.NVarChar).Value = _PollutionName;
                zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
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
            if (_PollutionKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Pollution WHERE PollutionKey = @PollutionKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = _PollutionKey;
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
