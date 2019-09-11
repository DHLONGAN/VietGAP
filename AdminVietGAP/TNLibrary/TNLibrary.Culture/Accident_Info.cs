using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class Accident_Info
{
 
#region [ Field Name ]
private int _AccidentKey = 0;
private DateTime _Datetime ;
private int _EquipmentKey = 0;
private int _SeedsKey = 0;
private string _Code = "";
private string _Treatments = "";
private string _Notice = "";
private int _MemberKey = 0;
private int _CooperativeKey = 0;
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Accident_Info()
{
}
public Accident_Info(int AccidentKey)
{
string zSQL = "SELECT * FROM PUL_Accident WHERE AccidentKey = @AccidentKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@AccidentKey", SqlDbType.Int).Value = AccidentKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_AccidentKey = int.Parse(zReader["AccidentKey"].ToString());
if (zReader["Datetime"] != DBNull.Value)
_Datetime = (DateTime)zReader["Datetime"];
_EquipmentKey = int.Parse(zReader["EquipmentKey"].ToString());
_SeedsKey = int.Parse(zReader["SeedsKey"].ToString());
_Code = zReader["Code"].ToString();
_Treatments = zReader["Treatments"].ToString();
_Notice = zReader["Notice"].ToString();
_MemberKey = int.Parse(zReader["MemberKey"].ToString());
_CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
if (zReader["CreatedDateTime"] != DBNull.Value)
_CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedDateTime"] != DBNull.Value)
_ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int AccidentKey
{
get { return _AccidentKey; }
set { _AccidentKey = value; }
}
public DateTime Datetime
{
get { return _Datetime; }
set { _Datetime = value; }
}
public int EquipmentKey
{
get { return _EquipmentKey; }
set { _EquipmentKey = value; }
}
public int SeedsKey
{
get { return _SeedsKey; }
set { _SeedsKey = value; }
}
public string Code
{
get { return _Code; }
set { _Code = value; }
}
public string Treatments
{
get { return _Treatments; }
set { _Treatments = value; }
}
public string Notice
{
get { return _Notice; }
set { _Notice = value; }
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
    string zSQL = "INSERT INTO PUL_Accident (" 
+ " Datetime ,EquipmentKey ,SeedsKey ,Code ,Treatments ,Notice ,MemberKey ,CooperativeKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@Datetime ,@EquipmentKey ,@SeedsKey ,@Code ,@Treatments ,@Notice ,@MemberKey ,@CooperativeKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@AccidentKey", SqlDbType.Int).Value = _AccidentKey;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
zCommand.Parameters.Add("@Treatments", SqlDbType.NVarChar).Value = _Treatments;
zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
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
string zSQL = "UPDATE PUL_Accident SET " 
            + " Datetime = @Datetime,"
            + " EquipmentKey = @EquipmentKey,"
            + " SeedsKey = @SeedsKey,"
            + " Code = @Code,"
            + " Treatments = @Treatments,"
            + " Notice = @Notice,"
            + " MemberKey = @MemberKey,"
            + " CooperativeKey = @CooperativeKey,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE AccidentKey = @AccidentKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@AccidentKey", SqlDbType.Int).Value = _AccidentKey;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
zCommand.Parameters.Add("@Treatments", SqlDbType.NVarChar).Value = _Treatments;
zCommand.Parameters.Add("@Notice", SqlDbType.NVarChar).Value = _Notice;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
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
if (_AccidentKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Accident WHERE AccidentKey = @AccidentKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@AccidentKey", SqlDbType.Int).Value = _AccidentKey;
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
