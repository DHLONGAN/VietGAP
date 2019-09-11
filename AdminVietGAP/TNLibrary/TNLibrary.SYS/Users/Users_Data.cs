using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;

namespace TNLibrary.SYS.Users
{
    public class Users_Data
    {
        #region [ Users ]
        public static DataTable UserList()
        {

            DataTable nTable = new DataTable();
            
            string nSQL = " SELECT A.*,B.LastName,B.FirstName FROM SYS_Users A "
                        + " LEFT JOIN HRM_Employees B ON A.EmployeeKey = B.EmployeeKey "
                        + " ORDER BY UserName DESC";
            string nConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataAdapter nUsers = new SqlDataAdapter(nCommand);

                nUsers.Fill(nTable);
                //---- Close Connect SQL ----
                nCommand.Dispose();
                nConnect.Close();
            }
            catch (Exception ex)
            {
                string strMessage = ex.ToString();
            }

            return nTable;
        }

        public static DataTable GetList(string UserName, int CooperativeID, int GroupID)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     UserKey, UserID, UserName, Password, Description, Activate, ExpireDate, LastLoginDate, FailedPasswordAttemptCount
                            FROM         dbo.SYS_Users";
            if (GroupID == 4)
            {
                zSQL += " WHERE 1=1";// dbo.SYS_Users.GroupKey =3 ";
            }
            if (GroupID == 2)
            {
                zSQL += " WHERE dbo.SYS_Users.CooperativeKey = @CooperativeID AND dbo.SYS_Users.GroupKey =1 ";
            }
            if (GroupID == 3)
            {
                zSQL += " WHERE dbo.SYS_Users.CooperativeKey IN(SELECT Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @CooperativeID)";
            }
            if (UserName != "")
            {
                zSQL += " AND dbo.SYS_Users.UserName like '%'+@UserName+'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                zCommand.Parameters.Add("@CooperativeID", SqlDbType.Int).Value = CooperativeID;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetList(string UserName, int CooperativeKey)
        {
            DataTable zTable = new DataTable();
//            string zSQL = @"SELECT     dbo.SYS_Users.UserKey, dbo.SYS_Users.UserID, dbo.SYS_Users.UserName, dbo.SYS_Users.Password, dbo.SYS_Users.Description, dbo.SYS_Users.Activate, dbo.SYS_Users.ExpireDate, 
//                      dbo.SYS_Users.LastLoginDate, dbo.SYS_Users.FailedPasswordAttemptCount, dbo.PUL_Member.Name
//                        FROM         dbo.SYS_Users INNER JOIN
//                      dbo.PUL_Member ON dbo.SYS_Users.EmployeeKey = dbo.PUL_Member.[Key] WHERE dbo.SYS_Users.CooperativeKey = @CooperativeID ";
            string zSQL = @"SELECT     UserKey, UserID, UserName, Password, Description, Activate, ExpireDate, LastLoginDate, FailedPasswordAttemptCount
                            FROM         dbo.SYS_Users ";
            if (UserName != "")
            {
                zSQL += " WHERE dbo.SYS_Users.UserName like '%'+@UserName+'%' ";
            }
            else
            {
                zSQL += " WHERE CooperativeKey = @CooperativeKey";
            }
            zSQL +=" or GroupKey = 3";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetList(int CooperativeID, int Group, string UserName)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     UserKey, UserID, UserName, Password, Description, Activate, ExpireDate, LastLoginDate, FailedPasswordAttemptCount
                            FROM         dbo.SYS_Users WHERE dbo.SYS_Users.CooperativeKey = @CooperativeID AND GroupKey = 2";// IN(SELECT Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @CooperativeID)";
            if (UserName != "")
            {
                zSQL += " AND dbo.SYS_Users.UserName like '%'+@UserName+'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CooperativeID", SqlDbType.Int).Value = CooperativeID;
                zCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable UserList(string UserID, string UserName)
        {

            DataTable nTable = new DataTable();
            string nConnectionString = ConnectDataBase.ConnectionString;
            string nSQL = " SELECT A.*,B.LastName,B.FirstName FROM SYS_Users A "
                        + " LEFT JOIN HRM_Employees B ON A.EmployeeKey = B.EmployeeKey "
                        + " WHERE UserID   LIKE @UserID "
                        + " AND UserName LIKE @UserName "
                        + " ORDER BY UserName DESC";
            try
            {
                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = "%" + UserID + "%"; ;
                nCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = "%" + UserName + "%";

                SqlDataAdapter mAdapter = new SqlDataAdapter(nCommand);
                mAdapter.Fill(nTable);
                //---- Close Connect SQL ----
                nCommand.Dispose();
                nConnect.Close();
            }
            catch (Exception ex)
            {
                string strMessage = ex.ToString();
            }
            return nTable;
        }
        #endregion

        public static string[] CheckUser(string UserName, string Pass)
        {

            string[] nResult = new string[8];
            User_Info nUserLogin = new User_Info(UserName, true);
            
            if (nUserLogin.Key.Trim().Length == 0)
            {
                nResult[0] = "ERR";
                nResult[1] ="CheckUser_Error01";
                return nResult;//"Don't have this UserName";
            }
            if (nUserLogin.GroupKey < 2)
            {
                nResult[0] = "ERR";
                nResult[1] = "CantLogin";
                return nResult;//"Don't have this UserName";
            }
            //LINH
            //string moa = MyCryptography.HashPass(Pass);
            //if (nUserLogin.Password != MyCryptography.HashPass(Pass))
            //{
            //    nUserLogin.UpdateFailedPass();
            //    nResult[0] = "ERR";
            //    nResult[1] = "CheckUser_Error01";
            //    return nResult;// "Wrong Password";
            //}
            
            if (!nUserLogin.Activate)
            {
                nResult[0] = "ERR";
                nResult[1] = "CheckUser_Error02";
                return nResult;//"Don't Activate"
            }
            
            //if (nUserLogin.ExpireDate < DateTime.Now)
            //{
            //    nResult[0] = "ERR";
            //    nResult[1] = "CheckUser_Error03";
            //    return nResult;//"Expire On"
            //}
            nResult[0] = "OK";
            nResult[1] = nUserLogin.Key;
            nResult[2] = nUserLogin.EmployeeKey.ToString();
            nResult[3] = nUserLogin.GroupKey.ToString();
            nResult[4] = nUserLogin.CooperativeKey.ToString();
            nResult[5] = nUserLogin.EmployeeName;
            nResult[6] = nUserLogin.BranchName;
            nResult[7] = nUserLogin.CooperativeVenturesKey.ToString();
            nUserLogin.UpdateDateLogin();
            return nResult;
        }

        #region [Check Role]
        public static DataTable RoleList(string UserKey)
        {

            DataTable nTable = new DataTable();
            string nConnectionString = ConnectDataBase.ConnectionString;
            string nSQL = " SELECT A.*,B.RoleID,B.RoleName,C.UserName FROM SYS_Users_Roles A"
                        + " LEFT JOIN SYS_Roles B ON A.RoleKey = B.RoleKey "
                        + " LEFT JOIN SYS_Users C ON A.UserKey = C.UserKey "
                        + " WHERE A.UserKey = @UserKey ORDER BY B.RoleID ";
            try
            {
                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = UserKey;

                SqlDataAdapter mAdapter = new SqlDataAdapter(nCommand);
                mAdapter.Fill(nTable);
                //---- Close Connect SQL ----
                nCommand.Dispose();
                nConnect.Close();
            }
            catch (Exception ex)
            {
                string strMessage = ex.ToString();
            }
            return nTable;
        }
        public static bool CheckModule(string UserKey, string ModuleName)
        {
            //---------- String SQL Access Database ---------------
            string nSQL = "SELECT COUNT(*) FROM SYS_User_Module WHERE UserKey = @UserKey AND ModuleName =@ModuleName ";
            string nResult = "0";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {

                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = UserKey;
                nCommand.Parameters.Add("@ModuleName", SqlDbType.NVarChar).Value = ModuleName;

                nResult = nCommand.ExecuteScalar().ToString();
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
            int nHaveRecords = 0;
            int.TryParse(nResult, out nHaveRecords);
            if (nHaveRecords > 0)
                return true;
            else
                return false;
        }
        #endregion


    }
}
