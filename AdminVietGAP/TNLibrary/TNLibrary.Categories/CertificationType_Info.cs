using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
using TNConfig;
using TNConfig;
namespace TNLibrary.Categories
{
    public class CertificationType_Info
    {

        #region [ Field Name ]
        private int _CertificationType_Key = 0;
        private string _CertificationType_ID = "";
        private string _CertificationType_Name = "";
        private string _Description = "";
        private string _CreatedBy = "";
        private DateTime _CreatedDateTime;
        private string _ModifiedBy = "";
        private DateTime _ModifiedDateTime;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public CertificationType_Info()
        {
        }
        public CertificationType_Info(int CertificationType_Key)
        {
            string zSQL = "SELECT * FROM PUL_CertificationType WHERE CertificationType_Key = @CertificationType_Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CertificationType_Key", SqlDbType.Int).Value = CertificationType_Key;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _CertificationType_Key = int.Parse(zReader["CertificationType_Key"].ToString());
                    _CertificationType_ID = zReader["CertificationType_ID"].ToString();
                    _CertificationType_Name = zReader["CertificationType_Name"].ToString();
                    _Description = zReader["Description"].ToString();
                    _CreatedBy = zReader["CreatedBy"].ToString();
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    _ModifiedBy = zReader["ModifiedBy"].ToString();
                    if (zReader["ModifiedDateTime"] != DBNull.Value)
                        _ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int CertificationType_Key
        {
            get { return _CertificationType_Key; }
            set { _CertificationType_Key = value; }
        }
        public string CertificationType_ID
        {
            get { return _CertificationType_ID; }
            set { _CertificationType_ID = value; }
        }
        public string CertificationType_Name
        {
            get { return _CertificationType_Name; }
            set { _CertificationType_Name = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public string ModifiedBy
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
            string zSQL = "INSERT INTO PUL_CertificationType ("
        + " CertificationType_ID ,CertificationType_Name ,Description ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@CertificationType_ID ,@CertificationType_Name ,@Description ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CertificationType_Key", SqlDbType.Int).Value = _CertificationType_Key;
                zCommand.Parameters.Add("@CertificationType_ID", SqlDbType.NVarChar).Value = _CertificationType_ID;
                zCommand.Parameters.Add("@CertificationType_Name", SqlDbType.NVarChar).Value = _CertificationType_Name;
                zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = _CreatedBy;
                if (_CreatedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = _ModifiedBy;
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


        public string Update()
        {
            string zSQL = "UPDATE PUL_CertificationType SET "
                        + " CertificationType_ID = @CertificationType_ID,"
                        + " CertificationType_Name = @CertificationType_Name,"
                        + " Description = @Description,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
                       + " WHERE CertificationType_Key = @CertificationType_Key";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CertificationType_Key", SqlDbType.Int).Value = _CertificationType_Key;
                zCommand.Parameters.Add("@CertificationType_ID", SqlDbType.NVarChar).Value = _CertificationType_ID;
                zCommand.Parameters.Add("@CertificationType_Name", SqlDbType.NVarChar).Value = _CertificationType_Name;
                zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = _CreatedBy;
                if (_CreatedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = _ModifiedBy;
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
            if (_CertificationType_Key == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_CertificationType WHERE CertificationType_Key = @CertificationType_Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CertificationType_Key", SqlDbType.Int).Value = _CertificationType_Key;
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
