using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
    public class Role_Info
    {

        #region [ Field Name ]
        private Guid _RoleKey;
        private string _RoleID = "";
        private string _RoleName = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Role_Info()
        {
        }
        public Role_Info(string RoleKey)
        {
            string zSQL = "SELECT * FROM SYS_Roles WHERE RoleKey = @RoleKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = new Guid(RoleKey);
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _RoleKey = Guid.Parse(zReader["RoleKey"].ToString());
                    _RoleID = zReader["RoleID"].ToString();
                    _RoleName = zReader["RoleName"].ToString();
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public Guid RoleKey
        {
            get { return _RoleKey; }
            set { _RoleKey = value; }
        }
        public string RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
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
            string zSQL = "INSERT INTO SYS_Roles ("
        + " RoleID ,RoleName ) "
         + " VALUES ( "
         + "@RoleID ,@RoleName ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = _RoleKey;
                zCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar).Value = _RoleID;
                zCommand.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = _RoleName;
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
            string zSQL = "UPDATE SYS_Roles SET "
                        + " RoleID = @RoleID,"
                        + " RoleName = @RoleName"
                       + " WHERE RoleKey = @RoleKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = _RoleKey;
                zCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar).Value = _RoleID;
                zCommand.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = _RoleName;
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
            if (_RoleKey.ToString() == "00000000-0000-0000-0000-000000000000")
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM SYS_Roles WHERE RoleKey = @RoleKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@RoleKey", SqlDbType.UniqueIdentifier).Value = _RoleKey;
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
