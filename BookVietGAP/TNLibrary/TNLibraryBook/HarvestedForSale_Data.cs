using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class HarvestedForSale_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_HarvestedForSale ";
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
        public static DataTable GetListByMember(int MemberKey, string YEAR, string MONTH)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     COUNT(Datetime) AS Count, Datetime
                            FROM         dbo.PUL_HarvestedForSale
                            WHERE     (MemberKey = @MemberKey) AND (MONTH(Datetime) = @Month) AND (YEAR(Datetime) = @Year)
                            GROUP BY Datetime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@YEAR", SqlDbType.NVarChar).Value = YEAR;
                zCommand.Parameters.Add("@MONTH", SqlDbType.NVarChar).Value = MONTH;
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
        public static DataTable GetListByMemberDay(int MemberKey, DateTime DateTimeUse)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey as KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_HarvestedForSale.IsActive
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.Datetime = @DateTimeUse)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateTimeUse", SqlDbType.DateTime).Value = DateTimeUse;
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
    }
}
