using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
using System.Web;
namespace TNLibrary.Book
{
    public class All_Data
    {
        #region [ Danh sách công việc chưa check All]
        public static DataTable GetListNotActiveByMemberAllDay(int MemberKey, DateTime DateOfManufacture, int Num)
        {
            DateTime DateNum = DateOfManufacture - new TimeSpan(Num, 0, 0, 0);
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT  *
FROM         (SELECT   LandUseKey AS KeyID, Action AS Name, Datetime, N'Quản lý đất' AS Type, '01' AS Sort, 'LandUse' as NameBook
FROM          dbo.PUL_LandUse
WHERE      (DATEDIFF(day, @DateNum, Datetime) >= 0)  AND (MemberKey = @MemberKey) AND (IsActive = 0)
UNION
SELECT   (dbo.PUL_SeedProcess.SeedProcessKey) AS KeyID, (dbo.PUL_Seeds.SeedsName) AS Name, 
                        (dbo.PUL_SeedProcess.DateOfManufacture) AS Datetime, N'Xử lý giống' AS Type, '02' AS Sort, 'SeedsProcess' as NameBook
FROM         dbo.PUL_Seeds INNER JOIN
                        dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey
WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_SeedProcess.DateOfManufacture) >= 0)  AND (dbo.PUL_SeedProcess.IsActive = 0)
UNION
SELECT     dbo.PUL_CompostingOrganic.CompostingKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, dbo.PUL_CompostingOrganic.StartDate AS Datetime, 
                      N'Quản lý ủ phân hữu cơ' AS Type, '03' AS Sort, 'CompostingOrganic' as NameBook
FROM         dbo.PUL_CompostingOrganic INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateNum, 
                      dbo.PUL_CompostingOrganic.StartDate) >= 0)       
UNION
SELECT      dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime, N'Mua phân bón' AS Type, '04' AS Sort, 'Fertilizer_Buy' as NameBook
FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0)  AND (dbo.PUL_Fertilizer_Buy.IsActive = 0)
UNION
SELECT      dbo.PUL_Fertilizer_Use.FertilizerUseKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_Fertilizer_Use.DateTimeUse AS Datetime, N'Sử dụng phân bón' AS Type, '05' AS Sort, 'Fertilizer_Use' as NameBook
FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0)  AND (dbo.PUL_Fertilizer_Use.IsActive = 0)
UNION
SELECT      dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, 
                      dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime, N'Mua thuốc BVTV' AS Type, '06' AS Sort, 'PesticideBuy' as NameBook
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey
WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND 
                      (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) 
UNION
SELECT      dbo.PUL_Pesticide_Use.PesticideUseKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, 
                      dbo.PUL_Pesticide_Use.DatetimeUse AS Datetime, N'Sử dụng thuốc BVTV' AS Type, '07' AS Sort, 'Pesticide_Use' as NameBook
FROM         dbo.PUL_Pesticide_Use INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey
WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND 
                      (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) 
UNION
SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_HarvestedForSale.Datetime, 
                      N'Thu hoạch xuất bán' AS Type, '08' AS Sort, 'HarvestedForSale' as NameBook
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND (DATEDIFF(day, @DateNum, 
                      dbo.PUL_HarvestedForSale.Datetime) >= 0) 
UNION
SELECT      dbo.PUL_CheckEquipment.CheckEquipmentKey AS KeyID, dbo.PUL_Equipment.EquipmentName AS Name, dbo.PUL_CheckEquipment.Datetime, 
                      N'Kiểm tra thiết bị' AS Type, '09' AS Sort, 'CheckEquipment' as NameBook
FROM         dbo.PUL_CheckEquipment INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey
WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND 
                      (DATEDIFF(day, @DateNum, dbo.PUL_CheckEquipment.Datetime) >= 0) 
UNION
SELECT      HandlingPackagingKey AS KeyID, Type AS Name, Datetime, N'Xử lý chất thải' AS Type, '10' AS Sort, 'HandlingPackaging' as NameBook
FROM         dbo.PUL_HandlingPackaging
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) 
UNION
SELECT     dbo.PUL_Inventory.InventoryKey AS KeyID, A.Name as Name,dbo.PUL_Inventory.Datetime, N'Kiểm kê tồn kho' AS Type, '11' AS Sort, 'Inventory' as NameBook
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0)
                       AND (DATEDIFF(day, @DateNum, dbo.PUL_Inventory.Datetime) >= 0)
UNION
SELECT     CheckAssessmentKey AS KeyID, DescribesError AS Name, Datetime, N'Kiểm tra đánh giá' AS Type, '12' AS Sort, 'CheckAssessment' as NameBook
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) ) as A
ORDER BY Sort,Datetime DESC";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateNum", SqlDbType.DateTime).Value = DateNum;
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

        public static DataTable GetCountsNotActiveByMemberAllDay(int MemberKey, DateTime DateOfManufacture, int Num)
        {
            DateTime DateNum = DateOfManufacture - new TimeSpan(Num, 0, 0, 0);
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT    SUM(Counts) AS Counts
                        FROM         (SELECT     COUNT(LandUseKey) AS Counts, N'Quản lý đất' AS Type, '01' AS Sort
                       FROM          dbo.PUL_LandUse
                       WHERE      (DATEDIFF(day, @DateNum, Datetime) >= 0)  AND (MemberKey = @MemberKey) AND (IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_SeedProcess.SeedProcessKey) AS Counts, N'Xử lý giống' AS Type, '02' AS Sort
                       FROM         dbo.PUL_Seeds INNER JOIN
                                             dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey
                       WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_SeedProcess.DateOfManufacture) >= 0)  AND (dbo.PUL_SeedProcess.IsActive = 0)
                       UNION
                       SELECT      COUNT(dbo.PUL_CompostingOrganic.CompostingKey) AS Counts, N'Quản lý ủ phân hữu cơ' AS Type, '03' AS Sort
                       FROM         dbo.PUL_CompostingOrganic INNER JOIN
                                             dbo.PUL_FertilizerOrganic ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_FertilizerOrganic.FertilizerOrganicKey
                       WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateNum, 
                       dbo.PUL_CompostingOrganic.StartDate) >= 0) 
                       UNION
                       SELECT     COUNT(dbo.PUL_Fertilizer_Buy.FertilizerBuyKey) AS Counts, N'Mua phân bón' AS Type, '04' AS Sort
                       FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                                             dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
                       WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0)  AND (dbo.PUL_Fertilizer_Buy.IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Fertilizer_Use.FertilizerUseKey) AS Counts, N'Sử dụng phân bón' AS Type, '05' AS Sort
                       FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                                             dbo.PUL_Fertilizers AS PUL_Fertilizers_1 ON dbo.PUL_Fertilizer_Use.FertilizerKey = PUL_Fertilizers_1.FertilizersKey
                       WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0)  AND (dbo.PUL_Fertilizer_Use.IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Pesticide_Buy.PesticideBuyKey) AS Counts, N'Mua thuốc BVTV' AS Type, '06' AS Sort
                       FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                                             dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey
                       WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) 
                       UNION
                       SELECT     COUNT(dbo.PUL_Pesticide_Use.PesticideUseKey) AS Counts, N'Sử dụng thuốc BVTV' AS Type, '07' AS Sort
                       FROM         dbo.PUL_Pesticide_Use INNER JOIN
                                             dbo.PUL_Pesticides AS PUL_Pesticides_1 ON dbo.PUL_Pesticide_Use.PesticideKey = PUL_Pesticides_1.PesticideKey
                       WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) 
                       UNION
                       SELECT     COUNT(dbo.PUL_HarvestedForSale.HarvestedForSaleKey) AS Counts, N'Thu hoạch xuất bán' AS Type, '08' AS Sort
                       FROM         dbo.PUL_HarvestedForSale INNER JOIN
                                             dbo.PUL_Seeds AS PUL_Seeds_1 ON dbo.PUL_HarvestedForSale.SeedsKey = PUL_Seeds_1.SeedsKey
                       WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_HarvestedForSale.Datetime) >= 0) 
                       UNION
                       SELECT     COUNT(dbo.PUL_CheckEquipment.CheckEquipmentKey) AS Counts, N'Kiểm tra thiết bị' AS Type, '09' AS Sort
                       FROM         dbo.PUL_CheckEquipment INNER JOIN
                                             dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey
                       WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_CheckEquipment.Datetime) >= 0) 
                       UNION
                       SELECT     COUNT(HandlingPackagingKey) AS Counts, N'Xử lý chất thải' AS Type, '10' AS Sort
                       FROM         dbo.PUL_HandlingPackaging
                       WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) 
                        UNION
                        SELECT     COUNT(dbo.PUL_Inventory.InventoryKey) AS Counts, N'Kiểm kê tồn kho' AS Type, '11' AS Sort
                        FROM         dbo.PUL_Inventory INNER JOIN
                                                  (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                                                    FROM          dbo.PUL_Fertilizers
                                                    UNION
                                                    SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                                                    FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                                              dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
                        WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0) 
                                               AND (DATEDIFF(day, @DateNum, dbo.PUL_Inventory.Datetime) >= 0)
