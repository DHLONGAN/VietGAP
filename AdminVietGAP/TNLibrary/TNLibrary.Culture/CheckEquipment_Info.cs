using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class CheckEquipment_Info
{
 
#region [ Field Name ]
private int _CheckEquipmentKey = 0;
private int _EquipmentKey = 0;
private string _Action = "";
private string _Info = "";
private DateTime _Datetime ;
private int _MemberKey = 0;
private int _SeedsKey = 0;
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public CheckEquipment_Info()
{
}
public CheckEquipment_Info(int CheckEquipmentKey)
{
string zSQL = "SELECT * FROM PUL_CheckEquipment WHERE CheckEquipmentKey = @CheckEquipmentKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@CheckEquipmentKey", SqlDbType.Int).Value = CheckEquipmentKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_CheckEquipmentKey = int.Parse(zReader["CheckEquipmentKey"].ToString());
_EquipmentKey = int.Parse(zReader["EquipmentKey"].ToString());
_Action = zReader["Action"].ToString();
_Info = zReader["Info"].ToString();
if (zReader["Datetime"] != DBNull.Value)
_Datetime = (DateTime)zReader["Datetime"];
_MemberKey = int.Parse(zReader["MemberKey"].ToString());
_SeedsKey = int.Parse(zReader["SeedsKey"].ToString());
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
if (zReader["CreatedDateTime"] != DBNull.Value)
_CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedDateTime"] != DBNull.Value)
_ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int CheckEquipmentKey
{
get { return _CheckEquipmentKey; }
set { _CheckEquipmentKey = value; }
}
public int EquipmentKey
{
get { return _EquipmentKey; }
set { _EquipmentKey = value; }
}
public string Action
{
get { return _Action; }
set { _Action = value; }
}
public string Info
{
get { return _Info; }
set { _Info = value; }
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
public int SeedsKey
{
get { return _SeedsKey; }
set { _SeedsKey = value; }
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
    string zSQL = "INSERT INTO PUL_CheckEquipment (" 
+ " EquipmentKey ,Action ,Info ,Datetime ,MemberKey ,SeedsKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@EquipmentKey ,@Action ,@Info ,@Datetime ,@MemberKey ,@SeedsKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@CheckEquipmentKey", SqlDbType.Int).Value = _CheckEquipmentKey;
zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
zCommand.Parameters.Add("@Action", SqlDbType.NVarChar).Value = _Action;
zCommand.Parameters.Add("@Info", SqlDbType.NVarChar).Value = _Info;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
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
string zSQL = "UPDATE PUL_CheckEquipment SET " 
            + " EquipmentKey = @EquipmentKey,"
            + " Action = @Action,"
            + " Info = @Info,"
            + " Datetime = @Datetime,"
            + " MemberKey = @MemberKey,"
            + " SeedsKey = @SeedsKey,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE CheckEquipmentKey = @CheckEquipmentKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@CheckEquipmentKey", SqlDbType.Int).Value = _CheckEquipmentKey;
zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
zCommand.Parameters.Add("@Action", SqlDbType.NVarChar).Value = _Action;
zCommand.Parameters.Add("@Info", SqlDbType.NVarChar).Value = _Info;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
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
if (_CheckEquipmentKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_CheckEquipment WHERE CheckEquipmentKey = @CheckEquipmentKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@CheckEquipmentKey", SqlDbType.Int).Value = _CheckEquipmentKey;
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
