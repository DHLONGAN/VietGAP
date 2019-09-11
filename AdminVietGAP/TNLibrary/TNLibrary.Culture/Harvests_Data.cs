using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Harvests_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_Member.Name, dbo.PUL_Harvests.MemberKey, dbo.PUL_Harvests.CooperativeKey, dbo.PUL_Harvests.Loss, dbo.PUL_Harvests.SlotAfter, 
                      dbo.PUL_Harvests.WeightAfter, dbo.PUL_Harvests.SlotBefor, dbo.PUL_Harvests.WeightBefor, dbo.PUL_Harvests.Code, dbo.PUL_Harvests.DateOn, dbo.PUL_Harvests.HarvestsKey
FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_Harvests ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_Harvests.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Harvests.MemberKey = dbo.PUL_Member.[Key] WHERE MemberKey = @MemberKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
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
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_Member.Name, dbo.PUL_Harvests.MemberKey, dbo.PUL_Harvests.CooperativeKey, dbo.PUL_Harvests.Loss, dbo.PUL_Harvests.SlotAfter, 
                      dbo.PUL_Harvests.WeightAfter, dbo.PUL_Harvests.SlotBefor, dbo.PUL_Harvests.WeightBefor, dbo.PUL_Harvests.Code, dbo.PUL_Harvests.DateOn, dbo.PUL_Harvests.HarvestsKey
FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_Harvests ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_Harvests.SeedsKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Harvests.MemberKey = dbo.PUL_Member.[Key] where dbo.PUL_Harvests.[DateOn] >= @fromdate and dbo.PUL_Harvests.[DateOn] <= @todate and MemberKey = @MemberKey";
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

        public static int Count(int MemberKey)
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
