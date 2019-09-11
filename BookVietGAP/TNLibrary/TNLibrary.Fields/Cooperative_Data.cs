using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Fields
{
    public class Cooperative_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Cooperative ";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetList_City()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     C.Cooperative_Key, C.Cooperative_ID, C.Cooperative_Name, C.ProvincesCities_ID, C.Address, C.VietGAPCode, C.Phone, C.Email, C.Members, C.Area, C.Quantity, C.TreeType, C.DateRange, 
                                                  C.DateExpiration, C.Owner, C.CertifiedOrganization, C.Lat, C.Lng, C.Images, C.Description, PC.ProvincesCities_Name, CO.CertifiedOrganization_Name
                            FROM         dbo.PUL_Cooperative AS C LEFT OUTER JOIN
                                                  dbo.PUL_ProvincesCities AS PC ON C.ProvincesCities_ID = PC.ProvincesCities_Key LEFT OUTER JOIN
                                                  dbo.PUL_CertifiedOrganization AS CO ON C.CertifiedOrganization = CO.CertifiedOrganization_Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
    }
}
