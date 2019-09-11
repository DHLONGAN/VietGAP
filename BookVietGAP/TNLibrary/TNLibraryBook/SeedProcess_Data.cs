using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class SeedProcess_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_SeedProcess ";
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
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds.CategoryKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.CompanyKey, dbo.PUL_Seeds.StatusKey, 
                      dbo.PUL_Seeds.SeasonKey, dbo.PUL_Seeds.Images, dbo.PUL_Seeds.Detail, dbo.PUL_Seeds.TypeKey, dbo.PUL_Seeds_Categories.CategoryName, 
                      dbo.PUL_Seeds_Status.StatusName, dbo.PUL_Seeds_Companies.CompanyName, dbo.PUL_SeedProcess.SeedProcessKey, 
                      dbo.PUL_SeedProcess.SeedsKey AS Expr1, dbo.PUL_SeedProcess.DateOfManufacture, dbo.PUL_SeedProcess.DateBuy, dbo.PUL_SeedProcess.Quantity, 
                      dbo.PUL_SeedProcess.Status, dbo.PUL_SeedProcess.PesticideKey, dbo.PUL_SeedProcess.Reasons, dbo.PUL_SeedProcess.MemberKey, 
                      dbo.PUL_SeedProcess.CooperativeKey, dbo.PUL_SeedProcess.Parcel, dbo.PUL_Member.Name, dbo.PUL_Unit.Name AS UNQ, dbo.PUL_SeedProcess.Area, 
                      PUL_Unit_1.Name AS UNA
FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_Seeds_Categories ON dbo.PUL_Seeds.CategoryKey = dbo.PUL_Seeds_Categories.CategoryKey INNER JOIN
                      dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_SeedProcess.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_SeedProcess.QuantityUnit = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_SeedProcess.AreaUnit = PUL_Unit_1.ID WHERE MemberKey = @MemberKey";
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
        public static DataTable GetList(string datetime, int MemberKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds.CategoryKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.CompanyKey, dbo.PUL_Seeds.StatusKey, 
                      dbo.PUL_Seeds.SeasonKey, dbo.PUL_Seeds.Images, dbo.PUL_Seeds.Detail, dbo.PUL_Seeds.TypeKey, dbo.PUL_Seeds_Categories.CategoryName, 
                      dbo.PUL_Seeds_Status.StatusName, dbo.PUL_Seeds_Companies.CompanyName, dbo.PUL_SeedProcess.SeedProcessKey, 
                      dbo.PUL_SeedProcess.SeedsKey AS Expr1, dbo.PUL_SeedProcess.DateOfManufacture, dbo.PUL_SeedProcess.DateBuy, dbo.PUL_SeedProcess.Quantity, 
                      dbo.PUL_SeedProcess.Status, dbo.PUL_SeedProcess.PesticideKey, dbo.PUL_SeedProcess.Reasons, dbo.PUL_SeedProcess.MemberKey, 
                      dbo.PUL_SeedProcess.CooperativeKey, dbo.PUL_SeedProcess.Parcel, dbo.PUL_Member.Name, dbo.PUL_Unit.Name AS UNQ, dbo.PUL_SeedProcess.Area, 
                      PUL_Unit_1.Name AS UNA
FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_Seeds_Categories ON dbo.PUL_Seeds.CategoryKey = dbo.PUL_Seeds_Categories.CategoryKey INNER JOIN
                      dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_SeedProcess.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_SeedProcess.QuantityUnit = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_SeedProcess.AreaUnit = PUL_Unit_1.ID where dbo.PUL_SeedProcess.[DateOfManufacture] = @datetime and MemberKey = @MemberKey and dbo.PUL_Seeds.SeedsKey IN (Select SeedKey from PUL_Seed_Cooperative where CooperativeKey = 1)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@datetime", SqlDbType.DateTime).Value = datetime;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
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

        public static DataTable GetList(int MemberKey,DateTime EndTime)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds.CategoryKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.CompanyKey, dbo.PUL_Seeds.StatusKey, dbo.PUL_Seeds.SeasonKey, 
                      dbo.PUL_Seeds.Images, dbo.PUL_Seeds.Detail, dbo.PUL_Seeds.TypeKey, dbo.PUL_Seeds_Categories.CategoryName, dbo.PUL_Seeds_Status.StatusName, 
                      dbo.PUL_Seeds_Companies.CompanyName, dbo.PUL_SeedProcess.SeedProcessKey, dbo.PUL_SeedProcess.SeedsKey AS Expr1, dbo.PUL_SeedProcess.DateOfManufacture, 
                      dbo.PUL_SeedProcess.DateBuy, dbo.PUL_SeedProcess.Quantity, dbo.PUL_SeedProcess.Status, dbo.PUL_SeedProcess.PesticideKey, dbo.PUL_SeedProcess.Reasons, 
                      dbo.PUL_SeedProcess.MemberKey, dbo.PUL_SeedProcess.CooperativeKey, dbo.PUL_SeedProcess.Parcel, dbo.PUL_Member.Name, dbo.PUL_Unit.Name AS UNQ, dbo.PUL_SeedProcess.Area, 
                      PUL_Unit_1.Name AS UNA, dbo.PUL_SeedProcess.EndTime
                        FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_Seeds_Categories ON dbo.PUL_Seeds.CategoryKey = dbo.PUL_Seeds_Categories.CategoryKey INNER JOIN
                      dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_SeedProcess.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_SeedProcess.QuantityUnit = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_SeedProcess.AreaUnit = PUL_Unit_1.ID
                        WHERE MemberKey = @MemberKey AND (dbo.PUL_SeedProcess.EndTime >= @EndTime)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = EndTime;
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

        public static DataTable GetListDate(int MemberKey, int Month, int Year)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     COUNT(DateOfManufacture) AS Count, DateOfManufacture
                        FROM         dbo.PUL_SeedProcess WHERE MemberKey = @MemberKey and MONTH(DateOfManufacture) = @Month and YEAR(DateOfManufacture) = @Year 
                        GROUP BY DateOfManufacture";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@Month", SqlDbType.Int).Value = Month;
                zCommand.Parameters.Add("@Year", SqlDbType.Int).Value = Year;
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
        public static DataTable GetListByMemberDay(int MemberKey, DateTime DateOfManufacture)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_SeedProcess.SeedProcessKey, dbo.PUL_SeedProcess.SeedsKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_SeedProcess.IsActive
                            FROM         dbo.PUL_Seeds INNER JOIN
                            dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey
                            WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (dbo.PUL_SeedProcess.DateOfManufacture = @DateOfManufacture)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
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
            string zSQL = @"SELECT     COUNT(DateOfManufacture) AS Count, DateOfManufacture
                            FROM         dbo.PUL_SeedProcess
                            WHERE     (MemberKey = @MemberKey) AND (MONTH(DateOfManufacture) = @Month) AND (YEAR(DateOfManufacture) = @Year)
                            GROUP BY DateOfManufacture";
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
        
    }
}
