using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class Seeds_Companie_Info
    {

        #region [ Field Name ]
        private int _CompanyKey = 0;
        private string _CompanyName = "";
        private int _Parent = 0;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Seeds_Companie_Info()
        {
        }
        public Seeds_Companie_Info(int CooperativeKey)
        {
            string zSQL = "SELECT * FROM PUL_Seeds_Companies WHERE CompanyKey = @CompanyKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = CooperativeKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _CompanyKey = int.Parse(zReader["CompanyKey"].ToString());
                    _CompanyName = zReader["CompanyName"].ToString();
                    _Parent = int.Parse(zReader["Parent"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT * FROM PUL_Seeds_Companies ORDER BY CompanyName ";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetListtop50()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT top (50) * FROM PUL_Seeds_Companies ";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        #endregion

        #region [ Properties ]
        public int CompanyKey
        {
            get { return _CompanyKey; }
            set { _CompanyKey = value; }
        }
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        public int Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
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
            string zSQL = "INSERT INTO PUL_Seeds_Companies ("
        + " CompanyName ,Parent ) "
         + " VALUES ( "
         + "@CompanyName ,@Parent ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = _CompanyName;
                zCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = _Parent;
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
            string zSQL = "UPDATE PUL_Seeds_Companies SET "
                        + " CompanyName = @CompanyName,"
                        + " Parent = @Parent"
                       + " WHERE CompanyKey = @CompanyKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = _CompanyName;
                zCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = _Parent;
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
            if (_CompanyKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Seeds_Companies WHERE CompanyKey = @CompanyKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
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
