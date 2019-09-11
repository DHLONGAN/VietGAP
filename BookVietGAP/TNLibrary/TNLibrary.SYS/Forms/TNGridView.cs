using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using TNConfig;

namespace TNLibrary.SYS.Forms
{
    public partial class TNGridView : UserControl
    {
        private string m_Table = "";
        private string m_FieldDisplay = "CategoryName";
        private string m_FieldValue = "CategoryKey";
        private int m_Parent = 0;
        private string m_Message = "";
        public TNGridView()
        {
            InitializeComponent();
        }

        private void TNGridView_Load(object sender, EventArgs e)
        {
            SetupLayoutGridView(GridViewData);

        }
        #region [ Process Gird ]
        private void SetupLayoutGridView(DataGridView GV)
        {
            // Setup Column 
            GV.Columns.Add("No", "STT");
            GV.Columns.Add("CategoryName", "Tên");

            GV.Columns[0].Width = 30;
            GV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GV.Columns[0].ReadOnly = true;

            GV.Columns[1].Width = this.Width - 35;
            GV.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            GV.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // setup style view
            GV.ScrollBars = ScrollBars.None;
            GV.BackgroundColor = Color.White;
            GV.MultiSelect = true;
            GV.AllowUserToResizeRows = false;

            GV.GridColor = Color.FromArgb(227, 239, 255);
            GV.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            GV.DefaultCellStyle.ForeColor = Color.Navy;
            GV.DefaultCellStyle.Font = new Font("Arial", 9);


            GV.RowHeadersVisible = false;
            GV.AllowUserToAddRows = true;
            GV.AllowUserToDeleteRows = true;

            // setup Height Header
            GV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            GV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            GV.Dock = DockStyle.Fill;
            GV.Rows[0].Cells[0].Value = 1;

        }
        private void GridViewData_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = e.Row.Index + 1;
            e.Row.Cells[1].Tag = "";
        }
        private void GridViewData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteRow();
            }
        }
        private void GridViewData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!cmdEdit.Enabled)
                e.Cancel = true; ;
        }
        private void GridViewData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow nRow = GridViewData.Rows[e.RowIndex];
            if (nRow.Cells[1].Value == null)
            {
                nRow.Cells[1].ErrorText = "Không được nhập rỗng";
            }
            else
            {
                nRow.Cells[1].ErrorText = null;
                string nCategoryName = nRow.Cells[1].Value.ToString();
                if (!nRow.IsNewRow && (nRow.Cells[1].Tag != null && nRow.Cells[1].Tag.ToString().Length != 0))
                {
                    string nCategoryKey = nRow.Cells[1].Tag.ToString();
                    UpdateCategory(nCategoryKey, nCategoryName);
                }
                if (nRow.Index == GridViewData.Rows.Count - 2 && (nRow.Cells[1].Tag == null || (int)nRow.Cells[1].Tag.ToString().Length == 0))
                {
                    nRow.Cells[1].Tag = CreateCategory(nCategoryName);
                }
            }
            GridViewData.AutoResizeRow(e.RowIndex);

        }
        #endregion

        #region [Action Button]
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            GridViewData.BeginEdit(true);
        }
        private void cmdDel_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }
        private void DeleteRow()
        {
            if (!cmdDel.Enabled)
                return;

            if (!GridViewData.CurrentRow.IsNewRow)
            {

                DeleteCategory(GridViewData.CurrentRow.Cells[1].Tag.ToString());
                GridViewData.Rows.Remove(GridViewData.CurrentRow);
            }
            int n = GridViewData.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                GridViewData.Rows[i].Cells[0].Value = i + 1;
            }
        }
        #endregion

        #region [Process Datebase]
        public void LoadDataBase()
        {
            string nSQL = "SELECT " + m_FieldValue + "," + m_FieldDisplay + " FROM " + m_Table;
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {

                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                SqlDataReader nReader = nCommand.ExecuteReader();
                int i = 0;
                if (nReader.HasRows)
                {

                    while (nReader.Read())
                    {
                        GridViewData.Rows.Add();
                        GridViewData.Rows[i].Cells[0].Value = i + 1;
                        GridViewData.Rows[i].Cells[1].Value = nReader[m_FieldDisplay].ToString();
                        GridViewData.Rows[i].Cells[1].Tag = nReader[m_FieldValue];
                        GridViewData.AutoResizeRow(i);

                        i++;

                    }
                }
                nReader.Close();
                nCommand.Dispose();
                GridViewData.Rows[i].Cells[0].Value = i + 1;
                GridViewData.Rows[i].Cells[1].Tag = "";
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
        public void LoadDataBase(string SQL)
        {
            string nSQL = SQL;
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {

                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                SqlDataReader nReader = nCommand.ExecuteReader();
                int i = 0;
                if (nReader.HasRows)
                {

                    while (nReader.Read())
                    {
                        GridViewData.Rows.Add();
                        GridViewData.Rows[i].Cells[0].Value = i + 1;
                        GridViewData.Rows[i].Cells[1].Value = nReader[m_FieldDisplay].ToString();
                        GridViewData.Rows[i].Cells[1].Tag = nReader[m_FieldValue];
                        GridViewData.AutoResizeRow(i);

                        i++;

                    }
                }
                nReader.Close();
                nCommand.Dispose();
                GridViewData.Rows[i].Cells[0].Value = i + 1;
                GridViewData.Rows[i].Cells[1].Tag = "";
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
        private void UpdateCategory(string CategoryKey, string CategoryName)
        {
            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE " + m_Table + " SET " + m_FieldDisplay + " = @FieldDisplay "
                        + " WHERE " + m_FieldValue + " = @FieldValue";

            string nResult = "";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@FieldValue", SqlDbType.NVarChar).Value = CategoryKey;
                nCommand.Parameters.Add("@FieldDisplay", SqlDbType.NText).Value = CategoryName;

                nResult = nCommand.ExecuteNonQuery().ToString();
                nCommand.Dispose();
            }
            catch (Exception Err)
            {
                string m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
        }
        private int CreateCategory(string CategoryName)
        {
            //---------- String SQL Access Database ---------------
            string nSQL = "";
            if (m_Parent == 0)
                nSQL = "INSERT INTO " + m_Table + "( " + m_FieldDisplay + ") VALUES(@FieldDisplay) "
                           + " SELECT " + m_FieldValue + " FROM " + m_Table + " WHERE " + m_FieldValue + " = SCOPE_IDENTITY()";
            else
                nSQL = "INSERT INTO " + m_Table + "( " + m_FieldDisplay + ",Parent) VALUES(@FieldDisplay,@Parent) "
                        + " SELECT " + m_FieldValue + " FROM " + m_Table + " WHERE " + m_FieldValue + " = SCOPE_IDENTITY()";

            string nResult = "";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {

                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@FieldDisplay", SqlDbType.NText).Value = CategoryName;
                nCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = m_Parent;

                nResult = nCommand.ExecuteScalar().ToString();
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
            int nCategoryKey = 0;
            int.TryParse(nResult, out nCategoryKey);
            return nCategoryKey;
        }
        private void DeleteCategory(string CategoryKey)
        {
            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "DELETE FROM " + m_Table + " WHERE " + m_FieldValue + " = @FieldValue";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);

                nCommand.Parameters.Add("@FieldValue", SqlDbType.NVarChar).Value = CategoryKey;
                nResult = nCommand.ExecuteNonQuery().ToString();
                nCommand.Dispose();
            }
            catch (Exception Err)
            {
                string m_Message = Err.ToString();
            }
            finally
            {
                nConnect.Close();
            }
        }
        #endregion

        #region [Properties]
        public string Table
        {
            set
            {
                m_Table = value;
            }
        }
        public string FieldDisplay
        {
            set
            {
                m_FieldDisplay = value;
            }
        }
        public string FieldValue
        {
            set
            {
                m_FieldValue = value;
            }
        }
        public int Parent
        {
            set
            {
                m_Parent = value;
            }
        }
        public string Message
        {
            get
            {
                return m_Message;
            }
        }
        public bool SetCmdEdit
        {
            set { cmdEdit.Enabled = value; }
        }
        public bool SetCmdDel
        {
            set { cmdDel.Enabled = value; }
        }
        #endregion

      


    }
}
