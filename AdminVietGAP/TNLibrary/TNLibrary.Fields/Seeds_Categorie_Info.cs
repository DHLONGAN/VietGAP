using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Fields
{
    public class Seeds_Categorie_Info
    {

        #region [ Field Name ]
        private int _CategoryKey = 0;
        private string _CategoryName = "";
        private int _Parent = 0;
        private string _Color = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Seeds_Categorie_Info()
        {
        }
        public Seeds_Categorie_Info(int CategoryKey)
        {
            string zSQL = "SELECT * FROM PUL_Seeds_Categories WHERE CategoryKey = @CategoryKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = CategoryKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _CategoryKey = int.Parse(zReader["CategoryKey"].ToString());
                    _CategoryName = zReader["CategoryName"].ToString();
                    _Parent = int.Parse(zReader["Parent"].ToString());
                    _Color = zReader["Color"].ToString();
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Seeds_Categories ";
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
        public int CategoryKey
        {
            get { return _CategoryKey; }
            set { _CategoryKey = value; }
        }
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        public int Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
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
            string zSQL = "INSERT INTO PUL_Seeds_Categories ("
        + " CategoryName ,Parent ,Color ) "
         + " VALUES ( "
         + "@CategoryName ,@Parent ,@Color ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = _CategoryKey;
                zCommand.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = _CategoryName;
                zCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = _Parent;
                zCommand.Parameters.Add("@Color", SqlDbType.NVarChar).Value = _Color;
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
            string zSQL = "UPDATE PUL_Seeds_Categories SET "
                        + " CategoryName = @CategoryName,"
                        + " Parent = @Parent,"
                        + " Color = @Color"
                       + " WHERE CategoryKey = @CategoryKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = _CategoryKey;
                zCommand.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = _CategoryName;
                zCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = _Parent;
                zCommand.Parameters.Add("@Color", SqlDbType.NVarChar).Value = _Color;
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
            if (_CategoryKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Seeds_Categories WHERE CategoryKey = @CategoryKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = _CategoryKey;
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
