using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
    public class Equipment_Info
    {

        #region [ Field Name ]
        private int _EquipmentKey = 0;
        private string _EquipmentName = "";
        private string _Notice = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Equipment_Info()
        {
        }
        public Equipment_Info(int EquipmentKey)
        {
            string zSQL = "SELECT * FROM PUL_Equipment WHERE EquipmentKey = @EquipmentKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = EquipmentKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _EquipmentKey = int.Parse(zReader["EquipmentKey"].ToString());
                    _EquipmentName = zReader["EquipmentName"].ToString();
                    _Notice = zReader["Notice"].ToString();
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int EquipmentKey
        {
            get { return _EquipmentKey; }
            set { _EquipmentKey = value; }
        }
        public string EquipmentName
        {
            get { return _EquipmentName; }
            set { _EquipmentName = value; }
        }
        public string Notice
        {
            get { return _Notice; }
            set { _Notice = value; }
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
            string zSQL = "INSERT INTO PUL_Equipment ("
        + " EquipmentName ,Notice ) "
         + " VALUES ( "
         + "@EquipmentName ,@Notice ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
                zCommand.Parameters.Add("@EquipmentName", SqlDbType.NVarChar).Value = _EquipmentName;
                zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
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
            string zSQL = "UPDATE PUL_Equipment SET "
                        + " EquipmentName = @EquipmentName,"
                        + " Notice = @Notice"
                       + " WHERE EquipmentKey = @EquipmentKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
                zCommand.Parameters.Add("@EquipmentName", SqlDbType.NVarChar).Value = _EquipmentName;
                zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
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
            if (_EquipmentKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Equipment WHERE EquipmentKey = @EquipmentKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
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
