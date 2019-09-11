using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using TNConfig;

namespace TNLibrary.SYS.Users
{
    public class User_Info
    {
        private string m_UserKey = "";
        private string m_UserName = "";
        private string m_Password = "";
        private string m_Description = "";

        private bool m_Activate;
        private DateTime m_ExpireDate = DateTime.Now.AddYears(1);
        private DateTime m_LastLoginDate = DateTime.Now;
        private int m_FailedPasswordAttemptCount = 0;

        private int m_EmployeeKey = 0;
        private string m_EmployeeName = "";
        private int m_BranchKey = 0;
        private string m_BranchName = "";

        private string m_CreatedBy = "";
        private DateTime m_CreatedDateTime;
        private string m_ModifiedBy = "";
        private DateTime m_ModifiedDateTime;
        private int m_GroupKey = 0;
        private string m_Message = "";
        private string m_Cookies = "";
        
        public User_Info()
        {
        }
        #region [ Constructor Get Information ]

        public User_Info(string UserKey)
        {
            string nSQL = " SELECT * FROM SYS_Users WHERE UserKey = @UserKey";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@UserKey", SqlDbType.UniqueIdentifier).Value = new Guid(UserKey);

                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_UserKey = nReader["UserKey"].ToString();
                    m_GroupKey = (int)nReader["GroupKey"];
                    m_Password = nReader["Password"].ToString();
                    m_UserName = nReader["UserName"].ToString();
                    m_Description = nReader["Description"].ToString();
                    m_Activate = (bool)nReader["Activate"];
                    m_ExpireDate = (DateTime)nReader["ExpireDate"];
                    m_CooperativeKey = (int)nReader["CooperativeKey"];

                    m_EmployeeKey = (int)nReader["EmployeeKey"];
                    m_EmployeeName = nReader["EmployeeName"].ToString();


                    if (nReader["LastLoginDate"] != DBNull.Value)
                        m_LastLoginDate = (DateTime)nReader["LastLoginDate"];
                    m_FailedPasswordAttemptCount = (int)nReader["FailedPasswordAttemptCount"];

                    m_CreatedBy = nReader["CreatedBy"].ToString();
                    if (nReader["CreatedDateTime"] != DBNull.Value)
                        m_CreatedDateTime = (DateTime)nReader["CreatedDateTime"];

                    m_ModifiedBy = nReader["ModifiedBy"].ToString();
                    if (nReader["ModifiedDateTime"] != DBNull.Value)
                        m_ModifiedDateTime = (DateTime)nReader["ModifiedDateTime"];
                    if (nReader["Cookies"] != DBNull.Value)
                        m_Cookies = (string)nReader["Cookies"];
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



        public User_Info(int EmployeeKey)
        {
            string nSQL = @"SELECT     dbo.SYS_Users.UserKey, dbo.SYS_Users.UserID, dbo.SYS_Users.UserName,dbo.SYS_Users.GroupKey,dbo.SYS_Users.CooperativeKey,dbo.SYS_Users.EmployeeKey, dbo.SYS_Users.Password, dbo.SYS_Users.Description, dbo.SYS_Users.Activate, dbo.SYS_Users.ExpireDate, dbo.SYS_Users.Cookies, 
                      dbo.SYS_Users.LastLoginDate, dbo.SYS_Users.FailedPasswordAttemptCount, dbo.PUL_Member.Name
                        FROM         dbo.SYS_Users INNER JOIN
                      dbo.PUL_Member ON dbo.SYS_Users.EmployeeKey = dbo.PUL_Member.[Key]" +

                " WHERE dbo.SYS_Users.EmployeeKey = @EmployeeKey";


            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@EmployeeKey", SqlDbType.Int).Value = EmployeeKey;

                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_UserKey = nReader["UserKey"].ToString();
                    //m_GroupKey = (int)nReader["GroupKey"];
                    m_Password = nReader["Password"].ToString();
                    m_UserName = nReader["UserName"].ToString();
                    m_Description = nReader["Description"].ToString();
                    m_Activate = (bool)nReader["Activate"];
                    m_ExpireDate = (DateTime)nReader["ExpireDate"];
                    m_CooperativeKey = (int)nReader["CooperativeKey"];

                    m_EmployeeKey = (int)nReader["EmployeeKey"];
                    m_EmployeeName = nReader["Name"].ToString();
                    m_Cookies = nReader["Cookies"].ToString();
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
        public User_Info(string Cookies, string test="Ac")
        {
            string nSQL = @"SELECT     dbo.SYS_Users.UserKey, dbo.SYS_Users.UserID, dbo.SYS_Users.UserName,dbo.SYS_Users.GroupKey,dbo.SYS_Users.CooperativeKey,dbo.SYS_Users.EmployeeKey, dbo.SYS_Users.Password, dbo.SYS_Users.Description, dbo.SYS_Users.Activate, dbo.SYS_Users.ExpireDate, dbo.SYS_Users.Cookies, 
                      dbo.SYS_Users.LastLoginDate, dbo.SYS_Users.FailedPasswordAttemptCount, dbo.PUL_Member.Name
                        FROM         dbo.SYS_Users INNER JOIN
                      dbo.PUL_Member ON dbo.SYS_Users.EmployeeKey = dbo.PUL_Member.[Key]" +

                " WHERE dbo.SYS_Users.Cookies = @Cookies";


            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@Cookies", SqlDbType.NVarChar).Value = Cookies;

                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_UserKey = nReader["UserKey"].ToString();
                    //m_GroupKey = (int)nReader["GroupKey"];
                    m_Password = nReader["Password"].ToString();
                    m_UserName = nReader["UserName"].ToString();
                    m_Description = nReader["Description"].ToString();
                    m_Activate = (bool)nReader["Activate"];
                    m_ExpireDate = (DateTime)nReader["ExpireDate"];
                    m_CooperativeKey = (int)nReader["CooperativeKey"];

                    m_EmployeeKey = (int)nReader["EmployeeKey"];
                    m_EmployeeName = nReader["Name"].ToString();
                    m_Cookies = nReader["Cookies"].ToString();
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


        public User_Info(string UserName, bool IsUserName)
        {
            string nSQL = @" SELECT     dbo.SYS_Users.*, dbo.PUL_Member.Name, dbo.PUL_Member.MemID, dbo.PUL_Member.Cooperative_Key, dbo.PUL_Member.Address, dbo.PUL_Member.Email, 
                      dbo.PUL_Cooperative.Cooperative_Name
                      FROM         dbo.PUL_Member INNER JOIN
                      dbo.SYS_Users ON dbo.PUL_Member.[Key] = dbo.SYS_Users.EmployeeKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Member.Cooperative_Key = dbo.PUL_Cooperative.Cooperative_Key "
                        + " WHERE dbo.SYS_Users.UserName = @UserName ";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;

                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_UserKey = nReader["UserKey"].ToString();
                    m_GroupKey = (int)nReader["GroupKey"];
                    m_UserName = nReader["UserName"].ToString();
                    m_Password = nReader["Password"].ToString();
                    m_Description = nReader["Description"].ToString();
                    m_CooperativeKey = (int)nReader["CooperativeKey"];
                    m_Activate = (bool)nReader["Activate"];
                    m_ExpireDate = (DateTime)nReader["ExpireDate"];

                    m_EmployeeKey = (int)nReader["EmployeeKey"];
                    m_EmployeeName = nReader["Name"].ToString();

                 //   m_BranchKey = (int)nReader["BranchKey"];
                    m_BranchName = nReader["Cooperative_Name"].ToString();

                    if (nReader["LastLoginDate"] != DBNull.Value)
                        m_LastLoginDate = (DateTime)nReader["LastLoginDate"];
                    m_FailedPasswordAttemptCount = (int)nReader["FailedPasswordAttemptCount"];

                    m_CreatedBy = nReader["CreatedBy"].ToString();
                    if (nReader["CreatedDateTime"] != DBNull.Value)
                        m_CreatedDateTime = (DateTime)nReader["CreatedDateTime"];

                    m_ModifiedBy = nReader["ModifiedBy"].ToString();
                    if (nReader["ModifiedDateTime"] != DBNull.Value)
                        m_ModifiedDateTime = (DateTime)nReader["ModifiedDateTime"];
                    if (nReader["Cookies"] != DBNull.Value)
                        m_Cookies = (string)nReader["Cookies"];
                }

                //---- Close Connect SQL ----
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

        public string Key
        {
            set { m_UserKey = value; }
            get { return m_UserKey; }
        }
        public string Name
        {
            set { m_UserName = value; }
            get { return m_UserName; }
        }
        public string Password
        {
            set
            {
                string nPassword = value;
                m_Password = MyCryptography.HashPass(nPassword);
            }
            get { return m_Password; }
        }
        public string Description
        {
            set { m_Description = value; }
            get { return m_Description; }
        }

        public bool Activate
        {
            set { m_Activate = value; }
            get { return m_Activate; }
        }
        public DateTime ExpireDate
        {
            set { m_ExpireDate = value; }
            get { return m_ExpireDate; }
        }

        private int m_CooperativeKey;
        public int CooperativeKey
        {
            set { m_CooperativeKey = value; }
            get { return m_CooperativeKey; }
        }

        public DateTime LastLoginDate
        {
            set { m_LastLoginDate = value; }
            get { return m_LastLoginDate; }
        }
        public int FailedPasswordAttemptCount
        {
            set { m_FailedPasswordAttemptCount = value; }
            get { return m_FailedPasswordAttemptCount; }
        }

        public int EmployeeKey
        {
            set { m_EmployeeKey = value; }
            get { return m_EmployeeKey; }
        }
        public string EmployeeName
        {
            set { m_EmployeeName = value; }
            get { return m_EmployeeName; }
        }
        public int BranchKey
        {
            set { m_BranchKey = value; }
            get { return m_BranchKey; }
        }
        public string BranchName
        {
            set { m_BranchName = value; }
            get { return m_BranchName; }
        }

        public int GroupKey
        {
            set { m_GroupKey = value; }
            get { return m_GroupKey; }
        }

        public string CreatedBy
        {
            set { m_CreatedBy = value; }
            get { return m_CreatedBy; }
        }
        public DateTime CreatedDateTime
        {
            set { m_CreatedDateTime = value; }
            get { return m_CreatedDateTime; }
        }

        public string ModifiedBy
        {
            set { m_ModifiedBy = value; }
            get { return m_ModifiedBy; }
        }
        public DateTime ModifiedDateTime
        {
            set { m_ModifiedDateTime = value; }
            get { return m_ModifiedDateTime; }
        }

        public string Message
        {
            set { m_Message = value; }
            get { return m_Message; }
        }
        public string Cookies
        {
            set { m_Cookies = value; }
            get { return m_Cookies; }
        }
        #endregion

        #region [ Constructor Update Information ]
        public string Save()
        {
            if (m_UserKey.Trim().Length > 0)
                return Update();
            else
                return Create();
        }
        public string Update()
        {
            string nResult = ""; ;

            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE SYS_USERS SET "
                        + " UserName = @UserName,"
                        + " GroupKey = @GroupKey,"
                        + " Password=@Password, "
                        + " Description = @Description,"

                        + " Activate=@Activate,"
                        + " ExpireDate=@ExpireDate,"
                        + " EmployeeKey =@EmployeeKey, "

                        + " ModifiedBy=@ModifiedBy,"
                        + " ModifiedDateTime=getdate(), "

                        + " Cookies=@Cookies "

                        + " WHERE UserKey = @UserKey";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
                nCommand.Parameters.Add("@GroupKey", SqlDbType.Int).Value = m_GroupKey;
                nCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = m_UserName;
                nCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = m_Password;
                nCommand.Parameters.Add("@Description", SqlDbType.NText).Value = m_Description;

                nCommand.Parameters.Add("@Activate", SqlDbType.Bit).Value = m_Activate;
                nCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = m_ExpireDate;
                nCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = m_CooperativeKey;
                nCommand.Parameters.Add("@EmployeeKey", SqlDbType.Int).Value = m_EmployeeKey;

                nCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = m_ModifiedBy;
                nCommand.Parameters.Add("@Cookies", SqlDbType.NVarChar).Value = m_Cookies;

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
            string nSQL = "INSERT INTO SYS_USERS( "
                        + " UserName, Password, Description, Activate,ExpireDate, FailedPasswordAttemptCount,EmployeeKey,CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,GroupKey)"
                        + " VALUES(@UserName, @Password, @Description,@Activate,@ExpireDate,@FailedPasswordAttemptCount,@EmployeeKey,@CreatedBy,getdate(),@ModifiedBy,getdate(),@GroupKey)";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {

                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
                nCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = m_UserName;
                nCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = m_Password;
                nCommand.Parameters.Add("@Description", SqlDbType.NText).Value = m_Description;
                nCommand.Parameters.Add("@GroupKey", SqlDbType.Int).Value = m_GroupKey;
                nCommand.Parameters.Add("@Activate", SqlDbType.Bit).Value = m_Activate;
                if (m_ExpireDate.Year == 001)
                    nCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    nCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = m_ExpireDate;

                nCommand.Parameters.Add("@FailedPasswordAttemptCount", SqlDbType.Int).Value = m_FailedPasswordAttemptCount;
                nCommand.Parameters.Add("@EmployeeKey", SqlDbType.Int).Value = m_EmployeeKey;
                nCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = m_CooperativeKey;

                nCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = m_CreatedBy;
                nCommand.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar).Value = m_ModifiedBy;

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
            string nSQL = "DELETE FROM SYS_USERS WHERE UserKey = @UserKey";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
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

        public string UpdatePass()
        {

            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE SYS_USERS SET "
                        + " Password=@Password "
                        + " WHERE UserKey = @UserKey";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;
                nCommand.Parameters.Add("@Password", SqlDbType.NVarChar).Value = m_Password;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();
            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close(); ;
            }
            return nResult;

        }
        public string UpdateFailedPass()
        {

            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE SYS_Users SET "
                        + " FailedPasswordAttemptCount = @FailedPasswordAttemptCount "
                        + " WHERE UserKey = @UserKey";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@FailedPasswordAttemptCount", SqlDbType.Int).Value = m_FailedPasswordAttemptCount;
                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;

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
        public string UpdateDateLogin()
        {

            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE SYS_Users SET "
                        + " LastLoginDate = getdate() "
                        + " WHERE UserKey = @UserKey";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = m_UserKey;

                nResult = nCommand.ExecuteNonQuery().ToString();
                nCommand.Dispose();
            }
            catch (Exception Err)
            {
                string n_Message = Err.ToString();
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
