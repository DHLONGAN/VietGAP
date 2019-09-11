using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class ForSale_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_ForSale.CooperativeKey, dbo.PUL_ForSale.MemberKey, dbo.PUL_ForSale.Bill, dbo.PUL_ForSale.PlaceOfBuy, dbo.PUL_ForSale.Slot, dbo.PUL_ForSale.Weight, 
                      dbo.PUL_ForSale.Code, dbo.PUL_ForSale.Datetime, dbo.PUL_ForSale.ForSaleKey, dbo.PUL_Seeds.SeedsName
                        FROM         dbo.PUL_ForSale INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_ForSale.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_ForSale.MemberKey = dbo.PUL_Member.[Key] WHERE MemberKey = @MemberKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
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
        public static DataTable GetList(DateTime fromdate, DateTime todate, int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_ForSale.CooperativeKey, dbo.PUL_ForSale.MemberKey, dbo.PUL_ForSale.Bill, dbo.PUL_ForSale.PlaceOfBuy, dbo.PUL_ForSale.Slot, dbo.PUL_ForSale.Weight, 
                      dbo.PUL_ForSale.Code, dbo.PUL_ForSale.Datetime, dbo.PUL_ForSale.ForSaleKey, dbo.PUL_Seeds.SeedsName
FROM         dbo.PUL_ForSale INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_ForSale.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_ForSale.MemberKey = dbo.PUL_Member.[Key] where dbo.PUL_ForSale.[Datetime] >= @fromdate and dbo.PUL_ForSale.[Datetime] <= @todate and MemberKey = @MemberKey";
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
        public static DataTable GetAddressList()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"Select PlaceOfBuy from PUL_ForSale group by PlaceOfBuy";
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
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_ForSale";

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
