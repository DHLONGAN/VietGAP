using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Fertilizer_Use_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Fertilizer_Use.FertilizerUseKey, dbo.PUL_Fertilizer_Use.DateTimeUse, dbo.PUL_Fertilizer_Use.Parcel, dbo.PUL_Fertilizer_Use.Area, 
                      dbo.PUL_Fertilizer_Use.FormulaUsed, dbo.PUL_Fertilizer_Use.Quantity, dbo.PUL_Fertilizer_Use.Howtouse, dbo.PUL_Fertilizers.TradeName, dbo.PUL_Seeds.SeedsName
FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Fertilizer_Use.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Fertilizer_Use.SeedKey = dbo.PUL_Seeds.SeedsKey  WHERE MemberKey = @MemberKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
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
        public static DataTable GetList(DateTime fromdate, DateTime todate, int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Fertilizer_Use.FertilizerUseKey, dbo.PUL_Fertilizer_Use.DateTimeUse, dbo.PUL_Fertilizer_Use.Parcel, dbo.PUL_Fertilizer_Use.Area, 
                      dbo.PUL_Fertilizer_Use.FormulaUsed, dbo.PUL_Fertilizer_Use.Quantity, dbo.PUL_Fertilizer_Use.Howtouse, dbo.PUL_Fertilizers.TradeName, dbo.PUL_Seeds.SeedsName
FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Fertilizer_Use.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Fertilizer_Use.SeedKey = dbo.PUL_Seeds.SeedsKey where dbo.PUL_Fertilizer_Use.[DatetimeUse] >= @fromdate and dbo.PUL_Fertilizer_Use.[DatetimeUse] <= @todate and MemberKey = @MemberKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
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

        public static int Count()
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Fertilizer_Use";

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
