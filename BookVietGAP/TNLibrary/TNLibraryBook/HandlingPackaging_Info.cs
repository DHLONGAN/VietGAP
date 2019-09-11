using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class HandlingPackaging_Info
    {

        #region [ Field Name ]
        private int _HandlingPackagingKey = 0;
        private DateTime _Datetime;
        private string _Type = "";
        private string _Place = "";
        private string _Treatment = "";
        private int _MemberKey = 0;
        private int _CooperativeKey = 0;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private bool _IsActive;
        private bool _IsSync;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public HandlingPackaging_Info()
        {
        }
        public HandlingPackaging_Info(int HandlingPackagingKey)
        {
            string zSQL = "SELECT * FROM PUL_HandlingPackaging WHERE HandlingPackagingKey = @HandlingPackagingKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@HandlingPackagingKey", SqlDbType.Int).Value = HandlingPackagingKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _HandlingPackagingKey = int.Parse(zReader["HandlingPackagingKey"].ToString());
                    if (zReader["Datetime"] != DBNull.Value)
                        _Datetime = (DateTime)zReader["Datetime"];
                    _Type = zReader["Type"].ToString();
                    _Place = zReader["Place"].ToString();
                    _Treatment = zReader["Treatment"].ToString();
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                    _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedBy"] != DBNull.Value)
                        _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    if (zReader["ModifiedBy"] != DBNull.Value)
                        _ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
                    if (zReader["ModifiedDateTime"] != DBNull.Value)
                        _ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
                    _IsActive = (bool)zReader["IsActive"];
                    _IsSync = (bool)zReader["IsSync"];
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int HandlingPackagingKey
        {
            get { return _HandlingPackagingKey; }
            set { _HandlingPackagingKey = value; }
        }
        public DateTime Datetime
        {
            get { return _Datetime; }
            set { _Datetime = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string Place
        {
            get { return _Place; }
            set { _Place = value; }
        }
        public string Treatment
        {
            get { return _Treatment; }
            set { _Treatment = value; }
        }
        public int MemberKey
        {
            get { return _MemberKey; }
            set { _MemberKey = value; }
        }
        public int CooperativeKey
        {
            get { return _CooperativeKey; }
            set { _CooperativeKey = value; }
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
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public bool IsSync
        {
            get { return _IsSync; }
            set { _IsSync = value; }
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
            string zSQL = "INSERT INTO PUL_HandlingPackaging ("
        + " Datetime ,Type ,Place ,Treatment ,MemberKey ,CooperativeKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ,IsActive ,IsSync ) "
         + " VALUES ( "
         + "@Datetime ,@Type ,@Place ,@Treatment ,@MemberKey ,@CooperativeKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ,@IsActive ,@IsSync ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@HandlingPackagingKey", SqlDbType.Int).Value = _HandlingPackagingKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = _Type;
                zCommand.Parameters.Add("@Place", SqlDbType.NVarChar).Value = _Place;
                zCommand.Parameters.Add("@Treatment", SqlDbType.NVarChar).Value = _Treatment;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
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
                zCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = _IsActive;
                zCommand.Parameters.Add("@IsSync", SqlDbType.Bit).Value = _IsSync;
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
            string zSQL = "UPDATE PUL_HandlingPackaging SET "
                        + " Datetime = @Datetime,"
                        + " Type = @Type,"
                        + " Place = @Place,"
                        + " Treatment = @Treatment,"
                        + " MemberKey = @MemberKey,"
                        + " CooperativeKey = @CooperativeKey,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime,"
                        + " IsActive = @IsActive,"
                        + " IsSync = @IsSync"
                       + " WHERE HandlingPackagingKey = @HandlingPackagingKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@HandlingPackagingKey", SqlDbType.Int).Value = _HandlingPackagingKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = _Type;
                zCommand.Parameters.Add("@Place", SqlDbType.NVarChar).Value = _Place;
                zCommand.Parameters.Add("@Treatment", SqlDbType.NVarChar).Value = _Treatment;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
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
                zCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = _IsActive;
                zCommand.Parameters.Add("@IsSync", SqlDbType.Bit).Value = _IsSync;
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
            if (_HandlingPackagingKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_HandlingPackaging WHERE HandlingPackagingKey = @HandlingPackagingKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@HandlingPackagingKey", SqlDbType.Int).Value = _HandlingPackagingKey;
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