UNION
SELECT     COUNT(CheckAssessmentKey) AS Counts, N'Kiểm tra đánh giá' AS Type, '12' AS Sort
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) 
) AS A";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateNum", SqlDbType.DateTime).Value = DateNum;
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
        #region [ Danh sách công việc chưa check theo ngày]
        public static DataTable GetListNotActiveByMemberDay(int MemberKey, DateTime DateOfManufacture,int Num)
        {
            DateTime DateMonday = DateOfManufacture.MondayOfWeek();
            DateOfManufacture = DateOfManufacture.SundayOfWeek();
            DateTime DateNum = DateOfManufacture - new TimeSpan(Num, 0, 0, 0);
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT  *
FROM         (SELECT   LandUseKey AS KeyID, Action AS Name, Datetime, N'Quản lý đất' AS Type, '01' AS Sort, 'LandUse' as NameBook
FROM          dbo.PUL_LandUse
WHERE    (DATEDIFF(day, @DateMonday, Datetime) >= 0) AND  (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) AND (MemberKey = @MemberKey) AND (IsActive = 0)
UNION
SELECT   (dbo.PUL_SeedProcess.SeedProcessKey) AS KeyID, (dbo.PUL_Seeds.SeedsName) AS Name, 
                        (dbo.PUL_SeedProcess.DateOfManufacture) AS Datetime, N'Xử lý giống' AS Type, '02' AS Sort, 'SeedsProcess' as NameBook
FROM         dbo.PUL_Seeds INNER JOIN
                        dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey
WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateMonday, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND (dbo.PUL_SeedProcess.IsActive = 0)
UNION
SELECT     dbo.PUL_CompostingOrganic.CompostingKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, dbo.PUL_CompostingOrganic.StartDate AS Datetime, 
                      N'Quản lý ủ phân hữu cơ' AS Type, '03' AS Sort, 'CompostingOrganic' as NameBook
FROM         dbo.PUL_CompostingOrganic INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateMonday, dbo.PUL_CompostingOrganic.StartDate) >= 0) AND (DATEDIFF(day, @DateNum, 
                      dbo.PUL_CompostingOrganic.StartDate) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CompostingOrganic.StartDate) <= 0)          
UNION
SELECT      dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime, N'Mua phân bón' AS Type, '04' AS Sort, 'Fertilizer_Buy' as NameBook
FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateMonday, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.IsActive = 0)
UNION
SELECT      dbo.PUL_Fertilizer_Use.FertilizerUseKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_Fertilizer_Use.DateTimeUse AS Datetime, N'Sử dụng phân bón' AS Type, '05' AS Sort, 'Fertilizer_Use' as NameBook
FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateMonday, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) <= 0) AND (dbo.PUL_Fertilizer_Use.IsActive = 0)
UNION
SELECT      dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, 
                      dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime, N'Mua thuốc BVTV' AS Type, '06' AS Sort, 'PesticideBuy' as NameBook
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey
WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND (DATEDIFF(day, @DateMonday, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND 
                      (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0)
UNION
SELECT      dbo.PUL_Pesticide_Use.PesticideUseKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, 
                      dbo.PUL_Pesticide_Use.DatetimeUse AS Datetime, N'Sử dụng thuốc BVTV' AS Type, '07' AS Sort, 'Pesticide_Use' as NameBook
FROM         dbo.PUL_Pesticide_Use INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey
WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND (DATEDIFF(day, @DateMonday, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND 
                      (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) <= 0)
UNION
SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_HarvestedForSale.Datetime, 
                      N'Thu hoạch xuất bán' AS Type, '08' AS Sort, 'HarvestedForSale' as NameBook
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND (DATEDIFF(day, @DateMonday, dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @DateNum, 
                      dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_HarvestedForSale.Datetime) <= 0)
UNION
SELECT      dbo.PUL_CheckEquipment.CheckEquipmentKey AS KeyID, dbo.PUL_Equipment.EquipmentName AS Name, dbo.PUL_CheckEquipment.Datetime, 
                      N'Kiểm tra thiết bị' AS Type, '09' AS Sort, 'CheckEquipment' as NameBook
FROM         dbo.PUL_CheckEquipment INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey
WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND (DATEDIFF(day, @DateMonday, dbo.PUL_CheckEquipment.Datetime) >= 0) AND 
                      (DATEDIFF(day, @DateNum, dbo.PUL_CheckEquipment.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) <= 0)
UNION
SELECT      HandlingPackagingKey AS KeyID, Type AS Name, Datetime, N'Xử lý chất thải' AS Type, '10' AS Sort, 'HandlingPackaging' as NameBook
FROM         dbo.PUL_HandlingPackaging
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateMonday, Datetime) >= 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
UNION
SELECT     dbo.PUL_Inventory.InventoryKey AS KeyID, A.Name as Name,dbo.PUL_Inventory.Datetime, N'Kiểm kê tồn kho' AS Type, '11' AS Sort, 'Inventory' as NameBook
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0) 
                      AND (DATEDIFF(day, @DateMonday, dbo.PUL_Inventory.Datetime) >= 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_Inventory.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) <= 0) 
