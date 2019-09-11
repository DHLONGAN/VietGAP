using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Harvest_Info
    {

        #region [ Field Name ]
        private int _HarvestsKey = 0;
        private DateTime _DateOn;
        private int _SeedsKey = 0;
        private string _Code = "";
        private float _WeightBefor;
        private string _SlotBefor = "";
        private float _WeightAfter;
        private string _SlotAfter = "";
        private string _Loss = "";
        private int _MemberKey = 0;
        private int _CooperativeKey = 0;
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Harvest_Info()
        {
        }
        public Harvest_Info(int HarvestsKey)
        {
            string zSQL = "SELECT * FROM PUL_Harvests WHERE HarvestsKey = @HarvestsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@HarvestsKey", SqlDbType.Int).Value = HarvestsKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _HarvestsKey = int.Parse(zReader["HarvestsKey"].ToString());
                    if (zReader["DateOn"] != DBNull.Value)
                        _DateOn = (DateTime)zReader["DateOn"];
                    _SeedsKey = int.Parse(zReader["SeedsKey"].ToString());
                    _Code = zReader["Code"].ToString();
                    _WeightBefor = float.Parse(zReader["WeightBefor"].ToString());
                    _SlotBefor = zReader["SlotBefor"].ToString();
                    _WeightAfter = float.Parse(zReader["WeightAfter"].ToString());
                    _SlotAfter = zReader["SlotAfter"].ToString();
                    _Loss = zReader["Loss"].ToString();
                    _MemberKey = int.Parse(zReader["MemberKey"].ToString());
                    _CooperativeKey = int.Parse(zReader["CooperativeKey"].ToString());
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int HarvestsKey
        {
            get { return _HarvestsKey; }
            set { _HarvestsKey = value; }
        }
        public DateTime DateOn
        {
            get { return _DateOn; }
            set { _DateOn = value; }
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
        public float WeightBefor
        {
            get { return _WeightBefor; }
            set { _WeightBefor = value; }
        }
        public string SlotBefor
        {
            get { return _SlotBefor; }
            set { _SlotBefor = value; }
        }
        public float WeightAfter
        {
            get { return _WeightAfter; }
            set { _WeightAfter = value; }
        }
        public string SlotAfter
        {
            get { return _SlotAfter; }
            set { _SlotAfter = value; }
        }
        public string Loss
        {
            get { return _Loss; }
            set { _Loss = value; }
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
            string zSQL = "INSERT INTO PUL_Harvests ("
        + " DateOn ,SeedsKey ,Code ,WeightBefor ,SlotBefor ,WeightAfter ,SlotAfter ,Loss ,MemberKey ,CooperativeKey ) "
         + " VALUES ( "
         + "@DateOn ,@SeedsKey ,@Code ,@WeightBefor ,@SlotBefor ,@WeightAfter ,@SlotAfter ,@Loss ,@MemberKey ,@CooperativeKey ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@HarvestsKey", SqlDbType.Int).Value = _HarvestsKey;
                if (_DateOn.Year == 0001)
                    zCommand.Parameters.Add("@DateOn", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DateOn", SqlDbType.DateTime).Value = _DateOn;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
                zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
                zCommand.Parameters.Add("@WeightBefor", SqlDbType.Float).Value = _WeightBefor;
                zCommand.Parameters.Add("@SlotBefor", SqlDbType.NVarChar).Value = _SlotBefor;
                zCommand.Parameters.Add("@WeightAfter", SqlDbType.Float).Value = _WeightAfter;
                zCommand.Parameters.Add("@SlotAfter", SqlDbType.NVarChar).Value = _SlotAfter;
                zCommand.Parameters.Add("@Loss", SqlDbType.NVarChar).Value = _Loss;
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
            string zSQL = "UPDATE PUL_Harvests SET "
                        + " DateOn = @DateOn,"
                        + " SeedsKey = @SeedsKey,"
                        + " Code = @Code,"
                        + " WeightBefor = @WeightBefor,"
                        + " SlotBefor = @SlotBefor,"
                        + " WeightAfter = @WeightAfter,"
                        + " SlotAfter = @SlotAfter,"
                        + " Loss = @Loss,"
                        + " MemberKey = @MemberKey,"
                        + " CooperativeKey = @CooperativeKey"
                       + " WHERE HarvestsKey = @HarvestsKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@HarvestsKey", SqlDbType.Int).Value = _HarvestsKey;
                if (_DateOn.Year == 0001)
                    zCommand.Parameters.Add("@DateOn", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    zCommand.Parameters.Add("@DateOn", SqlDbType.DateTime).Value = _DateOn;
                zCommand.Parameters.Add("@SeedsKey", SqlDbType.Int).Value = _SeedsKey;
                zCommand.Parameters.Add("@Code", SqlDbType.NVarChar).Value = _Code;
                zCommand.Parameters.Add("@WeightBefor", SqlDbType.Float).Value = _WeightBefor;
                zCommand.Parameters.Add("@SlotBefor", SqlDbType.NVarChar).Value = _SlotBefor;
                zCommand.Parameters.Add("@WeightAfter", SqlDbType.Float).Value = _WeightAfter;
                zCommand.Parameters.Add("@SlotAfter", SqlDbType.NVarChar).Value = _SlotAfter;
                zCommand.Parameters.Add("@Loss", SqlDbType.NVarChar).Value = _Loss;
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
            if (_HarvestsKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Harvests WHERE HarvestsKey = @HarvestsKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@HarvestsKey", SqlDbType.Int).Value = _HarvestsKey;
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
