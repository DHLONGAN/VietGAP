using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
    public class ProcessPlantDetail_Info
    {

        #region [ Field Name ]
        private int _ProcessPlantDetailKey = 0;
        private int _ProcessPlantKey = 0;
        private int _ProcessPlantDetai_Type = 0;
        private int _DateNum = 0;
        private string _Description = "";
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public ProcessPlantDetail_Info()
        {
        }
        public ProcessPlantDetail_Info(int ProcessPlantDetailKey)
        {
            string zSQL = "SELECT * FROM PUL_ProcessPlantDetail WHERE ProcessPlantDetailKey = @ProcessPlantDetailKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = ProcessPlantDetailKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _ProcessPlantDetailKey = int.Parse(zReader["ProcessPlantDetailKey"].ToString());
                    _ProcessPlantKey = int.Parse(zReader["ProcessPlantKey"].ToString());
                    _ProcessPlantDetai_Type = int.Parse(zReader["ProcessPlantDetai_Type"].ToString());
                    _DateNum = int.Parse(zReader["DateNum"].ToString());
                    _Description = zReader["Description"].ToString();
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
        public int ProcessPlantDetailKey
        {
            get { return _ProcessPlantDetailKey; }
            set { _ProcessPlantDetailKey = value; }
        }
        public int ProcessPlantKey
        {
            get { return _ProcessPlantKey; }
            set { _ProcessPlantKey = value; }
        }
        public int ProcessPlantDetai_Type
        {
            get { return _ProcessPlantDetai_Type; }
            set { _ProcessPlantDetai_Type = value; }
        }
        public int DateNum
        {
            get { return _DateNum; }
            set { _DateNum = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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
            string zSQL = "INSERT INTO PUL_ProcessPlantDetail ("
        + " ProcessPlantKey ,ProcessPlantDetai_Type ,DateNum ,Description ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) OUTPUT [INSERTED].[ProcessPlantDetailKey]"
         + " VALUES ( "
         + "@ProcessPlantKey ,@ProcessPlantDetai_Type ,@DateNum ,@Description ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = _ProcessPlantDetailKey;
                zCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = _ProcessPlantKey;
                zCommand.Parameters.Add("@ProcessPlantDetai_Type", SqlDbType.Int).Value = _ProcessPlantDetai_Type;
                zCommand.Parameters.Add("@DateNum", SqlDbType.Int).Value = _DateNum;
                zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
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
                zResult = zCommand.ExecuteScalar().ToString();// ExecuteNonQuery().ToString();
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
            string zSQL = "UPDATE PUL_ProcessPlantDetail SET "
                        + " ProcessPlantKey = @ProcessPlantKey,"
                        + " ProcessPlantDetai_Type = @ProcessPlantDetai_Type,"
                        + " DateNum = @DateNum,"
                        + " Description = @Description,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
                       + " WHERE ProcessPlantDetailKey = @ProcessPlantDetailKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = _ProcessPlantDetailKey;
                zCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = _ProcessPlantKey;
                zCommand.Parameters.Add("@ProcessPlantDetai_Type", SqlDbType.Int).Value = _ProcessPlantDetai_Type;
                zCommand.Parameters.Add("@DateNum", SqlDbType.Int).Value = _DateNum;
                zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
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
            if (_ProcessPlantDetailKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_ProcessPlantDetail WHERE ProcessPlantDetailKey = @ProcessPlantDetailKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = _ProcessPlantDetailKey;
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
