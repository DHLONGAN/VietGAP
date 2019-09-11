using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class Inventory_Data
    {
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Inventory ";
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
        public static DataTable GetListByMember(int MemberKey, string YEAR, string MONTH)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     COUNT(Datetime) AS Count, Datetime
                            FROM         dbo.PUL_Inventory
                            WHERE     (MemberKey = @MemberKey) AND (MONTH(Datetime) = @Month) AND (YEAR(Datetime) = @Year)
                            GROUP BY Datetime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@YEAR", SqlDbType.NVarChar).Value = YEAR;
                zCommand.Parameters.Add("@MONTH", SqlDbType.NVarChar).Value = MONTH;
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
        public static DataTable GetListByMemberDay(int MemberKey, DateTime Datetime)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT  dbo.PUL_Inventory.InventoryKey AS KeyID, A.Name, dbo.PUL_Inventory_Type.Name AS Type, dbo.PUL_Inventory.IsActive
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) >= 0) AND (DATEDIFF(day,@DateOfManufacture, dbo.PUL_Inventory.Datetime) <= 0)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = Datetime;
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
        public static DataTable GetListFP(int Type, int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"
IF @Type = 1
SELECT dbo.PUL_Fertilizers_Cooperative.FertilizersKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name
FROM         dbo.PUL_Fertilizers_Cooperative INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizers_Cooperative.FertilizersKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_Fertilizers_Cooperative.CooperativeKey = @CooperativeKey)
ORDER BY Name
IF @Type = 2
SELECT  dbo.PUL_Pesticide_Cooperative.PesticideKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name
FROM         dbo.PUL_Pesticide_Cooperative INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Cooperative.PesticideKey = dbo.PUL_Pesticides.PesticideKey
WHERE     (dbo.PUL_Pesticide_Cooperative.CooperativeKey = @CooperativeKey)
ORDER BY Name
";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Type", SqlDbType.Int).Value = Type;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
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
