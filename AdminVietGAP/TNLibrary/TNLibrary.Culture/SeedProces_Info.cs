using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Culture
{
    public class SeedProces_Info
    {

        #region [ Field Name ]
        private int _SeedProcessKey = 0;
        private string _SeedsKey = "";
        private DateTime _DateOfManufacture;
        private DateTime _DateBuy;
        private float _Quantity;
        private bool _Status;
        private int _PesticideKey = 0;
        private string _Reasons = "";
        private int _MemberKey = 0;
        private int _CooperativeKey = 0;
        private string _Parcel = "";
        private float _Area;
        private int _QuantityUnit = 0;
        private int _AreaUnit = 0;
        private DateTime _EndTime;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
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
                    _Quantity = float.Parse(zReader["Quantity"].ToString());
                    _Status = (bool)zReader["Status"];
                    _PesticideKey = int.Parse(zReader["PesticideKey"].ToString());
                    _Reasons = zReader["Reasons"].ToString();
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                    _Parcel = zReader["Parcel"].ToString();
                    _Area = float.Parse(zReader["Area"].ToString());
                    _QuantityUnit = int.Parse(zReader["QuantityUnit"].ToString());
                    _AreaUnit = int.Parse(zReader["AreaUnit"].ToString());
                    if (zReader["EndTime"] != DBNull.Value)
                        _EndTime = (DateTime)zReader["EndTime"];
                    if (zReader["CreatedBy"] != DBNull.Value)
                        _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    if (zReader["ModifiedBy"] != DBNull.Value)
                        _ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
                    if (zReader["ModifiedDateTime"] != DBNull.Value)
                        _ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
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
        public float Quantity
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
        public string Parcel
        {
            get { return _Parcel; }
            set { _Parcel = value; }
        }
        public float Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
        public int QuantityUnit
        {
            get { return _QuantityUnit; }
            set { _QuantityUnit = value; }
        }
        public int AreaUnit
        {
            get { return _AreaUnit; }
            set { _AreaUnit = value; }
        }
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        public Guid CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DateTime CreatedDateTime
        {
            get { return _CreatedDateTime; }
            set { _CreatedDateTime = value; }
        }
        public Guid ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime ModifiedDateTime
        {
            get { return _ModifiedDateTime; }
            set { _ModifiedDateTime = value; }
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
        + " SeedsKey ,DateOfManufacture ,DateBuy ,Quantity ,Status ,PesticideKey ,Reasons ,MemberKey ,CooperativeKey ,Parcel ,Area ,QuantityUnit ,AreaUnit ,EndTime ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@SeedsKey ,@DateOfManufacture ,@DateBuy ,@Quantity ,@Status ,@PesticideKey ,@Reasons ,@MemberKey ,@CooperativeKey ,@Parcel ,@Area ,@QuantityUnit ,@AreaUnit ,@EndTime ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
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
                zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Reasons", SqlDbType.NVarChar).Value = _Reasons;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
                zCommand.Parameters.Add("@Parcel", SqlDbType.NVarChar).Value = _Parcel;
                zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
                zCommand.Parameters.Add("@QuantityUnit", SqlDbType.Int).Value = _QuantityUnit;
                zCommand.Parameters.Add("@AreaUnit", SqlDbType.Int).Value = _AreaUnit;
                if (_EndTime.Year == 0001)
                    zCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = _EndTime;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
                if (_CreatedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
                if (_ModifiedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
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
                        + " CooperativeKey = @CooperativeKey,"
                        + " Parcel = @Parcel,"
                        + " Area = @Area,"
                        + " QuantityUnit = @QuantityUnit,"
                        + " AreaUnit = @AreaUnit,"
                        + " EndTime = @EndTime,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
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
                zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@PesticideKey", SqlDbType.Int).Value = _PesticideKey;
                zCommand.Parameters.Add("@Reasons", SqlDbType.NVarChar).Value = _Reasons;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
                zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = _CooperativeKey;
                zCommand.Parameters.Add("@Parcel", SqlDbType.NVarChar).Value = _Parcel;
                zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
                zCommand.Parameters.Add("@QuantityUnit", SqlDbType.Int).Value = _QuantityUnit;
                zCommand.Parameters.Add("@AreaUnit", SqlDbType.Int).Value = _AreaUnit;
                if (_EndTime.Year == 0001)
                    zCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = _EndTime;
                zCommand.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = _CreatedBy;
                if (_CreatedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreatedDateTime", SqlDbType.DateTime).Value = _CreatedDateTime;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = _ModifiedBy;
                if (_ModifiedDateTime.Year == 0001)
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ModifiedDateTime", SqlDbType.DateTime).Value = _ModifiedDateTime;
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
