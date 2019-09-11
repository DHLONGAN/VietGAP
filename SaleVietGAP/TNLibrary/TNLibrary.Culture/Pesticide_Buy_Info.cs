using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Pesticide_Buy_Info
    {

        #region [ Field Name ]
        private int _PesticideBuyKey = 0;
        private DateTime _DatetimeBuy;
        private int _PesticideKey = 0;
        private string _Quantity = "";
        private string _Price = "";
        private int _CompanyKey = 0;
        private string _Address = "";
        private int _MemberKey = 0;
        private int _CooperativeKey = 0;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Pesticide_Buy_Info()
        {
        }
        public Pesticide_Buy_Info(int PesticideBuyKey)
        {
            string zSQL = "SELECT * FROM PUL_Pesticide_Buy WHERE PesticideBuyKey = @PesticideBuyKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PesticideBuyKey", SqlDbType.Int).Value = PesticideBuyKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _PesticideBuyKey = int.Parse(zReader["PesticideBuyKey"].ToString());
                    if (zReader["DatetimeBuy"] != DBNull.Value)
                        _DatetimeBuy = (DateTime)zReader["DatetimeBuy"];
                    _PesticideKey = int.Parse(zReader["PesticideKey"].ToString());
                    _Quantity = zReader["Quantity"].ToString();
                    _Price = zReader["Price"].ToString();
                    _CompanyKey = int.Parse(zReader["CompanyKey"].ToString());
                    _Address = zReader["Address"].ToString();
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int PesticideBuyKey
        {
            get { return _PesticideBuyKey; }
            set { _PesticideBuyKey = value; }
        }
        public DateTime DatetimeBuy
        {
            get { return _DatetimeBuy; }
            set { _DatetimeBuy = value; }
        }
        public int PesticideKey
        {
            get { return _PesticideKey; }
            set { _PesticideKey = value; }
        }
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public string Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public int CompanyKey
        {
            get { return _CompanyKey; }
            set { _CompanyKey = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
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
            string zSQL = "INSERT INTO PUL_Pesticide_Buy ("
        + " DatetimeBuy ,PesticideKey ,Quantity ,Price ,CompanyKey ,Address ,MemberKey ,CooperativeKey ) "
         + " VALUES ( "
         + "@DatetimeBuy ,@PesticideKey ,@Quantity ,@Price ,@CompanyKey ,@Address ,@MemberKey ,@CooperativeKey ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PesticideBuyKey", SqlDbType.Int).Value = _PesticideBuyKey;
                if (_DatetimeBuy.Year == 0001)
                    zCommand.Parameters.Add("@DatetimeBuy", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DatetimeBuy", SqlDbType.DateTime).Value = _DatetimeBuy;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
                zCommand.Parameters.Add("@Price", SqlDbType.NVarChar).Value = _Price;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
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
            string zSQL = "UPDATE PUL_Pesticide_Buy SET "
                        + " DatetimeBuy = @DatetimeBuy,"
                        + " PesticideKey = @PesticideKey,"
                        + " Quantity = @Quantity,"
                        + " Price = @Price,"
                        + " CompanyKey = @CompanyKey,"
                        + " Address = @Address,"
                        + " MemberKey = @MemberKey,"
                        + " CooperativeKey = @CooperativeKey"
                       + " WHERE PesticideBuyKey = @PesticideBuyKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@PesticideBuyKey", SqlDbType.Int).Value = _PesticideBuyKey;
                if (_DatetimeBuy.Year == 0001)
                    zCommand.Parameters.Add("@DatetimeBuy", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DatetimeBuy", SqlDbType.DateTime).Value = _DatetimeBuy;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Quantity", SqlDbType.NVarChar).Value = _Quantity;
                zCommand.Parameters.Add("@Price", SqlDbType.NVarChar).Value = _Price;
                zCommand.Parameters.Add("@CompanyKey", SqlDbType.Int).Value = _CompanyKey;
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
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
            if (_PesticideBuyKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Pesticide_Buy WHERE PesticideBuyKey = @PesticideBuyKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@PesticideBuyKey", SqlDbType.Int).Value = _PesticideBuyKey;
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