UNION
SELECT     CheckAssessmentKey AS KeyID, DescribesError AS Name, Datetime, N'Kiểm tra đánh giá' AS Type, '12' AS Sort, 'CheckAssessment' as NameBook
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateMonday, Datetime) >= 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
) AS A
ORDER BY Sort,Datetime desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
                zCommand.Parameters.Add("@DateNum", SqlDbType.DateTime).Value = DateNum;
                zCommand.Parameters.Add("@DateMonday", SqlDbType.DateTime).Value = DateMonday;
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
        
        public static DataTable GetCountsNotActiveByMemberDay(int MemberKey, DateTime DateOfManufacture, int Num)
        {
            DateTime DateMonday = DateOfManufacture.MondayOfWeek();
            DateOfManufacture = DateOfManufacture.SundayOfWeek();
            DateTime DateNum = DateOfManufacture - new TimeSpan(Num, 0, 0, 0);
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT    SUM(Counts) AS Counts
                        FROM         (SELECT     COUNT(LandUseKey) AS Counts, N'Quản lý đất' AS Type, '01' AS Sort
                       FROM          dbo.PUL_LandUse
                       WHERE    (DATEDIFF(day, @DateMonday, Datetime) >= 0) AND  (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) AND (MemberKey = @MemberKey) AND (IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_SeedProcess.SeedProcessKey) AS Counts, N'Xử lý giống' AS Type, '02' AS Sort
                       FROM         dbo.PUL_Seeds INNER JOIN
                                             dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey
                       WHERE    (DATEDIFF(day, @DateMonday, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND (dbo.PUL_SeedProcess.IsActive = 0)
                       UNION
                       SELECT      COUNT(dbo.PUL_CompostingOrganic.CompostingKey) AS Counts, N'Quản lý ủ phân hữu cơ' AS Type, '03' AS Sort
                       FROM         dbo.PUL_CompostingOrganic INNER JOIN
                                             dbo.PUL_FertilizerOrganic ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_FertilizerOrganic.FertilizerOrganicKey
                       WHERE    (DATEDIFF(day, @DateMonday, dbo.PUL_CompostingOrganic.StartDate) >= 0) AND (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateNum, 
                       dbo.PUL_CompostingOrganic.StartDate) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CompostingOrganic.StartDate) <= 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Fertilizer_Buy.FertilizerBuyKey) AS Counts, N'Mua phân bón' AS Type, '04' AS Sort
                       FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                                             dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
                       WHERE   (DATEDIFF(day, @DateMonday, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND  (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Fertilizer_Use.FertilizerUseKey) AS Counts, N'Sử dụng phân bón' AS Type, '05' AS Sort
                       FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                                             dbo.PUL_Fertilizers AS PUL_Fertilizers_1 ON dbo.PUL_Fertilizer_Use.FertilizerKey = PUL_Fertilizers_1.FertilizersKey
                       WHERE    (DATEDIFF(day, @DateMonday, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) <= 0) AND (dbo.PUL_Fertilizer_Use.IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Pesticide_Buy.PesticideBuyKey) AS Counts, N'Mua thuốc BVTV' AS Type, '06' AS Sort
                       FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                                             dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey
                       WHERE   (DATEDIFF(day, @DateMonday, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND   (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Pesticide_Use.PesticideUseKey) AS Counts, N'Sử dụng thuốc BVTV' AS Type, '07' AS Sort
                       FROM         dbo.PUL_Pesticide_Use INNER JOIN
                                             dbo.PUL_Pesticides AS PUL_Pesticides_1 ON dbo.PUL_Pesticide_Use.PesticideKey = PUL_Pesticides_1.PesticideKey
                       WHERE    (DATEDIFF(day, @DateMonday, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) <= 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_HarvestedForSale.HarvestedForSaleKey) AS Counts, N'Thu hoạch xuất bán' AS Type, '08' AS Sort
                       FROM         dbo.PUL_HarvestedForSale INNER JOIN
                                             dbo.PUL_Seeds AS PUL_Seeds_1 ON dbo.PUL_HarvestedForSale.SeedsKey = PUL_Seeds_1.SeedsKey
                       WHERE    (DATEDIFF(day, @DateMonday, dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_HarvestedForSale.Datetime) <= 0)
                       UNION
                       SELECT    COUNT(dbo.PUL_CheckEquipment.CheckEquipmentKey) AS Counts, N'Kiểm tra thiết bị' AS Type, '09' AS Sort
                       FROM         dbo.PUL_CheckEquipment INNER JOIN
                                             dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey
                       WHERE    (DATEDIFF(day, @DateMonday, dbo.PUL_CheckEquipment.Datetime) >= 0) AND  (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateNum, dbo.PUL_CheckEquipment.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) <= 0)
                       UNION
                       SELECT     COUNT(HandlingPackagingKey) AS Counts, N'Xử lý chất thải' AS Type, '10' AS Sort
                       FROM         dbo.PUL_HandlingPackaging
                       WHERE   (DATEDIFF(day, @DateMonday, Datetime) >= 0) AND  (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
                        UNION
                        SELECT     COUNT(dbo.PUL_Inventory.InventoryKey) AS Counts, N'Kiểm kê tồn kho' AS Type, '11' AS Sort
                        FROM         dbo.PUL_Inventory INNER JOIN
                                                  (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                                                    FROM          dbo.PUL_Fertilizers
                                                    UNION
                                                    SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                                                    FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                                              dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
                        WHERE   (DATEDIFF(day, @DateMonday, dbo.PUL_Inventory.Datetime) >= 0) AND   (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) <= 0) 
                                               AND (DATEDIFF(day, @DateNum, dbo.PUL_Inventory.Datetime) >= 0)
UNION
SELECT     COUNT(CheckAssessmentKey) AS Counts, N'Kiểm tra đánh giá' AS Type, '12' AS Sort
FROM         dbo.PUL_CheckAssessment
WHERE   (DATEDIFF(day, @DateMonday, Datetime) >= 0) AND  (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
) AS A";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
                zCommand.Parameters.Add("@DateNum", SqlDbType.DateTime).Value = DateNum;
                zCommand.Parameters.Add("@DateMonday", SqlDbType.DateTime).Value = DateMonday;
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
        #region [ Danh sách công việc chưa check hôm nay]
        public static DataTable GetListNotActiveByMemberNow(int MemberKey, DateTime DateOfManufacture)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT  *
FROM         (SELECT   LandUseKey AS KeyID, Action AS Name, Datetime, N'Quản lý đất' AS Type, '01' AS Sort, 'LandUse' as NameBook
FROM          dbo.PUL_LandUse
WHERE    (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) AND (MemberKey = @MemberKey) AND (IsActive = 0)
UNION
SELECT   (dbo.PUL_SeedProcess.SeedProcessKey) AS KeyID, (dbo.PUL_Seeds.SeedsName) AS Name, 
                        (dbo.PUL_SeedProcess.DateOfManufacture) AS Datetime, N'Xử lý giống' AS Type, '02' AS Sort, 'SeedsProcess' as NameBook
FROM         dbo.PUL_Seeds INNER JOIN
                        dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey
WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND (dbo.PUL_SeedProcess.IsActive = 0)
UNION
SELECT     dbo.PUL_CompostingOrganic.CompostingKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, dbo.PUL_CompostingOrganic.StartDate AS Datetime, 
                      N'Quản lý ủ phân hữu cơ' AS Type, '03' AS Sort, 'CompostingOrganic' as NameBook
FROM         dbo.PUL_CompostingOrganic INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_CompostingOrganic.StartDate) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CompostingOrganic.StartDate) <= 0)              
UNION
SELECT      dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime, N'Mua phân bón' AS Type, '04' AS Sort, 'Fertilizer_Buy' as NameBook
FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.IsActive = 0)
UNION
SELECT      dbo.PUL_Fertilizer_Use.FertilizerUseKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_Fertilizer_Use.DateTimeUse AS Datetime, N'Sử dụng phân bón' AS Type, '05' AS Sort, 'Fertilizer_Use' as NameBook
FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) <= 0) AND (dbo.PUL_Fertilizer_Use.IsActive = 0)
UNION
SELECT      dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, 
                      dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime, N'Mua thuốc BVTV' AS Type, '06' AS Sort, 'PesticideBuy' as NameBook
FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey
WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND 
                      (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0)
UNION
SELECT      dbo.PUL_Pesticide_Use.PesticideUseKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name, 
                      dbo.PUL_Pesticide_Use.DatetimeUse AS Datetime, N'Sử dụng thuốc BVTV' AS Type, '07' AS Sort, 'Pesticide_Use' as NameBook
FROM         dbo.PUL_Pesticide_Use INNER JOIN
                      dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey
WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND 
                      (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) <= 0)
