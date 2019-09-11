using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
public class Process_SeedProces_Info
{
 
#region [ Field Name ]
private int _SeedProcessKey = 0;
private int _ProcessPlantDetailKey = 0;
private string _SeedsKey = "";
private string _CompanyName = "";
private int _DateOfManufacture_Num = 0;
private int _DateSowing_Num = 0;
private int _DateBuy_Num = 0;
private float _Quantity;
private bool _Status;
private int _PesticideKey = 0;
private string _Reasons = "";
private int _CooperativeKey = 0;
private string _Parcel = "";
private float _Area;
private int _QuantityUnit = 0;
private int _AreaUnit = 0;
private float _Total;
private int _EndTime_Num = 0;
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Process_SeedProces_Info()
{
}
public Process_SeedProces_Info(int SeedProcessKey)
{
string zSQL = "SELECT * FROM PUL_Process_SeedProcess WHERE SeedProcessKey = @SeedProcessKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = SeedProcessKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_SeedProcessKey = int.Parse(zReader["SeedProcessKey"].ToString());
_ProcessPlantDetailKey = int.Parse(zReader["ProcessPlantDetailKey"].ToString());
_SeedsKey = zReader["SeedsKey"].ToString();
_CompanyName = zReader["CompanyName"].ToString();
_DateOfManufacture_Num = int.Parse(zReader["DateOfManufacture_Num"].ToString());
_DateSowing_Num = int.Parse(zReader["DateSowing_Num"].ToString());
_DateBuy_Num = int.Parse(zReader["DateBuy_Num"].ToString());
_Quantity = float.Parse(zReader["Quantity"].ToString());
_Status = (bool)zReader["Status"];
_PesticideKey = int.Parse(zReader["PesticideKey"].ToString());
_Reasons = zReader["Reasons"].ToString();
_CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
_Parcel = zReader["Parcel"].ToString();
_Area = float.Parse(zReader["Area"].ToString());
_QuantityUnit = int.Parse(zReader["QuantityUnit"].ToString());
_AreaUnit = int.Parse(zReader["AreaUnit"].ToString());
_Total = float.Parse(zReader["Total"].ToString());
_EndTime_Num = int.Parse(zReader["EndTime_Num"].ToString());
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
public int SeedProcessKey
{
get { return _SeedProcessKey; }
set { _SeedProcessKey = value; }
}
public int ProcessPlantDetailKey
{
get { return _ProcessPlantDetailKey; }
set { _ProcessPlantDetailKey = value; }
}
public string SeedsKey
{
get { return _SeedsKey; }
set { _SeedsKey = value; }
}
public string CompanyName
{
get { return _CompanyName; }
set { _CompanyName = value; }
}
public int DateOfManufacture_Num
{
get { return _DateOfManufacture_Num; }
set { _DateOfManufacture_Num = value; }
}
public int DateSowing_Num
{
get { return _DateSowing_Num; }
set { _DateSowing_Num = value; }
}
public int DateBuy_Num
{
get { return _DateBuy_Num; }
set { _DateBuy_Num = value; }
}
public float Quantity
{
get { return _Quantity; }
set { _Quantity = value; }
}
public bool Status
{
get { return _Status; }
set { _Status = value; }
}
public int PesticideKey
{
get { return _PesticideKey; }
set { _PesticideKey = value; }
}
public string Reasons
{
get { return _Reasons; }
set { _Reasons = value; }
}
public int CooperativeKey
{
get { return _CooperativeKey; }
set { _CooperativeKey = value; }
}
public string Parcel
{
get { return _Parcel; }
set { _Parcel = value; }
}
public float Area
{
get { return _Area; }
set { _Area = value; }
}
public int QuantityUnit
{
get { return _QuantityUnit; }
set { _QuantityUnit = value; }
}
public int AreaUnit
{
get { return _AreaUnit; }
set { _AreaUnit = value; }
}
public float Total
{
get { return _Total; }
set { _Total = value; }
}
public int EndTime_Num
{
get { return _EndTime_Num; }
set { _EndTime_Num = value; }
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
    string zSQL = "INSERT INTO PUL_Process_SeedProcess (" 
+ " ProcessPlantDetailKey ,SeedsKey ,CompanyName ,DateOfManufacture_Num ,DateSowing_Num ,DateBuy_Num ,Quantity ,Status ,PesticideKey ,Reasons ,CooperativeKey ,Parcel ,Area ,QuantityUnit ,AreaUnit ,Total ,EndTime_Num ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@ProcessPlantDetailKey ,@SeedsKey ,@CompanyName ,@DateOfManufacture_Num ,@DateSowing_Num ,@DateBuy_Num ,@Quantity ,@Status ,@PesticideKey ,@Reasons ,@CooperativeKey ,@Parcel ,@Area ,@QuantityUnit ,@AreaUnit ,@Total ,@EndTime_Num ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = _ProcessPlantDetailKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.NVarChar).Value = _SeedsKey;
zCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = _CompanyName;
zCommand.Parameters.Add("@DateOfManufacture_Num", SqlDbType.Int).Value = _DateOfManufacture_Num;
zCommand.Parameters.Add("@DateSowing_Num", SqlDbType.Int).Value = _DateSowing_Num;
zCommand.Parameters.Add("@DateBuy_Num", SqlDbType.Int).Value = _DateBuy_Num;
zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
zCommand.Parameters.Add("@Reasons", SqlDbType.NVarChar).Value = _Reasons;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Parcel", SqlDbType.NVarChar).Value = _Parcel;
zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
zCommand.Parameters.Add("@QuantityUnit", SqlDbType.Int).Value = _QuantityUnit;
zCommand.Parameters.Add("@AreaUnit", SqlDbType.Int).Value = _AreaUnit;
zCommand.Parameters.Add("@Total", SqlDbType.Float).Value = _Total;
zCommand.Parameters.Add("@EndTime_Num", SqlDbType.Int).Value = _EndTime_Num;
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
string zSQL = "UPDATE PUL_Process_SeedProcess SET " 
            + " ProcessPlantDetailKey = @ProcessPlantDetailKey,"
            + " SeedsKey = @SeedsKey,"
            + " CompanyName = @CompanyName,"
            + " DateOfManufacture_Num = @DateOfManufacture_Num,"
            + " DateSowing_Num = @DateSowing_Num,"
            + " DateBuy_Num = @DateBuy_Num,"
            + " Quantity = @Quantity,"
            + " Status = @Status,"
            + " PesticideKey = @PesticideKey,"
            + " Reasons = @Reasons,"
            + " CooperativeKey = @CooperativeKey,"
            + " Parcel = @Parcel,"
            + " Area = @Area,"
            + " QuantityUnit = @QuantityUnit,"
            + " AreaUnit = @AreaUnit,"
            + " Total = @Total,"
            + " EndTime_Num = @EndTime_Num,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE SeedProcessKey = @SeedProcessKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = _ProcessPlantDetailKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.NVarChar).Value = _SeedsKey;
zCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = _CompanyName;
zCommand.Parameters.Add("@DateOfManufacture_Num", SqlDbType.Int).Value = _DateOfManufacture_Num;
zCommand.Parameters.Add("@DateSowing_Num", SqlDbType.Int).Value = _DateSowing_Num;
zCommand.Parameters.Add("@DateBuy_Num", SqlDbType.Int).Value = _DateBuy_Num;
zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
zCommand.Parameters.Add("@Reasons", SqlDbType.NVarChar).Value = _Reasons;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Parcel", SqlDbType.NVarChar).Value = _Parcel;
zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
zCommand.Parameters.Add("@QuantityUnit", SqlDbType.Int).Value = _QuantityUnit;
zCommand.Parameters.Add("@AreaUnit", SqlDbType.Int).Value = _AreaUnit;
zCommand.Parameters.Add("@Total", SqlDbType.Float).Value = _Total;
zCommand.Parameters.Add("@EndTime_Num", SqlDbType.Int).Value = _EndTime_Num;
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
if (_SeedProcessKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Process_SeedProcess WHERE SeedProcessKey = @SeedProcessKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
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
