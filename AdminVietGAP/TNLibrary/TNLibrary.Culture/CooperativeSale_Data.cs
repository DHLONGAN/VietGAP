using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
    public class CooperativeSale_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Unit.Name, dbo.PUL_CooperativeSale.CooperativeSaleKey, dbo.PUL_CooperativeSale.Code, dbo.PUL_CooperativeSale.Datetime, dbo.PUL_CooperativeSale.Address, 
                      dbo.PUL_CooperativeSale.Quantity, dbo.PUL_Seeds.SeedsName, dbo.PUL_CooperativeSale.Price
                        FROM         dbo.PUL_CooperativeSale INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_CooperativeSale.UnitKey = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_CooperativeSale.SeedKey = dbo.PUL_Seeds.SeedsKey where dbo.PUL_CooperativeSale.CooperativeKey = @CooperativeKey
                        ORDER BY dbo.PUL_CooperativeSale.Datetime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                //zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                //zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
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
        public static DataTable GetList(DateTime fromdate, DateTime todate, int PageSize, int PageNumber, int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Unit.Name, dbo.PUL_CooperativeSale.CooperativeSaleKey, dbo.PUL_CooperativeSale.Code, dbo.PUL_CooperativeSale.Datetime, dbo.PUL_CooperativeSale.Address, 
                      dbo.PUL_CooperativeSale.Quantity, dbo.PUL_Seeds.SeedsName, dbo.PUL_CooperativeSale.Price
                        FROM         dbo.PUL_CooperativeSale INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_CooperativeSale.UnitKey = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_CooperativeSale.SeedKey = dbo.PUL_Seeds.SeedsKey where dbo.PUL_CooperativeSale.[Datetime] >= @fromdate and dbo.PUL_CooperativeSale.[Datetime] <= @todate and dbo.PUL_CooperativeSale.CooperativeKey = @CooperativeKey
                       
                        order by dbo.PUL_CooperativeSale.Datetime";
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

        public static int Count(DateTime fromdate, DateTime todate, int CooperativeKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_CooperativeSale where dbo.PUL_CooperativeSale.CooperativeKey = @CooperativeKey";

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
    }
}
