using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Assessment_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Pollution.PollutionName, dbo.PUL_Assessment.AssessmentKey, dbo.PUL_Assessment.AssessmentName, dbo.PUL_Assessment.Status, dbo.PUL_Assessment.Solution
            FROM         dbo.PUL_Assessment INNER JOIN
                      dbo.PUL_Pollution ON dbo.PUL_Assessment.PollutionKey = dbo.PUL_Pollution.PollutionKey ";
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
