using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class LandUse_Info
    {

        #region [ Field Name ]
        private int _LandUseKey = 0;
        private string _Action = "";
        private string _Reason = "";
        private string _Solution = "";
        private string _Note = "";
        private int _MemberKey = 0;
        private int _SeedKey = 0;
        private DateTime _Datetime;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private bool _IsActive;
        private bool _IsSync;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public LandUse_Info()
        {
        }
        public LandUse_Info(int LandUseKey)
        {
            string zSQL = "SELECT * FROM PUL_LandUse WHERE LandUseKey = @LandUseKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@LandUseKey", SqlDbType.Int).Value = LandUseKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _LandUseKey = int.Parse(zReader["LandUseKey"].ToString());
                    _Action = zReader["Action"].ToString();
                    _Reason = zReader["Reason"].ToString();
                    _Solution = zReader["Solution"].ToString();
                    _Note = zReader["Note"].ToString();
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _SeedKey = int.Parse(zReader["SeedKey"].ToString());
                    if (zReader["Datetime"] != DBNull.Value)
                        _Datetime = (DateTime)zReader["Datetime"];
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
        public int LandUseKey
        {
            get { return _LandUseKey; }
            set { _LandUseKey = value; }
        }
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }
        public string Solution
        {
            get { return _Solution; }
            set { _Solution = value; }
        }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
        public int MemberKey
        {
            get { return _MemberKey; }
            set { _MemberKey = value; }
        }
        public int SeedKey
        {
            get { return _SeedKey; }
            set { _SeedKey = value; }
        }
        public DateTime Datetime
        {
            get { return _Datetime; }
            set { _Datetime = value; }
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
            string zSQL = "INSERT INTO PUL_LandUse ("
        + " Action ,Reason ,Solution ,Note ,MemberKey ,SeedKey ,Datetime ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ,IsActive ,IsSync ) "
         + " VALUES ( "
         + "@Action ,@Reason ,@Solution ,@Note ,@MemberKey ,@SeedKey ,@Datetime ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ,@IsActive ,@IsSync ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@LandUseKey", SqlDbType.Int).Value = _LandUseKey;
                zCommand.Parameters.Add("@Action", SqlDbType.NVarChar).Value = _Action;
                zCommand.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = _Reason;
                zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
                zCommand.Parameters.Add("@Note", SqlDbType.NVarChar).Value = _Note;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
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
            string zSQL = "UPDATE PUL_LandUse SET "
                        + " Action = @Action,"
                        + " Reason = @Reason,"
                        + " Solution = @Solution,"
                        + " Note = @Note,"
                        + " MemberKey = @MemberKey,"
                        + " SeedKey = @SeedKey,"
                        + " Datetime = @Datetime,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime,"
                        + " IsActive = @IsActive,"
                        + " IsSync = @IsSync"
                       + " WHERE LandUseKey = @LandUseKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@LandUseKey", SqlDbType.Int).Value = _LandUseKey;
                zCommand.Parameters.Add("@Action", SqlDbType.NVarChar).Value = _Action;
                zCommand.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = _Reason;
                zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
                zCommand.Parameters.Add("@Note", SqlDbType.NVarChar).Value = _Note;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
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
            if (_LandUseKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_LandUse WHERE LandUseKey = @LandUseKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@LandUseKey", SqlDbType.Int).Value = _LandUseKey;
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
