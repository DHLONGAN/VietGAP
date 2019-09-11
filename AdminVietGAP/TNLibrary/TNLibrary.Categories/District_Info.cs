using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
public class District_Info
{
 
#region [ Field Name ]
private int _ID = 0;
private int _ProvincesCities_ID = 0;
private int _DisID = 0;
private string _Name = "";
private string _Description = "";
private string _Population = "";
private string _Area = "";
private string _PopulationDensity = "";
private string _Locations = "";
private int _CreateBy = 0;
private DateTime _CreateOn ;
private int _ModifiedBy = 0;
private DateTime _ModifiedOn ;
private string _Message = "";
#endregion
 
#region [ Constructor Get Information ]
public District_Info()
{
}
public District_Info(int ID)
{
string zSQL = "SELECT * FROM PUL_District WHERE ID = @ID"; 
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
_ProvincesCities_ID = int.Parse(zReader["ProvincesCities_ID"].ToString());
_DisID = int.Parse(zReader["DisID"].ToString());
_Name = zReader["Name"].ToString();
_Description = zReader["Description"].ToString();
_Population = zReader["Population"].ToString();
_Area = zReader["Area"].ToString();
_PopulationDensity = zReader["PopulationDensity"].ToString();
_Locations = zReader["Locations"].ToString();
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
public int ProvincesCities_ID
{
get { return _ProvincesCities_ID; }
set { _ProvincesCities_ID = value; }
}
public int DisID
{
get { return _DisID; }
set { _DisID = value; }
}
public string Name
{
get { return _Name; }
set { _Name = value; }
}
public string Description
{
get { return _Description; }
set { _Description = value; }
}
public string Population
{
get { return _Population; }
set { _Population = value; }
}
public string Area
{
get { return _Area; }
set { _Area = value; }
}
public string PopulationDensity
{
get { return _PopulationDensity; }
set { _PopulationDensity = value; }
}
public string Locations
{
get { return _Locations; }
set { _Locations = value; }
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
    string zSQL = "INSERT INTO PUL_District (" 
+ " ProvincesCities_ID ,DisID ,Name ,Description ,Population ,Area ,PopulationDensity ,Locations ,CreateBy ,CreateOn ,ModifiedBy ,ModifiedOn ) "
 + " VALUES ( "
 + "@ProvincesCities_ID ,@DisID ,@Name ,@Description ,@Population ,@Area ,@PopulationDensity ,@Locations ,@CreateBy ,@CreateOn ,@ModifiedBy ,@ModifiedOn ) ";
string zResult = ""; 
string zConnectionString = ConnectDataBase.ConnectionString;
SqlConnection zConnect = new SqlConnection(zConnectionString);
zConnect.Open();
try
{
SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
zCommand.CommandType = CommandType.Text;
zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
zCommand.Parameters.Add("@ProvincesCities_ID", SqlDbType.Int).Value = _ProvincesCities_ID;
zCommand.Parameters.Add("@DisID", SqlDbType.Int).Value = _DisID;
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
zCommand.Parameters.Add("@Population", SqlDbType.NVarChar).Value = _Population;
zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
zCommand.Parameters.Add("@PopulationDensity", SqlDbType.NVarChar).Value = _PopulationDensity;
zCommand.Parameters.Add("@Locations", SqlDbType.NVarChar).Value = _Locations;
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
string zSQL = "UPDATE PUL_District SET " 
            + " ProvincesCities_ID = @ProvincesCities_ID,"
            + " DisID = @DisID,"
            + " Name = @Name,"
            + " Description = @Description,"
            + " Population = @Population,"
            + " Area = @Area,"
            + " PopulationDensity = @PopulationDensity,"
            + " Locations = @Locations,"
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
zCommand.Parameters.Add("@ProvincesCities_ID", SqlDbType.Int).Value = _ProvincesCities_ID;
zCommand.Parameters.Add("@DisID", SqlDbType.Int).Value = _DisID;
zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
zCommand.Parameters.Add("@Population", SqlDbType.NVarChar).Value = _Population;
zCommand.Parameters.Add("@Area", SqlDbType.NVarChar).Value = _Area;
zCommand.Parameters.Add("@PopulationDensity", SqlDbType.NVarChar).Value = _PopulationDensity;
zCommand.Parameters.Add("@Locations", SqlDbType.NVarChar).Value = _Locations;
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
string zSQL = "DELETE FROM PUL_District WHERE ID = @ID";
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
