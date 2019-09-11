using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class HarvestedForSale_Info
{
 
#region [ Field Name ]
private int _HarvestedForSaleKey = 0;
private DateTime _Datetime ;
private string _Code = "";
private float _QuantityHarvested;
private float _QuantitySale;
private string _WhereToBuy = "";
private int _MemberKey = 0;
private int _SeedsKey = 0;
private int _UnitKey = 0;
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public HarvestedForSale_Info()
{
}
public HarvestedForSale_Info(int HarvestedForSaleKey)
{
string zSQL = "SELECT * FROM PUL_HarvestedForSale WHERE HarvestedForSaleKey = @HarvestedForSaleKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@HarvestedForSaleKey", SqlDbType.Int).Value = HarvestedForSaleKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_HarvestedForSaleKey = int.Parse(zReader["HarvestedForSaleKey"].ToString());
if (zReader["Datetime"] != DBNull.Value)
_Datetime = (DateTime)zReader["Datetime"];
_Code = zReader["Code"].ToString();
_QuantityHarvested = float.Parse(zReader["QuantityHarvested"].ToString());
_QuantitySale = float.Parse(zReader["QuantitySale"].ToString());
_WhereToBuy = zReader["WhereToBuy"].ToString();
_MemberKey = int.Parse(zReader["MemberKey"].ToString());
_SeedsKey = int.Parse(zReader["SeedsKey"].ToString());
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
public int HarvestedForSaleKey
{
get { return _HarvestedForSaleKey; }
set { _HarvestedForSaleKey = value; }
}
public DateTime Datetime
{
get { return _Datetime; }
set { _Datetime = value; }
}
public string Code
{
get { return _Code; }
set { _Code = value; }
}
public float QuantityHarvested
{
get { return _QuantityHarvested; }
set { _QuantityHarvested = value; }
}
public float QuantitySale
{
get { return _QuantitySale; }
set { _QuantitySale = value; }
}
public string WhereToBuy
{
get { return _WhereToBuy; }
set { _WhereToBuy = value; }
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
    string zSQL = "INSERT INTO PUL_HarvestedForSale (" 
+ " Datetime ,Code ,QuantityHarvested ,QuantitySale ,WhereToBuy ,MemberKey ,SeedsKey ,UnitKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@Datetime ,@Code ,@QuantityHarvested ,@QuantitySale ,@WhereToBuy ,@MemberKey ,@SeedsKey ,@UnitKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@HarvestedForSaleKey", SqlDbType.Int).Value = _HarvestedForSaleKey;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
zCommand.Parameters.Add("@QuantityHarvested", SqlDbType.Float).Value = _QuantityHarvested;
zCommand.Parameters.Add("@QuantitySale", SqlDbType.Float).Value = _QuantitySale;
zCommand.Parameters.Add("@WhereToBuy", SqlDbType.NVarChar).Value = _WhereToBuy;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
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
string zSQL = "UPDATE PUL_HarvestedForSale SET " 
            + " Datetime = @Datetime,"
            + " Code = @Code,"
            + " QuantityHarvested = @QuantityHarvested,"
            + " QuantitySale = @QuantitySale,"
            + " WhereToBuy = @WhereToBuy,"
            + " MemberKey = @MemberKey,"
            + " SeedsKey = @SeedsKey,"
            + " UnitKey = @UnitKey,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE HarvestedForSaleKey = @HarvestedForSaleKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@HarvestedForSaleKey", SqlDbType.Int).Value = _HarvestedForSaleKey;
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
zCommand.Parameters.Add("@QuantityHarvested", SqlDbType.Float).Value = _QuantityHarvested;
zCommand.Parameters.Add("@QuantitySale", SqlDbType.Float).Value = _QuantitySale;
zCommand.Parameters.Add("@WhereToBuy", SqlDbType.NVarChar).Value = _WhereToBuy;
zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
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
if (_HarvestedForSaleKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_HarvestedForSale WHERE HarvestedForSaleKey = @HarvestedForSaleKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@HarvestedForSaleKey", SqlDbType.Int).Value = _HarvestedForSaleKey;
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
