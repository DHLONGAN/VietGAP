using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using TNConfig;

namespace TNLibrary.SYS.Users
{
    public class User_Role_Info
    {
        private string m_UserKey = "";
        private string m_UserName = "";

        private string m_RoleKey = "";
        private string m_RoleID = "";
        private string m_RoleName = "";

        private bool m_RoleRead;
        private bool m_RoleAdd;
        private bool m_RoleEdit;
        private bool m_RoleDel;

        private string m_Message = "";
        public User_Role_Info()
        {
        }
        #region [ Constructor Get Information ]
        public User_Role_Info(string UserKey)
        {
            m_UserKey = UserKey;
        }

        public User_Role_Info(string UserKey, string RoleKey)
        {
            string nSQL = " SELECT A.*,B.RoleID,B.RoleName,C.UserName FROM SYS_Users_Roles A"
                        + " LEFT JOIN SYS_Roles B ON A.RoleKey = B.RoleKey "
                        + " LEFT JOIN SYS_Users C ON A.UserKey = C.UserKey "
                        + " WHERE A.UserKey = @UserKey AND A.RoleKey = @RoleKey";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = UserKey;
                nCommand.Parameters.Add("@RoleKey", SqlDbType.NVarChar).Value = RoleKey;
                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_UserKey = nReader["UserKey"].ToString();
                    m_UserName = nReader["UserName"].ToString();

                    m_RoleKey = nReader["RoleKey"].ToString();
                    m_RoleID = nReader["RoleID"].ToString();
                    m_RoleName = nReader["RoleName"].ToString();

                    m_RoleRead = (bool)nReader["RoleRead"];
                    m_RoleAdd = (bool)nReader["RoleAdd"];
                    m_RoleEdit = (bool)nReader["RoleEdit"];
                    m_RoleDel = (bool)nReader["RoleDel"];
                }
                nReader.Close();
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }

        }

        public void Get_Role_Info(string UserName, string RoleID)
        {
            string nSQL = " SELECT * FROM SYS_Users_Roles A"
                        + " LEFT JOIN SYS_Roles B ON A.RoleKey = B.RoleKey "
                        + " LEFT JOIN SYS_Users C ON A.UserKey = C.UserKey "
                        + " WHERE C.UserName= @Username AND B.RoleID = @RoleID";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                nCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar).Value = RoleID;
                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_UserKey = nReader["UserKey"].ToString();
                    m_UserName = nReader["UserName"].ToString();

                    m_RoleKey = nReader["RoleKey"].ToString();
                    m_RoleID = nReader["RoleID"].ToString();
                    m_RoleName = nReader["RoleName"].ToString();

                    m_RoleRead = (bool)nReader["RoleRead"];
                    m_RoleAdd = (bool)nReader["RoleAdd"];
                    m_RoleEdit = (bool)nReader["RoleEdit"];
                    m_RoleDel = (bool)nReader["RoleDel"];
                }
                else
                {
                    //nReader.Read();
                    //m_UserKey = nReader["UserKey"].ToString();
                    //m_UserName = nReader["UserName"].ToString();

                    //m_RoleKey = nReader["RoleKey"].ToString();
                    //m_RoleID = nReader["RoleID"].ToString();
                    //m_RoleName = nReader["RoleName"].ToString();

                    m_RoleRead = false;
                    m_RoleAdd = false;
                    m_RoleEdit = false;
                    m_RoleDel = false;
                }
                nReader.Close();
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }

        }
        public void Check_Role(string RoleID)
        {
            string nSQL = " SELECT * FROM SYS_Users_Roles A"
                        + " INNER JOIN SYS_Roles B ON A.RoleKey = B.RoleKey "
                        + " WHERE A.UserKey = @UserKey AND B.RoleID = @RoleID";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);

            try
            {
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
                nCommand.Parameters.Add("@RoleID", SqlDbType.NVarChar).Value = RoleID;
                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_UserKey = nReader["UserKey"].ToString();
                    m_RoleKey = nReader["RoleKey"].ToString();
                    m_RoleID = nReader["RoleID"].ToString();
                    m_RoleName = nReader["RoleName"].ToString();

                    m_RoleRead = (bool)nReader["RoleRead"];
                    m_RoleAdd = (bool)nReader["RoleAdd"];
                    m_RoleEdit = (bool)nReader["RoleEdit"];
                    m_RoleDel = (bool)nReader["RoleDel"];
                }
                else
                {
                    m_RoleRead = false;
                    m_RoleAdd = false;
                    m_RoleEdit = false;
                    m_RoleDel = false;
                }
                nReader.Close();
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }

        }
        #endregion

        #region [Properties ]

        public string UserKey
        {
            set { m_UserKey = value; }
            get { return m_UserKey; }
        }
        public string UserName
        {
            set { m_UserName = value; }
            get { return m_UserName; }
        }
        public string Key
        {
            set
            {
                m_RoleKey = value;
            }
            get { return m_RoleKey; }
        }
        public string ID
        {
            set
            {
                 m_RoleID = value;
            }
            get { return m_RoleID; }
        }
        public string Name
        {
            set { m_RoleName = value; }
            get { return m_RoleName; }
        }

        public bool Read
        {
            set { m_RoleRead = value; }
            get { return m_RoleRead; }
        }
        public bool Add
        {
            set { m_RoleAdd = value; }
            get { return m_RoleAdd; }
        }

        public bool Edit
        {
            set { m_RoleEdit = value; }
            get { return m_RoleEdit; }
        }
        public bool Del
        {
            set { m_RoleDel = value; }
            get { return m_RoleDel; }
        }


        public string Message
        {
            set { m_Message = value; }
            get { return m_Message; }
        }
        #endregion

        #region [ Constructor Update Information ]

        public string Update()
        {
            string nResult = ""; ;

            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE SYS_Users_Roles SET "

                        + " RoleRead = @RoleRead,"
                        + " RoleAdd = @RoleAdd,"
                        + " RoleEdit = @RoleEdit,"
                        + " RoleDel = @RoleDel"

                        + " WHERE UserKey = @UserKey AND RoleKey = @RoleKey";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
                nCommand.Parameters.Add("@RoleKey", SqlDbType.NVarChar).Value = m_RoleKey;

                nCommand.Parameters.Add("@RoleRead", SqlDbType.Bit).Value = m_RoleRead;
                nCommand.Parameters.Add("@RoleAdd", SqlDbType.Bit).Value = m_RoleAdd;
                nCommand.Parameters.Add("@RoleEdit", SqlDbType.Bit).Value = m_RoleEdit;
                nCommand.Parameters.Add("@RoleDel", SqlDbType.Bit).Value = m_RoleDel;

                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();
            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        public string Create()
        {

            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "INSERT INTO SYS_Users_Roles( "
                        + " UserKey, RoleKey, RoleRead,RoleAdd, RoleEdit,RoleDel)"
                        + " VALUES(@UserKey, @RoleKey, @RoleRead,@RoleAdd, @RoleEdit,@RoleDel)";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {

                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
                nCommand.Parameters.Add("@RoleKey", SqlDbType.NVarChar).Value = m_RoleKey;

                nCommand.Parameters.Add("@RoleRead", SqlDbType.Bit).Value = m_RoleRead;
                nCommand.Parameters.Add("@RoleAdd", SqlDbType.Bit).Value = m_RoleAdd;
                nCommand.Parameters.Add("@RoleEdit", SqlDbType.Bit).Value = m_RoleEdit;
                nCommand.Parameters.Add("@RoleDel", SqlDbType.Bit).Value = m_RoleDel;


                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        public string Delete()
        {
            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "DELETE FROM SYS_Users_Roles WHERE UserKey = @UserKey AND RoleKey = @RoleKey";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
                nCommand.Parameters.Add("@RoleKey", SqlDbType.NVarChar).Value = m_RoleKey;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();
            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }

        #endregion

    }
}
