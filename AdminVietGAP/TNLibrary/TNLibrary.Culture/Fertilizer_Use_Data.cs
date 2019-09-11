using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Fertilizer_Use_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber, string SeedsName)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Fertilizer_Use.FertilizerUseKey, dbo.PUL_Fertilizer_Use.DateTimeUse, dbo.PUL_Fertilizer_Use.Parcel, 
                      dbo.PUL_Fertilizer_Use.Area, dbo.PUL_Fertilizer_Use.FormulaUsed, dbo.PUL_Fertilizer_Use.Quantity, dbo.PUL_Fertilizer_Use.Howtouse, 
                      dbo.PUL_Fertilizers.TradeName, dbo.PUL_Equipment.EquipmentName, dbo.PUL_Fertilizer_Use.QuarantinePeriod, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Unit.Name AS UNN
FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Fertilizer_Use.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Fertilizer_Use.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_Fertilizer_Use.CooperativeKey = dbo.PUL_Equipment.EquipmentKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_Fertilizer_Use.UnitKey = dbo.PUL_Unit.ID  WHERE MemberKey = @MemberKey AND dbo.PUL_Fertilizer_Use.SeedKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) order by dbo.PUL_Fertilizer_Use.DateTimeUse desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
                //Adapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetList(DateTime fromdate, DateTime todate, int MemberKey, int PageSize, int PageNumber, string SeedsName)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Fertilizer_Use.FertilizerUseKey, dbo.PUL_Fertilizer_Use.DateTimeUse, dbo.PUL_Fertilizer_Use.Parcel, 
                      dbo.PUL_Fertilizer_Use.Area, dbo.PUL_Fertilizer_Use.FormulaUsed, dbo.PUL_Fertilizer_Use.Quantity, dbo.PUL_Fertilizer_Use.Howtouse, 
                      dbo.PUL_Fertilizers.TradeName, dbo.PUL_Equipment.EquipmentName, dbo.PUL_Fertilizer_Use.QuarantinePeriod, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Unit.Name AS UNN
FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Fertilizer_Use.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Fertilizer_Use.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_Fertilizer_Use.CooperativeKey = dbo.PUL_Equipment.EquipmentKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_Fertilizer_Use.UnitKey = dbo.PUL_Unit.ID where dbo.PUL_Fertilizer_Use.[DatetimeUse] >= @fromdate and dbo.PUL_Fertilizer_Use.[DatetimeUse] <= @todate and MemberKey = @MemberKey  AND dbo.PUL_Fertilizer_Use.SeedKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) order by dbo.PUL_Fertilizer_Use.DateTimeUse desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
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

        public static int Count(int MemberKey, string SeedsName)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Fertilizer_Use WHERE MemberKey = @MemberKey  AND dbo.PUL_Fertilizer_Use.SeedKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName))";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                selectCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
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
