using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class SeedProces_Info
    {

        #region [ Field Name ]
        private int _SeedProcessKey = 0;
        private string _SeedsKey = "";
        private DateTime _DateOfManufacture;
        private DateTime _DateBuy;
        private string _Quantity = "";
        private bool _Status;
        private int _PesticideKey = 0;
        private string _Reasons = "";
        private int _MemberKey = 0;
        private int _CooperativeKey = 0;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public SeedProces_Info()
        {
        }
        public SeedProces_Info(int SeedProcessKey)
        {
            string zSQL = "SELECT * FROM PUL_SeedProcess WHERE SeedProcessKey = @SeedProcessKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = SeedProcessKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _SeedProcessKey = int.Parse(zReader["SeedProcessKey"].ToString());
                    _SeedsKey = zReader["SeedsKey"].ToString();
                    if (zReader["DateOfManufacture"] != DBNull.Value)
                        _DateOfManufacture = (DateTime)zReader["DateOfManufacture"];
                    if (zReader["DateBuy"] != DBNull.Value)
                        _DateBuy = (DateTime)zReader["DateBuy"];
                    _Quantity = zReader["Quantity"].ToString();
                    _Status = (bool)zReader["Status"];
                    _PesticideKey = int.Parse(zReader["PesticideKey"].ToString());
                    _Reasons = zReader["Reasons"].ToString();
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int SeedProcessKey
        {
            get { return _SeedProcessKey; }
            set { _SeedProcessKey = value; }
        }
        public string SeedsKey
        {
            get { return _SeedsKey; }
            set { _SeedsKey = value; }
        }
        public DateTime DateOfManufacture
        {
            get { return _DateOfManufacture; }
            set { _DateOfManufacture = value; }
        }
        public DateTime DateBuy
        {
            get { return _DateBuy; }
            set { _DateBuy = value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int PesticideKey
        {
            get { return _PesticideKey; }
            set { _PesticideKey = value; }
        }
        public string Reasons
        {
            get { return _Reasons; }
            set { _Reasons = value; }
        }
        public int MemberKey
        {
            get { return _MemberKey; }
            set { _MemberKey = value; }
        }
        public int CooperativeKey
        {
            get { return _CooperativeKey; }
            set { _CooperativeKey = value; }
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
            string zSQL = "INSERT INTO PUL_SeedProcess ("
        + " SeedsKey ,DateOfManufacture ,DateBuy ,Quantity ,Status ,PesticideKey ,Reasons ,MemberKey ,CooperativeKey ) "
         + " VALUES ( "
         + "@SeedsKey ,@DateOfManufacture ,@DateBuy ,@Quantity ,@Status ,@PesticideKey ,@Reasons ,@MemberKey ,@CooperativeKey ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.NVarChar).Value = _SeedsKey;
                if (_DateOfManufacture.Year == 0001)
                    zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = _DateOfManufacture;
                if (_DateBuy.Year == 0001)
                    zCommand.Parameters.Add("@DateBuy", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DateBuy", SqlDbType.DateTime).Value = _DateBuy;
                zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Reasons", SqlDbType.NVarChar).Value = _Reasons;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
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
            string zSQL = "UPDATE PUL_SeedProcess SET "
                        + " SeedsKey = @SeedsKey,"
                        + " DateOfManufacture = @DateOfManufacture,"
                        + " DateBuy = @DateBuy,"
                        + " Quantity = @Quantity,"
                        + " Status = @Status,"
                        + " PesticideKey = @PesticideKey,"
                        + " Reasons = @Reasons,"
                        + " MemberKey = @MemberKey,"
                        + " CooperativeKey = @CooperativeKey"
                       + " WHERE SeedProcessKey = @SeedProcessKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.NVarChar).Value = _SeedsKey;
                if (_DateOfManufacture.Year == 0001)
                    zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DateOfManufacture", SqlDbType.DateTime).Value = _DateOfManufacture;
                if (_DateBuy.Year == 0001)
                    zCommand.Parameters.Add("@DateBuy", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DateBuy", SqlDbType.DateTime).Value = _DateBuy;
                zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Reasons", SqlDbType.NVarChar).Value = _Reasons;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
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
            if (_SeedProcessKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_SeedProcess WHERE SeedProcessKey = @SeedProcessKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@SeedProcessKey", SqlDbType.Int).Value = _SeedProcessKey;
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
