using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class Training_Info
{
 
#region [ Field Name ]
private int _TrainingKey = 0;
private DateTime _Datetime ;
private int _MemberKey = 0;
private int _CooperativeKey = 0;
private string _Job = "";
private string _TrainingContent = "";
private DateTime _TrainingTime ;
private string _Trainer = "";
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Training_Info()
{
}
public Training_Info(int TrainingKey)
{
string zSQL = "SELECT * FROM PUL_Training WHERE TrainingKey = @TrainingKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@TrainingKey", SqlDbType.Int).Value = TrainingKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_TrainingKey = int.Parse(zReader["TrainingKey"].ToString());
if (zReader["Datetime"] != DBNull.Value)
_Datetime = (DateTime)zReader["Datetime"];
_MemberKey = int.Parse(zReader["MemberKey"].ToString());
_CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
_Job = zReader["Job"].ToString();
_TrainingContent = zReader["TrainingContent"].ToString();
if (zReader["TrainingTime"] != DBNull.Value)
_TrainingTime = (DateTime)zReader["TrainingTime"];
_Trainer = zReader["Trainer"].ToString();
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
if (zReader["CreatedDateTime"] != DBNull.Value)
_CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedDateTime"] != DBNull.Value)
_ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int TrainingKey
{
get { return _TrainingKey; }
set { _TrainingKey = value; }
}
public DateTime Datetime
{
get { return _Datetime; }
set { _Datetime = value; }
}
public int MemberKey
{
get { return _MemberKey; }
set { _MemberKey = value; }
}
public int CooperativeKey
{
get { return _CooperativeKey; }
set { _CooperativeKey = value; }
}
public string Job
{
get { return _Job; }
set { _Job = value; }
}
public string TrainingContent
{
get { return _TrainingContent; }
set { _TrainingContent = value; }
}
public DateTime TrainingTime
{
get { return _TrainingTime; }
set { _TrainingTime = value; }
}
public string Trainer
{
get { return _Trainer; }
set { _Trainer = value; }
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
    string zSQL = "INSERT INTO PUL_Training (" 
+ " Datetime ,MemberKey ,CooperativeKey ,Job ,TrainingContent ,TrainingTime ,Trainer ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@Datetime ,@MemberKey ,@CooperativeKey ,@Job ,@TrainingContent ,@TrainingTime ,@Trainer ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@TrainingKey", SqlDbType.Int).Value = _TrainingKey;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Job", SqlDbType.NVarChar).Value = _Job;
zCommand.Parameters.Add("@TrainingContent", SqlDbType.NVarChar).Value = _TrainingContent;
if (_TrainingTime.Year == 0001) 
zCommand.Parameters.Add("@TrainingTime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@TrainingTime", SqlDbType.DateTime).Value = _TrainingTime;
zCommand.Parameters.Add("@Trainer", SqlDbType.NVarChar).Value = _Trainer;
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
string zSQL = "UPDATE PUL_Training SET " 
            + " Datetime = @Datetime,"
            + " MemberKey = @MemberKey,"
            + " CooperativeKey = @CooperativeKey,"
            + " Job = @Job,"
            + " TrainingContent = @TrainingContent,"
            + " TrainingTime = @TrainingTime,"
            + " Trainer = @Trainer,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE TrainingKey = @TrainingKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@TrainingKey", SqlDbType.Int).Value = _TrainingKey;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Job", SqlDbType.NVarChar).Value = _Job;
zCommand.Parameters.Add("@TrainingContent", SqlDbType.NVarChar).Value = _TrainingContent;
if (_TrainingTime.Year == 0001) 
zCommand.Parameters.Add("@TrainingTime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@TrainingTime", SqlDbType.DateTime).Value = _TrainingTime;
zCommand.Parameters.Add("@Trainer", SqlDbType.NVarChar).Value = _Trainer;
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
if (_TrainingKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Training WHERE TrainingKey = @TrainingKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@TrainingKey", SqlDbType.Int).Value = _TrainingKey;
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
