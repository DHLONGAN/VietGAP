using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
    public class HarvestedForSale_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber, string SeedsName)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey, dbo.PUL_HarvestedForSale.Datetime, dbo.PUL_HarvestedForSale.Code, 
                      dbo.PUL_HarvestedForSale.QuantityHarvested, dbo.PUL_HarvestedForSale.QuantitySale, dbo.PUL_HarvestedForSale.WhereToBuy, 
                      dbo.PUL_HarvestedForSale.MemberKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_HarvestedForSale.UnitKey, dbo.PUL_Unit.Name AS UNN
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_HarvestedForSale.UnitKey = dbo.PUL_Unit.ID WHERE MemberKey = @MemberKey AND dbo.PUL_HarvestedForSale.SeedsKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) order by dbo.PUL_HarvestedForSale.Datetime";
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
            string zSQL = @"SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey, dbo.PUL_HarvestedForSale.Datetime, dbo.PUL_HarvestedForSale.Code, 
                      dbo.PUL_HarvestedForSale.QuantityHarvested, dbo.PUL_HarvestedForSale.QuantitySale, dbo.PUL_HarvestedForSale.WhereToBuy, 
                      dbo.PUL_HarvestedForSale.MemberKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_HarvestedForSale.UnitKey, dbo.PUL_Unit.Name AS UNN
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_HarvestedForSale.UnitKey = dbo.PUL_Unit.ID where dbo.PUL_HarvestedForSale.[Datetime] >= @fromdate and dbo.PUL_HarvestedForSale.[Datetime] <= @todate and MemberKey = @MemberKey AND dbo.PUL_HarvestedForSale.SeedsKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) order by dbo.PUL_HarvestedForSale.Datetime";
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
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_HarvestedForSale WHERE MemberKey=@MemberKey  AND dbo.PUL_HarvestedForSale.SeedsKey IN (
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
