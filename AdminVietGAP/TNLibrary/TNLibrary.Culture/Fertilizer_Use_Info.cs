using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class Fertilizer_Use_Info
{
 
#region [ Field Name ]
private int _FertilizerUseKey = 0;
private DateTime _DateTimeUse ;
private int _SeedKey = 0;
private string _Parcel = "";
private string _Area = "";
private int _FertilizerKey = 0;
private float _FormulaUsed;
private string _Quantity = "";
private string _Howtouse = "";
private int _MemberKey = 0;
private int _CooperativeKey = 0;
private string _QuarantinePeriod = "";
private int _UnitKey = 0;
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Fertilizer_Use_Info()
{
}
public Fertilizer_Use_Info(int FertilizerUseKey)
{
string zSQL = "SELECT * FROM PUL_Fertilizer_Use WHERE FertilizerUseKey = @FertilizerUseKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@FertilizerUseKey", SqlDbType.Int).Value = FertilizerUseKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_FertilizerUseKey = int.Parse(zReader["FertilizerUseKey"].ToString());
if (zReader["DateTimeUse"] != DBNull.Value)
_DateTimeUse = (DateTime)zReader["DateTimeUse"];
_SeedKey = int.Parse(zReader["SeedKey"].ToString());
_Parcel = zReader["Parcel"].ToString();
_Area = zReader["Area"].ToString();
_FertilizerKey = int.Parse(zReader["FertilizerKey"].ToString());
_FormulaUsed = float.Parse(zReader["FormulaUsed"].ToString());
_Quantity = zReader["Quantity"].ToString();
_Howtouse = zReader["Howtouse"].ToString();
_MemberKey = int.Parse(zReader["MemberKey"].ToString());
_CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
_QuarantinePeriod = zReader["QuarantinePeriod"].ToString();
_UnitKey = int.Parse(zReader["UnitKey"].ToString());
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
if (zReader["CreatedDateTime"] != DBNull.Value)
_CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedDateTime"] != DBNull.Value)
_ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int FertilizerUseKey
{
get { return _FertilizerUseKey; }
set { _FertilizerUseKey = value; }
}
public DateTime DateTimeUse
{
get { return _DateTimeUse; }
set { _DateTimeUse = value; }
}
public int SeedKey
{
get { return _SeedKey; }
set { _SeedKey = value; }
}
public string Parcel
{
get { return _Parcel; }
set { _Parcel = value; }
}
public string Area
{
get { return _Area; }
set { _Area = value; }
}
public int FertilizerKey
{
get { return _FertilizerKey; }
set { _FertilizerKey = value; }
}
public float FormulaUsed
{
get { return _FormulaUsed; }
set { _FormulaUsed = value; }
}
public string Quantity
{
get { return _Quantity; }
set { _Quantity = value; }
}
public string Howtouse
{
get { return _Howtouse; }
set { _Howtouse = value; }
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
public string QuarantinePeriod
{
get { return _QuarantinePeriod; }
set { _QuarantinePeriod = value; }
}
public int UnitKey
{
get { return _UnitKey; }
set { _UnitKey = value; }
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
    string zSQL = "INSERT INTO PUL_Fertilizer_Use (" 
+ " DateTimeUse ,SeedKey ,Parcel ,Area ,FertilizerKey ,FormulaUsed ,Quantity ,Howtouse ,MemberKey ,CooperativeKey ,QuarantinePeriod ,UnitKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@DateTimeUse ,@SeedKey ,@Parcel ,@Area ,@FertilizerKey ,@FormulaUsed ,@Quantity ,@Howtouse ,@MemberKey ,@CooperativeKey ,@QuarantinePeriod ,@UnitKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@FertilizerUseKey", SqlDbType.Int).Value = _FertilizerUseKey;
if (_DateTimeUse.Year == 0001) 
zCommand.Parameters.Add("@DateTimeUse", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@DateTimeUse", SqlDbType.DateTime).Value = _DateTimeUse;
zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
zCommand.Parameters.Add("@Parcel", SqlDbType.NVarChar).Value = _Parcel;
zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
zCommand.Parameters.Add("@FertilizerKey", SqlDbType.Int).Value = _FertilizerKey;
zCommand.Parameters.Add("@FormulaUsed", SqlDbType.Float).Value = _FormulaUsed;
zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
zCommand.Parameters.Add("@Howtouse", SqlDbType.NVarChar).Value = _Howtouse;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@QuarantinePeriod", SqlDbType.NVarChar).Value = _QuarantinePeriod;
zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
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
string zSQL = "UPDATE PUL_Fertilizer_Use SET " 
            + " DateTimeUse = @DateTimeUse,"
            + " SeedKey = @SeedKey,"
            + " Parcel = @Parcel,"
            + " Area = @Area,"
            + " FertilizerKey = @FertilizerKey,"
            + " FormulaUsed = @FormulaUsed,"
            + " Quantity = @Quantity,"
            + " Howtouse = @Howtouse,"
            + " MemberKey = @MemberKey,"
            + " CooperativeKey = @CooperativeKey,"
            + " QuarantinePeriod = @QuarantinePeriod,"
            + " UnitKey = @UnitKey,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE FertilizerUseKey = @FertilizerUseKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@FertilizerUseKey", SqlDbType.Int).Value = _FertilizerUseKey;
if (_DateTimeUse.Year == 0001) 
zCommand.Parameters.Add("@DateTimeUse", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@DateTimeUse", SqlDbType.DateTime).Value = _DateTimeUse;
zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
zCommand.Parameters.Add("@Parcel", SqlDbType.NVarChar).Value = _Parcel;
zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
zCommand.Parameters.Add("@FertilizerKey", SqlDbType.Int).Value = _FertilizerKey;
zCommand.Parameters.Add("@FormulaUsed", SqlDbType.Float).Value = _FormulaUsed;
zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
zCommand.Parameters.Add("@Howtouse", SqlDbType.NVarChar).Value = _Howtouse;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@QuarantinePeriod", SqlDbType.NVarChar).Value = _QuarantinePeriod;
zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
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
if (_FertilizerUseKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Fertilizer_Use WHERE FertilizerUseKey = @FertilizerUseKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@FertilizerUseKey", SqlDbType.Int).Value = _FertilizerUseKey;
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
