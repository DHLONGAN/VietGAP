using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
    public class Process_LandUse_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string search, int Cooperative_Key)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_ProcessPlant.ProcessPlantKey, dbo.PUL_ProcessPlant.Cooperative_Key, dbo.PUL_ProcessPlant_Type.Name, 
                           dbo.PUL_ProcessPlant.ProcessPlantName
                           FROM         dbo.PUL_Seeds INNER JOIN
                           dbo.PUL_ProcessPlant ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_ProcessPlant.SeedsKey INNER JOIN
                           dbo.PUL_ProcessPlant_Type ON dbo.PUL_ProcessPlant.ProcessPlant_Type = dbo.PUL_ProcessPlant_Type.ProcessPlant_TypeKey";
            if (Cooperative_Key != 0)
            {
                zSQL += " where dbo.PUL_ProcessPlant.Cooperative_Key = @Cooperative_Key";
                if (search != "")
                {
                    zSQL += " and dbo.PUL_ProcessPlant.ProcessPlantName like '%'+@search+'%'";
                }
            }
            else
            {
                if (search != "")
                {
                    zSQL += " where dbo.PUL_ProcessPlant.ProcessPlantName like '%'+@search+'%'";
                }
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = Cooperative_Key;
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

        public static int Count(string search, int Cooperative_Key)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  PUL_ProcessPlant";
            if (Cooperative_Key != 0)
            {
                cmdText += " where Cooperative_Key = @Cooperative_Key";
                if (search != "")
                {
                    cmdText += " and ProcessPlantName like '%'+@search+'%'";
                }
            }
            else
            {
                if (search != "")
                {
                    cmdText += " where ProcessPlantName like '%'+@search+'%'";
                }
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = Cooperative_Key;
                selectCommand.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
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
