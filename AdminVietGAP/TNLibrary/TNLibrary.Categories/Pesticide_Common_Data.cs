using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
    public class Pesticide_Common_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string Search)
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  PUL_Pesticide_Common.*, 2 As 'Loai' FROM PUL_Pesticide_Common ";
            if (Search != "")
            {
                zSQL += " WHERE Common_Name like N'%'+ @Search +'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
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
        public static int Count(string Search)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Pesticide_Common";
            if (Search != "")
            {
                cmdText += @" where Common_Name like N'%'+ @Search +'%'";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
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
    }
}
