using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
    public class Companies_Purchase_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string Search)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Country.CountryName, dbo.PUL_Companies_Purchase.City, dbo.PUL_Companies_Purchase.Address, dbo.PUL_Companies_Purchase.CompanyName, dbo.PUL_Companies_Purchase.CompanyKey
                            FROM         dbo.PUL_Companies_Purchase INNER JOIN
                      dbo.PUL_Country ON dbo.PUL_Companies_Purchase.Country = dbo.PUL_Country.CountryKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            if (Search != "")
            {
                zSQL += @" WHERE dbo.PUL_Country.CountryName like N'%'+ @Search +'%' or dbo.PUL_Companies_Purchase.City like N'%'+ @Search +'%' or dbo.PUL_Companies_Purchase.Address
                 like N'%'+ @Search +'%' or dbo.PUL_Companies_Purchase.CompanyName like N'%'+ @Search +'%'";
            }
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand selectCommand = new SqlCommand(zSQL, zConnect);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                SqlDataAdapter _Adapter = new SqlDataAdapter(selectCommand);
                _Adapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);

                selectCommand.Dispose();
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
            string cmdText = @"SELECT  Count(*)   FROM         dbo.PUL_Companies_Purchase INNER JOIN
                      dbo.PUL_Country ON dbo.PUL_Companies_Purchase.Country = dbo.PUL_Country.CountryKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            if (Search != "")
            {
                cmdText += @" WHERE dbo.PUL_Country.CountryName like N'%'+ @Search +'%' or dbo.PUL_Companies_Purchase.City like N'%'+ @Search +'%' or dbo.PUL_Companies_Purchase.Address
                 like N'%'+ @Search +'%' or dbo.PUL_Companies_Purchase.CompanyName like N'%'+ @Search +'%'";
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