UNION
SELECT     dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_HarvestedForSale.Datetime, 
                      N'Thu hoạch xuất bán' AS Type, '08' AS Sort, 'HarvestedForSale' as NameBook
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_HarvestedForSale.Datetime) <= 0)
UNION
SELECT      dbo.PUL_CheckEquipment.CheckEquipmentKey AS KeyID, dbo.PUL_Equipment.EquipmentName AS Name, dbo.PUL_CheckEquipment.Datetime, 
                      N'Kiểm tra thiết bị' AS Type, '09' AS Sort, 'CheckEquipment' as NameBook
FROM         dbo.PUL_CheckEquipment INNER JOIN
                      dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey
WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND 
                      (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) <= 0)
UNION
SELECT      HandlingPackagingKey AS KeyID, Type AS Name, Datetime, N'Xử lý chất thải' AS Type, '10' AS Sort, 'HandlingPackaging' as NameBook
FROM         dbo.PUL_HandlingPackaging
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
UNION
SELECT     dbo.PUL_Inventory.InventoryKey AS KeyID, A.Name,dbo.PUL_Inventory.Datetime, N'Kiểm kê tồn kho' AS Type, '11' AS Sort, 'Inventory' as NameBook
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey)AND (dbo.PUL_Inventory.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) <= 0) 
                       AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) >= 0)
UNION
SELECT     CheckAssessmentKey AS KeyID,DescribesError AS Name, Datetime, N'Kiểm tra đánh giá' AS Type, '12' AS Sort, 'CheckAssessment' as NameBook
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)

) AS A
ORDER BY Sort,Datetime desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
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
        public static DataTable GetCountsNotActiveByMemberNow(int MemberKey, DateTime DateOfManufacture)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT    SUM(Counts) AS Counts
                        FROM         (SELECT     COUNT(LandUseKey) AS Counts, N'Quản lý đất' AS Type, '01' AS Sort
                       FROM          dbo.PUL_LandUse
                       WHERE      (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) AND (MemberKey = @MemberKey) AND (IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_SeedProcess.SeedProcessKey) AS Counts, N'Xử lý giống' AS Type, '02' AS Sort
                       FROM         dbo.PUL_Seeds INNER JOIN
                                             dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey
                       WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND (dbo.PUL_SeedProcess.IsActive = 0)
                       UNION
                       SELECT      COUNT(dbo.PUL_CompostingOrganic.CompostingKey) AS Counts, N'Quản lý ủ phân hữu cơ' AS Type, '03' AS Sort
                       FROM         dbo.PUL_CompostingOrganic INNER JOIN
                                             dbo.PUL_FertilizerOrganic ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_FertilizerOrganic.FertilizerOrganicKey
                       WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                       dbo.PUL_CompostingOrganic.StartDate) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CompostingOrganic.StartDate) <= 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Fertilizer_Buy.FertilizerBuyKey) AS Counts, N'Mua phân bón' AS Type, '04' AS Sort
                       FROM         dbo.PUL_Fertilizer_Buy INNER JOIN
                                             dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey
                       WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Fertilizer_Use.FertilizerUseKey) AS Counts, N'Sử dụng phân bón' AS Type, '05' AS Sort
                       FROM         dbo.PUL_Fertilizer_Use INNER JOIN
                                             dbo.PUL_Fertilizers AS PUL_Fertilizers_1 ON dbo.PUL_Fertilizer_Use.FertilizerKey = PUL_Fertilizers_1.FertilizersKey
                       WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) <= 0) AND (dbo.PUL_Fertilizer_Use.IsActive = 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Pesticide_Buy.PesticideBuyKey) AS Counts, N'Mua thuốc BVTV' AS Type, '06' AS Sort
                       FROM         dbo.PUL_Pesticide_Buy INNER JOIN
                                             dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey
                       WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_Pesticide_Use.PesticideUseKey) AS Counts, N'Sử dụng thuốc BVTV' AS Type, '07' AS Sort
                       FROM         dbo.PUL_Pesticide_Use INNER JOIN
                                             dbo.PUL_Pesticides AS PUL_Pesticides_1 ON dbo.PUL_Pesticide_Use.PesticideKey = PUL_Pesticides_1.PesticideKey
                       WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) <= 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_HarvestedForSale.HarvestedForSaleKey) AS Counts, N'Thu hoạch xuất bán' AS Type, '08' AS Sort
                       FROM         dbo.PUL_HarvestedForSale INNER JOIN
                                             dbo.PUL_Seeds AS PUL_Seeds_1 ON dbo.PUL_HarvestedForSale.SeedsKey = PUL_Seeds_1.SeedsKey
                       WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateOfManufacture, dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_HarvestedForSale.Datetime) <= 0)
                       UNION
                       SELECT     COUNT(dbo.PUL_CheckEquipment.CheckEquipmentKey) AS Counts, N'Kiểm tra thiết bị' AS Type, '09' AS Sort
                       FROM         dbo.PUL_CheckEquipment INNER JOIN
                                             dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey
                       WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND 
                                             (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) <= 0)
                       UNION
                       SELECT     COUNT(HandlingPackagingKey) AS Counts, N'Xử lý chất thải' AS Type, '10' AS Sort
                       FROM         dbo.PUL_HandlingPackaging
                       WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
                        UNION
                        SELECT     COUNT(dbo.PUL_Inventory.InventoryKey) AS Counts, N'Kiểm kê tồn kho' AS Type, '11' AS Sort
                        FROM         dbo.PUL_Inventory INNER JOIN
                                                  (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                                                    FROM          dbo.PUL_Fertilizers
                                                    UNION
                                                    SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                                                    FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                                              dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
                        WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) <= 0) 
                                               AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) >= 0)
