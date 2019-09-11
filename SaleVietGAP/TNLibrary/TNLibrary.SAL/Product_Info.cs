using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.SAL
{
    public class Product_Info
    {

        #region [ Field Name ]
        private int _SAL_ProductsKey = 0;
        private int _ProductKey = 0;
        private string _ProductNameEN = "";
        private string _ProductNameVN = "";
        private string _DecriptionEN = "";
        private string _DecriptionVN = "";
        private string _Image = "";
        private double _Price = 0;
        private int _Cooperative_Key = 0;
        private bool _Publish;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Product_Info()
        {
        }
        public Product_Info(int SAL_ProductsKey)
        {
            string zSQL = "SELECT * FROM SAL_Products WHERE SAL_ProductsKey = @SAL_ProductsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SAL_ProductsKey", SqlDbType.Int).Value = SAL_ProductsKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _SAL_ProductsKey = int.Parse(zReader["SAL_ProductsKey"].ToString());
                    _ProductKey = int.Parse(zReader["ProductKey"].ToString());
                    _ProductNameEN = zReader["ProductNameEN"].ToString();
                    _ProductNameVN = zReader["ProductNameVN"].ToString();
                    _DecriptionEN = zReader["DecriptionEN"].ToString();
                    _DecriptionVN = zReader["DecriptionVN"].ToString();
                    _Image = zReader["Image"].ToString();
                    _Price = double.Parse(zReader["Price"].ToString());
                    _Cooperative_Key = int.Parse(zReader["Cooperative_Key"].ToString());
                    _Publish = (bool)zReader["Publish"];
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM SAL_Products ";
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
        public static DataTable GetListbyCoop(int Cooperative_Key)
        {
            DataTable zTable = new DataTable();
            string zSQL = @"SELECT     dbo.PUL_Seed_Cooperative.SeedKey, dbo.PUL_Seed_Cooperative.CooperativeKey, dbo.PUL_Seed_Cooperative.Price, dbo.PUL_Seeds.SeedsName, 
                      dbo.PUL_Seeds.Images, dbo.PUL_Seeds.Detail, dbo.PUL_Seeds.TypeKey, dbo.PUL_SeedTypes.TypeName, dbo.PUL_Seeds_Companies.CompanyName, 
                      dbo.PUL_Seeds_Status.StatusName, dbo.PUL_Cooperative.Cooperative_Name
FROM         dbo.PUL_Seeds INNER JOIN
                      dbo.PUL_SeedTypes ON dbo.PUL_Seeds.TypeKey = dbo.PUL_SeedTypes.TypeKey INNER JOIN
                      dbo.PUL_Seeds_Companies ON dbo.PUL_Seeds.CompanyKey = dbo.PUL_Seeds_Companies.CompanyKey INNER JOIN
                      dbo.PUL_Seeds_Status ON dbo.PUL_Seeds.StatusKey = dbo.PUL_Seeds_Status.StatusKey INNER JOIN
                      dbo.PUL_Seed_Cooperative ON dbo.PUL_Seeds.SeedsKey = dbo.PUL_Seed_Cooperative.SeedKey INNER JOIN
                      dbo.PUL_Cooperative ON dbo.PUL_Seed_Cooperative.CooperativeKey = dbo.PUL_Cooperative.Cooperative_Key
WHERE     (dbo.PUL_Seed_Cooperative.CooperativeKey = @Cooperative_Key)";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = Cooperative_Key;
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
        public int SAL_ProductsKey
        {
            get { return _SAL_ProductsKey; }
            set { _SAL_ProductsKey = value; }
        }
        public int ProductKey
        {
            get { return _ProductKey; }
            set { _ProductKey = value; }
        }
        public string ProductNameEN
        {
            get { return _ProductNameEN; }
            set { _ProductNameEN = value; }
        }
        public string ProductNameVN
        {
            get { return _ProductNameVN; }
            set { _ProductNameVN = value; }
        }
        public string DecriptionEN
        {
            get { return _DecriptionEN; }
            set { _DecriptionEN = value; }
        }
        public string DecriptionVN
        {
            get { return _DecriptionVN; }
            set { _DecriptionVN = value; }
        }
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public int Cooperative_Key
        {
            get { return _Cooperative_Key; }
            set { _Cooperative_Key = value; }
        }
        public bool Publish
        {
            get { return _Publish; }
            set { _Publish = value; }
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
            string zSQL = "INSERT INTO SAL_Products ("
        + " ProductKey ,ProductNameEN ,ProductNameVN ,DecriptionEN ,DecriptionVN ,Image ,Price ,Cooperative_Key ,Publish ) "
         + " VALUES ( "
         + "@ProductKey ,@ProductNameEN ,@ProductNameVN ,@DecriptionEN ,@DecriptionVN ,@Image ,@Price ,@Cooperative_Key ,@Publish ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SAL_ProductsKey", SqlDbType.Int).Value = _SAL_ProductsKey;
                zCommand.Parameters.Add("@ProductKey", SqlDbType.Int).Value = _ProductKey;
                zCommand.Parameters.Add("@ProductNameEN", SqlDbType.NVarChar).Value = _ProductNameEN;
                zCommand.Parameters.Add("@ProductNameVN", SqlDbType.NVarChar).Value = _ProductNameVN;
                zCommand.Parameters.Add("@DecriptionEN", SqlDbType.NText).Value = _DecriptionEN;
                zCommand.Parameters.Add("@DecriptionVN", SqlDbType.NText).Value = _DecriptionVN;
                zCommand.Parameters.Add("@Image", SqlDbType.NVarChar).Value = _Image;
                zCommand.Parameters.Add("@Price", SqlDbType.Money).Value = _Price;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
                zCommand.Parameters.Add("@Publish", SqlDbType.Bit).Value = _Publish;
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
            string zSQL = "UPDATE SAL_Products SET "
                        + " ProductKey = @ProductKey,"
                        + " ProductNameEN = @ProductNameEN,"
                        + " ProductNameVN = @ProductNameVN,"
                        + " DecriptionEN = @DecriptionEN,"
                        + " DecriptionVN = @DecriptionVN,"
                        + " Image = @Image,"
                        + " Price = @Price,"
                        + " Cooperative_Key = @Cooperative_Key,"
                        + " Publish = @Publish"
                       + " WHERE SAL_ProductsKey = @SAL_ProductsKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SAL_ProductsKey", SqlDbType.Int).Value = _SAL_ProductsKey;
                zCommand.Parameters.Add("@ProductKey", SqlDbType.Int).Value = _ProductKey;
                zCommand.Parameters.Add("@ProductNameEN", SqlDbType.NVarChar).Value = _ProductNameEN;
                zCommand.Parameters.Add("@ProductNameVN", SqlDbType.NVarChar).Value = _ProductNameVN;
                zCommand.Parameters.Add("@DecriptionEN", SqlDbType.NText).Value = _DecriptionEN;
                zCommand.Parameters.Add("@DecriptionVN", SqlDbType.NText).Value = _DecriptionVN;
                zCommand.Parameters.Add("@Image", SqlDbType.NVarChar).Value = _Image;
                zCommand.Parameters.Add("@Price", SqlDbType.Money).Value = _Price;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
                zCommand.Parameters.Add("@Publish", SqlDbType.Bit).Value = _Publish;
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
            if (_SAL_ProductsKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM SAL_Products WHERE SAL_ProductsKey = @SAL_ProductsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@SAL_ProductsKey", SqlDbType.Int).Value = _SAL_ProductsKey;
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
