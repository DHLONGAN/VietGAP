using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TNConfig;
using System.Data;
using System.Data.SqlClient;
namespace TNLibrary.Culture
{
    public class History_Data
    {
        public static DataTable GetList(DateTime fromdate, DateTime todate, int CooperativeKey, int PageSize, int PageNumber, int MemberKey, string SeedsName)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"WITH NhatKy
                        AS
                        (
	                        SELECT DatetimeBuy as NhatKyNgay,N'Mua phân bón ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Fertilizer_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '1' AS Sort, SeedsKey FROM dbo.PUL_Fertilizer_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Parcel + ', lý do: ' + Area + N', liều dùng: ' + Convert(nvarchar(20),FormulaUsed) AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '2' AS Sort, SeedKey As SeedsKey FROM dbo.PUL_Fertilizer_Use
	                        UNION ALL
	                        SELECT DatetimeBuy as NhatKyNgay,N'Mua thuốc BVTV ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Pesticide_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '3' AS Sort, SeedsKey FROM dbo.PUL_Pesticide_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Area + ', lý do: ' + PestName + N', liều dùng: ' + Convert(nvarchar(20),Dose) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +'/'+Dosage AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '4' AS Sort, SeedKey aS SeedsKey  FROM dbo.PUL_Pesticide_Use
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Xử lý đất, công việc: ' +Action + ', phương pháp: ' + Solution AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '5' AS Sort, SeedKey as SeedsKey  FROM dbo.PUL_LandUse
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Thu Hoạch - Xuất bán, mã truy vết: '+ Code+ N' , số lượng thu hoạch: ' +Convert(nvarchar(20),QuantityHarvested) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey))+N', số lượng xuất bán: ' + Convert(nvarchar(20),QuantitySale) +Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +N', nơi mua: ' + WhereToBuy AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '6' AS Sort, SeedsKey  FROM dbo.PUL_HarvestedForSale
                       
                        )
                        SELECT * FROM NhatKy where NhatKyNgay >= @fromdate and NhatKyNgay <= @todate and Cooperative_Key = @CooperativeKey and Name = @MemberKey and SeedsKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) and Name IS NOT NULL order by NhatKyNgay desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetList(int CooperativeKey, int PageSize, int PageNumber, int MemberKey, string SeedsName)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"WITH NhatKy
                        AS
                        (
	                        SELECT DatetimeBuy as NhatKyNgay,N'Mua phân bón ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Fertilizer_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '1' AS Sort, SeedsKey FROM dbo.PUL_Fertilizer_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Parcel + ', lý do: ' + Area + N', liều dùng: ' + Convert(nvarchar(20),FormulaUsed) AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '2' AS Sort, SeedKey As SeedsKey FROM dbo.PUL_Fertilizer_Use
	                        UNION ALL
	                        SELECT DatetimeBuy as NhatKyNgay,N'Mua thuốc BVTV ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Pesticide_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '3' AS Sort, SeedsKey FROM dbo.PUL_Pesticide_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Area + ', lý do: ' + PestName + N', liều dùng: ' + Convert(nvarchar(20),Dose) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +'/'+Dosage AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '4' AS Sort, SeedKey aS SeedsKey  FROM dbo.PUL_Pesticide_Use
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Xử lý đất, công việc: ' +Action + ', phương pháp: ' + Solution AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '5' AS Sort, SeedKey as SeedsKey  FROM dbo.PUL_LandUse
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Thu Hoạch - Xuất bán, mã truy vết: '+ Code+ N' , số lượng thu hoạch: ' +Convert(nvarchar(20),QuantityHarvested) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey))+N', số lượng xuất bán: ' + Convert(nvarchar(20),QuantitySale) +Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +N', nơi mua: ' + WhereToBuy AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '6' AS Sort, SeedsKey  FROM dbo.PUL_HarvestedForSale
                       
                        )
                        
