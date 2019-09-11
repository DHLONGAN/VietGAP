using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//using TNConfig;
using TNConfig;namespace TNLibrary.Categories
{
    public class Pesticides_Data
    {
        public static DataTable GetList(int PageSize, int PageNumber, string Search)
        {
            DataTable zTable = new DataTable();
            string zSQL = " SELECT PesticideKey,Common_Name,Trade_Name,Crop_Name,CompanyName" +
              " FROM dbo.PUL_Pesticides A" +
              " INNER JOIN dbo.PUL_Pesticide_Common B ON A.Common_Key = B.Common_Key" +
              " INNER JOIN dbo.PUL_Companies C ON A.CompanyKey = C.CompanyKey";
            if (Search != "")
            {
                zSQL += " WHERE Common_Name like N'%'+ @Search +'%' or Trade_Name like N'%'+ @Search +'%' or Crop_Name like N'%'+ @Search +'%' or CompanyName like N'%'+ @Search +'%'";
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
        public static DataTable GetList(int CategoryKey, int Status)
        {
            DataTable _Table = new DataTable();
            string _SQL = " SELECT PesticideKey,Common_Name,Trade_Name,Crop_Name,CompanyName" +
                          " FROM dbo.PUL_Pesticides A" +
                          " INNER JOIN dbo.PUL_Pesticide_Common B ON A.Common_Key = B.Common_Key" +
                          " INNER JOIN dbo.PUL_Companies C ON A.CompanyKey = C.CompanyKey" +
                          " WHERE UsingStatus = @Status";
            if (CategoryKey > 0)
                _SQL += " AND A.CategoryKey = @CategoryKey";
            string _ConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection _Connect = new SqlConnection(_ConnectionString);
                _Connect.Open();
                SqlCommand _Command = new SqlCommand(_SQL, _Connect);
                _Command.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = CategoryKey;
                _Command.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
                SqlDataAdapter _Adapter = new SqlDataAdapter(_Command);
                _Adapter.Fill(_Table);
                _Command.Dispose();
                _Connect.Close();
            }
            catch (Exception ex)
            {
                string _strMessage = ex.ToString();
            }
            return _Table;
        }

        public static int Count()
        {
            int _Result = 0;
            string cmdText = @"SELECT  Count(*)  FROM  dbo.PUL_Pesticides";

            string connectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand selectCommand = new SqlCommand(cmdText, connection);

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
