using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
public class ProvincesCitie_Info
{
 
#region [ Field Name ]
private int _ProvincesCities_Key = 0;
private string _ProvincesCities_ID = "";
private string _ProvincesCities_Name = "";
private string _Locations = "";
private int _Rank = 0;
private string _Description = "";
private DateTime _CreateOn ;
private DateTime _ModifiedOn ;
private Guid _CreatedBy;
private Guid _ModifiedBy;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public ProvincesCitie_Info()
{
}
public ProvincesCitie_Info(int ProvincesCities_Key)
{
string zSQL = "SELECT * FROM PUL_ProvincesCities WHERE ProvincesCities_Key = @ProvincesCities_Key"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProvincesCities_Key", SqlDbType.Int).Value = ProvincesCities_Key;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_ProvincesCities_Key = int.Parse(zReader["ProvincesCities_Key"].ToString());
_ProvincesCities_ID = zReader["ProvincesCities_ID"].ToString();
_ProvincesCities_Name = zReader["ProvincesCities_Name"].ToString();
_Locations = zReader["Locations"].ToString();
_Rank = int.Parse(zReader["Rank"].ToString());
_Description = zReader["Description"].ToString();
if (zReader["CreateOn"] != DBNull.Value)
_CreateOn = (DateTime)zReader["CreateOn"];
if (zReader["ModifiedOn"] != DBNull.Value)
_ModifiedOn = (DateTime)zReader["ModifiedOn"];
_CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
_ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int ProvincesCities_Key
{
get { return _ProvincesCities_Key; }
set { _ProvincesCities_Key = value; }
}
public string ProvincesCities_ID
{
get { return _ProvincesCities_ID; }
set { _ProvincesCities_ID = value; }
}
public string ProvincesCities_Name
{
get { return _ProvincesCities_Name; }
set { _ProvincesCities_Name = value; }
}
public string Locations
{
get { return _Locations; }
set { _Locations = value; }
}
public int Rank
{
get { return _Rank; }
set { _Rank = value; }
}
public string Description
{
get { return _Description; }
set { _Description = value; }
}
public DateTime CreateOn
{
get { return _CreateOn; }
set { _CreateOn = value; }
}
public DateTime ModifiedOn
{
get { return _ModifiedOn; }
set { _ModifiedOn = value; }
}
public Guid CreatedBy
{
get { return _CreatedBy; }
set { _CreatedBy = value; }
}
public Guid ModifiedBy
{
get { return _ModifiedBy; }
set { _ModifiedBy = value; }
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
    string zSQL = "INSERT INTO PUL_ProvincesCities (" 
+ " ProvincesCities_ID ,ProvincesCities_Name ,Locations ,Rank ,Description ,CreateOn ,ModifiedOn ,CreatedBy ,ModifiedBy ) "
 + " VALUES ( "
 + "@ProvincesCities_ID ,@ProvincesCities_Name ,@Locations ,@Rank ,@Description ,@CreateOn ,@ModifiedOn ,@CreatedBy ,@ModifiedBy ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProvincesCities_Key", SqlDbType.Int).Value = _ProvincesCities_Key;
zCommand.Parameters.Add("@ProvincesCities_ID", SqlDbType.NVarChar).Value = _ProvincesCities_ID;
zCommand.Parameters.Add("@ProvincesCities_Name", SqlDbType.NVarChar).Value = _ProvincesCities_Name;
zCommand.Parameters.Add("@Locations", SqlDbType.NVarChar).Value = _Locations;
zCommand.Parameters.Add("@Rank", SqlDbType.Int).Value = _Rank;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
if (_CreateOn.Year == 0001) 
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = _CreateOn;
if (_ModifiedOn.Year == 0001) 
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = _ModifiedOn;
zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
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
string zSQL = "UPDATE PUL_ProvincesCities SET " 
            + " ProvincesCities_ID = @ProvincesCities_ID,"
            + " ProvincesCities_Name = @ProvincesCities_Name,"
            + " Locations = @Locations,"
            + " Rank = @Rank,"
            + " Description = @Description,"
            + " CreateOn = @CreateOn,"
            + " ModifiedOn = @ModifiedOn,"
            + " CreatedBy = @CreatedBy,"
            + " ModifiedBy = @ModifiedBy"
           + " WHERE ProvincesCities_Key = @ProvincesCities_Key";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ProvincesCities_Key", SqlDbType.Int).Value = _ProvincesCities_Key;
zCommand.Parameters.Add("@ProvincesCities_ID", SqlDbType.NVarChar).Value = _ProvincesCities_ID;
zCommand.Parameters.Add("@ProvincesCities_Name", SqlDbType.NVarChar).Value = _ProvincesCities_Name;
zCommand.Parameters.Add("@Locations", SqlDbType.NVarChar).Value = _Locations;
zCommand.Parameters.Add("@Rank", SqlDbType.Int).Value = _Rank;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
if (_CreateOn.Year == 0001) 
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = _CreateOn;
if (_ModifiedOn.Year == 0001) 
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = _ModifiedOn;
zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
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
if (_ProvincesCities_Key == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_ProvincesCities WHERE ProvincesCities_Key = @ProvincesCities_Key";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@ProvincesCities_Key", SqlDbType.Int).Value = _ProvincesCities_Key;
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
