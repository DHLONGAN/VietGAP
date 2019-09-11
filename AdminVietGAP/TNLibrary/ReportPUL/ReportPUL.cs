using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using TNConfig;
using TNLibrary.Sys;
using iTextSharp.text;
using iTextSharp.text.pdf;

//Author: ngo.linh@daihoclongan.edu.vn  -  01696. 841. 948

namespace ReportPUL2
{
    public class ReportPUL
    {
        public static DataTable getFertilizer(string listUser, int fromYear, int toYear)
        {
            return getDataReport(@"select a2.UsedYear, a2.FertilizerKey, a2.TradeName, a1.CountMember, a2.SumFormulaUsed
                                from
                                (SELECT bang2.YearUsing, bang2.FertilizerKey, bang2.TradeName, Count(bang2.memberkey) AS CountMember
                                                            FROM (select distinct year(datetimeuse) as YearUsing, PUL_Fertilizer_Use.FertilizerKey, TradeName, memberkey from PUL_Fertilizer_Use inner join PUL_Fertilizers on PUL_Fertilizer_Use.FertilizerKey=PUL_Fertilizers.FertilizersKey where MemberKey in (" + listUser + ") and YEAR(DateTimeUse)>=" + fromYear + " and YEAR(DateTimeUse)<=" + toYear + @")  AS bang2  GROUP BY bang2.YearUsing, bang2.FertilizerKey, bang2.TradeName) as a1 inner join 
                                (select YEAR(a.DateTimeUse) as UsedYear, a.FertilizerKey, b.TradeName, SUM(
	                                case a.UnitKey
		                                when 3 then a.FormulaUsed
		                                when 4 then a.FormulaUsed/1000
	                                end
                                ) as SumFormulaUsed
                                from PUL_Fertilizer_Use a inner join PUL_Fertilizers b on a.FertilizerKey=b.FertilizersKey
                                where a.MemberKey in (" + listUser + ") and YEAR(a.DateTimeUse)>=" + fromYear + " and YEAR(a.DateTimeUse)<=" + toYear + @"
                                group by YEAR(a.DateTimeUse), a.FertilizerKey, b.TradeName) as a2 on a1.YearUsing = a2.UsedYear and a1.FertilizerKey = a2.FertilizerKey");
        }
        public static DataTable getPesticide(string listUser, int fromYear, int toYear)
        {
            return getDataReport(@"select a2.UsedYear, a2.PesticideKey, a2.Trade_Name, a1.CountMember, a2.SumDoseMg, a2.SumDoseMl
                                from
                                (SELECT bang2.YearUsing, bang2.pesticidekey, bang2.Trade_Name, Count(bang2.memberkey) AS CountMember
                                                            FROM (select distinct year(datetimeuse) as YearUsing, PUL_Pesticide_Use.pesticidekey, Trade_Name, memberkey from PUL_Pesticide_Use inner join PUL_Pesticides on PUL_Pesticide_Use.PesticideKey=PUL_Pesticides.PesticideKey where MemberKey in ("+ listUser +") and YEAR(DateTimeUse)>="+ fromYear +" and YEAR(DateTimeUse)<="+ toYear +@")  AS bang2  GROUP BY bang2.YearUsing, bang2.pesticidekey, bang2.Trade_Name) as a1 inner join 
                                (select YEAR(a.DateTimeUse) as UsedYear, a.PesticideKey, b.Trade_Name, SUM(
	                                case a.UnitKey
		                                when 3 then a.Dose*1000
		                                when 4 then a.Dose
	                                end
                                ) as SumDoseMg, SUM(
	                                case a.UnitKey
		                                when 6 then a.Dose*1000
		                                when 7 then a.Dose
	                                end
                                ) as SumDoseMl
                                from PUL_Pesticide_Use a inner join PUL_Pesticides b on a.PesticideKey=b.PesticideKey
                                where a.MemberKey in ("+ listUser +") and YEAR(a.DateTimeUse)>="+ fromYear +" and YEAR(a.DateTimeUse)<="+ toYear +@"
                                group by YEAR(a.DateTimeUse), a.PesticideKey, b.Trade_Name) as a2 on a1.YearUsing = a2.UsedYear and a1.PesticideKey = a2.PesticideKey");
        }
        public static DataTable getAreaQuantity(string listUser, int fromYear, int toYear)
        {
            return getDataReport(@"select z.UsedYear, z.SeedsKey, t.SeedsName, sum(z.AreaTotal) as AreaTotal, sum(z.QuantityTotal) as QuantityTotal
                            from
                            (select x.UsedYear, x.SeedsKey, x.MemberKey, x.AreaTotal, y.QuantityTotal
                            from
                            (select YEAR(a.DateOfManufacture) as UsedYear, a.SeedsKey, a.MemberKey, SUM(a.Area) as AreaTotal
                            from PUL_SeedProcess a
                            group by YEAR(a.DateOfManufacture), a.SeedsKey, a.MemberKey) x left join 
                            (select YEAR(a.DateOfManufacture) as UsedYear, a.SeedsKey, a.MemberKey, SUM(
	                            case
		                            when b.UnitKey=3 then b.QuantitySale
		                            when b.UnitKey=5 then b.QuantitySale*1000
	                            end
                            ) as QuantityTotal
                            from PUL_SeedProcess a inner join PUL_HarvestedForSale b on a.SeedProcessKey=b.SeedsKey
                            group by YEAR(a.DateOfManufacture), a.SeedsKey, a.MemberKey) y on x.UsedYear=y.UsedYear and x.SeedsKey=y.SeedsKey and x.MemberKey=y.MemberKey) z inner join PUL_Seeds t on z.SeedsKey=t.SeedsKey where z.MemberKey in (" + listUser + ") and z.UsedYear >= " + fromYear + " and z.UsedYear <=" + toYear + "group by z.UsedYear, z.SeedsKey, t.SeedsName");
        }
        public static DataTable getMember(int CooperativeKey)
        {
            return getDataReport("SELECT [Key], Name FROM PUL_Member WHERE Cooperative_Key=" + CooperativeKey);
        }
        public static DataTable getCooperative(int CooperativeVentureKey)
        {
            return getDataReport("SELECT b.Cooperative_Key, b.Cooperative_Name FROM PUL_ListCooperative a INNER JOIN PUL_Cooperative b on a.Cooperative_Key=b.Cooperative_Key WHERE a.CooperativeVenturesKey=" + CooperativeVentureKey);
        }
        public static DataTable getCooperativeInfo(int CooperativeKey)
        {
            return getDataReport("SELECT Cooperative_Key, Cooperative_Name FROM PUL_Cooperative WHERE Cooperative_Key=" + CooperativeKey);
        }
        public static DataTable getCooperativeVentureInfo(int CooperativeVentureKey)
        {
            return getDataReport("SELECT CooperativeVenturesKey, CooperativeVenturesName FROM SYS_CooperativeVentures WHERE CooperativeVenturesKey="+ CooperativeVentureKey);
        }
        public static DataTable getCooperativeVenture(int ProvincesCitiesKey)
        {
            return getDataReport("SELECT a.CooperativeVenturesKey, a.CooperativeVenturesName FROM SYS_CooperativeVentures a WHERE a.CooperativeVenturesKey IN (SELECT x.CooperativeVenturesKey FROM PUL_ListCooperative x INNER JOIN PUL_Cooperative y on x.Cooperative_Key=y.Cooperative_Key INNER JOIN PUL_Ward z ON y.Ward_ID=z.ID INNER JOIN PUL_District t ON z.District_ID=t.ID WHERE t.ProvincesCities_ID=" + ProvincesCitiesKey + ")");
        }
        public static DataTable getProvincesCities(){
            return getDataReport("SELECT ProvincesCities_Key, ProvincesCities_Name FROM PUL_ProvincesCities");
        }
        public static DataTable getProvincesUser(int CooperativeKey)
        {
            return getDataReport("SELECT PUL_District.ProvincesCities_ID FROM dbo.PUL_District INNER JOIN PUL_Ward ON dbo.PUL_District.ID = dbo.PUL_Ward.ID INNER JOIN PUL_Cooperative ON dbo.PUL_Ward.ID = dbo.PUL_Cooperative.Ward_ID WHERE     (dbo.PUL_Cooperative.Cooperative_Key = "+ CooperativeKey +")");
        }
        public static DataTable getDataReport(string zSQL)
        {
            DataTable zTable = new DataTable();
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
