using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class SeedProcess_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Seeds.SeedsName, dbo.PUL_SeedProcess.SeedProcessKey, dbo.PUL_SeedProcess.DateOfManufacture, dbo.PUL_SeedProcess.DateBuy, 
                      dbo.PUL_SeedProcess.Quantity, dbo.PUL_SeedProcess.Status, dbo.PUL_SeedProcess.Reasons, dbo.PUL_Pesticides.Trade_Name, dbo.PUL_Companies.CompanyName
FROM         dbo.PUL_Member INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_Member.[Key] = dbo.PUL_SeedProcess.MemberKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_SeedProcess.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Companies.CompanyKey WHERE MemberKey = @MemberKey";
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
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Seeds.SeedsName, dbo.PUL_SeedProcess.SeedProcessKey, dbo.PUL_SeedProcess.DateOfManufacture, dbo.PUL_SeedProcess.DateBuy, 
                      dbo.PUL_SeedProcess.Quantity, dbo.PUL_SeedProcess.Status, dbo.PUL_SeedProcess.Reasons, dbo.PUL_Pesticides.Trade_Name, dbo.PUL_Companies.CompanyName
                      FROM         dbo.PUL_Member INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_Member.[Key] = dbo.PUL_SeedProcess.MemberKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_SeedProcess.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Companies.CompanyKey where dbo.PUL_SeedProcess.[DateBuy] >= @fromdate and dbo.PUL_SeedProcess.[DateBuy] <= @todate and MemberKey = @MemberKey";
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
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_SeedProcess";

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
