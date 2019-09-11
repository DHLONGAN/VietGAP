using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
    public class CooperativePurchasing_Other_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_CooperativePurchasing_Other.Purchasing_OtherKey, dbo.PUL_CooperativePurchasing_Other.Code, dbo.PUL_CooperativePurchasing_Other.CooperativeKey, 
                      dbo.PUL_CooperativePurchasing_Other.Quantity, dbo.PUL_CooperativePurchasing_Other.Baskets, dbo.PUL_CooperativePurchasing_Other.Price, dbo.PUL_CooperativePurchasing_Other.Datetime, 
                      dbo.PUL_CooperativePurchasing_Other.Name, dbo.PUL_Unit.Name AS UName, dbo.PUL_CooperativePurchasing_Other.Solution, dbo.PUL_CooperativePurchasing_Other.Evaluate
FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_CooperativePurchasing_Other ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_CooperativePurchasing_Other.SeedKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_CooperativePurchasing_Other.UnitKey = dbo.PUL_Unit.ID where PUL_CooperativePurchasing_Other.CooperativeKey = @CooperativeKey
                        ORDER BY dbo.PUL_CooperativePurchasing_Other.Datetime";
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

        public static int Count(DateTime fromdate, DateTime todate, int CooperativeKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_CooperativePurchasing_Other where CooperativeKey =@CooperativeKey";

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
