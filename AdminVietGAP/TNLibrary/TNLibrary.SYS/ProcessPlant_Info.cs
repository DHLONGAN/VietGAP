using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
public class ProcessPlant_Info
{
 
#region [ Field Name ]
private int _ProcessPlantKey = 0;
private int _Cooperative_Key = 0;
private int _SeedsKey = 0;
private int _ProcessPlant_Type = 0;
private string _ProcessPlantName = "";
private string _Description = "";
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public ProcessPlant_Info()
{
}
public ProcessPlant_Info(int ProcessPlantKey)
{
string zSQL = "SELECT * FROM PUL_ProcessPlant WHERE ProcessPlantKey = @ProcessPlantKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = ProcessPlantKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_ProcessPlantKey = int.Parse(zReader["ProcessPlantKey"].ToString());
_Cooperative_Key = int.Parse(zReader["Cooperative_Key"].ToString());
_SeedsKey = int.Parse(zReader["SeedsKey"].ToString());
_ProcessPlant_Type = int.Parse(zReader["ProcessPlant_Type"].ToString());
_ProcessPlantName = zReader["ProcessPlantName"].ToString();
_Description = zReader["Description"].ToString();
if (zReader["CreatedBy"] != DBNull.Value)
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
if (zReader["CreatedDateTime"] != DBNull.Value)
_CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
if (zReader["ModifiedBy"] != DBNull.Value)
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedDateTime"] != DBNull.Value)
_ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int ProcessPlantKey
{
get { return _ProcessPlantKey; }
set { _ProcessPlantKey = value; }
}
public int Cooperative_Key
{
get { return _Cooperative_Key; }
set { _Cooperative_Key = value; }
}
public int SeedsKey
{
get { return _SeedsKey; }
set { _SeedsKey = value; }
}
public int ProcessPlant_Type
{
get { return _ProcessPlant_Type; }
set { _ProcessPlant_Type = value; }
}
public string ProcessPlantName
{
get { return _ProcessPlantName; }
set { _ProcessPlantName = value; }
}
public string Description
{
get { return _Description; }
set { _Description = value; }
}
public Guid CreatedBy
{
get { return _CreatedBy; }
set { _CreatedBy = value; }
}
public DateTime CreatedDateTime
{
get { return _CreatedDateTime; }
set { _CreatedDateTime = value; }
}
public Guid ModifiedBy
{
get { return _ModifiedBy; }
set { _ModifiedBy = value; }
}
public DateTime ModifiedDateTime
{
get { return _ModifiedDateTime; }
set { _ModifiedDateTime = value; }
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
    string zSQL = "INSERT INTO PUL_ProcessPlant (" 
+ " Cooperative_Key ,SeedsKey ,ProcessPlant_Type ,ProcessPlantName ,Description ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@Cooperative_Key ,@SeedsKey ,@ProcessPlant_Type ,@ProcessPlantName ,@Description ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = _ProcessPlantKey;
zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
zCommand.Parameters.Add("@ProcessPlant_Type", SqlDbType.Int).Value = _ProcessPlant_Type;
zCommand.Parameters.Add("@ProcessPlantName", SqlDbType.NVarChar).Value = _ProcessPlantName;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
if (_CreatedDateTime.Year == 0001) 
zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
if (_ModifiedDateTime.Year == 0001) 
zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
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
string zSQL = "UPDATE PUL_ProcessPlant SET " 
            + " Cooperative_Key = @Cooperative_Key,"
            + " SeedsKey = @SeedsKey,"
            + " ProcessPlant_Type = @ProcessPlant_Type,"
            + " ProcessPlantName = @ProcessPlantName,"
            + " Description = @Description,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE ProcessPlantKey = @ProcessPlantKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = _ProcessPlantKey;
zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
zCommand.Parameters.Add("@ProcessPlant_Type", SqlDbType.Int).Value = _ProcessPlant_Type;
zCommand.Parameters.Add("@ProcessPlantName", SqlDbType.NVarChar).Value = _ProcessPlantName;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
if (_CreatedDateTime.Year == 0001) 
zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
if (_ModifiedDateTime.Year == 0001) 
zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
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
if (_ProcessPlantKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_ProcessPlant WHERE ProcessPlantKey = @ProcessPlantKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@ProcessPlantKey", SqlDbType.Int).Value = _ProcessPlantKey;
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
