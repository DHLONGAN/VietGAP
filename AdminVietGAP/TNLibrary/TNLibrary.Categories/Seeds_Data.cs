using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
    public class Seeds_Data
    {
        public static DataTable GetList(string Search)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.CategoryKey, dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds_Categories.CategoryName, dbo.PUL_Seeds_Companies.CompanyName, 
            dbo.PUL_Seeds_Status.StatusName, dbo.PUL_Season_Categories.SeasonName
            FROM         dbo.PUL_Seeds_Categories INNER JOIN
            dbo.PUL_Seeds ON dbo.PUL_Seeds_Categories.CategoryKey = dbo.PUL_Seeds.CategoryKey INNER JOIN
            dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
            dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
            dbo.PUL_Season_Categories ON dbo.PUL_Seeds.SeasonKey = dbo.PUL_Season_Categories.SeasonKey";
            if (Search != "")
            {
                zSQL += @" where dbo.PUL_Seeds.SeedsName like N'%'+ @Search +'%' or dbo.PUL_Seeds_Categories.CategoryName like N'%'+ @Search +'%' or dbo.PUL_Seeds_Companies.CompanyName like N'%'+ @Search +'%' or
                        dbo.PUL_Seeds_Status.StatusName like N'%'+ @Search +'%' or dbo.PUL_Season_Categories.SeasonName like N'%'+ @Search +'%'";
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
        public static DataTable GetList(int CategoryKey)
        {
            DataTable dataTable = new DataTable();
            string cmdText = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.CategoryKey, dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds_Categories.CategoryName, dbo.PUL_Seeds_Companies.CompanyName, 
            dbo.PUL_Seeds_Status.StatusName, dbo.PUL_Season_Categories.SeasonName
            FROM         dbo.PUL_Seeds_Categories INNER JOIN
            dbo.PUL_Seeds ON dbo.PUL_Seeds_Categories.CategoryKey = dbo.PUL_Seeds.CategoryKey INNER JOIN
            dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
            dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
            dbo.PUL_Season_Categories ON dbo.PUL_Seeds.SeasonKey = dbo.PUL_Season_Categories.SeasonKey";
            if (CategoryKey > 0)
            {
                cmdText = cmdText + " AND (dbo.PUL_Seeds.CategoryKey = @CategoryKey)";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = CategoryKey;
                new SqlDataAdapter(selectCommand).Fill(dataTable);
                selectCommand.Dispose();
                connection.Close();
            }
            catch (Exception exception)
            {
                string str3 = exception.ToString();
            }
            return dataTable;
        }
        public static DataTable GetList(int PageSize, int PageNumber, string Search)
        {
            DataTable dataTable = new DataTable();
            string cmdText = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.CategoryKey, dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds_Categories.CategoryName, dbo.PUL_Seeds_Companies.CompanyName, 
            dbo.PUL_Seeds_Status.StatusName, dbo.PUL_Season_Categories.SeasonName
            FROM         dbo.PUL_Seeds_Categories INNER JOIN
            dbo.PUL_Seeds ON dbo.PUL_Seeds_Categories.CategoryKey = dbo.PUL_Seeds.CategoryKey INNER JOIN
            dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
            dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
            dbo.PUL_Season_Categories ON dbo.PUL_Seeds.SeasonKey = dbo.PUL_Season_Categories.SeasonKey ";
            if (Search != "")
            {
                cmdText += @" where dbo.PUL_Seeds.SeedsName like N'%'+ @Search +'%' or dbo.PUL_Seeds_Categories.CategoryName like N'%'+ @Search +'%' or dbo.PUL_Seeds_Companies.CompanyName like N'%'+ @Search +'%' or
                        dbo.PUL_Seeds_Status.StatusName like N'%'+ @Search +'%' or dbo.PUL_Season_Categories.SeasonName like N'%'+ @Search +'%'";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                SqlDataAdapter _Adapter = new SqlDataAdapter(selectCommand);
                _Adapter.Fill(PageSize * PageNumber - PageSize, PageSize, dataTable);
               
                selectCommand.Dispose();
                connection.Close();
            }
            catch (Exception exception)
            {
                string str3 = exception.ToString();
            }
            return dataTable;
        }
        public static int Count(string Search)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Seeds_Categories INNER JOIN
            dbo.PUL_Seeds ON dbo.PUL_Seeds_Categories.CategoryKey = dbo.PUL_Seeds.CategoryKey INNER JOIN
            dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
            dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
            dbo.PUL_Season_Categories ON dbo.PUL_Seeds.SeasonKey = dbo.PUL_Season_Categories.SeasonKey";
            if (Search != "")
            {
                cmdText += @" where dbo.PUL_Seeds.SeedsName like N'%'+ @Search +'%' or dbo.PUL_Seeds_Categories.CategoryName like N'%'+ @Search +'%' or dbo.PUL_Seeds_Companies.CompanyName like N'%'+ @Search +'%' or
                        dbo.PUL_Seeds_Status.StatusName like N'%'+ @Search +'%' or dbo.PUL_Season_Categories.SeasonName like N'%'+ @Search +'%'";
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
