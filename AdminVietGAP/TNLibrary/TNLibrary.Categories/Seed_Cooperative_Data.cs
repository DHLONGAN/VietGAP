﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
    public class Seed_Cooperative_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string Search, int CooperativeKey )
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seeds.SeedsName, dbo.PUL_Seed_Cooperative.*
                        FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey WHERE CooperativeKey = @CooperativeKey";
            if (Search != "")
            {
                zSQL += " and dbo.PUL_Seeds.SeedsName like N'%'+ @Search +'%'";
            }
            zSQL += " order by dbo.PUL_Seeds.SeedsName";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand selectCommand = new SqlCommand(zSQL, zConnect);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                selectCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                SqlDataAdapter _Adapter = new SqlDataAdapter(selectCommand);
                _Adapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);

                selectCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }

        public static int Count(string Search, int CooperativeKey)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)   FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey WHERE CooperativeKey = @CooperativeKey";
            if (Search != "")
            {
                cmdText += " and dbo.PUL_Seeds.SeedsName like N'%'+ @Search +'%'";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                selectCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
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
