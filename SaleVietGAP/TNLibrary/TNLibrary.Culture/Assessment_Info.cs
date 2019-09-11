using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;namespace TNLibrary.Culture
{
    public class Assessment_Info
    {

        #region [ Field Name ]
        private int _AssessmentKey = 0;
        private string _AssessmentName = "";
        private int _PollutionKey = 0;
        private bool _Status;
        private string _Solution = "";
        private string _Message = "";
        #endregion

        #region [ Constructor Get Information ]
        public Assessment_Info()
        {
        }
        public Assessment_Info(int AssessmentKey)
        {
            string zSQL = "SELECT * FROM PUL_Assessment WHERE AssessmentKey = @AssessmentKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@AssessmentKey", SqlDbType.Int).Value = AssessmentKey;
                SqlDataReader zReader = zCommand.ExecuteReader();
                if (zReader.HasRows)
                {
                    zReader.Read();
                    _AssessmentKey = int.Parse(zReader["AssessmentKey"].ToString());
                    _AssessmentName = zReader["AssessmentName"].ToString();
                    _PollutionKey = int.Parse(zReader["PollutionKey"].ToString());
                    _Status = (bool)zReader["Status"];
                    _Solution = zReader["Solution"].ToString();
                } zReader.Close(); zCommand.Dispose();
            }
            catch (Exception Err) { _Message = Err.ToString(); }
            finally { zConnect.Close(); }
        }
        #endregion

        #region [ Properties ]
        public int AssessmentKey
        {
            get { return _AssessmentKey; }
            set { _AssessmentKey = value; }
        }
        public string AssessmentName
        {
            get { return _AssessmentName; }
            set { _AssessmentName = value; }
        }
        public int PollutionKey
        {
            get { return _PollutionKey; }
            set { _PollutionKey = value; }
        }
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Solution
        {
            get { return _Solution; }
            set { _Solution = value; }
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
            string zSQL = "INSERT INTO PUL_Assessment ("
        + " AssessmentName ,PollutionKey ,Status ,Solution ) "
         + " VALUES ( "
         + "@AssessmentName ,@PollutionKey ,@Status ,@Solution ) ";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@AssessmentKey", SqlDbType.Int).Value = _AssessmentKey;
                zCommand.Parameters.Add("@AssessmentName", SqlDbType.NVarChar).Value = _AssessmentName;
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = _PollutionKey;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
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
            string zSQL = "UPDATE PUL_Assessment SET "
                        + " AssessmentName = @AssessmentName,"
                        + " PollutionKey = @PollutionKey,"
                        + " Status = @Status,"
                        + " Solution = @Solution"
                       + " WHERE AssessmentKey = @AssessmentKey";
            string zResult = "";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.CommandType = CommandType.Text;
                zCommand.Parameters.Add("@AssessmentKey", SqlDbType.Int).Value = _AssessmentKey;
                zCommand.Parameters.Add("@AssessmentName", SqlDbType.NVarChar).Value = _AssessmentName;
                zCommand.Parameters.Add("@PollutionKey", SqlDbType.Int).Value = _PollutionKey;
                zCommand.Parameters.Add("@Status", SqlDbType.Bit).Value = _Status;
                zCommand.Parameters.Add("@Solution", SqlDbType.NVarChar).Value = _Solution;
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
            if (_AssessmentKey == 0)
                zResult = Create();
            else
                zResult = Update();
            return zResult;
        }
        public string Delete()
        {
            string zResult = "";
            //---------- String SQL Access Database ---------------
            string zSQL = "DELETE FROM PUL_Assessment WHERE AssessmentKey = @AssessmentKey";
            string zConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            try
            {
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@AssessmentKey", SqlDbType.Int).Value = _AssessmentKey;
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
