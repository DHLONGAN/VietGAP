using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
    public class PostharvestHandling_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber, int SeedsKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_PostharvestHandling.PostharvestHandling_Key, dbo.PUL_PostharvestHandling.MemberKey, dbo.PUL_PostharvestHandling.SeedsKey, 
                      dbo.PUL_PostharvestHandling.Method, dbo.PUL_PostharvestHandling.Datetime
FROM         dbo.PUL_PostharvestHandling INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_PostharvestHandling.SeedsKey = dbo.PUL_Seeds.SeedsKey WHERE dbo.PUL_PostharvestHandling.SeedsKey = @SeedsKey and dbo.PUL_PostharvestHandling.MemberKey = @MemberKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
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
        public static DataTable GetList(DateTime fromdate, DateTime todate, int MemberKey, int PageSize, int PageNumber, int SeedsKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_PostharvestHandling.PostharvestHandling_Key, dbo.PUL_PostharvestHandling.MemberKey, dbo.PUL_PostharvestHandling.SeedsKey, 
                      dbo.PUL_PostharvestHandling.Method, dbo.PUL_PostharvestHandling.Datetime
                    FROM         dbo.PUL_PostharvestHandling INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_PostharvestHandling.SeedsKey = dbo.PUL_Seeds.SeedsKey 
             where dbo.PUL_PostharvestHandling.[Datetime] >= @fromdate and dbo.PUL_PostharvestHandling.[Datetime] <= @todate and MemberKey = @MemberKey AND dbo.PUL_PostharvestHandling.SeedsKey = @SeedsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
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
        public static int Count(int MemberKey, int SeedsKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_PostharvestHandling WHERE MemberKey = @MemberKey AND dbo.PUL_PostharvestHandling.SeedsKey = @SeedsKey";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                selectCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
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
