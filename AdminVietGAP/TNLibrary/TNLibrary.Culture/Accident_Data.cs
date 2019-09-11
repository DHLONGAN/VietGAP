using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Accident_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Seeds.SeedsName, dbo.PUL_Equipment.EquipmentName, dbo.PUL_Accident.AccidentKey, dbo.PUL_Accident.Datetime, dbo.PUL_Accident.Code, 
                      dbo.PUL_Accident.Treatments, dbo.PUL_Accident.Notice, dbo.PUL_Accident.MemberKey, dbo.PUL_Accident.CooperativeKey
FROM         dbo.PUL_Accident INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Accident.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_Accident.EquipmentKey = dbo.PUL_Equipment.EquipmentKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Accident.MemberKey = dbo.PUL_Member.[Key]";
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
        public static DataTable GetList(DateTime fromdate, DateTime todate)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Member.Name, dbo.PUL_Seeds.SeedsName, dbo.PUL_Equipment.EquipmentName, dbo.PUL_Accident.AccidentKey, dbo.PUL_Accident.Datetime, dbo.PUL_Accident.Code, 
                      dbo.PUL_Accident.Treatments, dbo.PUL_Accident.Notice, dbo.PUL_Accident.MemberKey, dbo.PUL_Accident.CooperativeKey
FROM         dbo.PUL_Accident INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Accident.SeedsKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_Accident.EquipmentKey = dbo.PUL_Equipment.EquipmentKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Accident.MemberKey = dbo.PUL_Member.[Key] where dbo.PUL_Accident.[Datetime] >= @fromdate and dbo.PUL_Accident.[Datetime] <= @todate";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
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
    }
}
