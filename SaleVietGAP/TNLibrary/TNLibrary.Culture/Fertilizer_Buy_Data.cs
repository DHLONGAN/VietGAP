using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Fertilizer_Buy_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Fertilizers.TradeName, dbo.PUL_Fertilizer_Buy.FertilizerBuyKey, dbo.PUL_Fertilizer_Buy.DatetimeBuy, dbo.PUL_Member.Name, dbo.PUL_Fertilizer_Buy.Quantity, 
                      dbo.PUL_Fertilizer_Buy.Price, dbo.PUL_Fertilizer_Buy.CompanyKey, dbo.PUL_Fertilizer_Buy.Address
FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Fertilizer_Buy.MemberKey = dbo.PUL_Member.[Key] WHERE MemberKey = @MemberKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.DateTime).Value = MemberKey;
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
        public static DataTable GetList(DateTime fromdate, DateTime todate, int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Fertilizers.TradeName, dbo.PUL_Fertilizer_Buy.FertilizerBuyKey, dbo.PUL_Fertilizer_Buy.DatetimeBuy, dbo.PUL_Member.Name, dbo.PUL_Fertilizer_Buy.Quantity, 
                      dbo.PUL_Fertilizer_Buy.Price, dbo.PUL_Fertilizer_Buy.CompanyKey, dbo.PUL_Fertilizer_Buy.Address
FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Fertilizer_Buy.MemberKey = dbo.PUL_Member.[Key] where dbo.PUL_Fertilizer_Buy.[DatetimeBuy] >= @fromdate and dbo.PUL_Fertilizer_Buy.[DatetimeBuy] <= @todate and MemberKey = @MemberKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.DateTime).Value = MemberKey;
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
        public static DataTable GetAddressList()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"Select Address from PUL_Fertilizer_Buy group by Address";
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

        public static int Count()
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Fertilizer_Buy";

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
