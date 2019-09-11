using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
    public class Companie_Info
    {

        #region [ Field Name ]
        private int _CompanyKey = 0;
        private string _CompanyName = "";
        private string _Address = "";
        private string _City = "";
        private int _Country = 0;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Companie_Info()
        {
        }
        public Companie_Info(int CompanyKey)
        {
            string zSQL = "SELECT * FROM PUL_Companies WHERE CompanyKey = @CompanyKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = CompanyKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _CompanyKey = int.Parse(zReader["CompanyKey"].ToString());
                    _CompanyName = zReader["CompanyName"].ToString();
                    _Address = zReader["Address"].ToString();
                    _City = zReader["City"].ToString();
                    _Country = int.Parse(zReader["Country"].ToString());
                    if (zReader["CreatedBy"] != DBNull.Value)
                    _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    if (zReader["ModifiedBy"] != DBNull.Value)
                    _ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
                    if (zReader["ModifiedDateTime"] != DBNull.Value)
                        _ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
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
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public int Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public Guid CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public Guid ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
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
            string zSQL = "INSERT INTO PUL_Companies ("
        + " CompanyName ,Address ,City ,Country)"// ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@CompanyName ,@Address ,@City ,@Country)";// ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
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
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
                zCommand.Parameters.Add("@City", SqlDbType.NVarChar).Value = _City;
                zCommand.Parameters.Add("@Country", SqlDbType.Int).Value = _Country;
                //zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
                //if (_CreatedDateTime.Year == 0001)
                //    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                //else
                //    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                //zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
                //if (_ModifiedDateTime.Year == 0001)
                //    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                //else
                //    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
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
            string zSQL = "UPDATE PUL_Companies SET "
                        + " CompanyName = @CompanyName,"
                        + " Address = @Address,"
                        + " City = @City,"
                        + " Country = @Country,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
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
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
                zCommand.Parameters.Add("@City", SqlDbType.NVarChar).Value = _City;
                zCommand.Parameters.Add("@Country", SqlDbType.Int).Value = _Country;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
                if (_CreatedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
                if (_ModifiedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
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
            string zSQL = "DELETE FROM PUL_Companies WHERE CompanyKey = @CompanyKey";
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
