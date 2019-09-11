using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
    public class Users_Roles_Data
    {
        public static DataTable GetList(string Search)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT DISTINCT      dbo.SYS_Users_Roles.UserKey, dbo.SYS_Users.UserName
                        FROM         dbo.SYS_Roles INNER JOIN
                      dbo.SYS_Users_Roles ON dbo.SYS_Roles.RoleKey = dbo.SYS_Users_Roles.RoleKey INNER JOIN
                      dbo.SYS_Users ON dbo.SYS_Users_Roles.UserKey = dbo.SYS_Users.UserKey";
            if (Search != "")
            {
                zSQL += " WHERE dbo.SYS_Users.UserName like '%'+ @Search +'%' or dbo.SYS_Roles.RoleName like N'%'+ @Search +'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
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

        public static DataTable GetList(int CooperativeKey, int Group, string UserName, int CooperativeVenturesKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT DISTINCT      dbo.SYS_Users.UserKey, dbo.SYS_Users.UserName, dbo.SYS_Users.GroupKey
                        FROM         dbo.SYS_Users ";
            if (Group == 4)
            {
                zSQL += " WHERE dbo.SYS_Users.GroupKey = 3 or dbo.SYS_Users.CooperativeKey = @CooperativeKey";// dbo.SYS_Users.CooperativeKey = 0 and dbo.SYS_Users.GroupKey = 3";
            }
            else if (Group ==3)
            {
                zSQL += " WHERE dbo.SYS_Users.CooperativeKey IN(SELECT Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @CooperativeVenturesKey) and dbo.SYS_Users.GroupKey = 2";
            }
            else if (Group == 2)
            {
                zSQL += " WHERE dbo.SYS_Users.CooperativeKey = @CooperativeKey AND dbo.SYS_Users.GroupKey =1 ";
            }
            if (UserName != "")
            {
                zSQL += " and dbo.SYS_Users.UserName like '%'+ @UserName +'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                zCommand.Parameters.Add("@CooperativeVenturesKey", SqlDbType.Int).Value = CooperativeVenturesKey;
                zCommand.Parameters.Add("@Group", SqlDbType.Int).Value = Group;
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


        public static int Count(int CooperativeKey, int Group, string UserName)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.SYS_Users_Roles
                                INNER JOIN SYS_Users ON dbo.SYS_Users_Roles.UserKey = SYS_Users.UserKey ";
            if (Group == 4)
            {
                cmdText += " WHERE 1=1";//dbo.SYS_Users.CooperativeKey = 0 and dbo.SYS_Users.GroupKey = 3";
            }
            else if (Group == 3)
            {
                cmdText += " WHERE dbo.SYS_Users.CooperativeKey IN(SELECT Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @CooperativeKey) and dbo.SYS_Users.GroupKey = 2";
            }
            else if (Group == 2)
            {
                cmdText += " WHERE dbo.SYS_Users.CooperativeKey = @CooperativeKey AND dbo.SYS_Users.GroupKey =1 ";
            }
            if (UserName != "")
            {
                cmdText += " and dbo.SYS_Users.UserName like '%'+ @UserName +'%'";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                selectCommand.Parameters.Add("@Group", SqlDbType.Int).Value = Group;
                selectCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
                _Result = (int)selectCommand.ExecuteScalar();
                selectCommand.Dispose();
                connection.Close();
            }
            catch (Exception exception)
            {
                string str3 = exception.ToString();
            }
            return _Result;
        }

        public static int Count(string UserKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.SYS_Users_Roles where UserKey = @UserKey";
            
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@UserKey", SqlDbType.NVarChar).Value = UserKey;
                _Result = (int)selectCommand.ExecuteScalar();
                selectCommand.Dispose();
                connection.Close();
            }
            catch (Exception exception)
            {
                string str3 = exception.ToString();
            }
            return _Result;
        }

        public static DataTable GetListByUserKey(string key, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.SYS_Users_Roles.UserKey, dbo.SYS_Users_Roles.RoleKey, dbo.SYS_Users_Roles.RoleRead, dbo.SYS_Users_Roles.RoleAdd, dbo.SYS_Users_Roles.RoleEdit, 
                      dbo.SYS_Users_Roles.RoleDel, dbo.SYS_Roles.RoleName, dbo.SYS_Users.UserName
                        FROM         dbo.SYS_Roles INNER JOIN
                      dbo.SYS_Users_Roles ON dbo.SYS_Roles.RoleKey = dbo.SYS_Users_Roles.RoleKey INNER JOIN
                      dbo.SYS_Users ON dbo.SYS_Users_Roles.UserKey = dbo.SYS_Users.UserKey";

            zSQL += " WHERE dbo.SYS_Users_Roles.UserKey =@key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
    }
}