UNION
SELECT     COUNT(CheckAssessmentKey) AS Counts, N'Kiểm tra đánh giá' AS Type, '12' AS Sort
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
) AS A";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
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
        #region [ load Danh sách Công việc Sẽ làm, chưa làm, mới ]
        public static DataTable GetListWilldo(string Type, int MemberKey, DateTime DateOfManufacture,int Num)
        {
            DateTime DateNum = DateOfManufacture - new TimeSpan(Num, 0, 0, 0);
            DataTable zTable = new DataTable();
            string zSQL = @"IF @Type = N'LandUse'
SELECT   LandUseKey AS KeyID, Action AS Name,Datetime FROM dbo.PUL_LandUse WHERE  (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) AND (MemberKey = @MemberKey) AND (IsActive = 0) ORDER BY Datetime
IF @Type = N'SeedsProcess'
SELECT   (dbo.PUL_SeedProcess.SeedProcessKey) AS KeyID, (dbo.PUL_Seeds.SeedsName) AS Name,dbo.PUL_SeedProcess.DateOfManufacture AS Datetime FROM dbo.PUL_Seeds INNER JOIN dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND (dbo.PUL_SeedProcess.IsActive = 0) ORDER BY Datetime
IF @Type = N'CompostingOrganic'
SELECT  dbo.PUL_CompostingOrganic.CompostingKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_CompostingOrganic.StartDate AS Datetime
FROM         dbo.PUL_CompostingOrganic INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_CompostingOrganic.StartDate) <= 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_CompostingOrganic.StartDate) >= 0)
ORDER BY Datetime
IF @Type = N'Fertilizer_Buy'
SELECT      dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name,dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime FROM dbo.PUL_Fertilizer_Buy INNER JOIN dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.IsActive = 0) ORDER BY Datetime
IF @Type = N'Fertilizer_Use'
SELECT      dbo.PUL_Fertilizer_Use.FertilizerUseKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name,dbo.PUL_Fertilizer_Use.DateTimeUse AS Datetime FROM dbo.PUL_Fertilizer_Use INNER JOIN dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateNum, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) <= 0) AND (dbo.PUL_Fertilizer_Use.IsActive = 0) ORDER BY Datetime
IF @Type = N'PesticideBuy'
SELECT      dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name,dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime FROM dbo.PUL_Pesticide_Buy INNER JOIN dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) ORDER BY Datetime
IF @Type = N'Pesticide_Use'
SELECT      dbo.PUL_Pesticide_Use.PesticideUseKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name,dbo.PUL_Pesticide_Use.DateTimeUse AS Datetime FROM dbo.PUL_Pesticide_Use INNER JOIN dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) <= 0) ORDER BY Datetime
IF @Type = N'HarvestedForSale'
SELECT    dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, dbo.PUL_HarvestedForSale.Datetime, dbo.PUL_Seeds.SeedsName AS Name
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_HarvestedForSale.Datetime) <= 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_HarvestedForSale.Datetime) >= 0)
ORDER BY dbo.PUL_HarvestedForSale.Datetime
IF @Type = N'CheckEquipment'
SELECT      dbo.PUL_CheckEquipment.CheckEquipmentKey AS KeyID, dbo.PUL_Equipment.EquipmentName AS Name,dbo.PUL_CheckEquipment.Datetime FROM         dbo.PUL_CheckEquipment INNER JOIN dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_CheckEquipment.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) <= 0) ORDER BY Datetime
IF @Type = N'HandlingPackaging'
SELECT      HandlingPackagingKey AS KeyID, Type AS Name,Datetime FROM dbo.PUL_HandlingPackaging WHERE (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) ORDER BY Datetime
IF @Type = N'Inventory'
SELECT     dbo.PUL_Inventory.InventoryKey AS KeyID, A.Name, dbo.PUL_Inventory.Datetime
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_Inventory.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) <= 0) 
ORDER BY Datetime
IF @Type = N'CheckAssessment'
SELECT  CheckAssessmentKey AS KeyID, DescribesError AS Name, Datetime
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateNum, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
ORDER BY Datetime
";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
                zCommand.Parameters.Add("@DateNum", SqlDbType.DateTime).Value = DateNum;
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
        public static DataTable GetListWilldoFuture(string Type, int MemberKey, DateTime DateOfManufacture, int Num)
        {
            DateTime DateNum = DateOfManufacture - new TimeSpan(Num, 0, 0, 0);
            DataTable zTable = new DataTable();
            string zSQL = @"IF @Type = N'LandUse'
SELECT   LandUseKey AS KeyID, Action AS Name,Datetime FROM dbo.PUL_LandUse WHERE (DATEDIFF(day, @DateOfManufacture, Datetime) > 0) AND (MemberKey = @MemberKey) AND (IsActive = 0) ORDER BY Datetime
IF @Type = N'SeedsProcess'
SELECT   (dbo.PUL_SeedProcess.SeedProcessKey) AS KeyID, (dbo.PUL_Seeds.SeedsName) AS Name,dbo.PUL_SeedProcess.DateOfManufacture AS Datetime FROM dbo.PUL_Seeds INNER JOIN dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) > 0) AND (dbo.PUL_SeedProcess.IsActive = 0) ORDER BY Datetime
IF @Type = N'CompostingOrganic'
SELECT   dbo.PUL_CompostingOrganic.CompostingKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_CompostingOrganic.StartDate AS Datetime
FROM         dbo.PUL_CompostingOrganic INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_CompostingOrganic.StartDate) > 0)
ORDER BY Datetime
IF @Type = N'Fertilizer_Buy'
SELECT      dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name,dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime FROM dbo.PUL_Fertilizer_Buy INNER JOIN dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) > 0) AND (dbo.PUL_Fertilizer_Buy.IsActive = 0) ORDER BY Datetime
IF @Type = N'Fertilizer_Use'
SELECT      dbo.PUL_Fertilizer_Use.FertilizerUseKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name,dbo.PUL_Fertilizer_Use.DateTimeUse AS Datetime FROM dbo.PUL_Fertilizer_Use INNER JOIN dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) > 0) AND (dbo.PUL_Fertilizer_Use.IsActive = 0) ORDER BY Datetime
IF @Type = N'PesticideBuy'
SELECT      dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name,dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime FROM dbo.PUL_Pesticide_Buy INNER JOIN dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) > 0) ORDER BY Datetime
IF @Type = N'Pesticide_Use'
SELECT      dbo.PUL_Pesticide_Use.PesticideUseKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name,dbo.PUL_Pesticide_Use.DateTimeUse AS Datetime FROM dbo.PUL_Pesticide_Use INNER JOIN dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DatetimeUse) > 0) ORDER BY Datetime
IF @Type = N'HarvestedForSale'
SELECT  dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_HarvestedForSale.Datetime
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_HarvestedForSale.Datetime) > 0)
ORDER BY dbo.PUL_HarvestedForSale.Datetime
IF @Type = N'CheckEquipment'
SELECT      dbo.PUL_CheckEquipment.CheckEquipmentKey AS KeyID, dbo.PUL_Equipment.EquipmentName AS Name,dbo.PUL_CheckEquipment.Datetime FROM         dbo.PUL_CheckEquipment INNER JOIN dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) > 0) ORDER BY Datetime
IF @Type = N'HandlingPackaging'
SELECT      HandlingPackagingKey AS KeyID, Type AS Name,Datetime FROM dbo.PUL_HandlingPackaging WHERE (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) > 0) ORDER BY Datetime
IF @Type = N'Inventory'
SELECT     dbo.PUL_Inventory.InventoryKey AS KeyID, A.Name, dbo.PUL_Inventory.Datetime
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) > 0) 
ORDER BY Datetime
IF @Type = N'CheckAssessment'
SELECT  CheckAssessmentKey AS KeyID, DescribesError AS Name, Datetime
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) > 0)
ORDER BY Datetime

";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
                zCommand.Parameters.Add("@DateNum", SqlDbType.DateTime).Value = DateNum;
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
        public static DataTable GetListWilldoNow(string Type, int MemberKey, DateTime DateOfManufacture, int Num)
        {
            DateTime DateNum = DateOfManufacture - new TimeSpan(Num, 0, 0, 0);
            DataTable zTable = new DataTable();
            string zSQL = @"IF @Type = N'LandUse'
SELECT   LandUseKey AS KeyID, Action AS Name,Datetime FROM dbo.PUL_LandUse WHERE  (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) AND (MemberKey = @MemberKey) AND (IsActive = 0) ORDER BY Datetime
IF @Type = N'SeedsProcess'
SELECT   (dbo.PUL_SeedProcess.SeedProcessKey) AS KeyID, (dbo.PUL_Seeds.SeedsName) AS Name,dbo.PUL_SeedProcess.DateOfManufacture AS Datetime FROM dbo.PUL_Seeds INNER JOIN dbo.PUL_SeedProcess ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_SeedProcess.SeedsKey WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.DateOfManufacture) <= 0) AND (dbo.PUL_SeedProcess.IsActive = 0) ORDER BY Datetime
IF @Type = N'CompostingOrganic'
SELECT  dbo.PUL_CompostingOrganic.CompostingKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name, 
                      dbo.PUL_CompostingOrganic.StartDate AS Datetime
