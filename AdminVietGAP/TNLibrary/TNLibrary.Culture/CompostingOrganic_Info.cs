using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class CompostingOrganic_Info
    {

        #region [ Field Name ]
        private int _CompostingKey = 0;
        private int _FertilizerOrganicKey = 0;
        private float _Quantity;
        private int _UnitKey = 0;
        private string _Method = "";
        private int _CompostingDates = 0;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private int _MemberKey = 0;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private bool _IsActive;
        private bool _IsSync;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public CompostingOrganic_Info()
        {
        }
        public CompostingOrganic_Info(int CompostingKey)
        {
            string zSQL = "SELECT * FROM PUL_CompostingOrganic WHERE CompostingKey = @CompostingKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CompostingKey", SqlDbType.Int).Value = CompostingKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _CompostingKey = int.Parse(zReader["CompostingKey"].ToString());
                    _FertilizerOrganicKey = int.Parse(zReader["FertilizerOrganicKey"].ToString());
                    _Quantity = float.Parse(zReader["Quantity"].ToString());
                    _UnitKey = int.Parse(zReader["UnitKey"].ToString());
                    _Method = zReader["Method"].ToString();
                    _CompostingDates = int.Parse(zReader["CompostingDates"].ToString());
                    if (zReader["StartDate"] != DBNull.Value)
                        _StartDate = (DateTime)zReader["StartDate"];
                    if (zReader["EndDate"] != DBNull.Value)
                        _EndDate = (DateTime)zReader["EndDate"];
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
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
        public int CompostingKey
        {
            get { return _CompostingKey; }
            set { _CompostingKey = value; }
        }
        public int FertilizerOrganicKey
        {
            get { return _FertilizerOrganicKey; }
            set { _FertilizerOrganicKey = value; }
        }
        public float Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public int UnitKey
        {
            get { return _UnitKey; }
            set { _UnitKey = value; }
        }
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        public int CompostingDates
        {
            get { return _CompostingDates; }
            set { _CompostingDates = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        public int MemberKey
        {
            get { return _MemberKey; }
            set { _MemberKey = value; }
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
            string zSQL = "INSERT INTO PUL_CompostingOrganic ("
        + " FertilizerOrganicKey ,Quantity ,UnitKey ,Method ,CompostingDates ,StartDate ,EndDate ,MemberKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ,IsActive ,IsSync ) "
         + " VALUES ( "
         + "@FertilizerOrganicKey ,@Quantity ,@UnitKey ,@Method ,@CompostingDates ,@StartDate ,@EndDate ,@MemberKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ,@IsActive ,@IsSync ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CompostingKey", SqlDbType.Int).Value = _CompostingKey;
                zCommand.Parameters.Add("@FertilizerOrganicKey", SqlDbType.Int).Value = _FertilizerOrganicKey;
                zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
                zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
                zCommand.Parameters.Add("@Method", SqlDbType.NVarChar).Value = _Method;
                zCommand.Parameters.Add("@CompostingDates", SqlDbType.Int).Value = _CompostingDates;
                if (_StartDate.Year == 0001)
                    zCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = _StartDate;
                if (_EndDate.Year == 0001)
                    zCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = _EndDate;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
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
            string zSQL = "UPDATE PUL_CompostingOrganic SET "
                        + " FertilizerOrganicKey = @FertilizerOrganicKey,"
                        + " Quantity = @Quantity,"
                        + " UnitKey = @UnitKey,"
                        + " Method = @Method,"
                        + " CompostingDates = @CompostingDates,"
                        + " StartDate = @StartDate,"
                        + " EndDate = @EndDate,"
                        + " MemberKey = @MemberKey,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime,"
                        + " IsActive = @IsActive,"
                        + " IsSync = @IsSync"
                       + " WHERE CompostingKey = @CompostingKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CompostingKey", SqlDbType.Int).Value = _CompostingKey;
                zCommand.Parameters.Add("@FertilizerOrganicKey", SqlDbType.Int).Value = _FertilizerOrganicKey;
                zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
                zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
                zCommand.Parameters.Add("@Method", SqlDbType.NVarChar).Value = _Method;
                zCommand.Parameters.Add("@CompostingDates", SqlDbType.Int).Value = _CompostingDates;
                if (_StartDate.Year == 0001)
                    zCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = _StartDate;
                if (_EndDate.Year == 0001)
                    zCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = _EndDate;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
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
            if (_CompostingKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_CompostingOrganic WHERE CompostingKey = @CompostingKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CompostingKey", SqlDbType.Int).Value = _CompostingKey;
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
