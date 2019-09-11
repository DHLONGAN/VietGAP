using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using TNConfig;

namespace TNLibrary.WEB
{
    public class LoadDataToToolboxWeb
    {
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
                    nRow[0] = 0;
                    nRow[1] = "";
                    nTable.Rows.InsertAt(nRow, 0);
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
            if (num > 10)
            {
                num = 10;
            }
            for (int i = 1; i <= num; i++)
            {
                DataRow row = table.NewRow();
                row["PageNumberKey"] = i;
                row["PageNumberName"] = i.ToString();
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable LoadPageSize(int PageSize, int Record, int currentclick)
        {
            DataTable table = new DataTable();
            table.Columns.Add("PageNumberKey", typeof(int));
            table.Columns.Add("PageNumberName", typeof(string));
            int num = PageNumber(PageSize, Record);
            if (currentclick - 5 > 0)
            {
                if (((currentclick - 4) + 10) > num)
                {
                    for (int i = num - 10; i <= num; i++)
                    {
                        DataRow row = table.NewRow();
                        row["PageNumberKey"] = i;
                        row["PageNumberName"] = i.ToString();
                        table.Rows.Add(row);
                    }
                }
                else
                {
                    for (int i = currentclick - 4; i <= num && i < currentclick + 6; i++)
                    {
                        DataRow row = table.NewRow();
                        row["PageNumberKey"] = i;
                        row["PageNumberName"] = i.ToString();
                        table.Rows.Add(row);
                    }
                }
            }
            else if (currentclick < 6)
            {
                for (int i = 1; i <= num && i < 11; i++)
                {
                    DataRow row = table.NewRow();
                    row["PageNumberKey"] = i;
                    row["PageNumberName"] = i.ToString();
                    table.Rows.Add(row);
                }
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

        public static string GetName(string StringSQL)
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
                nResult =  nTable.Rows[0][0].ToString();
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

        public static int GetID(string StringSQL)
        {
            int nResult = 0;
            DataTable nTable = new DataTable();
            SqlConnection nConnect = new SqlConnection(ConnectDataBase.ConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(StringSQL, nConnect);
                SqlDataAdapter nAdapter = new SqlDataAdapter(nCommand);
                nAdapter.Fill(nTable);
                if (nTable.Rows.Count > 0)
                {
                    nResult = int.Parse(nTable.Rows[0][0].ToString());
                }
            }
            catch
            {

            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
    }
}
