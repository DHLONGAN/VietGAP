using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class CooperativePurchasing_Info
{
 
#region [ Field Name ]
private int _CooperativePurchasingKey = 0;
private int _SeedProcessKey = 0;
private int _HarvestedForSaleKey = 0;
private int _CooperativeKey = 0;
private int _Quantity = 0;
private int _UnitKey = 0;
private int _Baskets = 0;
private int _Price = 0;
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private DateTime _Datetime ;
private string _Solution = "";
private int _Evaluate = 0;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public CooperativePurchasing_Info()
{
}
public CooperativePurchasing_Info(int CooperativePurchasingKey)
{
string zSQL = "SELECT * FROM PUL_CooperativePurchasing WHERE CooperativePurchasingKey = @CooperativePurchasingKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@CooperativePurchasingKey", SqlDbType.Int).Value = CooperativePurchasingKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_CooperativePurchasingKey = int.Parse(zReader["CooperativePurchasingKey"].ToString());
_SeedProcessKey = int.Parse(zReader["SeedProcessKey"].ToString());
_HarvestedForSaleKey = int.Parse(zReader["HarvestedForSaleKey"].ToString());
_CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
_Quantity = int.Parse(zReader["Quantity"].ToString());
_UnitKey = int.Parse(zReader["UnitKey"].ToString());
_Baskets = int.Parse(zReader["Baskets"].ToString());
_Price = int.Parse(zReader["Price"].ToString());
if (zReader["CreatedBy"] != DBNull.Value)
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
if (zReader["CreatedDateTime"] != DBNull.Value)
_CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
if (zReader["ModifiedBy"] != DBNull.Value)
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedDateTime"] != DBNull.Value)
_ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
if (zReader["Datetime"] != DBNull.Value)
_Datetime = (DateTime)zReader["Datetime"];
_Solution = zReader["Solution"].ToString();
_Evaluate = int.Parse(zReader["Evaluate"].ToString());
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int CooperativePurchasingKey
{
get { return _CooperativePurchasingKey; }
set { _CooperativePurchasingKey = value; }
}
public int SeedProcessKey
{
get { return _SeedProcessKey; }
set { _SeedProcessKey = value; }
}
public int HarvestedForSaleKey
{
get { return _HarvestedForSaleKey; }
set { _HarvestedForSaleKey = value; }
}
public int CooperativeKey
{
get { return _CooperativeKey; }
set { _CooperativeKey = value; }
}
public int Quantity
{
get { return _Quantity; }
set { _Quantity = value; }
}
public int UnitKey
{
get { return _UnitKey; }
set { _UnitKey = value; }
}
public int Baskets
{
get { return _Baskets; }
set { _Baskets = value; }
}
public int Price
{
get { return _Price; }
set { _Price = value; }
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
public DateTime Datetime
{
get { return _Datetime; }
set { _Datetime = value; }
}
public string Solution
{
get { return _Solution; }
set { _Solution = value; }
}
public int Evaluate
{
get { return _Evaluate; }
set { _Evaluate = value; }
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
    string zSQL = "INSERT INTO PUL_CooperativePurchasing (" 
+ " SeedProcessKey ,HarvestedForSaleKey ,CooperativeKey ,Quantity ,UnitKey ,Baskets ,Price ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ,Datetime ,Solution ,Evaluate ) "
 + " VALUES ( "
 + "@SeedProcessKey ,@HarvestedForSaleKey ,@CooperativeKey ,@Quantity ,@UnitKey ,@Baskets ,@Price ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ,@Datetime ,@Solution ,@Evaluate ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@CooperativePurchasingKey", SqlDbType.Int).Value = _CooperativePurchasingKey;
zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
zCommand.Parameters.Add("@HarvestedForSaleKey", SqlDbType.Int).Value = _HarvestedForSaleKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = _Quantity;
zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
zCommand.Parameters.Add("@Baskets", SqlDbType.Int).Value = _Baskets;
zCommand.Parameters.Add("@Price", SqlDbType.Int).Value = _Price;
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
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
zCommand.Parameters.Add("@Evaluate", SqlDbType.Int).Value = _Evaluate;
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
string zSQL = "UPDATE PUL_CooperativePurchasing SET " 
            + " SeedProcessKey = @SeedProcessKey,"
            + " HarvestedForSaleKey = @HarvestedForSaleKey,"
            + " CooperativeKey = @CooperativeKey,"
            + " Quantity = @Quantity,"
            + " UnitKey = @UnitKey,"
            + " Baskets = @Baskets,"
            + " Price = @Price,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime,"
            + " Datetime = @Datetime,"
            + " Solution = @Solution,"
            + " Evaluate = @Evaluate"
           + " WHERE CooperativePurchasingKey = @CooperativePurchasingKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@CooperativePurchasingKey", SqlDbType.Int).Value = _CooperativePurchasingKey;
zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
zCommand.Parameters.Add("@HarvestedForSaleKey", SqlDbType.Int).Value = _HarvestedForSaleKey;
zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
zCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = _Quantity;
zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
zCommand.Parameters.Add("@Baskets", SqlDbType.Int).Value = _Baskets;
zCommand.Parameters.Add("@Price", SqlDbType.Int).Value = _Price;
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
if (_Datetime.Year == 0001) 
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
zCommand.Parameters.Add("@Evaluate", SqlDbType.Int).Value = _Evaluate;
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
if (_CooperativePurchasingKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_CooperativePurchasing WHERE CooperativePurchasingKey = @CooperativePurchasingKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@CooperativePurchasingKey", SqlDbType.Int).Value = _CooperativePurchasingKey;
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
