using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
    public class Fertilizers_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Fertilizers ";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
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
        public static DataTable GetInfoByKey(int Key)
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Fertilizers WHERE FertilizersKey = @key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@key", SqlDbType.Int).Value = Key;
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
        public static DataTable GetList(int PageSize, int PageNumber, string Search)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT dbo.PUL_Fertilizers.FertilizersKey, dbo.PUL_Fertilizers.TradeName, dbo.PUL_Fertilizer_Unit.Fertilizer_Unit_Name, dbo.PUL_Fertilizer_Common.Common_Name, dbo.PUL_Fertilizer_Categories.CategoryName, 
                      dbo.PUL_Companies.CompanyName
                      FROM         dbo.PUL_Fertilizer_Unit INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Unit.Fertilizer_Unit_Key = dbo.PUL_Fertilizers.UnitKey INNER JOIN
                      dbo.PUL_Fertilizer_Common ON dbo.PUL_Fertilizers.CommonKey = dbo.PUL_Fertilizer_Common.Common_Key INNER JOIN
                      dbo.PUL_Fertilizer_Categories ON dbo.PUL_Fertilizers.CategoryKey = dbo.PUL_Fertilizer_Categories.CategoryKey INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Fertilizers.CompanyKey = dbo.PUL_Companies.CompanyKey";
            if (Search != "")
            {
                zSQL += @" WHERE dbo.PUL_Fertilizers.TradeName like N'%'+ @Search +'%' or dbo.PUL_Fertilizer_Unit.Fertilizer_Unit_Name like N'%'+ @Search +'%' or 
                        dbo.PUL_Fertilizer_Common.Common_Name like N'%'+ @Search +'%' or dbo.PUL_Fertilizer_Categories.CategoryName like N'%'+ @Search +'%' or dbo.PUL_Companies.CompanyName like N'%'+ @Search +'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
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

        public static int Count()
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Fertilizer_Categories INNER JOIN
            dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Categories.CategoryKey = dbo.PUL_Fertilizers.CategoryKey INNER JOIN
            dbo.PUL_Fertilizer_Unit ON dbo.PUL_Fertilizers.UnitKey = dbo.PUL_Fertilizer_Unit.Fertilizer_Unit_Key INNER JOIN
            dbo.PUL_Fertilizer_Common ON dbo.PUL_Fertilizers.CommonKey = dbo.PUL_Fertilizer_Common.Common_Key INNER JOIN
            dbo.PUL_Companies ON dbo.PUL_Fertilizers.CompanyKey = dbo.PUL_Companies.CompanyKey";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);

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
