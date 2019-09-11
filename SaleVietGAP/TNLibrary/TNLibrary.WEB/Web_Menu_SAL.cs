using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TNConfig;
namespace TNLibrary.WEB
{
    public class Web_Menu_SAL
    {

        public static DataTable List(int Parent)
        {
            string _Query = "SELECT * FROM SAL_Web_Menu WHERE Parent = @Parent AND Activate=1";
            string _ConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection _Connect = new SqlConnection(_ConnectionString);
            DataTable _Data = new DataTable();
            _Connect.Open();
            try
            {
                SqlCommand _Command = new SqlCommand(_Query, _Connect);
                _Command.Parameters.Add("@Parent", SqlDbType.Int).Value = Parent;
                SqlDataAdapter adapter = new SqlDataAdapter(_Command);
                adapter.Fill(_Data);
                //---- Close Connect SQL ----
                _Command.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Connect.Close();
            }
            return _Data;
        }
    }
}
