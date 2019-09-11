using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
using System.Globalization;
namespace TNLibrary.Culture
{
    public class Assessment_Data
    {
        public static DataTable GetList(int MemberKey, DateTime fromdate = default(DateTime), DateTime todate = default(DateTime))
        {
            DataTable zTable = new DataTable();
            string zSQL = @"
            SELECT     dbo.PUL_ProcessEnvironmental.ProcessDate
            FROM         dbo.PUL_ProcessEnvironmental INNER JOIN
                      dbo.PUL_Assessment ON dbo.PUL_ProcessEnvironmental.AssessmentKey = dbo.PUL_Assessment.AssessmentKey INNER JOIN
                      dbo.PUL_ProcessEnvironmental AS PUL_ProcessEnvironmental_1 ON 
                      dbo.PUL_ProcessEnvironmental.ProcessEnvironmentalKey = PUL_ProcessEnvironmental_1.ProcessEnvironmentalKey 
                      WHERE dbo.PUL_ProcessEnvironmental.MemberKey = @MemberKey";
                      
            if (fromdate != default(DateTime))
            {
                zSQL += " and dbo.PUL_ProcessEnvironmental.ProcessDate >= @fromdate and dbo.PUL_ProcessEnvironmental.ProcessDate <= @todate";
            }
            zSQL += " group by dbo.PUL_ProcessEnvironmental.ProcessDate order by dbo.PUL_ProcessEnvironmental.ProcessDate desc";
            string zConnectionString = ConnectDataBase.ConnectionString;
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                if (fromdate != default(DateTime))
                {
                    zCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
                    zCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
                }
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
        public static DataTable GetListInfo(string ProcessDate, int MemberKey)
        {
            DateTime ProcessDate_ = DateTime.Now;
            DataTable zTable = new DataTable();
            string zConnectionString = ConnectDataBase.ConnectionString;
            string zSQL = "";
            if (ProcessDate != "" && ProcessDate != "0")
            {
                zSQL = @"SELECT     dbo.PUL_ProcessEnvironmental.ProcessEnvironmentalKey, dbo.PUL_ProcessEnvironmental.ProcessDate, dbo.PUL_ProcessEnvironmental.Solution, 
                      dbo.PUL_ProcessEnvironmental.Status, dbo.PUL_ProcessEnvironmental.CooperativeKey, dbo.PUL_ProcessEnvironmental.MemberKey, 
                      dbo.PUL_Assessment.AssessmentName, dbo.PUL_Pollution.PollutionName
                    FROM         dbo.PUL_ProcessEnvironmental INNER JOIN
                      dbo.PUL_Assessment ON dbo.PUL_ProcessEnvironmental.AssessmentKey = dbo.PUL_Assessment.AssessmentKey INNER JOIN
                      dbo.PUL_Pollution ON dbo.PUL_ProcessEnvironmental.PollutionKey = dbo.PUL_Pollution.PollutionKey
                        WHERE dbo.PUL_ProcessEnvironmental.MemberKey = @MemberKey and dbo.PUL_ProcessEnvironmental.ProcessDate = @ProcessDate";
                ProcessDate_ = DateTime.ParseExact(ProcessDate, "MM/dd/yyyy", CultureInfo.CurrentCulture);
            }

            else
            {
                zSQL = @"SELECT     dbo.PUL_ProcessEnvironmental.ProcessEnvironmentalKey, dbo.PUL_ProcessEnvironmental.ProcessDate, dbo.PUL_ProcessEnvironmental.Solution, 
                      dbo.PUL_ProcessEnvironmental.Status, dbo.PUL_ProcessEnvironmental.CooperativeKey, dbo.PUL_ProcessEnvironmental.MemberKey, 
                      dbo.PUL_Assessment.AssessmentName, dbo.PUL_Pollution.PollutionName
                        FROM         dbo.PUL_ProcessEnvironmental INNER JOIN
                      dbo.PUL_Assessment ON dbo.PUL_ProcessEnvironmental.AssessmentKey = dbo.PUL_Assessment.AssessmentKey INNER JOIN
                      dbo.PUL_Pollution ON dbo.PUL_ProcessEnvironmental.PollutionKey = dbo.PUL_Pollution.PollutionKey
                        WHERE dbo.PUL_ProcessEnvironmental.MemberKey = @MemberKey and dbo.PUL_ProcessEnvironmental.ProcessDate Is NULL";
            }
            try
            {
                SqlConnection zConnect = new SqlConnection(zConnectionString);
                zConnect.Open();
                SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                zCommand.Parameters.Add("@ProcessDate", SqlDbType.DateTime).Value = ProcessDate_;
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

        public static void CreatNewInfo(string ProcessDate, int CooperativeKey, int MemberKey)
        {
            DataTable dt = Assessment_Data.GetListInfo(ProcessDate, MemberKey);
            if (dt.Rows.Count == 0)
            {
                string zSQL = @"INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,3,'False','.',@CooperativeKey,@MemberKey,1,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,4,'False','.',@CooperativeKey,@MemberKey,1,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,5,'False','.',@CooperativeKey,@MemberKey,1,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,3,'False','.',@CooperativeKey,@MemberKey,2,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,4,'False','.',@CooperativeKey,@MemberKey,2,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,5,'False','.',@CooperativeKey,@MemberKey,2,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,6,'False','.',@CooperativeKey,@MemberKey,2,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,3,'False','.',@CooperativeKey,@MemberKey,3,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,4,'False','.',@CooperativeKey,@MemberKey,3,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,5,'False','.',@CooperativeKey,@MemberKey,3,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,6,'False','.',@CooperativeKey,@MemberKey,3,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,3,'False','.',@CooperativeKey,@MemberKey,4,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,4,'False','.',@CooperativeKey,@MemberKey,4,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,5,'False','.',@CooperativeKey,@MemberKey,4,NULL,NULL,NULL,NULL);
                        INSERT INTO PUL_ProcessEnvironmental VALUES (NULL,6,'False','.',@CooperativeKey,@MemberKey,4,NULL,NULL,NULL,NULL);";
                string zConnectionString = ConnectDataBase.ConnectionString;
                try
                {
                    SqlConnection zConnect = new SqlConnection(zConnectionString);
                    zConnect.Open();
                    SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
                    zCommand.Parameters.Add("@CooperativeKey", SqlDbType.Int).Value = CooperativeKey;
                    zCommand.Parameters.Add("@MemberKey", SqlDbType.Int).Value = MemberKey;
                    zCommand.Parameters.Add("@ProcessDate", SqlDbType.DateTime).Value = DateTime.Now;
                    zCommand.ExecuteNonQuery();
                    SqlDataAdapter zAdapter = new SqlDataAdapter(zCommand);
                    zCommand.Dispose();
                    zConnect.Close();
                }
                catch (Exception ex)
                {
                    string zstrMessage = ex.ToString();
                }
            }
        }

        public static void UpdateInfo(string p, string CurrentDatetime)
        {
            DateTime time = DateTime.ParseExact(p, "dd/MM/yyyy", CultureInfo.CurrentCulture);
            DateTime currentdate = DateTime.Now;
            string zConnectionString = ConnectDataBase.ConnectionString;
            string zSQL = "UPDATE PUL_ProcessEnvironmental SET ProcessDate = @time where ProcessDate Is NULL";
            if (CurrentDatetime != "")
            {
                currentdate = DateTime.ParseExact(CurrentDatetime, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
                zSQL += " or ProcessDate = @currentdate";
            }
            SqlConnection zConnect = new SqlConnection(zConnectionString);
            zConnect.Open();
            SqlCommand zCommand = new SqlCommand(zSQL, zConnect);
            zCommand.Parameters.Add("@time", SqlDbType.DateTime).Value = time;
            zCommand.Parameters.Add("@currentdate", SqlDbType.DateTime).Value = currentdate;
            zCommand.ExecuteNonQuery();
            zCommand.Dispose();
            zConnect.Close();
        }
    }
}
