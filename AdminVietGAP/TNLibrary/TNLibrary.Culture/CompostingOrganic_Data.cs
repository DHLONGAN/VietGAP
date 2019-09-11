using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class CompostingOrganic_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_CompostingOrganic.CompostingKey, dbo.PUL_CompostingOrganic.Quantity, dbo.PUL_CompostingOrganic.Method, dbo.PUL_CompostingOrganic.CompostingDates, 
                      dbo.PUL_CompostingOrganic.StartDate, dbo.PUL_Unit.Name, dbo.PUL_FertilizerOrganic.Name AS TenPhan
FROM         dbo.PUL_FertilizerOrganic INNER JOIN
                      dbo.PUL_CompostingOrganic ON dbo.PUL_FertilizerOrganic.FertilizerOrganicKey = dbo.PUL_CompostingOrganic.FertilizerOrganicKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_CompostingOrganic.UnitKey = dbo.PUL_Unit.ID
                       WHERE dbo.PUL_CompostingOrganic.MemberKey =@MemberKey";
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
            string zSQL = @"SELECT     dbo.PUL_CompostingOrganic.CompostingKey, dbo.PUL_CompostingOrganic.Quantity, dbo.PUL_CompostingOrganic.Method, dbo.PUL_CompostingOrganic.CompostingDates, 
                      dbo.PUL_CompostingOrganic.StartDate, dbo.PUL_Unit.Name, dbo.PUL_FertilizerOrganic.Name AS TenPhan
FROM         dbo.PUL_FertilizerOrganic INNER JOIN
                      dbo.PUL_CompostingOrganic ON dbo.PUL_FertilizerOrganic.FertilizerOrganicKey = dbo.PUL_CompostingOrganic.FertilizerOrganicKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_CompostingOrganic.UnitKey = dbo.PUL_Unit.ID
                       WHERE dbo.PUL_CompostingOrganic.MemberKey =@MemberKey and dbo.PUL_CompostingOrganic.[StartDate] >= @fromdate and dbo.PUL_CompostingOrganic.[StartDate] <= @todate";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
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
            string zSQL = @"SELECT     COUNT(StartDate) AS Count, StartDate
                            FROM         dbo.PUL_CompostingOrganic
                            WHERE     (MemberKey = @MemberKey) AND (MONTH(StartDate) = @Month) AND (YEAR(StartDate) = @Year)
                            GROUP BY StartDate";
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
        public static DataTable GetListByMemberDay(int MemberKey, DateTime DatetimeBuy)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_CompostingOrganic.CompostingKey as KeyID, dbo.PUL_CompostingOrganic.FertilizerOrganicKey as ID, dbo.PUL_FertilizerOrganic.Name as Name, dbo.PUL_CompostingOrganic.IsActive
                            FROM         dbo.PUL_CompostingOrganic INNER JOIN
                                                  dbo.PUL_FertilizerOrganic ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_FertilizerOrganic.FertilizerOrganicKey
                            WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.StartDate = @DatetimeBuy)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DatetimeBuy", SqlDbType.DateTime).Value = DatetimeBuy;
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

        public static int Count(int MemberKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_CompostingOrganic WHERE MemberKey = @MemberKey";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
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
