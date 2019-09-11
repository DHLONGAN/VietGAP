using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
public class Process_PesticideUse_Info
{
 
#region [ Field Name ]
private int _PesticideUseKey = 0;
private int _ProcessPlantDetailKey = 0;
private int _DateTimeUse_Num = 0;
private int _SeedKey = 0;
private string _PestName = "";
private string _Area = "";
private int _PesticideKey = 0;
private float _Dose;
private string _Dosage = "";
private int _EquipmentKey = 0;
private int _CooperativeKey = 0;
private string _Solution = "";
private string _QuarantinePeriod = "";
private int _UnitKey = 0;
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Process_PesticideUse_Info()
{
}
public Process_PesticideUse_Info(int PesticideUseKey)
{
string zSQL = "SELECT * FROM PUL_Process_PesticideUse WHERE PesticideUseKey = @PesticideUseKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@PesticideUseKey", SqlDbType.Int).Value = PesticideUseKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_PesticideUseKey = int.Parse(zReader["PesticideUseKey"].ToString());
_ProcessPlantDetailKey = int.Parse(zReader["ProcessPlantDetailKey"].ToString());
_DateTimeUse_Num = int.Parse(zReader["DateTimeUse_Num"].ToString());
_SeedKey = int.Parse(zReader["SeedKey"].ToString());
_PestName = zReader["PestName"].ToString();
_Area = zReader["Area"].ToString();
_PesticideKey = int.Parse(zReader["PesticideKey"].ToString());
_Dose = float.Parse(zReader["Dose"].ToString());
_Dosage = zReader["Dosage"].ToString();
_EquipmentKey = int.Parse(zReader["EquipmentKey"].ToString());
_CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
_Solution = zReader["Solution"].ToString();
_QuarantinePeriod = zReader["QuarantinePeriod"].ToString();
_UnitKey = int.Parse(zReader["UnitKey"].ToString());
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
public int PesticideUseKey
{
get { return _PesticideUseKey; }
set { _PesticideUseKey = value; }
}
public int ProcessPlantDetailKey
{
get { return _ProcessPlantDetailKey; }
set { _ProcessPlantDetailKey = value; }
}
public int DateTimeUse_Num
{
get { return _DateTimeUse_Num; }
set { _DateTimeUse_Num = value; }
}
public int SeedKey
{
get { return _SeedKey; }
set { _SeedKey = value; }
}
public string PestName
{
get { return _PestName; }
set { _PestName = value; }
}
public string Area
{
get { return _Area; }
set { _Area = value; }
}
public int PesticideKey
{
get { return _PesticideKey; }
set { _PesticideKey = value; }
}
public float Dose
{
get { return _Dose; }
set { _Dose = value; }
}
public string Dosage
{
get { return _Dosage; }
set { _Dosage = value; }
}
public int EquipmentKey
{
get { return _EquipmentKey; }
set { _EquipmentKey = value; }
}
public int CooperativeKey
{
get { return _CooperativeKey; }
set { _CooperativeKey = value; }
}
public string Solution
{
get { return _Solution; }
set { _Solution = value; }
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
    string zSQL = "INSERT INTO PUL_Process_PesticideUse (" 
+ " ProcessPlantDetailKey ,DateTimeUse_Num ,SeedKey ,PestName ,Area ,PesticideKey ,Dose ,Dosage ,EquipmentKey ,CooperativeKey ,Solution ,QuarantinePeriod ,UnitKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@ProcessPlantDetailKey ,@DateTimeUse_Num ,@SeedKey ,@PestName ,@Area ,@PesticideKey ,@Dose ,@Dosage ,@EquipmentKey ,@CooperativeKey ,@Solution ,@QuarantinePeriod ,@UnitKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@PesticideUseKey", SqlDbType.Int).Value = _PesticideUseKey;
zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = _ProcessPlantDetailKey;
zCommand.Parameters.Add("@DateTimeUse_Num", SqlDbType.Int).Value = _DateTimeUse_Num;
zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
zCommand.Parameters.Add("@PestName", SqlDbType.NVarChar).Value = _PestName;
zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
zCommand.Parameters.Add("@Dose", SqlDbType.Float).Value = _Dose;
zCommand.Parameters.Add("@Dosage", SqlDbType.NVarChar).Value = _Dosage;
zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
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
string zSQL = "UPDATE PUL_Process_PesticideUse SET " 
            + " ProcessPlantDetailKey = @ProcessPlantDetailKey,"
            + " DateTimeUse_Num = @DateTimeUse_Num,"
            + " SeedKey = @SeedKey,"
            + " PestName = @PestName,"
            + " Area = @Area,"
            + " PesticideKey = @PesticideKey,"
            + " Dose = @Dose,"
            + " Dosage = @Dosage,"
            + " EquipmentKey = @EquipmentKey,"
            + " CooperativeKey = @CooperativeKey,"
            + " Solution = @Solution,"
            + " QuarantinePeriod = @QuarantinePeriod,"
            + " UnitKey = @UnitKey,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE PesticideUseKey = @PesticideUseKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@PesticideUseKey", SqlDbType.Int).Value = _PesticideUseKey;
zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = _ProcessPlantDetailKey;
zCommand.Parameters.Add("@DateTimeUse_Num", SqlDbType.Int).Value = _DateTimeUse_Num;
zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
zCommand.Parameters.Add("@PestName", SqlDbType.NVarChar).Value = _PestName;
zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
zCommand.Parameters.Add("@Dose", SqlDbType.Float).Value = _Dose;
zCommand.Parameters.Add("@Dosage", SqlDbType.NVarChar).Value = _Dosage;
zCommand.Parameters.Add("@EquipmentKey", SqlDbType.Int).Value = _EquipmentKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
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
if (_PesticideUseKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Process_PesticideUse WHERE PesticideUseKey = @PesticideUseKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@PesticideUseKey", SqlDbType.Int).Value = _PesticideUseKey;
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
