using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Book
{
    public class Inventory_Info
    {

        #region [ Field Name ]
        private int _InventoryKey = 0;
        private int _Type = 0;
        private int _FertilizersPesticidesKey = 0;
        private DateTime _Datetime;
        private float _Quantity;
        private int _UnitKey = 0;
        private DateTime _ExpireDate;
        private int _MemberKey = 0;
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
        private bool _IsActive;
        private bool _IsSync;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Inventory_Info()
        {
        }
        public Inventory_Info(int InventoryKey)
        {
            string zSQL = "SELECT * FROM PUL_Inventory WHERE InventoryKey = @InventoryKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@InventoryKey", SqlDbType.Int).Value = InventoryKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _InventoryKey = int.Parse(zReader["InventoryKey"].ToString());
                    _Type = int.Parse(zReader["Type"].ToString());
                    _FertilizersPesticidesKey = int.Parse(zReader["FertilizersPesticidesKey"].ToString());
                    if (zReader["Datetime"] != DBNull.Value)
                        _Datetime = (DateTime)zReader["Datetime"];
                    _Quantity = float.Parse(zReader["Quantity"].ToString());
                    _UnitKey = int.Parse(zReader["UnitKey"].ToString());
                    if (zReader["ExpireDate"] != DBNull.Value)
                        _ExpireDate = (DateTime)zReader["ExpireDate"];
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    if (zReader["CreatedBy"] != DBNull.Value)
                        _CreatedBy = Guid.Parse(zReader["CreatedBy"].ToString());
                    if (zReader["CreatedDateTime"] != DBNull.Value)
                        _CreatedDateTime = (DateTime)zReader["CreatedDateTime"];
                    if (zReader["ModifiedBy"] != DBNull.Value)
                        _ModifiedBy = Guid.Parse(zReader["ModifiedBy"].ToString());
                    if (zReader["ModifiedDateTime"] != DBNull.Value)
                        _ModifiedDateTime = (DateTime)zReader["ModifiedDateTime"];
                    _IsActive = (bool)zReader["IsActive"];
                    _IsSync = (bool)zReader["IsSync"];
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int InventoryKey
        {
            get { return _InventoryKey; }
            set { _InventoryKey = value; }
        }
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public int FertilizersPesticidesKey
        {
            get { return _FertilizersPesticidesKey; }
            set { _FertilizersPesticidesKey = value; }
        }
        public DateTime Datetime
        {
            get { return _Datetime; }
            set { _Datetime = value; }
        }
        public float Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public int UnitKey
        {
            get { return _UnitKey; }
            set { _UnitKey = value; }
        }
        public DateTime ExpireDate
        {
            get { return _ExpireDate; }
            set { _ExpireDate = value; }
        }
        public int MemberKey
        {
            get { return _MemberKey; }
            set { _MemberKey = value; }
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
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public bool IsSync
        {
            get { return _IsSync; }
            set { _IsSync = value; }
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
            string zSQL = "INSERT INTO PUL_Inventory ("
        + " Type ,FertilizersPesticidesKey ,Datetime ,Quantity ,UnitKey ,ExpireDate ,MemberKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ,IsActive ,IsSync ) "
         + " VALUES ( "
         + "@Type ,@FertilizersPesticidesKey ,@Datetime ,@Quantity ,@UnitKey ,@ExpireDate ,@MemberKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ,@IsActive ,@IsSync ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@InventoryKey", SqlDbType.Int).Value = _InventoryKey;
                zCommand.Parameters.Add("@Type", SqlDbType.Int).Value = _Type;
                zCommand.Parameters.Add("@FertilizersPesticidesKey", SqlDbType.Int).Value = _FertilizersPesticidesKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
                zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
                if (_ExpireDate.Year == 0001)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
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
                zCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = _IsActive;
                zCommand.Parameters.Add("@IsSync", SqlDbType.Bit).Value = _IsSync;
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
            string zSQL = "UPDATE PUL_Inventory SET "
                        + " Type = @Type,"
                        + " FertilizersPesticidesKey = @FertilizersPesticidesKey,"
                        + " Datetime = @Datetime,"
                        + " Quantity = @Quantity,"
                        + " UnitKey = @UnitKey,"
                        + " ExpireDate = @ExpireDate,"
                        + " MemberKey = @MemberKey,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime,"
                        + " IsActive = @IsActive,"
                        + " IsSync = @IsSync"
                       + " WHERE InventoryKey = @InventoryKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@InventoryKey", SqlDbType.Int).Value = _InventoryKey;
                zCommand.Parameters.Add("@Type", SqlDbType.Int).Value = _Type;
                zCommand.Parameters.Add("@FertilizersPesticidesKey", SqlDbType.Int).Value = _FertilizersPesticidesKey;
                if (_Datetime.Year == 0001)
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@Datetime", SqlDbType.DateTime).Value = _Datetime;
                zCommand.Parameters.Add("@Quantity", SqlDbType.Float).Value = _Quantity;
                zCommand.Parameters.Add("@UnitKey", SqlDbType.Int).Value = _UnitKey;
                if (_ExpireDate.Year == 0001)
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = _ExpireDate;
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = _MemberKey;
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
                zCommand.Parameters.Add("@IsActive", SqlDbType.Bit).Value = _IsActive;
                zCommand.Parameters.Add("@IsSync", SqlDbType.Bit).Value = _IsSync;
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
            if (_InventoryKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Inventory WHERE InventoryKey = @InventoryKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@InventoryKey", SqlDbType.Int).Value = _InventoryKey;
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