FROM         dbo.PUL_CompostingOrganic INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_CompostingOrganic.FertilizerOrganicKey = dbo.PUL_Fertilizers.FertilizersKey
WHERE     (dbo.PUL_CompostingOrganic.MemberKey = @MemberKey) AND (dbo.PUL_CompostingOrganic.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_CompostingOrganic.StartDate) <= 0) AND (DATEDIFF(day, @DateNum, dbo.PUL_CompostingOrganic.StartDate) >= 0)
ORDER BY Datetime
IF @Type = N'Fertilizer_Buy'
SELECT      dbo.PUL_Fertilizer_Buy.FertilizerBuyKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name,dbo.PUL_Fertilizer_Buy.DatetimeBuy AS Datetime FROM dbo.PUL_Fertilizer_Buy INNER JOIN dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Buy.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey WHERE     (dbo.PUL_Fertilizer_Buy.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Buy.DatetimeBuy) <= 0) AND (dbo.PUL_Fertilizer_Buy.IsActive = 0) ORDER BY Datetime
IF @Type = N'Fertilizer_Use'
SELECT      dbo.PUL_Fertilizer_Use.FertilizerUseKey AS KeyID, dbo.PUL_Fertilizers.TradeName AS Name,dbo.PUL_Fertilizer_Use.DateTimeUse AS Datetime FROM dbo.PUL_Fertilizer_Use INNER JOIN dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Use.FertilizerKey = dbo.PUL_Fertilizers.FertilizersKey WHERE     (dbo.PUL_Fertilizer_Use.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Fertilizer_Use.DateTimeUse) <= 0) AND (dbo.PUL_Fertilizer_Use.IsActive = 0) ORDER BY Datetime
IF @Type = N'PesticideBuy'
SELECT      dbo.PUL_Pesticide_Buy.PesticideBuyKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name,dbo.PUL_Pesticide_Buy.DatetimeBuy AS Datetime FROM dbo.PUL_Pesticide_Buy INNER JOIN dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Buy.PesticideKey = dbo.PUL_Pesticides.PesticideKey WHERE     (dbo.PUL_Pesticide_Buy.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Buy.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Buy.DatetimeBuy) <= 0) ORDER BY Datetime
IF @Type = N'Pesticide_Use'
SELECT      dbo.PUL_Pesticide_Use.PesticideUseKey AS KeyID, dbo.PUL_Pesticides.Trade_Name AS Name,dbo.PUL_Pesticide_Use.DateTimeUse AS Datetime FROM dbo.PUL_Pesticide_Use INNER JOIN dbo.PUL_Pesticides ON dbo.PUL_Pesticide_Use.PesticideKey = dbo.PUL_Pesticides.PesticideKey WHERE     (dbo.PUL_Pesticide_Use.MemberKey = @MemberKey) AND (dbo.PUL_Pesticide_Use.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Pesticide_Use.DateTimeUse) <= 0) ORDER BY Datetime
IF @Type = N'HarvestedForSale'
SELECT   dbo.PUL_HarvestedForSale.HarvestedForSaleKey AS KeyID, dbo.PUL_Seeds.SeedsName AS Name, dbo.PUL_HarvestedForSale.Datetime
FROM         dbo.PUL_HarvestedForSale INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.MemberKey = @MemberKey) AND (dbo.PUL_HarvestedForSale.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, 
                      dbo.PUL_HarvestedForSale.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_HarvestedForSale.Datetime) <= 0)
ORDER BY dbo.PUL_HarvestedForSale.Datetime
IF @Type = N'CheckEquipment'
SELECT      dbo.PUL_CheckEquipment.CheckEquipmentKey AS KeyID, dbo.PUL_Equipment.EquipmentName AS Name,dbo.PUL_CheckEquipment.Datetime FROM         dbo.PUL_CheckEquipment INNER JOIN dbo.PUL_Equipment ON dbo.PUL_CheckEquipment.EquipmentKey = dbo.PUL_Equipment.EquipmentKey WHERE     (dbo.PUL_CheckEquipment.MemberKey = @MemberKey) AND (dbo.PUL_CheckEquipment.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_CheckEquipment.Datetime) <= 0) ORDER BY Datetime
IF @Type = N'HandlingPackaging'
SELECT      HandlingPackagingKey AS KeyID, Type AS Name,Datetime FROM dbo.PUL_HandlingPackaging WHERE (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0) ORDER BY Datetime
IF @Type = N'Inventory'
SELECT     dbo.PUL_Inventory.InventoryKey AS KeyID, A.Name, dbo.PUL_Inventory.Datetime
FROM         dbo.PUL_Inventory INNER JOIN
                          (SELECT     FertilizersKey AS KeyID, TradeName AS Name, 1 AS Type
                            FROM          dbo.PUL_Fertilizers
                            UNION
                            SELECT     PesticideKey AS KeyID, Trade_Name AS Name, 2 AS Type
                            FROM         dbo.PUL_Pesticides) AS A ON dbo.PUL_Inventory.Type = A.Type AND dbo.PUL_Inventory.FertilizersPesticidesKey = A.KeyID INNER JOIN
                      dbo.PUL_Inventory_Type ON dbo.PUL_Inventory.Type = dbo.PUL_Inventory_Type.InventoryTypeKey
