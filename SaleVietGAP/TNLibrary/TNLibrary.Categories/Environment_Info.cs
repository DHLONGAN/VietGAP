using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Categories
{
public class Environment_Info
{
 
#region [ Field Name ]
private int _EnvironmentsKey = 0;
private string _EnvironmentsName = "";
private string _Notice = "";
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Environment_Info()
{
}
public Environment_Info(int EnvironmentsKey)
{
string zSQL = "SELECT * FROM PUL_Environments WHERE EnvironmentsKey = @EnvironmentsKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@EnvironmentsKey", SqlDbType.Int).Value = EnvironmentsKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_EnvironmentsKey = int.Parse(zReader["EnvironmentsKey"].ToString());
_EnvironmentsName = zReader["EnvironmentsName"].ToString();
_Notice = zReader["Notice"].ToString();
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int EnvironmentsKey
{
get { return _EnvironmentsKey; }
set { _EnvironmentsKey = value; }
}
public string EnvironmentsName
{
get { return _EnvironmentsName; }
set { _EnvironmentsName = value; }
}
public string Notice
{
get { return _Notice; }
set { _Notice = value; }
}
public string Message 
{
get { return _Message; }
set { _Message = value; }
}
#endregion
 
#region [ Constructor Update Information ]
 
public string Create()
{
//---------- String SQL Access Database ---------------
    string zSQL = "INSERT INTO PUL_Environments (" 
+ " EnvironmentsName ,Notice ) "
 + " VALUES ( "
 + "@EnvironmentsName ,@Notice ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@EnvironmentsKey", SqlDbType.Int).Value = _EnvironmentsKey;
zCommand.Parameters.Add("@EnvironmentsName", SqlDbType.NVarChar).Value = _EnvironmentsName;
zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
zResult = zCommand.ExecuteNonQuery().ToString();
zCommand.Dispose();
}
catch (Exception Err)
{
 _Message = Err.ToString();
}
 finally
 {
     zConnect.Close();
  }
  return zResult;
  }

 
public string Update() 
{ 
string zSQL = "UPDATE PUL_Environments SET " 
            + " EnvironmentsName = @EnvironmentsName,"
            + " Notice = @Notice"
           + " WHERE EnvironmentsKey = @EnvironmentsKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@EnvironmentsKey", SqlDbType.Int).Value = _EnvironmentsKey;
zCommand.Parameters.Add("@EnvironmentsName", SqlDbType.NVarChar).Value = _EnvironmentsName;
zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
zResult = zCommand.ExecuteNonQuery().ToString();
zCommand.Dispose();
}
catch (Exception Err)
{
 _Message = Err.ToString();
}
 finally
 {
     zConnect.Close();
  }
  return zResult;
  }

 
public string Save()
{
string zResult;
if (_EnvironmentsKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Environments WHERE EnvironmentsKey = @EnvironmentsKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@EnvironmentsKey", SqlDbType.Int).Value = _EnvironmentsKey;
zResult = zCommand.ExecuteNonQuery().ToString();
zCommand.Dispose();
}
catch (Exception Err)
{
_Message = Err.ToString();
}
finally
{
zConnect.Close();
}
return zResult;
}
#endregion
}
}
