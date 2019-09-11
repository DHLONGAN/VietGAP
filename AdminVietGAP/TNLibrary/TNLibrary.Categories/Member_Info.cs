using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.Categories
{
    public class Member_Info
    {

        #region [ Field Name ]
        private int _Key = 0;
        private string _MemID = "";
        private string _Name = "";
        private int _Cooperative_Key = 0;
        private string _Address = "";
        private string _Email = "";
        private string _Phone = "";
        private string _LatLng = "";
        private string _Description = "";
        private int _CreateBy = 0;
        private DateTime _CreateOn;
        private int _ModifiedBy = 0;
        private DateTime _ModifiedOn;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Member_Info()
        {
        }
        public Member_Info(int Key)
        {
            string zSQL = "SELECT * FROM PUL_Member WHERE Key = @Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Key", SqlDbType.Int).Value = Key;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _Key = int.Parse(zReader["Key"].ToString());
                    _MemID = zReader["MemID"].ToString();
                    _Name = zReader["Name"].ToString();
                    _Cooperative_Key = int.Parse(zReader["Cooperative_Key"].ToString());
                    _Address = zReader["Address"].ToString();
                    _Email = zReader["Email"].ToString();
                    _Phone = zReader["Phone"].ToString();
                    _LatLng = zReader["LatLng"].ToString();
                    _Description = zReader["Description"].ToString();
                    _CreateBy = int.Parse(zReader["CreateBy"].ToString());
                    if (zReader["CreateOn"] != DBNull.Value)
                        _CreateOn = (DateTime)zReader["CreateOn"];
                    _ModifiedBy = int.Parse(zReader["ModifiedBy"].ToString());
                    if (zReader["ModifiedOn"] != DBNull.Value)
                        _ModifiedOn = (DateTime)zReader["ModifiedOn"];
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        public Member_Info(string Name, int Cooperative_Key)
        {
            string zSQL = "SELECT * FROM PUL_Member WHERE Name = @Name and Cooperative_Key = @Cooperative_Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = Cooperative_Key;
                zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _Key = int.Parse(zReader["Key"].ToString());
                    _MemID = zReader["MemID"].ToString();
                    _Name = zReader["Name"].ToString();
                    _Cooperative_Key = int.Parse(zReader["Cooperative_Key"].ToString());
                    _Address = zReader["Address"].ToString();
                    _Email = zReader["Email"].ToString();
                    _Phone = zReader["Phone"].ToString();
                    _LatLng = zReader["LatLng"].ToString();
                    _Description = zReader["Description"].ToString();
                    _CreateBy = int.Parse(zReader["CreateBy"].ToString());
                    if (zReader["CreateOn"] != DBNull.Value)
                        _CreateOn = (DateTime)zReader["CreateOn"];
                    _ModifiedBy = int.Parse(zReader["ModifiedBy"].ToString());
                    if (zReader["ModifiedOn"] != DBNull.Value)
                        _ModifiedOn = (DateTime)zReader["ModifiedOn"];
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        public string MemID
        {
            get { return _MemID; }
            set { _MemID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Cooperative_Key
        {
            get { return _Cooperative_Key; }
            set { _Cooperative_Key = value; }
        }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string LatLng
        {
            get { return _LatLng; }
            set { _LatLng = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public int CreateBy
        {
            get { return _CreateBy; }
            set { _CreateBy = value; }
        }
        public DateTime CreateOn
        {
            get { return _CreateOn; }
            set { _CreateOn = value; }
        }
        public int ModifiedBy
        {
            get { return _ModifiedBy; }
            set { _ModifiedBy = value; }
        }
        public DateTime ModifiedOn
        {
            get { return _ModifiedOn; }
            set { _ModifiedOn = value; }
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
            string zSQL = "INSERT INTO PUL_Member ("
        + " MemID ,Name ,Cooperative_Key ,Address ,Email ,Phone ,LatLng ,Description ,CreateBy ,CreateOn ,ModifiedBy ,ModifiedOn ) "
         + " VALUES ( "
         + "@MemID ,@Name ,@Cooperative_Key ,@Address ,@Email ,@Phone ,@LatLng ,@Description ,@CreateBy ,@CreateOn ,@ModifiedBy ,@ModifiedOn ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Key", SqlDbType.Int).Value = _Key;
                zCommand.Parameters.Add("@MemID", SqlDbType.NVarChar).Value = _MemID;
                zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = _Phone;
                zCommand.Parameters.Add("@LatLng", SqlDbType.NVarChar).Value = _LatLng;
                zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
                zCommand.Parameters.Add("@CreateBy", SqlDbType.Int).Value = _CreateBy;
                if (_CreateOn.Year == 0001)
                    zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = _CreateOn;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = _ModifiedBy;
                if (_ModifiedOn.Year == 0001)
                    zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = _ModifiedOn;
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
            string zSQL = "UPDATE PUL_Member SET "
                        + " MemID = @MemID,"
                        + " Name = @Name,"
                        + " Cooperative_Key = @Cooperative_Key,"
                        + " Address = @Address,"
                        + " Email = @Email,"
                        + " Phone = @Phone,"
                        + " LatLng = @LatLng,"
                        + " Description = @Description,"
                        + " CreateBy = @CreateBy,"
                        + " CreateOn = @CreateOn,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedOn = @ModifiedOn"
                       + " WHERE Key = @Key";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Key", SqlDbType.Int).Value = _Key;
                zCommand.Parameters.Add("@MemID", SqlDbType.NVarChar).Value = _MemID;
                zCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = _Name;
                zCommand.Parameters.Add("@Cooperative_Key", SqlDbType.Int).Value = _Cooperative_Key;
                zCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = _Address;
                zCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = _Email;
                zCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = _Phone;
                zCommand.Parameters.Add("@LatLng", SqlDbType.NVarChar).Value = _LatLng;
                zCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = _Description;
                zCommand.Parameters.Add("@CreateBy", SqlDbType.Int).Value = _CreateBy;
                if (_CreateOn.Year == 0001)
                    zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@CreateOn", SqlDbType.DateTime).Value = _CreateOn;
                zCommand.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = _ModifiedBy;
                if (_ModifiedOn.Year == 0001)
                    zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = _ModifiedOn;
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
            if (_Key == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Member WHERE Key = @Key";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@Key", SqlDbType.Int).Value = _Key;
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
