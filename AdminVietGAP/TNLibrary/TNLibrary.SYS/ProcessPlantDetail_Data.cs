using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
    public class ProcessPlantDetail_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string search, int ProcessPlantKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_ProcessPlantDetail.ProcessPlantDetailKey, dbo.PUL_ProcessPlantDetail.ProcessPlantDetai_Type, dbo.PUL_ProcessPlantDetai_Type.Name, 
                      dbo.PUL_ProcessPlantDetail.ProcessPlantKey
                      FROM         dbo.PUL_ProcessPlantDetail INNER JOIN
                      dbo.PUL_ProcessPlantDetai_Type ON dbo.PUL_ProcessPlantDetail.ProcessPlantDetai_Type = dbo.PUL_ProcessPlantDetai_Type.ProcessPlantDetai_TypeKey where dbo.PUL_ProcessPlantDetail.ProcessPlantKey = @ProcessPlantKey";
            if (search != "")
            {
                zSQL += " and dbo.PUL_ProcessPlantDetai_Type.Name like '%'+@search+'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
                zCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = ProcessPlantKey;
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

        public static int Count(string search, int ProcessPlantKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM         dbo.PUL_ProcessPlantDetail INNER JOIN
                      dbo.PUL_ProcessPlantDetai_Type ON dbo.PUL_ProcessPlantDetail.ProcessPlantDetai_Type = dbo.PUL_ProcessPlantDetai_Type.ProcessPlantDetai_TypeKey  where dbo.PUL_ProcessPlantDetail.ProcessPlantKey = @ProcessPlantKey";
            if (search != "")
            {
                cmdText += " and dbo.PUL_ProcessPlantDetai_Type.Name like '%'+@search+'%'";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
                selectCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = ProcessPlantKey;
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
