using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Pesticide_Buy_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Companies.CompanyName, dbo.PUL_Pesticide_Buy.PesticideBuyKey, dbo.PUL_Pesticide_Buy.DatetimeBuy, dbo.PUL_Pesticide_Buy.Quantity, dbo.PUL_Pesticide_Buy.Price, 
                      dbo.PUL_Pesticide_Buy.Address, dbo.PUL_Member.Name, dbo.PUL_Pesticides.Trade_Name
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Pesticide_Buy.CompanyKey = dbo.PUL_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Pesticide_Buy.MemberKey = dbo.PUL_Member.[Key]  WHERE MemberKey = @MemberKey";
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
            string zSQL = @"SELECT     dbo.PUL_Companies.CompanyName, dbo.PUL_Pesticide_Buy.PesticideBuyKey, dbo.PUL_Pesticide_Buy.DatetimeBuy, dbo.PUL_Pesticide_Buy.Quantity, dbo.PUL_Pesticide_Buy.Price, 
                      dbo.PUL_Pesticide_Buy.Address, dbo.PUL_Member.Name, dbo.PUL_Pesticides.Trade_Name
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Pesticide_Buy.CompanyKey = dbo.PUL_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Pesticide_Buy.MemberKey = dbo.PUL_Member.[Key] where dbo.PUL_Pesticide_Buy.[DatetimeBuy] >= @fromdate and dbo.PUL_Pesticide_Buy.[DatetimeBuy] <= @todate and MemberKey = @MemberKey";
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

        public static int Count()
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Pesticide_Buy";

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
