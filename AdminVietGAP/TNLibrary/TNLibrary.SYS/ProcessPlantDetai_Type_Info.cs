using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
public class ProcessPlantDetai_Type_Info
{
 
#region [ Field Name ]
private int _ProcessPlantDetai_TypeKey = 0;
private string _Name = "";
private string _NameTables = "";
private string _Description = "";
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public ProcessPlantDetai_Type_Info()
{
}
public ProcessPlantDetai_Type_Info(int ProcessPlantDetai_TypeKey)
{
string zSQL = "SELECT * FROM PUL_ProcessPlantDetai_Type WHERE ProcessPlantDetai_TypeKey = @ProcessPlantDetai_TypeKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProcessPlantDetai_TypeKey", SqlDbType.Int).Value = ProcessPlantDetai_TypeKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_ProcessPlantDetai_TypeKey = int.Parse(zReader["ProcessPlantDetai_TypeKey"].ToString());
_Name = zReader["Name"].ToString();
_NameTables = zReader["NameTables"].ToString();
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
public int ProcessPlantDetai_TypeKey
{
get { return _ProcessPlantDetai_TypeKey; }
set { _ProcessPlantDetai_TypeKey = value; }
}
public string Name
{
get { return _Name; }
set { _Name = value; }
}
public string NameTables
{
get { return _NameTables; }
set { _NameTables = value; }
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
    string zSQL = "INSERT INTO PUL_ProcessPlantDetai_Type (" 
+ " Name ,NameTables ,Description ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@Name ,@NameTables ,@Description ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProcessPlantDetai_TypeKey", SqlDbType.Int).Value = _ProcessPlantDetai_TypeKey;
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
zCommand.Parameters.Add("@NameTables", SqlDbType.NVarChar).Value = _NameTables;
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
string zSQL = "UPDATE PUL_ProcessPlantDetai_Type SET " 
            + " Name = @Name,"
            + " NameTables = @NameTables,"
            + " Description = @Description,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE ProcessPlantDetai_TypeKey = @ProcessPlantDetai_TypeKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProcessPlantDetai_TypeKey", SqlDbType.Int).Value = _ProcessPlantDetai_TypeKey;
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
zCommand.Parameters.Add("@NameTables", SqlDbType.NVarChar).Value = _NameTables;
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
if (_ProcessPlantDetai_TypeKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_ProcessPlantDetai_Type WHERE ProcessPlantDetai_TypeKey = @ProcessPlantDetai_TypeKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@ProcessPlantDetai_TypeKey", SqlDbType.Int).Value = _ProcessPlantDetai_TypeKey;
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
