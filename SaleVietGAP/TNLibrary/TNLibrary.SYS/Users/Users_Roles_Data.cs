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
            string zSQL = @"SELECT     dbo.SYS_Users_Roles.UserKey, dbo.SYS_Users_Roles.RoleKey, dbo.SYS_Users_Roles.RoleRead, dbo.SYS_Users_Roles.RoleAdd, dbo.SYS_Users_Roles.RoleEdit, 
                      dbo.SYS_Users_Roles.RoleDel, dbo.SYS_Roles.RoleName, dbo.SYS_Users.UserName
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
    }
}