WHERE     (dbo.PUL_Inventory.MemberKey = @MemberKey) AND (dbo.PUL_Inventory.IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_Inventory.Datetime) <= 0) 
ORDER BY Datetime
IF @Type = N'CheckAssessment'
SELECT  CheckAssessmentKey AS KeyID, DescribesError AS Name, Datetime
FROM         dbo.PUL_CheckAssessment
WHERE     (MemberKey = @MemberKey) AND (IsActive = 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) >= 0) AND (DATEDIFF(day, @DateOfManufacture, Datetime) <= 0)
ORDER BY Datetime
";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
                zCommand.Parameters.Add("@DateNum", SqlDbType.DateTime).Value = DateNum;
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
        #region [ Update công việc chưa check ]
        public static string UpdateListIsActive(int key,string Type, int IsActive)
        {
            string nResult = ""; ;
            string nSQL = @"IF @Type = N'Quản lý đất'
                            UPDATE PUL_LandUse SET IsActive = @IsActive, IsSync = 1 WHERE LandUseKey = @KeyID;
                            IF @Type = N'Xử lý giống'
                            UPDATE PUL_SeedProcess SET IsActive = @IsActive, IsSync = 1  WHERE SeedProcessKey = @KeyID;
                            IF @Type = N'Quản lý ủ phân hữu cơ'
                            UPDATE PUL_CompostingOrganic SET IsActive = @IsActive, IsSync = 1  WHERE CompostingKey = @KeyID;
                            IF @Type = N'Mua phân bón'
                            UPDATE PUL_Fertilizer_Buy SET IsActive = @IsActive, IsSync = 1  WHERE FertilizerBuyKey = @KeyID;
                            IF @Type = N'Sử dụng phân bón'
                            UPDATE PUL_Fertilizer_Use SET IsActive = @IsActive, IsSync = 1  WHERE FertilizerUseKey = @KeyID;
                            IF @Type = N'Mua thuốc BVTV'
                            UPDATE PUL_Pesticide_Buy SET IsActive = @IsActive, IsSync = 1 WHERE PesticideBuyKey = @KeyID;
                            IF @Type = N'Sử dụng thuốc BVTV'
                            UPDATE PUL_Pesticide_Use SET IsActive = @IsActive, IsSync = 1 WHERE PesticideUseKey = @KeyID;
                            IF @Type = N'Thu hoạch xuất bán'
                            UPDATE PUL_HarvestedForSale SET IsActive = @IsActive, IsSync = 1 WHERE HarvestedForSaleKey = @KeyID;
                            IF @Type = N'Kiểm tra thiết bị'
                            UPDATE PUL_CheckEquipment SET IsActive = @IsActive, IsSync = 1 WHERE CheckEquipmentKey = @KeyID;
                            IF @Type = N'Xử lý chất thải'
                            UPDATE PUL_HandlingPackaging SET IsActive = @IsActive, IsSync = 1 WHERE HandlingPackagingKey = @KeyID;
                            IF @Type = N'Kiểm kê tồn kho'
                            UPDATE PUL_Inventory SET IsActive = @IsActive, IsSync = 1 WHERE InventoryKey = @KeyID;
                            IF @Type = N'Kiểm tra đánh giá'
                            UPDATE PUL_CheckAssessment SET IsActive = @IsActive, IsSync = 1 WHERE CheckAssessmentKey = @KeyID;";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                nCommand.Parameters.Add("@KeyID", SqlDbType.NVarChar).Value = key;
                nCommand.Parameters.Add("@IsActive", SqlDbType.Int).Value = IsActive;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();
            }
            catch (Exception Err)
            {

            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        #endregion
        #region [ Xóa công việc]
        public static string DelbyKeyAll(int key, string Type)
        {
            int memberID = HttpContext.Current.Session["MemberID"].ToInt();
            string nResult = ""; ;

            //---------- String SQL Access Database ---------------
            string nSQL = @"IF @Type = N'LandUse'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_LandUse',@KeyID,@MemberKey) 
                                DELETE PUL_LandUse  WHERE LandUseKey = @KeyID;
                            IF @Type = N'SeedsProcess'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_SeedProcess',@KeyID,@MemberKey) 
                                DELETE PUL_SeedProcess  WHERE SeedProcessKey = @KeyID
                                DELETE PUL_Fertilizer_Buy  WHERE SeedsKey = @KeyID
                                DELETE PUL_Fertilizer_Use  WHERE SeedKey = @KeyID
                                DELETE PUL_Pesticide_Buy  WHERE SeedsKey = @KeyID
                                DELETE PUL_Pesticide_Use  WHERE SeedKey = @KeyID
                                DELETE PUL_HarvestedForSale  WHERE SeedsKey = @KeyID
                                DELETE PUL_CheckEquipment  WHERE SeedsKey = @KeyID
                                DELETE PUL_CheckAssessment WHERE SeedsKey = @KeyID


                            IF @Type = N'CompostingOrganic'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_CompostingOrganic',@KeyID,@MemberKey)
                                DELETE PUL_CompostingOrganic WHERE CompostingKey = @KeyID;
                            IF @Type = N'Fertilizer_Buy'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_Fertilizer_Buy',@KeyID,@MemberKey)
                                DELETE PUL_Fertilizer_Buy  WHERE FertilizerBuyKey = @KeyID;
                            IF @Type = N'Fertilizer_Use'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('Fertilizer_Use',@KeyID,@MemberKey)
                                DELETE PUL_Fertilizer_Use  WHERE FertilizerUseKey = @KeyID;
                            IF @Type = N'PesticideBuy'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_Pesticide_Buy',@KeyID,@MemberKey)
                                DELETE PUL_Pesticide_Buy  WHERE PesticideBuyKey = @KeyID;
                            IF @Type = N'Pesticide_Use'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_Pesticide_Use',@KeyID,@MemberKey)
                                DELETE PUL_Pesticide_Use  WHERE PesticideUseKey = @KeyID;
                            IF @Type = N'HarvestedForSale'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_HarvestedForSale',@KeyID,@MemberKey)
                                DELETE PUL_HarvestedForSale  WHERE HarvestedForSaleKey = @KeyID;
                            IF @Type = N'CheckEquipment'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_CheckEquipment',@KeyID,@MemberKey)
                                DELETE PUL_CheckEquipment  WHERE CheckEquipmentKey = @KeyID;
                            IF @Type = N'HandlingPackaging'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_HandlingPackaging',@KeyID,@MemberKey)
                                DELETE PUL_HandlingPackaging  WHERE HandlingPackagingKey = @KeyID;
                            IF @Type = N'Inventory'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_Inventory',@KeyID,@MemberKey)
                                DELETE PUL_Inventory WHERE InventoryKey = @KeyID;
                            IF @Type = N'CheckAssessment'
                                INSERT INTO Del_Temp(NameTable,ServerKey_Table,MemberKey)VALUES('PUL_CheckAssessment',@KeyID,@MemberKey)
                                DELETE PUL_CheckAssessment WHERE CheckAssessmentKey = @KeyID;";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                nCommand.Parameters.Add("@KeyID", SqlDbType.NVarChar).Value = key;
                nCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = memberID;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();
            }
            catch (Exception Err)
            {

            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        #endregion
        #region [ Load ProcessDetail]
        public static DataTable GetListSeedProcess(int Type,int CooperativeKey,int MemberKey, DateTime DateOfManufacture)
        {

            DataTable zTable = new DataTable();
            string zSQL = @"
IF(@Type = 1)
SELECT     dbo.PUL_Seed_Cooperative.SeedKey as SeedsKey, dbo.PUL_Seed_Cooperative.ID, dbo.PUL_Seeds.SeedsName, '0' as Datetime
                            FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                                                  dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey
                            WHERE     (dbo.PUL_Seed_Cooperative.CooperativeKey = @CooperativeKey)
ORDER BY dbo.PUL_Seeds.SeedsName
IF @Type = 2
SELECT     dbo.PUL_SeedProcess.SeedProcessKey as SeedsKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_SeedProcess.DateOfManufacture as Datetime, dbo.PUL_SeedProcess.Parcel
FROM         dbo.PUL_SeedProcess INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, PUL_SeedProcess.DateOfManufacture) <= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.EndTime) >= 0)
ORDER BY dbo.PUL_Seeds.SeedsName, Datetime
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
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DateOfManufacture;
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
        public static DataTable LoadProcessDetail(int CooperativeKey, int SeedsKey, int ProcessPlant_Type)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     ProcessPlantKey, Cooperative_Key, SeedsKey, ProcessPlant_Type, ProcessPlantName, Description
