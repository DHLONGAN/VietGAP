using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class Report_Data
    {
        #region [ Danh sách giống]
        public static DataTable SeedProcessListReport(int MemberKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     SeedsKey
FROM         dbo.PUL_SeedProcess
WHERE     (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, DateOfManufacture) >= 0) AND (DATEDIFF(day, @To, DateOfManufacture) <= 0)
GROUP BY SeedsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        #region [ Quản lý giống]
        public static DataTable SeedProcessReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_SeedProcess.SeedProcessKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_SeedProcess.CompanyName, 
                      dbo.PUL_SeedProcess.DateOfManufacture AS DateTime, dbo.PUL_SeedProcess.Parcel, dbo.PUL_SeedProcess.Area, dbo.PUL_SeedProcess.Quantity, 
                      dbo.PUL_Unit.Name AS UnitArea, PUL_Unit_1.Name AS UnitQuantity
FROM         dbo.PUL_Unit INNER JOIN
                      dbo.PUL_SeedProcess INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey ON dbo.PUL_Unit.ID = dbo.PUL_SeedProcess.AreaUnit INNER JOIN
                      dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_SeedProcess.QuantityUnit = PUL_Unit_1.ID
WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (dbo.PUL_SeedProcess.IsActive = 1) AND (DATEDIFF(day, @From, 
                      dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND 
                      (dbo.PUL_SeedProcess.SeedsKey = @SeedsKey)
ORDER BY dbo.PUL_SeedProcess.DateOfManufacture";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        #region [ Quản lý Phân hửu cơ]
        public static DataTable CompostingOrganicReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_CompostingOrganic.CompostingKey, dbo.PUL_FertilizerOrganic.Name, dbo.PUL_CompostingOrganic.Quantity, dbo.PUL_Unit.Name AS Unit, 
                      dbo.PUL_CompostingOrganic.Method, dbo.PUL_CompostingOrganic.CompostingDates, dbo.PUL_Member.Name AS Member_Name
FROM         dbo.PUL_FertilizerOrganic INNER JOIN
                      dbo.PUL_CompostingOrganic ON dbo.PUL_FertilizerOrganic.FertilizerOrganicKey = dbo.PUL_CompostingOrganic.FertilizerOrganicKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_CompostingOrganic.MemberKey = dbo.PUL_Member.[Key] INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_CompostingOrganic.UnitKey = dbo.PUL_Unit.ID
WHERE     (dbo.PUL_CompostingOrganic.IsActive = 1) AND (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (DATEDIFF(day, @From, 
                      dbo.PUL_CompostingOrganic.StartDate) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_CompostingOrganic.StartDate) <= 0)
ORDER BY dbo.PUL_CompostingOrganic.StartDate";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        #region [ Quản lý mua phân bón hóa chất]
        public static DataTable FertilizerPesticideBuyReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT KeyID, DateTime, Name, Common, Company, Address, Quantity, Unit, Type
FROM         (SELECT     dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizer_Buy.DatetimeBuy AS DateTime, dbo.PUL_Fertilizers.TradeName AS Name, 
                                              dbo.PUL_Fertilizer_Common.Common_Name AS Common, dbo.PUL_Companies.CompanyName AS Company, dbo.PUL_Fertilizer_Buy.Address, 
                                              dbo.PUL_Fertilizer_Buy.Quantity, dbo.PUL_Unit.Name AS Unit, 'F' AS Type
                       FROM          dbo.PUL_Fertilizer_Buy INNER JOIN
                                              dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                                              dbo.PUL_Fertilizer_Common ON dbo.PUL_Fertilizers.CommonKey = dbo.PUL_Fertilizer_Common.Common_Key INNER JOIN
                                              dbo.PUL_Companies ON dbo.PUL_Fertilizer_Buy.CompanyKey = dbo.PUL_Companies.CompanyKey INNER JOIN
                                              dbo.PUL_Unit ON dbo.PUL_Fertilizer_Buy.UnitKey = dbo.PUL_Unit.ID
                       WHERE      (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Fertilizer_Buy.IsActive = 1) AND (dbo.PUL_Fertilizer_Buy.SeedsKey IN
                                                  (SELECT     SeedProcessKey AS SeedsKey
                                                    FROM          dbo.PUL_SeedProcess
                                                    WHERE      (MemberKey = @MemberKey) AND (IsActive = 1))) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND 
                                              (DATEDIFF(day, @To, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0)
                       UNION
                       SELECT     dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticide_Buy.DatetimeBuy AS DateTime, dbo.PUL_Pesticides.Trade_Name AS Name, 
                                             dbo.PUL_Pesticide_Common.Common_Name AS Common, PUL_Companies_1.CompanyName AS Company, dbo.PUL_Pesticide_Buy.Address, 
                                             dbo.PUL_Pesticide_Buy.Quantity, PUL_Unit_1.Name AS Unit, 'P' AS Type
                       FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                                             dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                                             dbo.PUL_Pesticide_Common ON dbo.PUL_Pesticides.Common_Key = dbo.PUL_Pesticide_Common.Common_Key INNER JOIN
                                             dbo.PUL_Companies AS PUL_Companies_1 ON dbo.PUL_Pesticides.CompanyKey = PUL_Companies_1.CompanyKey INNER JOIN
                                             dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_Pesticide_Buy.UnitKey = PUL_Unit_1.ID
                       WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 1) AND (dbo.PUL_Pesticide_Buy.SeedsKey IN
                                                 (SELECT     SeedProcessKey AS SeedsKey
                                                   FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                   WHERE      (MemberKey = @MemberKey) AND (IsActive = 1))) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND 
                                             (DATEDIFF(day, @To, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0)) AS A
ORDER BY DateTime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        #region [ Quản lý mua phân bón hóa chất]
        public static DataTable FertilizerPesticideUseReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT  KeyID, DateTime, Parcel, Area, Solution, EquipmentName, Name, Dose, Dosage, TGCL, Type
FROM         (SELECT     dbo.PUL_Fertilizer_Use.FertilizerUseKey AS KeyID, dbo.PUL_Fertilizer_Use.DateTimeUse AS DateTime, dbo.PUL_Fertilizer_Use.Parcel, 
                                              dbo.PUL_Fertilizer_Use.Area, dbo.PUL_Fertilizer_Use.Howtouse AS Solution, dbo.PUL_Equipment.EquipmentName, 
                                              dbo.PUL_Fertilizers.TradeName AS Name, dbo.PUL_Fertilizer_Use.FormulaUsed AS Dose, dbo.PUL_Fertilizer_Use.Quantity AS Dosage, 
                                              dbo.PUL_Fertilizer_Use.QuarantinePeriod AS TGCL, 'F' AS Type
                       FROM          dbo.PUL_Fertilizer_Use INNER JOIN
                                              dbo.PUL_Equipment ON dbo.PUL_Fertilizer_Use.CooperativeKey = dbo.PUL_Equipment.EquipmentKey INNER JOIN
                                              dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
                       WHERE      (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (dbo.PUL_Fertilizer_Use.IsActive = 1) AND (DATEDIFF(day, @From, 
                                              dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_Fertilizer_Use.DateTimeUse) <= 0) AND 
                                              (dbo.PUL_Fertilizer_Use.SeedKey IN
                                                  (SELECT     SeedProcessKey AS SeedKey
                                                    FROM          dbo.PUL_SeedProcess
                                                    WHERE      (MemberKey = @MemberKey) AND (IsActive = 1)))
                       UNION
                       SELECT     dbo.PUL_Pesticide_Use.PesticideUseKey AS KeyID, dbo.PUL_Pesticide_Use.DateTimeUse AS DateTime, dbo.PUL_Pesticide_Use.PestName AS Parcel, 
                                             dbo.PUL_Pesticide_Use.Area, dbo.PUL_Pesticide_Use.Solution, PUL_Equipment_1.EquipmentName, dbo.PUL_Pesticides.Trade_Name AS Name, 
                                             dbo.PUL_Pesticide_Use.Dose, dbo.PUL_Pesticide_Use.Dosage, dbo.PUL_Pesticide_Use.QuarantinePeriod AS TGCL, 'P' AS Type
                       FROM         dbo.PUL_Pesticide_Use INNER JOIN
                                             dbo.PUL_Equipment AS PUL_Equipment_1 ON dbo.PUL_Pesticide_Use.EquipmentKey = PUL_Equipment_1.EquipmentKey INNER JOIN
                                             dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey
                       WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 1) AND (DATEDIFF(day, @From, 
                                             dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_Pesticide_Use.DateTimeUse) <= 0) AND 
                                             (dbo.PUL_Pesticide_Use.SeedKey IN
                                                 (SELECT     SeedProcessKey AS SeedKey
                                                   FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                   WHERE      (MemberKey = @MemberKey) AND (IsActive = 1)))) AS A
ORDER BY DateTime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        #region [ Kiểm tra thiết bị]
        public static DataTable CheckEquipmentReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT dbo.PUL_CheckEquipment.CheckEquipmentKey AS KeyID, dbo.PUL_CheckEquipment.Datetime, dbo.PUL_Equipment.EquipmentName AS Name, 
                      dbo.PUL_CheckEquipment.Action, dbo.PUL_CheckEquipment.Info, dbo.PUL_Member.Name AS MemberName
FROM         dbo.PUL_CheckEquipment INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_CheckEquipment.MemberKey = dbo.PUL_Member.[Key]
WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 1) AND (DATEDIFF(day, @From, 
                      dbo.PUL_CheckEquipment.Datetime) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_CheckEquipment.Datetime) <= 0) AND (dbo.PUL_CheckEquipment.SeedsKey IN
                          (SELECT     SeedProcessKey AS SeedKey
                            FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                            WHERE      (MemberKey = @MemberKey) AND (IsActive = 1)))
ORDER BY dbo.PUL_CheckEquipment.Datetime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        #region [ Quản lý thu hoạch xuất bán ]
        public static DataTable HarvestedForSaleReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT    dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, dbo.PUL_HarvestedForSale.Datetime as DateTime, dbo.PUL_HarvestedForSale.Code, 
                      dbo.PUL_HarvestedForSale.QuantityHarvested, dbo.PUL_Unit.Name AS Unit, dbo.PUL_Member.Name AS MemberName, dbo.PUL_HarvestedForSale.QuantitySale, 
                      dbo.PUL_HarvestedForSale.WhereToBuy
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_HarvestedForSale.UnitKey = dbo.PUL_Unit.ID INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_HarvestedForSale.MemberKey = dbo.PUL_Member.[Key]
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 1) AND (DATEDIFF(day, @From, 
                      dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_HarvestedForSale.Datetime) <= 0) AND 
                      (dbo.PUL_HarvestedForSale.SeedsKey IN
                          (SELECT     SeedProcessKey AS SeedKey
                            FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                            WHERE      (MemberKey = @MemberKey) AND (IsActive = 1)))
ORDER BY dbo.PUL_HarvestedForSale.Datetime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        #region [ KIỂM KÊ TỒN KHO ]
        public static DataTable InventoryReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT dbo.PUL_Inventory.InventoryKey AS KeyID, dbo.PUL_Inventory.Datetime AS DateTime, A.Name, dbo.PUL_Inventory.Quantity, 
                      dbo.PUL_Unit.Name AS Unit, dbo.PUL_Inventory.ExpireDate, dbo.PUL_Inventory_Type.Name AS Type
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_Inventory.UnitKey = dbo.PUL_Unit.ID
WHERE     (dbo.PUL_Inventory.IsActive = 1) AND (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (DATEDIFF(day, @From, dbo.PUL_Inventory.Datetime) >= 0) AND 
                      (DATEDIFF(day, @To, dbo.PUL_Inventory.Datetime) <= 0)
ORDER BY DateTime";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion

        #region [ Báo cáo chi phí]
        public static DataTable MoneyReport(int MemberKey,int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"if(@SeedsKey > '0') 
SELECT     *
FROM         (SELECT     dbo.PUL_SeedProcess.SeedProcessKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_SeedProcess.Quantity, dbo.PUL_Unit.Name AS Unit, 
                      0 AS TotalSale, dbo.PUL_SeedProcess.Total AS TotalBuy, dbo.PUL_SeedProcess.DateOfManufacture AS Datetime, dbo.PUL_SeedProcess.CompanyName AS Address, 
                      N'Mua giống' AS Type, '01' AS Sort
FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey INNER JOIN
                      dbo.PUL_Unit ON dbo.PUL_SeedProcess.QuantityUnit = dbo.PUL_Unit.ID
WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (dbo.PUL_SeedProcess.IsActive = 1) AND (DATEDIFF(day, @From, 
                      dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND 
                      (dbo.PUL_SeedProcess.SeedsKey = @SeedsKey)
                       UNION
                       SELECT     dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, dbo.PUL_Fertilizer_Buy.Quantity, PUL_Unit_3.Name AS Unit, 
                      0 AS TotalSale, dbo.PUL_Fertilizer_Buy.Total AS TotalBuy, dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime, dbo.PUL_Fertilizer_Buy.Address, 
                      N'Mua phân bón' AS Type, '02' AS Sort
FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                      dbo.PUL_Unit AS PUL_Unit_3 ON dbo.PUL_Fertilizer_Buy.UnitKey = PUL_Unit_3.ID
WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Fertilizer_Buy.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) 
                      >= 0) AND (DATEDIFF(day, @To, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.SeedsKey IN
                          (SELECT     SeedProcessKey AS SeedsKey
                            FROM          dbo.PUL_SeedProcess
                            WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, 
                                                   dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (SeedsKey = @SeedsKey)))
                       UNION
                       SELECT     dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, dbo.PUL_Pesticide_Buy.Quantity, PUL_Unit_2.Name AS Unit, 
                      0 AS TotalSale, dbo.PUL_Pesticide_Buy.Total AS TotalBuy, dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime, dbo.PUL_Pesticide_Buy.Address, 
                      N'Mua thuốc BVTV' AS Type, '03' AS Sort
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                      dbo.PUL_Unit AS PUL_Unit_2 ON dbo.PUL_Pesticide_Buy.UnitKey = PUL_Unit_2.ID
WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy)
                       >= 0) AND (DATEDIFF(day, @To, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Pesticide_Buy.SeedsKey IN
                          (SELECT     SeedProcessKey AS SeedsKey
                            FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                            WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, 
                                                   dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) AND (SeedsKey = @SeedsKey)))
                       UNION
                       SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, PUL_Seeds_1.SeedsName AS Name, dbo.PUL_HarvestedForSale.QuantitySale AS Quantity, 
                      PUL_Unit_1.Name AS Unit, dbo.PUL_HarvestedForSale.Total AS TotalSale, 0 AS TotalBuy, dbo.PUL_HarvestedForSale.Datetime, 
                      dbo.PUL_HarvestedForSale.WhereToBuy AS Address, N'Thu hoạch xuất bán' AS Type, '04' AS Sort
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess AS PUL_SeedProcess_1 ON dbo.PUL_HarvestedForSale.SeedsKey = PUL_SeedProcess_1.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds AS PUL_Seeds_1 ON PUL_SeedProcess_1.SeedsKey = PUL_Seeds_1.SeedsKey INNER JOIN
                      dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_HarvestedForSale.UnitKey = PUL_Unit_1.ID
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 1) AND (DATEDIFF(day, @From, 
                      dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_HarvestedForSale.Datetime) <= 0) AND 
                      (dbo.PUL_HarvestedForSale.SeedsKey IN
                          (SELECT     SeedProcessKey AS SeedKey
                            FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                            WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (SeedsKey = @SeedsKey)))) AS A
ORDER BY Datetime, Sort
if(@SeedsKey = '0') 
SELECT KeyID, Name, Quantity, Unit, TotalSale, TotalBuy, Datetime, Address, Type, Sort
FROM         (SELECT     dbo.PUL_SeedProcess.SeedProcessKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_SeedProcess.Quantity, dbo.PUL_Unit.Name AS Unit, 
                                              0 AS TotalSale, dbo.PUL_SeedProcess.Total AS TotalBuy, dbo.PUL_SeedProcess.DateOfManufacture AS Datetime, 
                                              dbo.PUL_SeedProcess.CompanyName AS Address, N'Mua giống' AS Type, '01' AS Sort
                       FROM          dbo.PUL_Seeds INNER JOIN
                                              dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey INNER JOIN
                                              dbo.PUL_Unit ON dbo.PUL_SeedProcess.QuantityUnit = dbo.PUL_Unit.ID
                       WHERE      (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (dbo.PUL_SeedProcess.IsActive = 1) AND (DATEDIFF(day, @From, 
                                              dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_SeedProcess.DateOfManufacture) <= 0)
                       UNION
                       SELECT     dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, dbo.PUL_Fertilizer_Buy.Quantity, 
                                             PUL_Unit_3.Name AS Unit, 0 AS TotalSale, dbo.PUL_Fertilizer_Buy.Total AS TotalBuy, dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime, 
                                             dbo.PUL_Fertilizer_Buy.Address, N'Mua phân bón' AS Type, '02' AS Sort
                       FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                                             dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                                             dbo.PUL_Unit AS PUL_Unit_3 ON dbo.PUL_Fertilizer_Buy.UnitKey = PUL_Unit_3.ID
                       WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Fertilizer_Buy.IsActive = 1) AND (DATEDIFF(day, @From, 
                                             dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND 
                                             (dbo.PUL_Fertilizer_Buy.SeedsKey IN
                                                 (SELECT     SeedProcessKey AS SeedsKey
                                                   FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_2
                                                   WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND 
                                                                          (DATEDIFF(day, @To, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0)))
                       UNION
                       SELECT     dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, dbo.PUL_Pesticide_Buy.Quantity, 
                                             PUL_Unit_2.Name AS Unit, 0 AS TotalSale, dbo.PUL_Pesticide_Buy.Total AS TotalBuy, dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime, 
                                             dbo.PUL_Pesticide_Buy.Address, N'Mua thuốc BVTV' AS Type, '03' AS Sort
                       FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                                             dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                                             dbo.PUL_Unit AS PUL_Unit_2 ON dbo.PUL_Pesticide_Buy.UnitKey = PUL_Unit_2.ID
                       WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 1) AND (DATEDIFF(day, @From, 
                                             dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) AND 
                                             (dbo.PUL_Pesticide_Buy.SeedsKey IN
                                                 (SELECT     SeedProcessKey AS SeedsKey
                                                   FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                   WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND 
                                                                          (DATEDIFF(day, @To, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0)))
                       UNION
                       SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, PUL_Seeds_1.SeedsName AS Name, dbo.PUL_HarvestedForSale.QuantitySale AS Quantity, 
                                             PUL_Unit_1.Name AS Unit, dbo.PUL_HarvestedForSale.Total AS TotalSale, 0 AS TotalBuy, dbo.PUL_HarvestedForSale.Datetime, 
                                             dbo.PUL_HarvestedForSale.WhereToBuy AS Address, N'Thu hoạch xuất bán' AS Type, '04' AS Sort
                       FROM         dbo.PUL_HarvestedForSale INNER JOIN
                                             dbo.PUL_SeedProcess AS PUL_SeedProcess_1 ON dbo.PUL_HarvestedForSale.SeedsKey = PUL_SeedProcess_1.SeedProcessKey INNER JOIN
                                             dbo.PUL_Seeds AS PUL_Seeds_1 ON PUL_SeedProcess_1.SeedsKey = PUL_Seeds_1.SeedsKey INNER JOIN
                                             dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_HarvestedForSale.UnitKey = PUL_Unit_1.ID
                       WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 1) AND (DATEDIFF(day, @From, 
                                             dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @To, dbo.PUL_HarvestedForSale.Datetime) <= 0) AND 
                                             (dbo.PUL_HarvestedForSale.SeedsKey IN
                                                 (SELECT     SeedProcessKey AS SeedKey
                                                   FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                   WHERE      (MemberKey = @MemberKey) AND (IsActive = 1)))) AS A
ORDER BY Datetime, Sort
";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        public static DataTable SumMoneyReport(int MemberKey, int SeedsKey, DateTime From, DateTime To)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"if(@SeedsKey > '0') 
SELECT     Name, TotalSale, TotalBuy, Type
FROM         (SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM          (SELECT     dbo.PUL_Seeds.SeedsName AS Name, 0 AS TotalSale, dbo.PUL_SeedProcess.Total AS TotalBuy, N'Mua giống' AS Type, '01' AS Sort
                                               FROM          dbo.PUL_Seeds INNER JOIN
                                                                      dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey INNER JOIN
                                                                      dbo.PUL_Unit ON dbo.PUL_SeedProcess.QuantityUnit = dbo.PUL_Unit.ID
                                               WHERE      (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (dbo.PUL_SeedProcess.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) 
                                                                      AND (DATEDIFF(day, @To, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND (dbo.PUL_SeedProcess.SeedsKey = @SeedsKey)) AS A_4
                       GROUP BY Name, Type
                       UNION
                       SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM         (SELECT     dbo.PUL_Fertilizers.TradeName AS Name, 0 AS TotalSale, dbo.PUL_Fertilizer_Buy.Total AS TotalBuy, N'Mua phân bón' AS Type, '02' AS Sort
                                              FROM          dbo.PUL_Fertilizer_Buy INNER JOIN
                                                                     dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                                                                     dbo.PUL_Unit AS PUL_Unit_3 ON dbo.PUL_Fertilizer_Buy.UnitKey = PUL_Unit_3.ID
                                              WHERE      (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Fertilizer_Buy.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND
                                                                      (DATEDIFF(day, @To, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.SeedsKey IN
                                                                         (SELECT     SeedProcessKey AS SeedsKey
                                                                           FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_2
                                                                           WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, 
                                                                                                  dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (SeedsKey = @SeedsKey)))) AS A_3
                       GROUP BY Name, Type
                       UNION
                       SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM         (SELECT     dbo.PUL_Pesticides.Trade_Name AS Name, 0 AS TotalSale, dbo.PUL_Pesticide_Buy.Total AS TotalBuy, N'Mua thuốc BVTV' AS Type, '03' AS Sort
                                              FROM          dbo.PUL_Pesticide_Buy INNER JOIN
                                                                     dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                                                                     dbo.PUL_Unit AS PUL_Unit_2 ON dbo.PUL_Pesticide_Buy.UnitKey = PUL_Unit_2.ID
                                              WHERE      (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) 
                                                                     AND (DATEDIFF(day, @To, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Pesticide_Buy.SeedsKey IN
                                                                         (SELECT     SeedProcessKey AS SeedsKey
                                                                           FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                                           WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, 
                                                                                                  dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) AND (SeedsKey = @SeedsKey)))) AS A_2
                       GROUP BY Name, Type
                       UNION
                       SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM         (SELECT     PUL_Seeds_1.SeedsName AS Name, dbo.PUL_HarvestedForSale.Total AS TotalSale, 0 AS TotalBuy, N'Thu hoạch xuất bán' AS Type, '04' AS Sort
                                              FROM          dbo.PUL_HarvestedForSale INNER JOIN
                                                                     dbo.PUL_SeedProcess AS PUL_SeedProcess_1 ON dbo.PUL_HarvestedForSale.SeedsKey = PUL_SeedProcess_1.SeedProcessKey INNER JOIN
                                                                     dbo.PUL_Seeds AS PUL_Seeds_1 ON PUL_SeedProcess_1.SeedsKey = PUL_Seeds_1.SeedsKey INNER JOIN
                                                                     dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_HarvestedForSale.UnitKey = PUL_Unit_1.ID
                                              WHERE      (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_HarvestedForSale.Datetime) 
                                                                     >= 0) AND (DATEDIFF(day, @To, dbo.PUL_HarvestedForSale.Datetime) <= 0) AND (dbo.PUL_HarvestedForSale.SeedsKey IN
                                                                         (SELECT     SeedProcessKey AS SeedKey
                                                                           FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                                           WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (SeedsKey = @SeedsKey)))) AS A_1
                       GROUP BY Name, Type) AS A
if(@SeedsKey = '0') 
SELECT     Name, TotalSale, TotalBuy, Type
FROM         (SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM          (SELECT     dbo.PUL_Seeds.SeedsName AS Name, 0 AS TotalSale, dbo.PUL_SeedProcess.Total AS TotalBuy, N'Mua giống' AS Type, '01' AS Sort
                                               FROM          dbo.PUL_Seeds INNER JOIN
                                                                      dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey INNER JOIN
                                                                      dbo.PUL_Unit ON dbo.PUL_SeedProcess.QuantityUnit = dbo.PUL_Unit.ID
                                               WHERE      (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (dbo.PUL_SeedProcess.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) 
                                                                      AND (DATEDIFF(day, @To, dbo.PUL_SeedProcess.DateOfManufacture) <= 0)) AS A_4
                       GROUP BY Name, Type
                       UNION
                       SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM         (SELECT     dbo.PUL_Fertilizers.TradeName AS Name, 0 AS TotalSale, dbo.PUL_Fertilizer_Buy.Total AS TotalBuy, N'Mua phân bón' AS Type, '02' AS Sort
                                              FROM          dbo.PUL_Fertilizer_Buy INNER JOIN
                                                                     dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey INNER JOIN
                                                                     dbo.PUL_Unit AS PUL_Unit_3 ON dbo.PUL_Fertilizer_Buy.UnitKey = PUL_Unit_3.ID
                                              WHERE      (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Fertilizer_Buy.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND
                                                                      (DATEDIFF(day, @To, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.SeedsKey IN
                                                                         (SELECT     SeedProcessKey AS SeedsKey
                                                                           FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_2
                                                                           WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, 
                                                                                                  dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0)))) AS A_3
                       GROUP BY Name, Type
                       UNION
                       SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM         (SELECT     dbo.PUL_Pesticides.Trade_Name AS Name, 0 AS TotalSale, dbo.PUL_Pesticide_Buy.Total AS TotalBuy, N'Mua thuốc BVTV' AS Type, '03' AS Sort
                                              FROM          dbo.PUL_Pesticide_Buy INNER JOIN
                                                                     dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey INNER JOIN
                                                                     dbo.PUL_Unit AS PUL_Unit_2 ON dbo.PUL_Pesticide_Buy.UnitKey = PUL_Unit_2.ID
                                              WHERE      (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) 
                                                                     AND (DATEDIFF(day, @To, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Pesticide_Buy.SeedsKey IN
                                                                         (SELECT     SeedProcessKey AS SeedsKey
                                                                           FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                                           WHERE      (MemberKey = @MemberKey) AND (IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @To, 
                                                                                                  dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0)))) AS A_2
                       GROUP BY Name, Type
                       UNION
                       SELECT     Name, SUM(TotalSale) AS TotalSale, SUM(TotalBuy) AS TotalBuy, Type
                       FROM         (SELECT     PUL_Seeds_1.SeedsName AS Name, dbo.PUL_HarvestedForSale.Total AS TotalSale, 0 AS TotalBuy, N'Thu hoạch xuất bán' AS Type, '04' AS Sort
                                              FROM          dbo.PUL_HarvestedForSale INNER JOIN
                                                                     dbo.PUL_SeedProcess AS PUL_SeedProcess_1 ON dbo.PUL_HarvestedForSale.SeedsKey = PUL_SeedProcess_1.SeedProcessKey INNER JOIN
                                                                     dbo.PUL_Seeds AS PUL_Seeds_1 ON PUL_SeedProcess_1.SeedsKey = PUL_Seeds_1.SeedsKey INNER JOIN
                                                                     dbo.PUL_Unit AS PUL_Unit_1 ON dbo.PUL_HarvestedForSale.UnitKey = PUL_Unit_1.ID
                                              WHERE      (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 1) AND (DATEDIFF(day, @From, dbo.PUL_HarvestedForSale.Datetime) 
                                                                     >= 0) AND (DATEDIFF(day, @To, dbo.PUL_HarvestedForSale.Datetime) <= 0) AND (dbo.PUL_HarvestedForSale.SeedsKey IN
                                                                         (SELECT     SeedProcessKey AS SeedKey
                                                                           FROM          dbo.PUL_SeedProcess AS PUL_SeedProcess_1
                                                                           WHERE      (MemberKey = @MemberKey) AND (IsActive = 1)))) AS A_1
                       GROUP BY Name, Type) AS A
";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@From", SqlDbType.DateTime).Value = From;
                zCommand.Parameters.Add("@To", SqlDbType.DateTime).Value = To;
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
        #endregion
        
       
    }
}
