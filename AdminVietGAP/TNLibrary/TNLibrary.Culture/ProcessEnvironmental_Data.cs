﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class ProcessEnvironmental_Data
{
public static DataTable GetList()
{
DataTable zTable = new DataTable();
string zSQL = "SELECT  * FROM PUL_ProcessEnvironmental " ;
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
