using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
    public class ListCooperative_Info
    {

        #region [ Field Name ]
        private int _ID = 0;
        private int _CooperativeVenturesKey = 0;
        private int _Cooperative_Key = 0;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private Guid _ModifiedDateTime;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public ListCooperative_Info()
        {
        }
        public ListCooperative_Info(int ID)
        {
            string zSQL = "SELECT * FROM PUL_ListCooperative WHERE ID = @ID";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _ID = int.Parse(zReader["ID"].ToString());
                    _CooperativeVenturesKey = int.Parse(zReader["CooperativeVenturesKey"].ToString());
                    _Cooperative_Key = int.Parse(zReader["Cooperative_Key"].ToString());
                    _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    _ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
                    _ModifiedDateTime = Guid.Parse(zReader["ModifiedDateTime"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        
        public ListCooperative_Info(string CooperativeVenturesKey, string Cooperative_Key)
        {
            string zSQL = "SELECT * FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @CooperativeVenturesKey and Cooperative_Key = @Cooperative_Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@CooperativeVenturesKey", SqlDbType.Int).Value = CooperativeVenturesKey;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = Cooperative_Key;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _ID = int.Parse(zReader["ID"].ToString());
                    _CooperativeVenturesKey = int.Parse(zReader["CooperativeVenturesKey"].ToString());
                    _Cooperative_Key = int.Parse(zReader["Cooperative_Key"].ToString());
                    _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    _ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
                    _ModifiedDateTime = Guid.Parse(zReader["ModifiedDateTime"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int CooperativeVenturesKey
        {
            get { return _CooperativeVenturesKey; }
            set { _CooperativeVenturesKey = value; }
        }
        public int Cooperative_Key
        {
            get { return _Cooperative_Key; }
            set { _Cooperative_Key = value; }
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
        public Guid ModifiedDateTime
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
            string zSQL = "INSERT INTO PUL_ListCooperative ("
        + " CooperativeVenturesKey ,Cooperative_Key ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@CooperativeVenturesKey ,@Cooperative_Key ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
                zCommand.Parameters.Add("@CooperativeVenturesKey", SqlDbType.Int).Value = _CooperativeVenturesKey;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
                if (_CreatedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
                zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.UniqueIdentifier).Value = _ModifiedDateTime;
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
            string zSQL = "UPDATE PUL_ListCooperative SET "
                        + " CooperativeVenturesKey = @CooperativeVenturesKey,"
                        + " Cooperative_Key = @Cooperative_Key,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
                       + " WHERE ID = @ID";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
                zCommand.Parameters.Add("@CooperativeVenturesKey", SqlDbType.Int).Value = _CooperativeVenturesKey;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
                if (_CreatedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
                zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.UniqueIdentifier).Value = _ModifiedDateTime;
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
            if (_ID == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_ListCooperative WHERE ID = @ID";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
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
