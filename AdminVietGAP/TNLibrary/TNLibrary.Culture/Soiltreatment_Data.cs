using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Soiltreatment_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Soiltreatment.SoiltreatmentKey, dbo.PUL_Soiltreatment.Datetime, dbo.PUL_Soiltreatment.Quantity, dbo.PUL_Soiltreatment.Area, dbo.PUL_Soiltreatment.Howtouse, 
                      dbo.PUL_Soiltreatment.Weather, dbo.PUL_Additives.AdditivesName
                      FROM         dbo.PUL_Additives INNER JOIN
                      dbo.PUL_Soiltreatment ON dbo.PUL_Additives.AdditivesKey = dbo.PUL_Soiltreatment.AdditivesKey";
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
            string zSQL = @"SELECT     dbo.PUL_Soiltreatment.SoiltreatmentKey, dbo.PUL_Soiltreatment.Datetime, dbo.PUL_Soiltreatment.Quantity, dbo.PUL_Soiltreatment.Area, dbo.PUL_Soiltreatment.Howtouse, 
                      dbo.PUL_Soiltreatment.Weather, dbo.PUL_Additives.AdditivesName
                      FROM         dbo.PUL_Additives INNER JOIN
                      dbo.PUL_Soiltreatment ON dbo.PUL_Additives.AdditivesKey = dbo.PUL_Soiltreatment.AdditivesKey where dbo.PUL_Soiltreatment.[Datetime] >= @fromdate and dbo.PUL_Soiltreatment.[Datetime] <= @todate";
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
    }
}
