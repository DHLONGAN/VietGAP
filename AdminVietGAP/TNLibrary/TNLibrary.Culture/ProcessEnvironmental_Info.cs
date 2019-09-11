using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
    public class ProcessEnvironmental_Info
    {

        #region [ Field Name ]
        private int _ProcessEnvironmentalKey = 0;
        private DateTime _ProcessDate;
        private int _PollutionKey = 0;
        private bool _Status;
        private string _Solution = "";
        private int _CooperativeKey = 0;
        private int _MemberKey = 0;
        private int _AssessmentKey = 0;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public ProcessEnvironmental_Info()
        {
        }
        public ProcessEnvironmental_Info(int ProcessEnvironmentalKey)
        {
            string zSQL = "SELECT * FROM PUL_ProcessEnvironmental WHERE ProcessEnvironmentalKey = @ProcessEnvironmentalKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ProcessEnvironmentalKey", SqlDbType.Int).Value = ProcessEnvironmentalKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _ProcessEnvironmentalKey = int.Parse(zReader["ProcessEnvironmentalKey"].ToString());
                    if (zReader["ProcessDate"] != DBNull.Value)
                        _ProcessDate = (DateTime)zReader["ProcessDate"];
                    _PollutionKey = int.Parse(zReader["PollutionKey"].ToString());
                    _Status = (bool)zReader["Status"];
                    _Solution = zReader["Solution"].ToString();
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _AssessmentKey = int.Parse(zReader["AssessmentKey"].ToString());
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
        public int ProcessEnvironmentalKey
        {
            get { return _ProcessEnvironmentalKey; }
            set { _ProcessEnvironmentalKey = value; }
        }
        public DateTime ProcessDate
        {
            get { return _ProcessDate; }
            set { _ProcessDate = value; }
        }
        public int PollutionKey
        {
            get { return _PollutionKey; }
            set { _PollutionKey = value; }
        }
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Solution
        {
            get { return _Solution; }
            set { _Solution = value; }
        }
        public int CooperativeKey
        {
            get { return _CooperativeKey; }
            set { _CooperativeKey = value; }
        }
        public int MemberKey
        {
            get { return _MemberKey; }
            set { _MemberKey = value; }
        }
        public int AssessmentKey
        {
            get { return _AssessmentKey; }
            set { _AssessmentKey = value; }
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
            string zSQL = "INSERT INTO PUL_ProcessEnvironmental ("
        + " ProcessDate ,PollutionKey ,Status ,Solution ,CooperativeKey ,MemberKey ,AssessmentKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@ProcessDate ,@PollutionKey ,@Status ,@Solution ,@CooperativeKey ,@MemberKey ,@AssessmentKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ProcessEnvironmentalKey", SqlDbType.Int).Value = _ProcessEnvironmentalKey;
                if (_ProcessDate.Year == 0001)
                    zCommand.Parameters.Add("@ProcessDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ProcessDate", SqlDbType.DateTime).Value = _ProcessDate;
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = _PollutionKey;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@AssessmentKey", SqlDbType.Int).Value = _AssessmentKey;
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
            string zSQL = "UPDATE PUL_ProcessEnvironmental SET "
                        + " ProcessDate = @ProcessDate,"
                        + " PollutionKey = @PollutionKey,"
                        + " Status = @Status,"
                        + " Solution = @Solution,"
                        + " CooperativeKey = @CooperativeKey,"
                        + " MemberKey = @MemberKey,"
                        + " AssessmentKey = @AssessmentKey,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
                       + " WHERE ProcessEnvironmentalKey = @ProcessEnvironmentalKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ProcessEnvironmentalKey", SqlDbType.Int).Value = _ProcessEnvironmentalKey;
                if (_ProcessDate.Year == 0001)
                    zCommand.Parameters.Add("@ProcessDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ProcessDate", SqlDbType.DateTime).Value = _ProcessDate;
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = _PollutionKey;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@AssessmentKey", SqlDbType.Int).Value = _AssessmentKey;
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
            if (_ProcessEnvironmentalKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_ProcessEnvironmental WHERE ProcessEnvironmentalKey = @ProcessEnvironmentalKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@ProcessEnvironmentalKey", SqlDbType.Int).Value = _ProcessEnvironmentalKey;
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
