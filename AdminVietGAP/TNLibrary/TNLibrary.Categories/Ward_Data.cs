﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
    public class Ward_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string Search, int District_ID)
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  ID, District_ID, WardID, Name, Description FROM PUL_Ward WHERE District_ID =@District_ID";
            if (Search != "")
            {
                zSQL += " and Name like N'%'+ @Search +'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand selectCommand = new SqlCommand(zSQL, zConnect);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                selectCommand.Parameters.Add("@District_ID", SqlDbType.Int).Value = District_ID;
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
        public static int Count(string Search, int key)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Ward WHERE District_ID = @key";
            if (Search != "")
            {
                cmdText += " AND Name like N'%'+ @Search +'%'";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                selectCommand.Parameters.Add("@key", SqlDbType.NVarChar).Value = key;
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
