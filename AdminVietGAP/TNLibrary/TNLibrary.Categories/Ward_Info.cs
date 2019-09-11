using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
public class Ward_Info
{
 
#region [ Field Name ]
private int _ID = 0;
private int _District_ID = 0;
private int _WardID = 0;
private string _Name = "";
private string _Locations = "";
private string _Description = "";
private int _CreateBy = 0;
private DateTime _CreateOn ;
private int _ModifiedBy = 0;
private DateTime _ModifiedOn ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public Ward_Info()
{
}
public Ward_Info(int ID)
{
string zSQL = "SELECT * FROM PUL_Ward WHERE ID = @ID"; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
SqlDataReader zReader = zCommand.ExecuteReader();
if (zReader.HasRows)
{
zReader.Read();
_ID = int.Parse(zReader["ID"].ToString());
_District_ID = int.Parse(zReader["District_ID"].ToString());
_WardID = int.Parse(zReader["WardID"].ToString());
_Name = zReader["Name"].ToString();
_Locations = zReader["Locations"].ToString();
_Description = zReader["Description"].ToString();
_CreateBy = int.Parse(zReader["CreateBy"].ToString());
if (zReader["CreateOn"] != DBNull.Value)
_CreateOn = (DateTime)zReader["CreateOn"];
_ModifiedBy = int.Parse(zReader["ModifiedBy"].ToString());
if (zReader["ModifiedOn"] != DBNull.Value)
_ModifiedOn = (DateTime)zReader["ModifiedOn"];
 }zReader.Close();zCommand.Dispose();}catch (Exception Err){_Message = Err.ToString();}finally{zConnect.Close();}}
#endregion
 
#region [ Properties ]
public int ID
{
get { return _ID; }
set { _ID = value; }
}
public int District_ID
{
get { return _District_ID; }
set { _District_ID = value; }
}
public int WardID
{
get { return _WardID; }
set { _WardID = value; }
}
public string Name
{
get { return _Name; }
set { _Name = value; }
}
public string Locations
{
get { return _Locations; }
set { _Locations = value; }
}
public string Description
{
get { return _Description; }
set { _Description = value; }
}
public int CreateBy
{
get { return _CreateBy; }
set { _CreateBy = value; }
}
public DateTime CreateOn
{
get { return _CreateOn; }
set { _CreateOn = value; }
}
public int ModifiedBy
{
get { return _ModifiedBy; }
set { _ModifiedBy = value; }
}
public DateTime ModifiedOn
{
get { return _ModifiedOn; }
set { _ModifiedOn = value; }
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
    string zSQL = "INSERT INTO PUL_Ward (" 
+ " District_ID ,WardID ,Name ,Locations ,Description ,CreateBy ,CreateOn ,ModifiedBy ,ModifiedOn ) "
 + " VALUES ( "
 + "@District_ID ,@WardID ,@Name ,@Locations ,@Description ,@CreateBy ,@CreateOn ,@ModifiedBy ,@ModifiedOn ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
zCommand.Parameters.Add("@District_ID", SqlDbType.Int).Value = _District_ID;
zCommand.Parameters.Add("@WardID", SqlDbType.Int).Value = _WardID;
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
zCommand.Parameters.Add("@Locations", SqlDbType.NVarChar).Value = _Locations;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
zCommand.Parameters.Add("@CreateBy", SqlDbType.Int).Value = _CreateBy;
if (_CreateOn.Year == 0001) 
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = _CreateOn;
zCommand.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = _ModifiedBy;
if (_ModifiedOn.Year == 0001) 
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = _ModifiedOn;
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
string zSQL = "UPDATE PUL_Ward SET " 
            + " District_ID = @District_ID,"
            + " WardID = @WardID,"
            + " Name = @Name,"
            + " Locations = @Locations,"
            + " Description = @Description,"
            + " CreateBy = @CreateBy,"
            + " CreateOn = @CreateOn,"
            + " ModifiedBy = @ModifiedBy,"
            + " ModifiedOn = @ModifiedOn"
           + " WHERE ID = @ID";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
zCommand.Parameters.Add("@District_ID", SqlDbType.Int).Value = _District_ID;
zCommand.Parameters.Add("@WardID", SqlDbType.Int).Value = _WardID;
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
zCommand.Parameters.Add("@Locations", SqlDbType.NVarChar).Value = _Locations;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
zCommand.Parameters.Add("@CreateBy", SqlDbType.Int).Value = _CreateBy;
if (_CreateOn.Year == 0001) 
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = _CreateOn;
zCommand.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = _ModifiedBy;
if (_ModifiedOn.Year == 0001) 
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = DBNull.Value;
else
zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = _ModifiedOn;
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
if (_ID == 0)
zResult = Create();
else
zResult = Update();
return zResult;
}
public string Delete()
{
string zResult = "";
//---------- String SQL Access Database ---------------
string zSQL = "DELETE FROM PUL_Ward WHERE ID = @ID";
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
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
