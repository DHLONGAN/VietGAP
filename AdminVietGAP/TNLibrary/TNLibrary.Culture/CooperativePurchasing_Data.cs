using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
    public class CooperativePurchasing_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     TOP (100) PERCENT dbo.PUL_Member.Name, dbo.PUL_SeedProcess.DateOfManufacture, dbo.PUL_Seeds.SeedsName, dbo.PUL_CooperativePurchasing.Baskets, 
                      dbo.PUL_CooperativePurchasing.Quantity, dbo.PUL_CooperativePurchasing.CooperativePurchasingKey, dbo.PUL_Unit.Name AS UName, dbo.PUL_CooperativePurchasing.Datetime, 
                      dbo.PUL_HarvestedForSale.Code, dbo.PUL_HarvestedForSale.QuantitySale, dbo.PUL_CooperativePurchasing.Price, dbo.PUL_CooperativePurchasing.Evaluate, 
                      dbo.PUL_CooperativePurchasing.Solution
                      FROM         dbo.PUL_CooperativePurchasing INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_CooperativePurchasing.SeedProcessKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_SeedProcess.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_CooperativePurchasing.UnitKey = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_HarvestedForSale ON dbo.PUL_CooperativePurchasing.HarvestedForSaleKey = dbo.PUL_HarvestedForSale.HarvestedForSaleKey where PUL_CooperativePurchasing.CooperativeKey = @CooperativeKey
                      ORDER BY dbo.PUL_CooperativePurchasing.Datetime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                //zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
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
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) 
                       
order by dbo.PUL_HarvestedForSale.Datetime";
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

        public static int Count(DateTime fromdate, DateTime todate, int CooperativeKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_CooperativePurchasing where CooperativeKey =@CooperativeKey";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                selectCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                selectCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
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
        public static int Count2(DateTime fromdate, DateTime todate, int CooperativeKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_HarvestedForSale where dbo.PUL_HarvestedForSale.[Datetime] >= @fromdate and dbo.PUL_HarvestedForSale.[Datetime] <= @todate and MemberKey IN(Select [Key] from PUL_Member where Cooperative_Key =@CooperativeKey)";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                selectCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                selectCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
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
        public static DataTable GetListSale(DateTime fromdate, DateTime todate, int PageSize, int PageNumber, int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey, dbo.PUL_HarvestedForSale.Datetime, dbo.PUL_HarvestedForSale.Code, dbo.PUL_HarvestedForSale.QuantityHarvested, 
                      dbo.PUL_HarvestedForSale.QuantitySale, dbo.PUL_HarvestedForSale.WhereToBuy, dbo.PUL_HarvestedForSale.MemberKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_HarvestedForSale.UnitKey, 
                      dbo.PUL_Unit.Name AS UNN, dbo.PUL_Member.Name
                    FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_HarvestedForSale.UnitKey = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_HarvestedForSale.MemberKey = dbo.PUL_Member.[Key] where dbo.PUL_HarvestedForSale.[Datetime] >= @fromdate and dbo.PUL_HarvestedForSale.[Datetime] <= @todate 
                     and dbo.PUL_HarvestedForSale.HarvestedForSaleKey NOT IN(Select HarvestedForSaleKey from PUL_CooperativePurchasing) and dbo.PUL_HarvestedForSale.MemberKey IN(Select [Key] from PUL_Member where Cooperative_Key =@CooperativeKey)
                        order by dbo.PUL_HarvestedForSale.Datetime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
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