FROM         dbo.PUL_ProcessPlant
WHERE     (Cooperative_Key = @Cooperative_Key) AND (SeedsKey = @SeedsKey) AND (ProcessPlant_Type = @ProcessPlant_Type)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = CooperativeKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                zCommand.Parameters.Add("@ProcessPlant_Type", SqlDbType.Int).Value = ProcessPlant_Type;
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

        #region [ Save Quy trinh cong viec]
        public static string SaveExportProcessSeed(int key,int Type,int SeedKey, DateTime day, int memberID, int Area, int AreaUnit, int Quantity, int QuantityUnit)
        {
            string nResult = "";
            string nSQL = @" IF @Type = 1
INSERT INTO PUL_LandUse
                            ([Action],Reason,Solution,Note,MemberKey,SeedKey,[Datetime],CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,IsActive)
                            SELECT  [Action], Reason, Solution, Note, @MemberKey, SeedKey,DATEADD (Day, Datetime_Num, @day) , @CreatedBy,@CreatedDateTime,@CreatedBy,@CreatedDateTime,0
                            FROM         dbo.PUL_Process_LandUse AS pplu
                            WHERE pplu.ProcessPlantDetailKey IN(
                            SELECT ProcessPlantDetailKey FROM PUL_ProcessPlantDetail AS pppd
                            WHERE ProcessPlantKey=@KeyID AND ProcessPlantDetai_Type=1)
INSERT INTO PUL_SeedProcess
                            (
	                            SeedsKey,	CompanyName,	DateOfManufacture,	DateSowing,	DateBuy,	Quantity,	[Status],	PesticideKey,	Reasons,	MemberKey,	CooperativeKey,	Parcel,	Area,	QuantityUnit,	AreaUnit,	Total,	EndTime,	CreatedBy,	CreatedDateTime,ModifiedBy,ModifiedDateTime,	IsActive
                            )
                            SELECT      SeedsKey, CompanyName, DATEADD (Day, DateOfManufacture_Num, @day), DATEADD (Day, DateSowing_Num, @day), DATEADD (Day, DateBuy_Num, @day), @Quantity, [Status], PesticideKey, 
                                                  Reasons, @MemberKey, CooperativeKey, N'Chưa nhập mã lô', @Area, @QuantityUnit, @AreaUnit, Total, DATEADD (Day, EndTime_Num, @day), @CreatedBy,@CreatedDateTime,@CreatedBy,@CreatedDateTime,0
                            FROM         dbo.PUL_Process_SeedProcess AS ppsp
                            WHERE ppsp.ProcessPlantDetailKey   IN(
                            SELECT ProcessPlantDetailKey FROM PUL_ProcessPlantDetail AS pppd
                            WHERE ProcessPlantKey= @KeyID AND ProcessPlantDetai_Type=2)
INSERT INTO PUL_Fertilizer_Use
                            (
	                            DateTimeUse,	SeedKey,	Parcel,	Area,	FertilizerKey,	FormulaUsed,	Quantity,	Howtouse,	MemberKey,	CooperativeKey,	QuarantinePeriod,	UnitKey,	CreatedBy,	CreatedDateTime,ModifiedBy,ModifiedDateTime,	IsActive
                            )
                            SELECT DATEADD (Day, DateTimeUse_Num, @day) , (SELECT psp.SeedProcessKey FROM PUL_SeedProcess AS psp
								WHERE psp.CreatedBy=@CreatedBy AND psp.CreatedDateTime=@CreatedDateTime) as SeedKey, Parcel, Area, FertilizerKey, FormulaUsed, Quantity, Howtouse,@MemberKey, CooperativeKey, 
                                                  QuarantinePeriod, UnitKey, @CreatedBy,@CreatedDateTime,@CreatedBy,@CreatedDateTime,0
                            FROM         dbo.PUL_Process_FertilizerUse AS ppfu
                            WHERE ppfu.ProcessPlantDetailKey  IN(
                            SELECT ProcessPlantDetailKey FROM PUL_ProcessPlantDetail AS pppd
                            WHERE ProcessPlantKey= @KeyID AND ProcessPlantDetai_Type=3)
INSERT INTO PUL_Pesticide_Use
                            (
	                            DateTimeUse,	SeedKey,	PestName,	Area,	PesticideKey,	Dose,	Dosage,	EquipmentKey,	MemberKey,	CooperativeKey,	Solution,	QuarantinePeriod,	UnitKey,	CreatedBy,	CreatedDateTime,ModifiedBy,ModifiedDateTime,	IsActive
                            )
                            SELECT DATEADD (Day, DateTimeUse_Num, @day), (SELECT psp.SeedProcessKey FROM PUL_SeedProcess AS psp
								WHERE psp.CreatedBy=@CreatedBy AND psp.CreatedDateTime=@CreatedDateTime) as SeedKey, PestName, Area, PesticideKey, Dose, N'Chưa nhập', EquipmentKey,@MemberKey, CooperativeKey, Solution, 
                                                  QuarantinePeriod, UnitKey, @CreatedBy,@CreatedDateTime,@CreatedBy,@CreatedDateTime,0
                            FROM         dbo.PUL_Process_PesticideUse AS pppu
                            WHERE pppu.ProcessPlantDetailKey IN(
                            SELECT ProcessPlantDetailKey FROM PUL_ProcessPlantDetail AS pppd
                            WHERE ProcessPlantKey= @KeyID AND ProcessPlantDetai_Type=4);
IF @Type = 2
INSERT INTO PUL_LandUse
                            ([Action],Reason,Solution,Note,MemberKey,SeedKey,[Datetime],CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,IsActive)
                            SELECT  [Action], Reason, Solution, Note, @MemberKey, SeedKey,DATEADD (Day, Datetime_Num, @day) , @CreatedBy,@CreatedDateTime,@CreatedBy,@CreatedDateTime,0
                            FROM         dbo.PUL_Process_LandUse AS pplu
                            WHERE pplu.ProcessPlantDetailKey IN(
                            SELECT ProcessPlantDetailKey FROM PUL_ProcessPlantDetail AS pppd
                            WHERE ProcessPlantKey=@KeyID AND ProcessPlantDetai_Type=1)
IF @Type = 2
INSERT INTO PUL_Fertilizer_Use
                            (
	                            DateTimeUse,	SeedKey,	Parcel,	Area,	FertilizerKey,	FormulaUsed,	Quantity,	Howtouse,	MemberKey,	CooperativeKey,	QuarantinePeriod,	UnitKey,	CreatedBy,	CreatedDateTime,ModifiedBy,ModifiedDateTime,	IsActive
                            )
                            SELECT DATEADD (Day, DateTimeUse_Num, @day) , @SeedKey, Parcel, Area, FertilizerKey, FormulaUsed, Quantity, Howtouse,@MemberKey, CooperativeKey, 
                                                  QuarantinePeriod, UnitKey, @CreatedBy,@CreatedDateTime,@CreatedBy,@CreatedDateTime,0
                            FROM         dbo.PUL_Process_FertilizerUse AS ppfu
                            WHERE ppfu.ProcessPlantDetailKey  IN(
                            SELECT ProcessPlantDetailKey FROM PUL_ProcessPlantDetail AS pppd
                            WHERE ProcessPlantKey= @KeyID AND ProcessPlantDetai_Type=3)
IF @Type = 2
INSERT INTO PUL_Pesticide_Use
                            (
	                            DateTimeUse,	SeedKey,	PestName,	Area,	PesticideKey,	Dose,	Dosage,	EquipmentKey,	MemberKey,	CooperativeKey,	Solution,	QuarantinePeriod,	UnitKey,	CreatedBy,	CreatedDateTime,ModifiedBy,ModifiedDateTime,	IsActive
                            )
                            SELECT DATEADD (Day, DateTimeUse_Num, @day), @SeedKey, PestName, Area, PesticideKey, Dose, N'Chưa nhập', EquipmentKey,@MemberKey, CooperativeKey, Solution, 
                                                  QuarantinePeriod, UnitKey, @CreatedBy,@CreatedDateTime,@CreatedBy,@CreatedDateTime,0
                            FROM         dbo.PUL_Process_PesticideUse AS pppu
                            WHERE pppu.ProcessPlantDetailKey IN(
                            SELECT ProcessPlantDetailKey FROM PUL_ProcessPlantDetail AS pppd
                            WHERE ProcessPlantKey= @KeyID AND ProcessPlantDetai_Type=4)
                            ";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();

            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@day", SqlDbType.DateTime).Value = day;
                nCommand.Parameters.Add("@Type", SqlDbType.Int).Value = Type;
                nCommand.Parameters.Add("@KeyID", SqlDbType.NVarChar).Value = key;
                nCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = SeedKey;
                nCommand.Parameters.Add("@MemberKey", SqlDbType.NVarChar).Value = memberID;
                nCommand.Parameters.Add("@Area", SqlDbType.Int).Value = Area;
                nCommand.Parameters.Add("@AreaUnit", SqlDbType.Int).Value = AreaUnit;
                nCommand.Parameters.Add("@Quantity", SqlDbType.Int).Value = Quantity;
                nCommand.Parameters.Add("@QuantityUnit", SqlDbType.Int).Value = QuantityUnit;
                nCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = new Guid(HttpContext.Current.Session["SysUserKey"].ToString());
                if (DateTime.Now.Year == 0001)
                    nCommand.Parameters.Add("@  ", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    nCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DateTime.Now;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();
            }
            catch (Exception Err)
            {

            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        #endregion
    }
}
