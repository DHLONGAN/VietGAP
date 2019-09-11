using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class ForSale_Info
    {

        #region [ Field Name ]
        private int _ForSaleKey = 0;
        private DateTime _Datetime;
        private int _SeedsKey = 0;
        private string _Code = "";
        private float _Weight;
        private string _Slot = "";
        private string _PlaceOfBuy = "";
        private string _Bill = "";
        private int _MemberKey = 0;
        private int _CooperativeKey = 0;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public ForSale_Info()
        {
        }
        public ForSale_Info(int ForSaleKey)
        {
            string zSQL = "SELECT * FROM PUL_ForSale WHERE ForSaleKey = @ForSaleKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ForSaleKey", SqlDbType.Int).Value = ForSaleKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _ForSaleKey = int.Parse(zReader["ForSaleKey"].ToString());
                    if (zReader["Datetime"] != DBNull.Value)
                        _Datetime = (DateTime)zReader["Datetime"];
                    _SeedsKey = int.Parse(zReader["SeedsKey"].ToString());
                    _Code = zReader["Code"].ToString();
                    _Weight = float.Parse(zReader["Weight"].ToString());
                    _Slot = zReader["Slot"].ToString();
                    _PlaceOfBuy = zReader["PlaceOfBuy"].ToString();
                    _Bill = zReader["Bill"].ToString();
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int ForSaleKey
        {
            get { return _ForSaleKey; }
            set { _ForSaleKey = value; }
        }
        public DateTime Datetime
        {
            get { return _Datetime; }
            set { _Datetime = value; }
        }
        public int SeedsKey
        {
            get { return _SeedsKey; }
            set { _SeedsKey = value; }
        }
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public float Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        public string Slot
        {
            get { return _Slot; }
            set { _Slot = value; }
        }
        public string PlaceOfBuy
        {
            get { return _PlaceOfBuy; }
            set { _PlaceOfBuy = value; }
        }
        public string Bill
        {
            get { return _Bill; }
            set { _Bill = value; }
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
            string zSQL = "INSERT INTO PUL_ForSale ("
        + " Datetime ,SeedsKey ,Code ,Weight ,Slot ,PlaceOfBuy ,Bill ,MemberKey ,CooperativeKey ) "
         + " VALUES ( "
         + "@Datetime ,@SeedsKey ,@Code ,@Weight ,@Slot ,@PlaceOfBuy ,@Bill ,@MemberKey ,@CooperativeKey ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ForSaleKey", SqlDbType.Int).Value = _ForSaleKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
                zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
                zCommand.Parameters.Add("@Weight", SqlDbType.Float).Value = _Weight;
                zCommand.Parameters.Add("@Slot", SqlDbType.NVarChar).Value = _Slot;
                zCommand.Parameters.Add("@PlaceOfBuy", SqlDbType.NVarChar).Value = _PlaceOfBuy;
                zCommand.Parameters.Add("@Bill", SqlDbType.NChar).Value = _Bill;
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
            string zSQL = "UPDATE PUL_ForSale SET "
                        + " Datetime = @Datetime,"
                        + " SeedsKey = @SeedsKey,"
                        + " Code = @Code,"
                        + " Weight = @Weight,"
                        + " Slot = @Slot,"
                        + " PlaceOfBuy = @PlaceOfBuy,"
                        + " Bill = @Bill,"
                        + " MemberKey = @MemberKey,"
                        + " CooperativeKey = @CooperativeKey"
                       + " WHERE ForSaleKey = @ForSaleKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@ForSaleKey", SqlDbType.Int).Value = _ForSaleKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
                zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
                zCommand.Parameters.Add("@Weight", SqlDbType.Float).Value = _Weight;
                zCommand.Parameters.Add("@Slot", SqlDbType.NVarChar).Value = _Slot;
                zCommand.Parameters.Add("@PlaceOfBuy", SqlDbType.NVarChar).Value = _PlaceOfBuy;
                zCommand.Parameters.Add("@Bill", SqlDbType.NChar).Value = _Bill;
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
            if (_ForSaleKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_ForSale WHERE ForSaleKey = @ForSaleKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@ForSaleKey", SqlDbType.Int).Value = _ForSaleKey;
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
