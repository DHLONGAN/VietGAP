using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Pesticide_Use_Data
    {
        public static DataTable GetList(int MemberKey, int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Equipment.EquipmentName, dbo.PUL_Pesticides.Trade_Name, dbo.PUL_Member.Name, dbo.PUL_Pesticide_Use.PesticideUseKey, dbo.PUL_Pesticide_Use.DateTimeUse, 
                      dbo.PUL_Seeds.SeedsName, dbo.PUL_Pesticide_Use.PestName, dbo.PUL_Pesticide_Use.Area, dbo.PUL_Pesticide_Use.Dose, dbo.PUL_Pesticide_Use.Dosage
FROM         dbo.PUL_Equipment INNER JOIN
                      dbo.PUL_Pesticide_Use ON dbo.PUL_Equipment.EquipmentKey = dbo.PUL_Pesticide_Use.EquipmentKey INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Pesticide_Use.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Pesticide_Use.SeedKey = dbo.PUL_Seeds.SeedsKey";
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
            string zSQL = @"SELECT     dbo.PUL_Equipment.EquipmentName, dbo.PUL_Pesticides.Trade_Name, dbo.PUL_Member.Name, dbo.PUL_Pesticide_Use.PesticideUseKey, dbo.PUL_Pesticide_Use.DateTimeUse, 
                      dbo.PUL_Seeds.SeedsName, dbo.PUL_Pesticide_Use.PestName, dbo.PUL_Pesticide_Use.Area, dbo.PUL_Pesticide_Use.Dose, dbo.PUL_Pesticide_Use.Dosage
FROM         dbo.PUL_Equipment INNER JOIN
                      dbo.PUL_Pesticide_Use ON dbo.PUL_Equipment.EquipmentKey = dbo.PUL_Pesticide_Use.EquipmentKey INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Pesticide_Use.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Pesticide_Use.SeedKey = dbo.PUL_Seeds.SeedsKey where dbo.PUL_Pesticide_Use.[DatetimeUse] >= @fromdate and dbo.PUL_Pesticide_Use.[DatetimeUse] <= @todate and MemberKey = @MemberKey";
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
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Pesticide_Use";

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
