using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
using System.Web.UI.WebControls;
namespace TNLibrary.Fields
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
        private float _Area;
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
            string zSQL = "SELECT * FROM PUL_Member WHERE [Key] = @Key";
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
                    _Area = float.Parse(zReader["Area"].ToString());
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
        public static DataTable GetList()
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Member ";
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
        public static DataTable GetList(int CooperativeKey)
        {
            DataTable zTable = new DataTable();
            string zSQL = "SELECT  * FROM PUL_Member WHERE [Key] IN (Select EmployeeKey FROM SYS_Users) and Cooperative_Key = '" + CooperativeKey + "'";
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
        public float Area
        {
            get { return _Area; }
            set { _Area = value; }
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
+ " MemID ,Name ,Cooperative_Key ,Address ,Email ,Phone ,Area ,LatLng ,Description ,CreateBy ,CreateOn ,ModifiedBy ,ModifiedOn ) "
 + " VALUES ( "
 + "@MemID ,@Name ,@Cooperative_Key ,@Address ,@Email ,@Phone ,@Area ,@LatLng ,@Description ,@CreateBy ,@CreateOn ,@ModifiedBy ,@ModifiedOn ) ";
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
                zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
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
                        + " Area = @Area,"
                        + " LatLng = @LatLng,"
                        + " Description = @Description,"
                        + " CreateBy = @CreateBy,"
                        + " CreateOn = @CreateOn,"
                        + " ModifiedBy = @ModifiedBy,"
                        + " ModifiedOn = @ModifiedOn"
                       + " WHERE [Key] = @Key";
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
                zCommand.Parameters.Add("@Area", SqlDbType.Float).Value = _Area;
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
        public string Updatelat(int _Key, string _LatLng)
        {
            string zSQL = @"UPDATE PUL_Member SET 
                            LatLng = @LatLng
                           WHERE [Key] = @Key";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@Key", SqlDbType.Int).Value = _Key;
                zCommand.Parameters.Add("@LatLng", SqlDbType.NVarChar).Value = _LatLng;
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
            string zSQL = "DELETE FROM PUL_Member WHERE [Key] = @Key";
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

        #region [list]
        public static string DropDown_DDL(DropDownList DDL, string StringSQL, bool IsView)
        {
            string nResult = "";
            DataTable nTable = new DataTable();
            SqlConnection nConnect = new SqlConnection(ConnectDataBase.ConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(StringSQL, nConnect);
                SqlDataAdapter nAdapter = new SqlDataAdapter(nCommand);
                nAdapter.Fill(nTable);
                if (IsView)
                {
                    DataRow nRow = nTable.NewRow();
                    //nRow[0] = 0;
                    //nRow[1] = "";
                    //nTable.Rows.InsertAt(nRow, 0);
                }

                DDL.DataSource = nTable;

                DDL.DataTextField = nTable.Columns[1].ColumnName;
                DDL.DataValueField = nTable.Columns[0].ColumnName;
                DDL.DataBind();
            }
            catch (Exception ex)
            {
                nResult = ex.ToString();
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        public static DataTable LoadPageSize(int PageSize, int Record)
        {
            DataTable table = new DataTable();
            table.Columns.Add("PageNumberKey", typeof(int));
            table.Columns.Add("PageNumberName", typeof(string));
            int num = PageNumber(PageSize, Record);
            for (int i = 1; i <= num; i++)
            {
                DataRow row = table.NewRow();
                row["PageNumberKey"] = i;
                row["PageNumberName"] = i.ToString();
                table.Rows.Add(row);
            }
            return table;
        }

        public static int PageNumber(int PageSize, int Record)
        {
            int num = Record / PageSize;
            int num2 = Record % PageSize;
            if (num2 > 0)
            {
                num++;
            }
            return num;
        }
        #endregion
    }
}
