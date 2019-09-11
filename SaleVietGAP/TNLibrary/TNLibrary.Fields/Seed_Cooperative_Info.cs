using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Fields
{
    public class Seed_Cooperative_Info
    {

        #region [ Field Name ]
        private int _ID = 0;
        private int _CooperativeKey = 0;
        private int _SeedKey = 0;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Seed_Cooperative_Info()
        {
        }
        public Seed_Cooperative_Info(int ID)
        {
            string zSQL = "SELECT * FROM PUL_Seed_Cooperative WHERE ID = @ID";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _ID = int.Parse(zReader["ID"].ToString());
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                    _SeedKey = int.Parse(zReader["SeedKey"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Seed_Cooperative ";
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
        public static DataTable GetListSeed()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seed_Cooperative.SeedKey as SeedsKey, dbo.PUL_Seed_Cooperative.CooperativeKey as Cooperative_Key, dbo.PUL_Seeds.SeedsName, dbo.PUL_Cooperative.Cooperative_Name, 
                      dbo.PUL_Seeds.Images, dbo.PUL_Cooperative.Phone, dbo.PUL_Cooperative.Address, dbo.PUL_Cooperative.VietGAPCode, dbo.PUL_Seed_Cooperative.Price
FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Seed_Cooperative.CooperativeKey = dbo.PUL_Cooperative.Cooperative_Key";
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
        public static DataTable GetListSeed(int TypeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT    dbo.PUL_Seed_Cooperative.SeedKey as SeedsKey, dbo.PUL_Seed_Cooperative.CooperativeKey as Cooperative_Key, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Cooperative.Cooperative_Name, dbo.PUL_Seeds.Images, dbo.PUL_Cooperative.Phone, dbo.PUL_Cooperative.Address, dbo.PUL_Cooperative.VietGAPCode, 
                      dbo.PUL_Seed_Cooperative.Price
FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Seed_Cooperative.CooperativeKey = dbo.PUL_Cooperative.Cooperative_Key
                            WHERE     (dbo.PUL_Seeds.TypeKey = @TypeKey)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@TypeKey", SqlDbType.Int).Value = TypeKey;
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
        public static DataTable GetListSeedNew()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seed_Cooperative.SeedKey  as SeedsKey, dbo.PUL_Seed_Cooperative.CooperativeKey as Cooperative_Key, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Cooperative.Cooperative_Name, dbo.PUL_Seeds.Images, dbo.PUL_Cooperative.Phone, dbo.PUL_Cooperative.Address, dbo.PUL_Cooperative.VietGAPCode, 
                      dbo.PUL_Seed_Cooperative.Price
FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Seed_Cooperative.CooperativeKey = dbo.PUL_Cooperative.Cooperative_Key
ORDER BY dbo.PUL_Seed_Cooperative.ModifiedDateTime DESC";
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
        public static DataTable GetListSeedNew(int TypeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT    dbo.PUL_Seed_Cooperative.SeedKey  as SeedsKey, dbo.PUL_Seed_Cooperative.CooperativeKey as Cooperative_Key, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Cooperative.Cooperative_Name, dbo.PUL_Seeds.Images, dbo.PUL_Cooperative.Phone, dbo.PUL_Cooperative.Address, dbo.PUL_Cooperative.VietGAPCode, 
                      dbo.PUL_Seed_Cooperative.Price
FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Seed_Cooperative.CooperativeKey = dbo.PUL_Cooperative.Cooperative_Key
                            WHERE     (dbo.PUL_Seeds.TypeKey = @TypeKey)
                            ORDER BY dbo.PUL_Seed_Cooperative.CreatedDateTime, dbo.PUL_Seed_Cooperative.ModifiedDateTime DESC";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@TypeKey", SqlDbType.Int).Value = TypeKey;
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
        public static DataTable GetListSeedPrice()
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seed_Cooperative.SeedKey  as SeedsKey, dbo.PUL_Seed_Cooperative.CooperativeKey as Cooperative_Key, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Cooperative.Cooperative_Name, dbo.PUL_Seeds.Images, dbo.PUL_Cooperative.Phone, dbo.PUL_Cooperative.Address, dbo.PUL_Cooperative.VietGAPCode, 
                      dbo.PUL_Seed_Cooperative.Price
FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Seed_Cooperative.CooperativeKey = dbo.PUL_Cooperative.Cooperative_Key
ORDER BY dbo.PUL_Seed_Cooperative.Price";
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
        public static DataTable GetListSeedPrice(int TypeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT    dbo.PUL_Seed_Cooperative.SeedKey as SeedsKey, dbo.PUL_Seed_Cooperative.CooperativeKey as Cooperative_Key, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Cooperative.Cooperative_Name, dbo.PUL_Seeds.Images, dbo.PUL_Cooperative.Phone, dbo.PUL_Cooperative.Address, dbo.PUL_Cooperative.VietGAPCode, 
                      dbo.PUL_Seed_Cooperative.Price
FROM         dbo.PUL_Seed_Cooperative INNER JOIN
                      dbo.PUL_Seeds ON dbo.PUL_Seed_Cooperative.SeedKey = dbo.PUL_Seeds.SeedsKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Seed_Cooperative.CooperativeKey = dbo.PUL_Cooperative.Cooperative_Key
                            WHERE     (dbo.PUL_Seeds.TypeKey = @TypeKey)
                            ORDER BY dbo.PUL_Seed_Cooperative.Price";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@TypeKey", SqlDbType.Int).Value = TypeKey;
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
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int CooperativeKey
        {
            get { return _CooperativeKey; }
            set { _CooperativeKey = value; }
        }
        public int SeedKey
        {
            get { return _SeedKey; }
            set { _SeedKey = value; }
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
            string zSQL = "INSERT INTO PUL_Seed_Cooperative ("
        + " CooperativeKey ,SeedKey ) "
         + " VALUES ( "
         + "@CooperativeKey ,@SeedKey ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
                zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
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
            string zSQL = "UPDATE PUL_Seed_Cooperative SET "
                        + " CooperativeKey = @CooperativeKey,"
                        + " SeedKey = @SeedKey"
                       + " WHERE ID = @ID";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
                zCommand.Parameters.Add("@SeedKey", SqlDbType.Int).Value = _SeedKey;
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
            if (_ID == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Seed_Cooperative WHERE ID = @ID";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@ID", SqlDbType.Int).Value = _ID;
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
