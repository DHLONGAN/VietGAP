using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.System
{
    public class TimeLimit_Cooperative_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string search, int ID, int GroupKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_TimeLimit_Cooperative.ID, dbo.PUL_TimeLimit_Cooperative.TimeLimit, dbo.PUL_Cooperative.Cooperative_Name
                        FROM         dbo.PUL_Cooperative INNER JOIN
                      dbo.PUL_TimeLimit_Cooperative ON dbo.PUL_Cooperative.Cooperative_Key = dbo.PUL_TimeLimit_Cooperative.CooperativeKey";
            switch (GroupKey)
            {
                case 2:
                    zSQL += "  where dbo.PUL_TimeLimit_Cooperative.CooperativeKey = @ID";
                    if (search != "")
                    {
                        zSQL += " and dbo.PUL_Cooperative.Cooperative_Name like '%'+@search+'%'";
                    }
                    break;
                case 3:
                    zSQL += "  where dbo.PUL_TimeLimit_Cooperative.CooperativeKey IN(SELECT Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @ID)";
                    if (search != "")
                    {
                        zSQL += " and dbo.PUL_Cooperative.Cooperative_Name like '%'+@search+'%'";
                    }
                    break;
                case 4:                   
                    if (search != "")
                    {
                        zSQL += " where dbo.PUL_Cooperative.Cooperative_Name like '%'+@search+'%'";
                    }
                    break;
            }
            
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
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

        public static int Count(string search, int ID, int GroupKey)
        {
            int _Result = 0;
            string zSQL = @"SELECT COUNT(*)     FROM         dbo.PUL_Cooperative INNER JOIN  dbo.PUL_TimeLimit_Cooperative ON dbo.PUL_Cooperative.Cooperative_Key = dbo.PUL_TimeLimit_Cooperative.CooperativeKey ";
            switch (GroupKey)
            {
                case 2:
                    zSQL += "  where dbo.PUL_TimeLimit_Cooperative.ID = @ID";
                    if (search != "")
                    {
                        zSQL += " and dbo.PUL_Cooperative.Cooperative_Name like '%'+@search+'%'";
                    }
                    break;
                case 3:
                    zSQL += "  where dbo.PUL_Cooperative.Cooperative_Key IN(SELECT Cooperative_Key FROM PUL_ListCooperative WHERE CooperativeVenturesKey = @ID)";
                    if (search != "")
                    {
                        zSQL += " and dbo.PUL_Cooperative.Cooperative_Name like '%'+@search+'%'";
                    }
                    break;
                case 4:
                    if (search != "")
                    {
                        zSQL += " where dbo.PUL_Cooperative.Cooperative_Name like '%'+@search+'%'";
                    }
                    break;
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(zSQL, connection);
                selectCommand.Parameters.Add("@search", SqlDbType.NVarChar).Value = search;
                selectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
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
