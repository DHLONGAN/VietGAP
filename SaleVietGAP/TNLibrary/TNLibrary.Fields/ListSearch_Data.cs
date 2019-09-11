using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
using System.Globalization;
namespace TNLibrary.Fields
{
    public class ListSearch_Data
    {
        #region [ Constructor Get Information ]
        public static DataTable GetListByMemberDay(int MemberKey, DateTime EndTime)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_SeedProcess.SeedsKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.Images
                                FROM         dbo.PUL_SeedProcess INNER JOIN
                                                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
                                WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (dbo.PUL_SeedProcess.EndTime >= @EndTime)
                                GROUP BY dbo.PUL_SeedProcess.SeedsKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds.Images";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = EndTime;
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
        #region [ Seed]
        public static DataTable GetListBySeed(string Search)
        {
            string SearchNoUnicode = RemoveUnicode(Search);
            DataTable zTable = new DataTable();
            string zSQL = "";
            zSQL = @"SELECT  dbo.PUL_Seeds.SeedsKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_Seeds_Categories.CategoryName, dbo.PUL_Seeds_Companies.CompanyName, 
                                    dbo.PUL_Seeds_Status.StatusName, dbo.PUL_Season_Categories.SeasonName,dbo.PUL_Seeds.Images
                            FROM    dbo.PUL_Seeds_Categories INNER JOIN
                                    dbo.PUL_Seeds ON dbo.PUL_Seeds_Categories.CategoryKey = dbo.PUL_Seeds.CategoryKey INNER JOIN
                                    dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
                                    dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
                                    dbo.PUL_Season_Categories ON dbo.PUL_Seeds.SeasonKey = dbo.PUL_Season_Categories.SeasonKey
                            where	dbo.PUL_Seeds.SeedsName like N'%' + @Search + '%' or 
		                            dbo.PUL_Seeds_Categories.CategoryName like N'%' + @Search + '%' or 
		                            dbo.PUL_Seeds_Companies.CompanyName like N'%' + @Search + '%' or
		                            dbo.PUL_Seeds_Status.StatusName like N'%' + @Search + '%' or 
		                            dbo.PUL_Season_Categories.SeasonName like N'%' + @Search + '%' 
--or
  --                                  dbo.RemoveUnicode(dbo.PUL_Seeds.SeedsName) like N'%' + @SearchNoUnicode + '%' or 
	--	                            dbo.RemoveUnicode(dbo.PUL_Seeds_Categories.CategoryName) like N'%' + @SearchNoUnicode + '%' or 
		--                            dbo.RemoveUnicode(dbo.PUL_Seeds_Companies.CompanyName) like N'%' + @SearchNoUnicode + '%' or
		  --                          dbo.RemoveUnicode(dbo.PUL_Seeds_Status.StatusName) like N'%' + @SearchNoUnicode + '%' or 
		    --                        dbo.RemoveUnicode(dbo.PUL_Season_Categories.SeasonName) like N'%' + @SearchNoUnicode + '%'
";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                zCommand.Parameters.Add("@SearchNoUnicode", SqlDbType.NVarChar).Value = SearchNoUnicode;
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
        #region [ Fertilizers]
        public static DataTable GetListByFertilizers(string Search)
        {
            string SearchNoUnicode = RemoveUnicode(Search);
            DataTable zTable = new DataTable();
            string zSQL = "";
            zSQL = @"SELECT dbo.PUL_Fertilizers.FertilizersKey, dbo.PUL_Fertilizers.TradeName, dbo.PUL_Fertilizer_Unit.Fertilizer_Unit_Name, dbo.PUL_Fertilizer_Common.Common_Name, dbo.PUL_Fertilizer_Categories.CategoryName, 
                      dbo.PUL_Companies.CompanyName,dbo.PUL_Fertilizers.Images
                      FROM         dbo.PUL_Fertilizer_Unit INNER JOIN
                      dbo.PUL_Fertilizers ON dbo.PUL_Fertilizer_Unit.Fertilizer_Unit_Key = dbo.PUL_Fertilizers.UnitKey INNER JOIN
                      dbo.PUL_Fertilizer_Common ON dbo.PUL_Fertilizers.CommonKey = dbo.PUL_Fertilizer_Common.Common_Key INNER JOIN
                      dbo.PUL_Fertilizer_Categories ON dbo.PUL_Fertilizers.CategoryKey = dbo.PUL_Fertilizer_Categories.CategoryKey INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Fertilizers.CompanyKey = dbo.PUL_Companies.CompanyKey
                WHERE 
                        dbo.PUL_Fertilizers.TradeName like N'%'+ @Search +'%' or
                        dbo.PUL_Fertilizer_Unit.Fertilizer_Unit_Name like N'%'+ @Search +'%' or 
                        dbo.PUL_Fertilizer_Common.Common_Name like N'%'+ @Search +'%' or 
                        dbo.PUL_Fertilizer_Categories.CategoryName like N'%'+ @Search +'%' or 
                        dbo.PUL_Companies.CompanyName like N'%'+ @Search +'%'
";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                zCommand.Parameters.Add("@SearchNoUnicode", SqlDbType.NVarChar).Value = SearchNoUnicode;
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
        #region [ Pesticides]
        public static DataTable GetListByPesticides(string Search)
        {
            string SearchNoUnicode = RemoveUnicode(Search);
            DataTable zTable = new DataTable();
            string zSQL = "";
            zSQL = @"SELECT     dbo.PUL_Pesticides.PesticideKey, dbo.PUL_Pesticide_Common.Common_Name, dbo.PUL_Pesticides.Trade_Name as TradeName, dbo.PUL_Pesticides.Crop_Name, 
                      dbo.PUL_Companies.CompanyName, dbo.PUL_Pesticides.Images,dbo.PUL_Pesticides.UsingStatus as SeasonName
                    FROM dbo.PUL_Pesticides INNER JOIN
                      dbo.PUL_Pesticide_Common ON dbo.PUL_Pesticides.Common_Key = dbo.PUL_Pesticide_Common.Common_Key INNER JOIN
                      dbo.PUL_Companies ON dbo.PUL_Pesticides.CompanyKey = dbo.PUL_Companies.CompanyKey
                    WHERE 
                      dbo.PUL_Pesticide_Common.Common_Name LIKE N'%' + @Search + '%' OR
                      dbo.PUL_Pesticides.Trade_Name LIKE N'%' + @Search + '%' OR
                      dbo.PUL_Pesticides.Crop_Name LIKE N'%' + @Search + '%' OR
                      dbo.PUL_Companies.CompanyName LIKE N'%' + @Search + '%'
                    ";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                zCommand.Parameters.Add("@SearchNoUnicode", SqlDbType.NVarChar).Value = SearchNoUnicode;
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
        #region [ Mã truy vết]
        public static DataTable GetListByCode(string Code)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_HarvestedForSale.Code, dbo.PUL_Member.Name, dbo.PUL_Cooperative.Cooperative_Name, dbo.PUL_Cooperative.Address, 
                      dbo.PUL_Cooperative.VietGAPCode, dbo.PUL_Cooperative.Phone, dbo.PUL_Cooperative.Email, dbo.PUL_Cooperative.DateRange, 
                      dbo.PUL_Cooperative.DateExpiration, dbo.PUL_HarvestedForSale.Datetime AS DatetimeSale, dbo.PUL_SeedProcess.DateOfManufacture, 
                      dbo.PUL_Seeds.SeedsName, dbo.PUL_HarvestedForSale.WhereToBuy, dbo.PUL_Cooperative.TreeType, dbo.PUL_Cooperative.Cooperative_Key
FROM         dbo.PUL_Cooperative INNER JOIN
                      dbo.PUL_Member ON dbo.PUL_Cooperative.Cooperative_Key = dbo.PUL_Member.Cooperative_Key INNER JOIN
                      dbo.PUL_HarvestedForSale ON dbo.PUL_Member.[Key] = dbo.PUL_HarvestedForSale.MemberKey INNER JOIN
                      dbo.PUL_SeedProcess ON dbo.PUL_HarvestedForSale.SeedsKey = dbo.PUL_SeedProcess.SeedProcessKey INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_HarvestedForSale.Code = @Code)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Code;
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
        #endregion
        public static string RemoveUnicode(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(stFormD[ich]);
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
    }
}
