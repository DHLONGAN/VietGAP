using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
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
        + " Datetime ,Type ,Place ,Treatment ,MemberKey ,CooperativeKey ) "
         + " VALUES ( "
         + "@Datetime ,@Type ,@Place ,@Treatment ,@MemberKey ,@CooperativeKey ) ";
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
                        + " CooperativeKey = @CooperativeKey"
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
