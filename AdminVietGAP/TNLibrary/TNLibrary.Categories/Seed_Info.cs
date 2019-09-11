using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
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
        private Guid _CreatedBy;
        private DateTime _CreatedDateTime;
        private Guid _ModifiedBy;
        private DateTime _ModifiedDateTime;
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
            string zSQL = "INSERT INTO PUL_Seeds ("
        + " CategoryKey ,SeedsName ,CompanyKey ,StatusKey ,SeasonKey ,Images ,Detail ,TypeKey ,CreatedBy ,CreatedDateTime ,ModifiedBy ,ModifiedDateTime ) "
         + " VALUES ( "
         + "@CategoryKey ,@SeedsName ,@CompanyKey ,@StatusKey ,@SeasonKey ,@Images ,@Detail ,@TypeKey ,@CreatedBy ,@CreatedDateTime ,@ModifiedBy ,@ModifiedDateTime ) ";
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
            string zSQL = "UPDATE PUL_Seeds SET "
                        + " CategoryKey = @CategoryKey,"
                        + " SeedsName = @SeedsName,"
                        + " CompanyKey = @CompanyKey,"
                        + " StatusKey = @StatusKey,"
                        + " SeasonKey = @SeasonKey,"
                        + " Images = @Images,"
                        + " Detail = @Detail,"
                        + " TypeKey = @TypeKey,"
                        + " CreatedBy = @CreatedBy,"
                        + " CreatedDateTime = @CreatedDateTime,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedDateTime = @ModifiedDateTime"
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
