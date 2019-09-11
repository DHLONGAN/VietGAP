using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Sys
{
public class Cooperative_Info
{
 
#region [ Field Name ]
private int _Cooperative_Key = 0;
private string _Cooperative_ID = "";
private string _Cooperative_Name = "";
private string _ProvincesCities_ID = "";
private string _Address = "";
private string _VietGAPCode = "";
private string _Phone = "";
private string _Email = "";
private int _Members = 0;
private float _Area;
private float _Quantity;
private string _TreeType = "";
private DateTime _DateRange ;
private DateTime _DateExpiration ;
private string _Owner = "";
private string _CertifiedOrganization = "";
private string _Lat = "";
private string _Lng = "";
private string _Images = "";
private string _Description = "";
private Guid _CreatedBy;
private DateTime _CreatedDateTime ;
private Guid _ModifiedBy;
private DateTime _ModifiedDateTime ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Cooperative_Info()
{
}
public Cooperative_Info(int Cooperative_Key)
{
string zSQL = "SELECT * FROM PUL_Cooperative WHERE Cooperative_Key = @Cooperative_Key"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = Cooperative_Key;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_Cooperative_Key = int.Parse(zReader["Cooperative_Key"].ToString());
_Cooperative_ID = zReader["Cooperative_ID"].ToString();
_Cooperative_Name = zReader["Cooperative_Name"].ToString();
_ProvincesCities_ID = zReader["ProvincesCities_ID"].ToString();
_Address = zReader["Address"].ToString();
_VietGAPCode = zReader["VietGAPCode"].ToString();
_Phone = zReader["Phone"].ToString();
_Email = zReader["Email"].ToString();
_Members = int.Parse(zReader["Members"].ToString());
_Area = float.Parse(zReader["Area"].ToString());
_Quantity = float.Parse(zReader["Quantity"].ToString());
_TreeType = zReader["TreeType"].ToString();
if (zReader["DateRange"] != DBNull.Value)
_DateRange = (DateTime)zReader["DateRange"];
if (zReader["DateExpiration"] != DBNull.Value)
_DateExpiration = (DateTime)zReader["DateExpiration"];
_Owner = zReader["Owner"].ToString();
_CertifiedOrganization = zReader["CertifiedOrganization"].ToString();
_Lat = zReader["Lat"].ToString();
_Lng = zReader["Lng"].ToString();
_Images = zReader["Images"].ToString();
_Description = zReader["Description"].ToString();
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
if (zReader["CreatedDateTime"] != DBNull.Value)
_CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedDateTime"] != DBNull.Value)
_ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int Cooperative_Key
{
get { return _Cooperative_Key; }
set { _Cooperative_Key = value; }
}
public string Cooperative_ID
{
get { return _Cooperative_ID; }
set { _Cooperative_ID = value; }
}
public string Cooperative_Name
{
get { return _Cooperative_Name; }
set { _Cooperative_Name = value; }
}
public string ProvincesCities_ID
{
get { return _ProvincesCities_ID; }
set { _ProvincesCities_ID = value; }
}
public string Address
{
get { return _Address; }
set { _Address = value; }
}
public string VietGAPCode
{
get { return _VietGAPCode; }
set { _VietGAPCode = value; }
}
public string Phone
{
get { return _Phone; }
set { _Phone = value; }
}
public string Email
{
get { return _Email; }
set { _Email = value; }
}
public int Members
{
get { return _Members; }
set { _Members = value; }
}
public float Area
{
get { return _Area; }
set { _Area = value; }
}
public float Quantity
{
get { return _Quantity; }
set { _Quantity = value; }
}
public string TreeType
{
get { return _TreeType; }
set { _TreeType = value; }
}
public DateTime DateRange
{
get { return _DateRange; }
set { _DateRange = value; }
}
public DateTime DateExpiration
{
get { return _DateExpiration; }
set { _DateExpiration = value; }
}
public string Owner
{
get { return _Owner; }
set { _Owner = value; }
}
public string CertifiedOrganization
{
get { return _CertifiedOrganization; }
set { _CertifiedOrganization = value; }
}
public string Lat
{
get { return _Lat; }
set { _Lat = value; }
}
public string Lng
{
get { return _Lng; }
set { _Lng = value; }
}
public string Images
{
get { return _Images; }
set { _Images = value; }
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
    string zSQL = "INSERT INTO PUL_Cooperative (" 
+ " Cooperative_ID ,Cooperative_Name ,ProvincesCities_ID ,Address ,VietGAPCode ,Phone ,Email ,Members ,Area ,Quantity ,TreeType ,DateRange ,DateExpiration ,Owner ,CertifiedOrganization ,Lat ,Lng ,Images ,Description ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
 + " VALUES ( "
 + "@Cooperative_ID ,@Cooperative_Name ,@ProvincesCities_ID ,@Address ,@VietGAPCode ,@Phone ,@Email ,@Members ,@Area ,@Quantity ,@TreeType ,@DateRange ,@DateExpiration ,@Owner ,@CertifiedOrganization ,@Lat ,@Lng ,@Images ,@Description ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
zCommand.Parameters.Add("@Cooperative_ID", SqlDbType.NVarChar).Value = _Cooperative_ID;
zCommand.Parameters.Add("@Cooperative_Name", SqlDbType.NVarChar).Value = _Cooperative_Name;
zCommand.Parameters.Add("@ProvincesCities_ID", SqlDbType.NVarChar).Value = _ProvincesCities_ID;
zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
zCommand.Parameters.Add("@VietGAPCode", SqlDbType.NVarChar).Value = _VietGAPCode;
zCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = _Phone;
zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
zCommand.Parameters.Add("@Members", SqlDbType.Int).Value = _Members;
zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
zCommand.Parameters.Add("@TreeType", SqlDbType.NVarChar).Value = _TreeType;
if (_DateRange.Year == 0001) 
zCommand.Parameters.Add("@DateRange", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@DateRange", SqlDbType.DateTime).Value = _DateRange;
if (_DateExpiration.Year == 0001) 
zCommand.Parameters.Add("@DateExpiration", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@DateExpiration", SqlDbType.DateTime).Value = _DateExpiration;
zCommand.Parameters.Add("@Owner", SqlDbType.NVarChar).Value = _Owner;
zCommand.Parameters.Add("@CertifiedOrganization", SqlDbType.NVarChar).Value = _CertifiedOrganization;
zCommand.Parameters.Add("@Lat", SqlDbType.NVarChar).Value = _Lat;
zCommand.Parameters.Add("@Lng", SqlDbType.NVarChar).Value = _Lng;
zCommand.Parameters.Add("@Images", SqlDbType.NVarChar).Value = _Images;
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
string zSQL = "UPDATE PUL_Cooperative SET " 
            + " Cooperative_ID = @Cooperative_ID,"
            + " Cooperative_Name = @Cooperative_Name,"
            + " ProvincesCities_ID = @ProvincesCities_ID,"
            + " Address = @Address,"
            + " VietGAPCode = @VietGAPCode,"
            + " Phone = @Phone,"
            + " Email = @Email,"
            + " Members = @Members,"
            + " Area = @Area,"
            + " Quantity = @Quantity,"
            + " TreeType = @TreeType,"
            + " DateRange = @DateRange,"
            + " DateExpiration = @DateExpiration,"
            + " Owner = @Owner,"
            + " CertifiedOrganization = @CertifiedOrganization,"
            + " Lat = @Lat,"
            + " Lng = @Lng,"
            + " Images = @Images,"
            + " Description = @Description,"
            + " CreatedBy = @CreatedBy,"
            + " CreatedDateTime = @CreatedDateTime,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedDateTime = @ModifiedDateTime"
           + " WHERE Cooperative_Key = @Cooperative_Key";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
zCommand.Parameters.Add("@Cooperative_ID", SqlDbType.NVarChar).Value = _Cooperative_ID;
zCommand.Parameters.Add("@Cooperative_Name", SqlDbType.NVarChar).Value = _Cooperative_Name;
zCommand.Parameters.Add("@ProvincesCities_ID", SqlDbType.NVarChar).Value = _ProvincesCities_ID;
zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
zCommand.Parameters.Add("@VietGAPCode", SqlDbType.NVarChar).Value = _VietGAPCode;
zCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = _Phone;
zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
zCommand.Parameters.Add("@Members", SqlDbType.Int).Value = _Members;
zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
zCommand.Parameters.Add("@TreeType", SqlDbType.NVarChar).Value = _TreeType;
if (_DateRange.Year == 0001) 
zCommand.Parameters.Add("@DateRange", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@DateRange", SqlDbType.DateTime).Value = _DateRange;
if (_DateExpiration.Year == 0001) 
zCommand.Parameters.Add("@DateExpiration", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@DateExpiration", SqlDbType.DateTime).Value = _DateExpiration;
zCommand.Parameters.Add("@Owner", SqlDbType.NVarChar).Value = _Owner;
zCommand.Parameters.Add("@CertifiedOrganization", SqlDbType.NVarChar).Value = _CertifiedOrganization;
zCommand.Parameters.Add("@Lat", SqlDbType.NVarChar).Value = _Lat;
zCommand.Parameters.Add("@Lng", SqlDbType.NVarChar).Value = _Lng;
zCommand.Parameters.Add("@Images", SqlDbType.NVarChar).Value = _Images;
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
if (_Cooperative_Key == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Cooperative WHERE Cooperative_Key = @Cooperative_Key";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
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
