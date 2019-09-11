using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
    public class CertifiedOrganization_Info
    {

        #region [ Field Name ]
        private int _CertifiedOrganization_Key = 0;
        private string _CertifiedOrganization_ID = "";
        private string _CertifiedOrganization_Name = "";
        private int _CertificationType_Key = 0;
        private string _Address = "";
        private string _Phone = "";
        private string _Fax = "";
        private string _Email = "";
        private string _Website = "";
        private string _Infrastructure = "";
        private string _Examination_Process = "";
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public CertifiedOrganization_Info()
        {
        }
        public CertifiedOrganization_Info(int CertifiedOrganization_Key)
        {
            string zSQL = "SELECT * FROM PUL_CertifiedOrganization WHERE CertifiedOrganization_Key = @CertifiedOrganization_Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CertifiedOrganization_Key", SqlDbType.Int).Value = CertifiedOrganization_Key;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _CertifiedOrganization_Key = int.Parse(zReader["CertifiedOrganization_Key"].ToString());
                    _CertifiedOrganization_ID = zReader["CertifiedOrganization_ID"].ToString();
                    _CertifiedOrganization_Name = zReader["CertifiedOrganization_Name"].ToString();
                    _CertificationType_Key = int.Parse(zReader["CertificationType_Key"].ToString());
                    _Address = zReader["Address"].ToString();
                    _Phone = zReader["Phone"].ToString();
                    _Fax = zReader["Fax"].ToString();
                    _Email = zReader["Email"].ToString();
                    _Website = zReader["Website"].ToString();
                    _Infrastructure = zReader["Infrastructure"].ToString();
                    _Examination_Process = zReader["Examination_Process"].ToString();
                    _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
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
        public int CertifiedOrganization_Key
        {
            get { return _CertifiedOrganization_Key; }
            set { _CertifiedOrganization_Key = value; }
        }
        public string CertifiedOrganization_ID
        {
            get { return _CertifiedOrganization_ID; }
            set { _CertifiedOrganization_ID = value; }
        }
        public string CertifiedOrganization_Name
        {
            get { return _CertifiedOrganization_Name; }
            set { _CertifiedOrganization_Name = value; }
        }
        public int CertificationType_Key
        {
            get { return _CertificationType_Key; }
            set { _CertificationType_Key = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string Website
        {
            get { return _Website; }
            set { _Website = value; }
        }
        public string Infrastructure
        {
            get { return _Infrastructure; }
            set { _Infrastructure = value; }
        }
        public string Examination_Process
        {
            get { return _Examination_Process; }
            set { _Examination_Process = value; }
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
            string zSQL = "INSERT INTO PUL_CertifiedOrganization ("
        + " CertifiedOrganization_ID ,CertifiedOrganization_Name ,CertificationType_Key ,Address ,Phone ,Fax ,Email ,Website ,Infrastructure ,Examination_Process ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@CertifiedOrganization_ID ,@CertifiedOrganization_Name ,@CertificationType_Key ,@Address ,@Phone ,@Fax ,@Email ,@Website ,@Infrastructure ,@Examination_Process ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CertifiedOrganization_Key", SqlDbType.Int).Value = _CertifiedOrganization_Key;
                zCommand.Parameters.Add("@CertifiedOrganization_ID", SqlDbType.NVarChar).Value = _CertifiedOrganization_ID;
                zCommand.Parameters.Add("@CertifiedOrganization_Name", SqlDbType.NVarChar).Value = _CertifiedOrganization_Name;
                zCommand.Parameters.Add("@CertificationType_Key", SqlDbType.Int).Value = _CertificationType_Key;
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
                zCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = _Phone;
                zCommand.Parameters.Add("@Fax", SqlDbType.NVarChar).Value = _Fax;
                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@Website", SqlDbType.NVarChar).Value = _Website;
                zCommand.Parameters.Add("@Infrastructure", SqlDbType.NVarChar).Value = _Infrastructure;
                zCommand.Parameters.Add("@Examination_Process", SqlDbType.NVarChar).Value = _Examination_Process;
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


        public string Update()
        {
            string zSQL = "UPDATE PUL_CertifiedOrganization SET "
                        + " CertifiedOrganization_ID = @CertifiedOrganization_ID,"
                        + " CertifiedOrganization_Name = @CertifiedOrganization_Name,"
                        + " CertificationType_Key = @CertificationType_Key,"
                        + " Address = @Address,"
                        + " Phone = @Phone,"
                        + " Fax = @Fax,"
                        + " Email = @Email,"
                        + " Website = @Website,"
                        + " Infrastructure = @Infrastructure,"
                        + " Examination_Process = @Examination_Process,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
                       + " WHERE CertifiedOrganization_Key = @CertifiedOrganization_Key";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CertifiedOrganization_Key", SqlDbType.Int).Value = _CertifiedOrganization_Key;
                zCommand.Parameters.Add("@CertifiedOrganization_ID", SqlDbType.NVarChar).Value = _CertifiedOrganization_ID;
                zCommand.Parameters.Add("@CertifiedOrganization_Name", SqlDbType.NVarChar).Value = _CertifiedOrganization_Name;
                zCommand.Parameters.Add("@CertificationType_Key", SqlDbType.Int).Value = _CertificationType_Key;
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
                zCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = _Phone;
                zCommand.Parameters.Add("@Fax", SqlDbType.NVarChar).Value = _Fax;
                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@Website", SqlDbType.NVarChar).Value = _Website;
                zCommand.Parameters.Add("@Infrastructure", SqlDbType.NVarChar).Value = _Infrastructure;
                zCommand.Parameters.Add("@Examination_Process", SqlDbType.NVarChar).Value = _Examination_Process;
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
            if (_CertifiedOrganization_Key == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_CertifiedOrganization WHERE CertifiedOrganization_Key = @CertifiedOrganization_Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CertifiedOrganization_Key", SqlDbType.Int).Value = _CertifiedOrganization_Key;
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
