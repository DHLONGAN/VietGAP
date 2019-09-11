using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
public class CooperativePurchasing_Other_Info
{
 
#region [ Field Name ]
private int _Purchasing_OtherKey = 0;
private int _SeedKey = 0;
private string _Code = "";
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
private string _Name = "";
private string _Solution = "";
private int _Evaluate = 0;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public CooperativePurchasing_Other_Info()
{
}
public CooperativePurchasing_Other_Info(int Purchasing_OtherKey)
{
string zSQL = "SELECT * FROM PUL_CooperativePurchasing_Other WHERE Purchasing_OtherKey = @Purchasing_OtherKey"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@Purchasing_OtherKey", SqlDbType.Int).Value = Purchasing_OtherKey;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_Purchasing_OtherKey = int.Parse(zReader["Purchasing_OtherKey"].ToString());
_SeedKey = int.Parse(zReader["SeedKey"].ToString());
_Code = zReader["Code"].ToString();
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
_Name = zReader["Name"].ToString();
_Solution = zReader["Solution"].ToString();
_Evaluate = int.Parse(zReader["Evaluate"].ToString());
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int Purchasing_OtherKey
{
get { return _Purchasing_OtherKey; }
set { _Purchasing_OtherKey = value; }
}
public int SeedKey
{
get { return _SeedKey; }
set { _SeedKey = value; }
}
public string Code
{
get { return _Code; }
set { _Code = value; }
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
public string Name
{
get { return _Name; }
set { _Name = value; }
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
    string zSQL = "INSERT INTO PUL_CooperativePurchasing_Other (" 
+ " SeedKey ,Code ,CooperativeKey ,Quantity ,UnitKey ,Baskets ,Price ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ,Datetime ,Name ,Solution ,Evaluate ) "
 + " VALUES ( "
 + "@SeedKey ,@Code ,@CooperativeKey ,@Quantity ,@UnitKey ,@Baskets ,@Price ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ,@Datetime ,@Name ,@Solution ,@Evaluate ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@Purchasing_OtherKey", SqlDbType.Int).Value = _Purchasing_OtherKey;
zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
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
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
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
string zSQL = "UPDATE PUL_CooperativePurchasing_Other SET " 
            + " SeedKey = @SeedKey,"
            + " Code = @Code,"
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
            + " Name = @Name,"
            + " Solution = @Solution,"
            + " Evaluate = @Evaluate"
           + " WHERE Purchasing_OtherKey = @Purchasing_OtherKey";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@Purchasing_OtherKey", SqlDbType.Int).Value = _Purchasing_OtherKey;
zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
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
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
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
if (_Purchasing_OtherKey == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_CooperativePurchasing_Other WHERE Purchasing_OtherKey = @Purchasing_OtherKey";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@Purchasing_OtherKey", SqlDbType.Int).Value = _Purchasing_OtherKey;
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
