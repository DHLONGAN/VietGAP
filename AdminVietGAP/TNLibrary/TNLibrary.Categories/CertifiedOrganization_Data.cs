using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using TNConfig;namespace TNLibrary.Categories
{
    public class CertifiedOrganization_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string Search)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_CertifiedOrganization.CertifiedOrganization_Key, dbo.PUL_CertifiedOrganization.CertifiedOrganization_ID, dbo.PUL_CertifiedOrganization.CertifiedOrganization_Name, 
                      dbo.PUL_CertifiedOrganization.Address, dbo.PUL_CertifiedOrganization.Phone, dbo.PUL_CertifiedOrganization.Email, dbo.PUL_CertifiedOrganization.Fax, dbo.PUL_CertifiedOrganization.Website, 
                      dbo.PUL_CertifiedOrganization.Infrastructure, dbo.PUL_CertifiedOrganization.Examination_Process, dbo.PUL_CertificationType.CertificationType_Name
                      FROM         dbo.PUL_CertificationType INNER JOIN
                      dbo.PUL_CertifiedOrganization ON dbo.PUL_CertificationType.CertificationType_Key = dbo.PUL_CertifiedOrganization.CertificationType_Key";
            if (Search != "")
            {
                zSQL += @" WHERE CertifiedOrganization_Name like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Address like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Phone like N'%'+ @Search +'%' or
                dbo.PUL_CertifiedOrganization.Email like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Fax like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Website like N'%'+ @Search +'%' 
                 or dbo.PUL_CertificationType.CertificationType_Name like N'%'+ @Search +'%'";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand selectCommand = new SqlCommand(zSQL, zConnect);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
                SqlDataAdapter _Adapter = new SqlDataAdapter(selectCommand);
                _Adapter.Fill(PageSize * PageNumber - PageSize, PageSize, zTable);

                selectCommand.Dispose();
                zConnect.Close();
            }
            catch (Exception ex)
            {
                string zstrMessage = ex.ToString();
            }
            return zTable;
        }

        public static DataTable GetList(int CategoryKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_CertifiedOrganization.CertifiedOrganization_Key, dbo.PUL_CertifiedOrganization.CertifiedOrganization_ID, dbo.PUL_CertifiedOrganization.CertifiedOrganization_Name, 
                      dbo.PUL_CertifiedOrganization.Address, dbo.PUL_CertifiedOrganization.Phone, dbo.PUL_CertifiedOrganization.Email, dbo.PUL_CertifiedOrganization.Fax, dbo.PUL_CertifiedOrganization.Website, 
                      dbo.PUL_CertifiedOrganization.Infrastructure, dbo.PUL_CertifiedOrganization.Examination_Process, dbo.PUL_CertificationType.CertificationType_Name
                      FROM         dbo.PUL_CertificationType INNER JOIN
                      dbo.PUL_CertifiedOrganization ON dbo.PUL_CertificationType.CertificationType_Key = dbo.PUL_CertifiedOrganization.CertificationType_Key";
                      
            if (CategoryKey > 0)
            {
                zSQL+=" WHERE     (dbo.PUL_CertificationType.CertificationType_Key = @CategoryKey)";
            }
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = CategoryKey;
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

        public static int Count(string Search)
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM         dbo.PUL_CertificationType INNER JOIN
                      dbo.PUL_CertifiedOrganization ON dbo.PUL_CertificationType.CertificationType_Key = dbo.PUL_CertifiedOrganization.CertificationType_Key";
            if (Search != "")
            {
                cmdText += @" WHERE CertifiedOrganization_Name like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Address like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Phone like N'%'+ @Search +'%' or
                dbo.PUL_CertifiedOrganization.Email like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Fax like N'%'+ @Search +'%' or dbo.PUL_CertifiedOrganization.Website like N'%'+ @Search +'%' 
                 or dbo.PUL_CertificationType.CertificationType_Name like N'%'+ @Search +'%'";
            }
            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                selectCommand.Parameters.Add("@Search", SqlDbType.NVarChar).Value = Search;
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
    }
}
