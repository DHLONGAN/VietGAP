using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class Seed_Info
    {

        #region [ Field Name ]
        private int _SeedsKey = 0;
        private int _CategoryKey = 0;
        private string _SeedsName = "";
        private int _CompanyKey = 0;
        private int _StatusKey = 0;
        private int _SeasonKey = 0;
        private string _Images = "";
        private string _Detail = "";
        private int _TypeKey = 0;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Seed_Info()
        {
        }
        public Seed_Info(int SeedsKey)
        {
            string zSQL = "SELECT * FROM PUL_Seeds WHERE SeedsKey = @SeedsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = SeedsKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _SeedsKey = int.Parse(zReader["SeedsKey"].ToString());
                    _CategoryKey = int.Parse(zReader["CategoryKey"].ToString());
                    _SeedsName = zReader["SeedsName"].ToString();
                    _CompanyKey = int.Parse(zReader["CompanyKey"].ToString());
                    _StatusKey = int.Parse(zReader["StatusKey"].ToString());
                    _SeasonKey = int.Parse(zReader["SeasonKey"].ToString());
                    _Images = zReader["Images"].ToString();
                    _Detail = zReader["Detail"].ToString();
                    _TypeKey = int.Parse(zReader["TypeKey"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        public static DataTable GetList(int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seed_Cooperative.SeedKey as SeedsKey, dbo.PUL_Seed_Cooperative.ID, dbo.PUL_Seeds.SeedsName
                            FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                                                  dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey
                            WHERE     (dbo.PUL_Seed_Cooperative.CooperativeKey = @CooperativeKey)
ORDER BY dbo.PUL_Seeds.SeedsName";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
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
        public static DataTable GetListSeedProcess(int MemberKey, DateTime DateOfManufacture)
        {
            
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_SeedProcess.SeedProcessKey as SeedsKey, dbo.PUL_Seeds.SeedsName, dbo.PUL_SeedProcess.DateOfManufacture as Datetime, dbo.PUL_SeedProcess.Parcel
FROM         dbo.PUL_SeedProcess INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_SeedProcess.SeedsKey = dbo.PUL_Seeds.SeedsKey
WHERE     (dbo.PUL_SeedProcess.MemberKey = @MemberKey) AND (DATEDIFF(day, @DateOfManufacture, PUL_SeedProcess.DateOfManufacture) <= 0) AND (DATEDIFF(day, @DateOfManufacture, dbo.PUL_SeedProcess.EndTime) >= 0)
ORDER BY dbo.PUL_Seeds.SeedsName, Datetime";
            
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
        public static DataTable GetListtop50()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT top(50) * FROM PUL_Seeds ";
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
        #endregion

        #region [ Properties ]
        public int SeedsKey
        {
            get { return _SeedsKey; }
            set { _SeedsKey = value; }
        }
        public int CategoryKey
        {
            get { return _CategoryKey; }
            set { _CategoryKey = value; }
        }
        public string SeedsName
        {
            get { return _SeedsName; }
            set { _SeedsName = value; }
        }
        public int CompanyKey
        {
            get { return _CompanyKey; }
            set { _CompanyKey = value; }
        }
        public int StatusKey
        {
            get { return _StatusKey; }
            set { _StatusKey = value; }
        }
        public int SeasonKey
        {
            get { return _SeasonKey; }
            set { _SeasonKey = value; }
        }
        public string Images
        {
            get { return _Images; }
            set { _Images = value; }
        }
        public string Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }
        public int TypeKey
        {
            get { return _TypeKey; }
            set { _TypeKey = value; }
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
            string zSQL = "INSERT INTO PUL_Seeds ("
        + " CategoryKey ,SeedsName ,CompanyKey ,StatusKey ,SeasonKey ,Images ,Detail ,TypeKey ) "
         + " VALUES ( "
         + "@CategoryKey ,@SeedsName ,@CompanyKey ,@StatusKey ,@SeasonKey ,@Images ,@Detail ,@TypeKey ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = _CategoryKey;
                zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = _SeedsName;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@StatusKey", SqlDbType.Int).Value = _StatusKey;
                zCommand.Parameters.Add("@SeasonKey", SqlDbType.Int).Value = _SeasonKey;
                zCommand.Parameters.Add("@Images", SqlDbType.NVarChar).Value = _Images;
                zCommand.Parameters.Add("@Detail", SqlDbType.NVarChar).Value = _Detail;
                zCommand.Parameters.Add("@TypeKey", SqlDbType.Int).Value = _TypeKey;
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
            string zSQL = "UPDATE PUL_Seeds SET "
                        + " CategoryKey = @CategoryKey,"
                        + " SeedsName = @SeedsName,"
                        + " CompanyKey = @CompanyKey,"
                        + " StatusKey = @StatusKey,"
                        + " SeasonKey = @SeasonKey,"
                        + " Images = @Images,"
                        + " Detail = @Detail,"
                        + " TypeKey = @TypeKey"
                       + " WHERE SeedsKey = @SeedsKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
                zCommand.Parameters.Add("@CategoryKey", SqlDbType.Int).Value = _CategoryKey;
                zCommand.Parameters.Add("@SeedsName", SqlDbType.NVarChar).Value = _SeedsName;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@StatusKey", SqlDbType.Int).Value = _StatusKey;
                zCommand.Parameters.Add("@SeasonKey", SqlDbType.Int).Value = _SeasonKey;
                zCommand.Parameters.Add("@Images", SqlDbType.NVarChar).Value = _Images;
                zCommand.Parameters.Add("@Detail", SqlDbType.NVarChar).Value = _Detail;
                zCommand.Parameters.Add("@TypeKey", SqlDbType.Int).Value = _TypeKey;
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
            if (_SeedsKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Seeds WHERE SeedsKey = @SeedsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
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
