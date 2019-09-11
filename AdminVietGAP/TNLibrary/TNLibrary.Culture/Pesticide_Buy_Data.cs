using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Pesticide_Buy_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber, string SeedsName)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Companies.CompanyName, dbo.PUL_Pesticide_Buy.PesticideBuyKey, dbo.PUL_Pesticide_Buy.DatetimeBuy, dbo.PUL_Pesticide_Buy.Quantity, 
                      dbo.PUL_Pesticide_Buy.Price, dbo.PUL_Pesticide_Buy.Address, dbo.PUL_Member.Name, dbo.PUL_Pesticides.Trade_Name, 
                      dbo.PUL_Pesticide_Common.Common_Name, dbo.PUL_Seeds.SeedsName, dbo.PUL_Unit.Name AS UNN
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Pesticide_Buy.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Pesticide_Common ON dbo.PUL_Pesticides.Common_Key = dbo.PUL_Pesticide_Common.Common_Key INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Pesticides.CompanyKey = dbo.PUL_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Pesticide_Buy.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_Pesticide_Buy.UnitKey = dbo.PUL_Unit.ID  WHERE MemberKey = @MemberKey AND dbo.PUL_Pesticide_Buy.SeedsKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) order by dbo.PUL_Pesticide_Buy.DatetimeBuy desc";
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
            string zSQL = @"SELECT     dbo.PUL_Companies.CompanyName, dbo.PUL_Pesticide_Buy.PesticideBuyKey, dbo.PUL_Pesticide_Buy.DatetimeBuy, dbo.PUL_Pesticide_Buy.Quantity, 
                      dbo.PUL_Pesticide_Buy.Price, dbo.PUL_Pesticide_Buy.Address, dbo.PUL_Member.Name, dbo.PUL_Pesticides.Trade_Name, 
                      dbo.PUL_Pesticide_Common.Common_Name, dbo.PUL_Seeds.SeedsName, dbo.PUL_Unit.Name AS UNN
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Pesticide_Buy.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Pesticide_Common ON dbo.PUL_Pesticides.Common_Key = dbo.PUL_Pesticide_Common.Common_Key INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Pesticides.CompanyKey = dbo.PUL_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Pesticide_Buy.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_Pesticide_Buy.UnitKey = dbo.PUL_Unit.ID where dbo.PUL_Pesticide_Buy.[DatetimeBuy] >= @fromdate and dbo.PUL_Pesticide_Buy.[DatetimeBuy] <= @todate and MemberKey = @MemberKey  AND dbo.PUL_Pesticide_Buy.SeedsKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) order by dbo.PUL_Pesticide_Buy.DatetimeBuy desc";
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
        public static DataTable GetAddressList()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"Select Address from PUL_Pesticide_Buy group by Address";
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

        public static int Count(int MemberKey, string SeedsName)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Pesticide_Buy WHERE MemberKey = @MemberKey and dbo.PUL_Pesticide_Buy.SeedsKey IN (
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
