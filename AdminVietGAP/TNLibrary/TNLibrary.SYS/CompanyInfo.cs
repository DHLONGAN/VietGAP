using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.SYS
{
    public class CompanyInfo
    {
        // Field
        private string m_CompanyName;
        private string m_Address;

        private string m_Tel;
        private string m_Fax;
        private string m_Email;
        private string m_Web;
        private byte[] m_Logo;
        private string m_CodeTax;

        private string m_Message = "";

        public CompanyInfo()
        {
            string nSQL = "SELECT * FROM SYS_CompanyInfo";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {

                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                SqlDataReader nReader = nCommand.ExecuteReader();
                if (nReader.HasRows)
                {
                    nReader.Read();
                    m_CompanyName = nReader["CompanyName"].ToString();
                    m_Address = nReader["Address"].ToString();


                    m_Tel = nReader["Tel"].ToString();
                    m_Fax = nReader["Fax"].ToString();
                    m_Email = nReader["Email"].ToString();
                    m_Web = nReader["Web"].ToString();
                    m_CodeTax = nReader["Tax"].ToString();
                    m_Logo = (byte[])(nReader["Logo"]);
                }
                nReader.Close();
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
        }

        #region [ Properties ]

        public string CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; }
        }

        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; }
        }

        public string Tel
        {
            get { return m_Tel; }
            set { m_Tel = value; }
        }

        public string Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; }
        }
        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; }
        }

        public string Web
        {
            get { return m_Web; }
            set { m_Web = value; }
        }

        public string CodeTax
        {
            get { return m_CodeTax; }
            set { m_CodeTax = value; }
        }

        public byte[] Logo
        {
            get { return m_Logo; }
            set { m_Logo = value; }
        }

        public string Message
        {
            set { m_Message = value; }
            get { return m_Message; }
        }
        #endregion

        #region [ Methor ]
        public string Update()
        {

            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE SYS_CompanyInfo SET "
                        + " CompanyName = @CompanyName,"
                        + " Address=@Address,"
                        + " Tel=@Tel,"
                        + " Fax=@Fax,"
                        + " Email=@Email,"
                        + " Web=@Web,"
                        + " Tax=@Tax,"
                        + " Logo=@Logo";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.CommandType = CommandType.Text;
                nCommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = m_CompanyName;
                nCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = m_Address;
                nCommand.Parameters.Add("@Tel", SqlDbType.NVarChar).Value = m_Tel;
                nCommand.Parameters.Add("@Fax", SqlDbType.NVarChar).Value = m_Fax;
                nCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = m_Email;
                nCommand.Parameters.Add("@Web", SqlDbType.NVarChar).Value = m_Web;
                nCommand.Parameters.Add("@Tax", SqlDbType.NVarChar).Value = m_CodeTax;
                nCommand.Parameters.Add("@Logo ", SqlDbType.Image).Value = m_Logo;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        #endregion
    }
}