                        SELECT * FROM NhatKy where Name = @MemberKey and SeedsKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName)) and Name IS NOT NULL order by NhatKyNgay desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            //SELECT * FROM NhatKy where Cooperative_Key = @CooperativeKey and Name = @MemberKey and SeedsKey IN (
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }
        public static DataTable GetListofMember(int PageSize, int PageNumber, int MemberKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"WITH NhatKy
                        AS
                        (
	                        SELECT DatetimeBuy as NhatKyNgay,N'Mua phân bón ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Fertilizer_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '1' AS Sort, SeedsKey FROM dbo.PUL_Fertilizer_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Parcel + ', lý do: ' + Area + N', liều dùng: ' + Convert(nvarchar(20),FormulaUsed) AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '2' AS Sort, SeedKey As SeedsKey FROM dbo.PUL_Fertilizer_Use
	                        UNION ALL
	                        SELECT DatetimeBuy as NhatKyNgay,N'Mua thuốc BVTV ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Pesticide_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '3' AS Sort, SeedsKey FROM dbo.PUL_Pesticide_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Area + ', lý do: ' + PestName + N', liều dùng: ' + Convert(nvarchar(20),Dose) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +'/'+Dosage AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '4' AS Sort, SeedKey aS SeedsKey  FROM dbo.PUL_Pesticide_Use
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Xử lý đất, công việc: ' +Action + ', phương pháp: ' + Solution AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '5' AS Sort, SeedKey as SeedsKey  FROM dbo.PUL_LandUse
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Thu Hoạch - Xuất bán, loại cây:'+Convert(nvarchar(20),(Select SeedsName from PUL_Seeds where SeedsKey  = PUL_HarvestedForSale.SeedsKey)) +N', mã truy vết: '+ Code+ N' , số lượng thu hoạch: ' +Convert(nvarchar(20),QuantityHarvested) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey))+N', số lượng xuất bán: ' + Convert(nvarchar(20),QuantitySale) +Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +N', nơi mua: ' + WhereToBuy AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name, '6' AS Sort, SeedsKey  FROM dbo.PUL_HarvestedForSale
                       
                        )
                        SELECT * FROM NhatKy where Name = @MemberKey and Name IS NOT NULL order by NhatKyNgay desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
                zCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }

        public static int Count(int MemberKey, string SeedsName)
        {
            int _Result = 0;
            string cmdText = @"
                            WITH NhatKy
                            AS
                            (
	                            SELECT DatetimeBuy as NhatKyNgay,N'Mua phân bón ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Fertilizer_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name,  SeedsKey FROM dbo.PUL_Fertilizer_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Parcel + ', lý do: ' + Area + N', liều dùng: ' + Convert(nvarchar(20),FormulaUsed) AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name,  SeedKey  FROM dbo.PUL_Fertilizer_Use
	                        UNION ALL
	                        SELECT DatetimeBuy as NhatKyNgay,N'Mua thuốc BVTV ' + N', số lượng: ' + Convert(nvarchar(20),Quantity) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = dbo.PUL_Pesticide_Buy.UnitKey)) + Convert(nvarchar(20),Price) + N' ,địa chỉ mua: ' + Address AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name,  SeedsKey FROM dbo.PUL_Pesticide_Buy
	                        UNION ALL
	                        SELECT DateTimeUse as NhatKyNgay,N'Sử dụng phân bón, công việc: ' +Area + ', lý do: ' + PestName + N', liều dùng: ' + Convert(nvarchar(20),Dose) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +'/'+Dosage AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name,  SeedKey  FROM dbo.PUL_Pesticide_Use
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Xử lý đất, công việc: ' +Action + ', phương pháp: ' + Solution AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name,  SeedKey  FROM dbo.PUL_LandUse
							UNION ALL
	                        SELECT Datetime as NhatKyNgay,N'Thu Hoạch - Xuất bán, loại cây:'+Convert(nvarchar(20),(Select SeedsName from PUL_Seeds where SeedsKey  = PUL_HarvestedForSale.SeedsKey)) +N', mã truy vết: '+ Code+ N' , số lượng thu hoạch: ' +Convert(nvarchar(20),QuantityHarvested) + Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey))+N', số lượng xuất bán: ' + Convert(nvarchar(20),QuantitySale) +Convert(nvarchar(20),(Select Name from PUL_Unit where ID = UnitKey)) +N', nơi mua: ' + WhereToBuy AS Description, (Select Cooperative_Key from PUL_Member where PUL_Member.[Key] = MemberKey) As Cooperative_Key, CONVERT(nvarchar(100),(Select [Key] from PUL_Member where PUL_Member.[Key] = MemberKey)) As Name,  SeedsKey  FROM dbo.PUL_HarvestedForSale
                        )
                            SELECT Count(NhatKyNgay) FROM NhatKy where Name =@MemberKey and SeedsKey IN (
						Select SeedProcessKey from PUL_SeedProcess where SeedsKey IN(Select SeedsKey from PUL_Seeds where SeedsName = @SeedsName))";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                selectCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = SeedsName;
                _Result = (int)selectCommand.ExecuteScalar();
                selectCommand.Dispose();
                connection.Close();
            }
            catch (Exception exception)
            {
                string str3 = exception.ToString();
            }
            return _Result;
        }

        public static int Count2(int key)
        {
            int _Result = 0;
            string cmdText = @"With ThongTin AS
                            ( 
                            Select Datetime_Num As NgayThu, N'Xử lý đất' As ColumName,
                            N'Hành động: '+ [Action] 
                            + N', lý do: ' + Reason
                            + N', phương pháp: ' + Solution
                            + N', ghi chú: '+ Note
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_LandUse.ProcessPlantDetailKey) AS ID  from PUL_Process_LandUse

                            Union 
                            Select DateOfManufacture_Num As NgayThu, N'Gieo giống' As ColumName,
                            N'Số lượng: '+ Convert(nvarchar(50) ,Quantity) + (Select Name from PUL_Unit where ID = PUL_Process_SeedProcess.QuantityUnit)
                            + N', diện tích: ' + Convert(nvarchar(50),Area) +(Select Name from PUL_Unit where ID = PUL_Process_SeedProcess.AreaUnit)
                            + N', sau ' + Convert(nvarchar(50),EndTime_Num) + N' ngày sẽ thu hoạch'
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_SeedProcess.ProcessPlantDetailKey) AS ID  from PUL_Process_SeedProcess

                            Union 
                            Select DateTimeUse_Num As NgayThu, N'Bón phân' As ColumName,
                            N'Loại phân: ' + (Select TradeName from PUL_Fertilizers where FertilizersKey = PUL_Process_FertilizerUse.FertilizerKey)
                            + N', số lượng: '+ Convert(nvarchar(50) ,FormulaUsed) + (Select Name from PUL_Unit where ID = PUL_Process_FertilizerUse.UnitKey)
                            + N', công việc: ' + Parcel
                            + N', lý do áp dụng: ' + Area
                            + N', phương pháp ' + Howtouse
                            + N', thiết bị: '+ (Select EquipmentName from PUL_Equipment where EquipmentKey = PUL_Process_FertilizerUse.CooperativeKey)
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_FertilizerUse.ProcessPlantDetailKey) AS ID from PUL_Process_FertilizerUse

                            Union 
                            Select DateTimeUse_Num As NgayThu, N'Phun thuốc' As ColumName,
                            N'Loại phân: ' + (Select Trade_Name from PUL_Pesticides where PesticideKey = PUL_Process_PesticideUse.PesticideUseKey)
                            + N', liều lượng: '+ Convert(nvarchar(50) ,Dose) + (Select Name from PUL_Unit where ID = PUL_Process_PesticideUse.UnitKey)
                            + N', công việc: ' + Area
                            + N', lý do áp dụng: ' + PestName
                            + N', phương pháp ' + Solution
                            + N', thiết bị: '+ (Select EquipmentName from PUL_Equipment where EquipmentKey = PUL_Process_PesticideUse.EquipmentKey)
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_PesticideUse.ProcessPlantDetailKey) AS ID  from PUL_Process_PesticideUse
                            )
                            Select Count(NgayThu) from ThongTin where  ID = @key";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@key", SqlDbType.Int).Value = key;
                _Result = (int)selectCommand.ExecuteScalar();
                selectCommand.Dispose();
                connection.Close();
            }
            catch (Exception exception)
            {
                string str3 = exception.ToString();
            }
            return _Result;
        }
        public static DataTable GetDetail(int ProcessPlantDetailKey,int PageSize, int PageNumber)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"With ThongTin AS
                            ( 
                            Select Datetime_Num As NgayThu, N'Xử lý đất' As ColumName,
                            N'Hành động: '+ [Action] 
                            + N', lý do: ' + Reason
                            + N', phương pháp: ' + Solution
                            + N', ghi chú: '+ Note
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_LandUse.ProcessPlantDetailKey) AS ID  from PUL_Process_LandUse

                            Union 
                            Select DateOfManufacture_Num As NgayThu, N'Gieo giống' As ColumName,
                            N'Số lượng: '+ Convert(nvarchar(50) ,Quantity) + (Select Name from PUL_Unit where ID = PUL_Process_SeedProcess.QuantityUnit)
                            + N', diện tích: ' + Convert(nvarchar(50),Area) +(Select Name from PUL_Unit where ID = PUL_Process_SeedProcess.AreaUnit)
                            + N', sau ' + Convert(nvarchar(50),EndTime_Num) + N' ngày sẽ thu hoạch'
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_SeedProcess.ProcessPlantDetailKey) AS ID  from PUL_Process_SeedProcess

                            Union 
                            Select DateTimeUse_Num As NgayThu, N'Bón phân' As ColumName,
                            N'Loại phân: ' + (Select TradeName from PUL_Fertilizers where FertilizersKey = PUL_Process_FertilizerUse.FertilizerKey)
                            + N', số lượng: '+ Convert(nvarchar(50) ,FormulaUsed) + (Select Name from PUL_Unit where ID = PUL_Process_FertilizerUse.UnitKey)
                            + N', công việc: ' + Parcel
                            + N', lý do áp dụng: ' + Area
                            + N', phương pháp ' + Howtouse
                            + N', thiết bị: '+ (Select EquipmentName from PUL_Equipment where EquipmentKey = PUL_Process_FertilizerUse.CooperativeKey)
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_FertilizerUse.ProcessPlantDetailKey) AS ID from PUL_Process_FertilizerUse

                            Union 
                            Select DateTimeUse_Num As NgayThu, N'Phun thuốc' As ColumName,
                            N'Loại phân: ' + (Select Trade_Name from PUL_Pesticides where PesticideKey = PUL_Process_PesticideUse.PesticideUseKey)
                            + N', liều lượng: '+ Convert(nvarchar(50) ,Dose) + (Select Name from PUL_Unit where ID = PUL_Process_PesticideUse.UnitKey)
                            + N', công việc: ' + Area
                            + N', lý do áp dụng: ' + PestName
                            + N', phương pháp ' + Solution
                            + N', thiết bị: '+ (Select EquipmentName from PUL_Equipment where EquipmentKey = PUL_Process_PesticideUse.EquipmentKey)
                            As Detail, (Select ProcessPlantKey from PUL_ProcessPlantDetail where ProcessPlantDetailKey = PUL_Process_PesticideUse.ProcessPlantDetailKey) AS ID  from PUL_Process_PesticideUse
                            )
                            Select * from ThongTin where ID = @ProcessPlantDetailKey order by NgayThu";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@ProcessPlantDetailKey", SqlDbType.Int).Value = ProcessPlantDetailKey;
                SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                zAdapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);
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
