using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
    public class Cooperative_Data
    {
        public static DataTable GetList(int CooperativeVenturesKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  Cooperative_Key,Cooperative_Name FROM PUL_Cooperative where Cooperative_Key IN (Select Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @CooperativeVenturesKey )";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CooperativeVenturesKey", SqlDbType.Int).Value = CooperativeVenturesKey;
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
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Cooperative ";
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
