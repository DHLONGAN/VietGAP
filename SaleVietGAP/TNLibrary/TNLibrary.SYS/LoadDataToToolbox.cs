using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using System.Data;
using System.Data.SqlClient;

using TNConfig;
namespace TNLibrary.SYS
{
    public class LoadDataToToolbox
    {
        private static string nConnectionString = ConnectDataBase.ConnectionString;
        public static ArrayList GetDataBase(string SQL, int Parent)
        {
            string nSQL = SQL + " WHERE Parent=@Parent";

            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
            nCommand.CommandType = CommandType.Text;

            nCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = Parent;
            SqlDataReader nReader = nCommand.ExecuteReader();

            ArrayList ListItems = new ArrayList();
            while (nReader.Read())
            {
                TN_Item li = new TN_Item();
                li.Name = nReader[1].ToString().Trim();
                li.Value = nReader[0].ToString();
                ListItems.Add(li);
            }
            nReader.Close();
            nCommand.Dispose();
            nConnect.Close();
            return ListItems;
        }

        public static void ListViewData(ListView LV, string SQL)
        {

            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                ListViewItem lvi;
                ListViewItem.ListViewSubItem lvsi;
                //  MessageBox.Show(  mReader.FieldCount.ToString());


                int i = 1;
                string StyleField;
                LV.Items.Clear();
                while (nReader.Read())
                {

                    lvi = new ListViewItem();
                    lvi.Text = nReader[1].ToString();
                    lvi.Tag = nReader[0]; // Set the tag to 

                    lvi.ForeColor = Color.DarkBlue;
                    lvi.BackColor = Color.White;

                    lvi.ImageIndex = 0;

                    for (i = 2; i < nReader.FieldCount; i++)
                    {

                        lvsi = new ListViewItem.ListViewSubItem();
                        StyleField = nReader.GetDataTypeName(i).ToString();


                        switch (StyleField)
                        {
                            case "nvarchar":
                                lvsi.Text = nReader[i].ToString().Trim();
                                break;
                            case "datetime":
                                lvsi.Text = DateTime.Parse(nReader[i].ToString()).ToShortDateString();
                                break;

                            case "int":
                                lvsi.Text = string.Format("{0:###,###,###}", nReader[i]);
                                break;

                            default:
                                lvsi.Text = nReader[i].ToString().Trim();
                                break;
                        }
                        lvsi.ForeColor = Color.Silver;
                        lvi.SubItems.Add(lvsi);

                    }
                    LV.Items.Add(lvi);
                }
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }
        public static void ListViewData(ListView LV, string SQL, Color color)
        {

            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                ListViewItem lvi;
                ListViewItem.ListViewSubItem lvsi;
                //  MessageBox.Show(  mReader.FieldCount.ToString());


                int i = 1;
                string StyleField;
                LV.Items.Clear();
                while (nReader.Read())
                {

                    lvi = new ListViewItem();
                    lvi.Text = nReader[1].ToString();
                    lvi.Tag = nReader[0]; // Set the tag to 

                    lvi.ForeColor = color;
                    lvi.BackColor = Color.White;

                    lvi.ImageIndex = 0;

                    for (i = 2; i < nReader.FieldCount; i++)
                    {

                        lvsi = new ListViewItem.ListViewSubItem();
                        StyleField = nReader.GetDataTypeName(i).ToString();

                        switch (StyleField)
                        {
                            case "nvarchar":
                                lvsi.Text = nReader[i].ToString().Trim();
                                break;
                            case "datetime":
                                lvsi.Text = DateTime.Parse(nReader[i].ToString()).ToShortDateString();
                                break;

                            case "int":
                                lvsi.Text = string.Format("{0:###,###,###}", nReader[i]);
                                break;

                            default:
                                lvsi.Text = nReader[i].ToString().Trim();
                                break;
                        }
                        lvsi.ForeColor = color;
                        lvi.SubItems.Add(lvsi);

                    }
                    LV.Items.Add(lvi);
                }
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }
        public static void ListBoxData(ListBox LB, string SQL)
        {

            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                TN_Item item;

                ArrayList ListItems = new ArrayList();

                while (nReader.Read())
                {

                    item = new TN_Item();

                    item.Name = nReader[1].ToString();
                    item.Value = nReader[0];

                    ListItems.Add(item);

                }

                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();
                LB.DataSource = ListItems;
                LB.DisplayMember = "Name";
                LB.ValueMember = "Value";

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }

        public static void ComboBoxData(ComboBox CB, int Month, int Year)
        {
            ArrayList ListItems = new ArrayList();
            int n = 12;
            int nMonthFinacial = 1;
            TN_Item li;
            for (int i = Month; i <= n; i++)
            {
                li = new TN_Item();
                li.Value = nMonthFinacial;
                if (i > 9)
                    li.Name = i.ToString() + "/" + Year.ToString();
                else
                    li.Name = "0" + i.ToString() + "/" + Year.ToString();

                ListItems.Add(li);

                nMonthFinacial++;
            }
            if (Month > 1)
            {
                Year = Year + 1;
                for (int i = 1; i < Month; i++)
                {
                    li = new TN_Item();
                    li.Value = nMonthFinacial;
                    if (i > 9)
                        li.Name = i.ToString() + "/" + Year.ToString();
                    else
                        li.Name = "0" + i.ToString() + "/" + Year.ToString();

                    ListItems.Add(li);
                    nMonthFinacial++;
                }
            }

            if (ListItems.Count > 0)
            {
                CB.DataSource = ListItems;
                CB.DisplayMember = "Name";
                CB.ValueMember = "Value";
            }
        }
        public static void ComboBoxData(ComboBox CB, string SQL, bool IsHaveFirstItem)
        {
            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                ArrayList ListItems = new ArrayList();
                int n = nReader.FieldCount;
                TN_Item li;

                if (IsHaveFirstItem)
                {
                    li = new TN_Item();
                    li.Value = 0;
                    li.Name = "    ";
                    ListItems.Add(li);
                }
                while (nReader.Read())
                {

                    li = new TN_Item();
                    int nValue = 0;
                    if (int.TryParse(nReader[0].ToString(), out nValue))
                        li.Value = nReader[0];
                    else
                        li.Value = nReader[0].ToString();

                    if (n == 2)
                        li.Name = nReader[1].ToString().Trim();
                    else
                    {
                        li.Name = "";
                        for (int i = 1; i < n; i++)
                        {
                            li.Name = li.Name + nReader[i].ToString().Trim();
                            if (i < n - 1)
                                li.Name = li.Name + "  ";
                        }
                    }
                    ListItems.Add(li);
                }
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();
                CB.DataSource = ListItems;
                if (ListItems.Count > 0)
                {
                    CB.DisplayMember = "Name";
                    CB.ValueMember = "Value";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }
        public static void ComboBoxData(ComboBox CB, TN_Item ItemFirst, string SQL)
        {
            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                ArrayList ListItems = new ArrayList();
                int n = nReader.FieldCount;
                TN_Item li;
                ListItems.Add(ItemFirst);
                while (nReader.Read())
                {

                    li = new TN_Item();
                    int nValue = 0;
                    if (int.TryParse(nReader[0].ToString(), out nValue))
                        li.Value = nReader[0];
                    else
                        li.Value = nReader[0].ToString();

                    if (n == 2)
                        li.Name = nReader[1].ToString().Trim();
                    else
                    {
                        li.Name = "";
                        for (int i = 1; i < n; i++)
                        {
                            li.Name = li.Name + nReader[i].ToString().Trim();
                            if (i < n - 1)
                                li.Name = li.Name + "  ";
                        }
                    }
                    ListItems.Add(li);
                }
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();

                CB.DataSource = ListItems;
                if (ListItems.Count > 0)
                {
                    CB.DisplayMember = "Name";
                    CB.ValueMember = "Value";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }
        public static void ComboBoxData(ComboBox CB, string SQL, int MaxFirstCol, bool IsHaveFirstItem)
        {
            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                ArrayList ListItems = new ArrayList();
                int n = nReader.FieldCount;
                TN_Item li;

                if (IsHaveFirstItem)
                {
                    li = new TN_Item();
                    li.Value = 0;
                    li.Name = "    ";
                    ListItems.Add(li);
                }
                while (nReader.Read())
                {

                    li = new TN_Item();
                    int nValue = 0;
                    if (int.TryParse(nReader[0].ToString(), out nValue))
                        li.Value = nReader[0];
                    else
                        li.Value = nReader[0].ToString();
                    if (n == 2)
                        li.Name = nReader[1].ToString().Trim();
                    else
                    {
                        li.Name = "";
                        for (int i = 1; i < n; i++)
                        {
                            if (i == 1)
                                li.Name = nReader[i].ToString().Trim().PadRight(MaxFirstCol, ' ') + ":";
                            else
                                li.Name = li.Name + nReader[i].ToString().Trim();

                            if (i < n - 1)
                                li.Name = li.Name + "  ";
                        }
                    }
                    ListItems.Add(li);
                }
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();
                CB.DataSource = ListItems;
                if (ListItems.Count > 0)
                {
                    CB.DisplayMember = "Name";
                    CB.ValueMember = "Value";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }

        public static void ComboBoxData(DataGridViewComboBoxColumn CB, string SQL, bool IsHaveFirstItem)
        {
            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                ArrayList ListItems = new ArrayList();
                int n = nReader.FieldCount;
                TN_Item li;

                if (IsHaveFirstItem)
                {
                    li = new TN_Item();
                    li.Value = 0;
                    li.Name = "    ";
                    ListItems.Add(li);
                }
                while (nReader.Read())
                {

                    li = new TN_Item();
                    li.Value = nReader[0];
                    if (n == 2)
                        li.Name = nReader[1].ToString().Trim();
                    else
                    {
                        li.Name = "";
                        for (int i = 1; i < n; i++)
                        {
                            li.Name = li.Name + nReader[i].ToString().Trim();
                            if (i < n - 1)
                                li.Name = li.Name + "  ";
                        }
                    }
                    ListItems.Add(li);
                }
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();
                CB.DataSource = ListItems;
                if (ListItems.Count > 0)
                {
                    CB.DisplayMember = "Name";
                    CB.ValueMember = "Value";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }

        public static void ComboBoxColumn(DataGridViewComboBoxColumn CB, string SQL)
        {
            string nSQL;
            try
            {
                nSQL = SQL;

                SqlConnection nConnect = new SqlConnection(nConnectionString);
                nConnect.Open();
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                SqlDataReader nReader = nCommand.ExecuteReader();

                //----------------------------------------------------------

                ArrayList ListItems = new ArrayList();
                int n = nReader.FieldCount;
                TN_Item li;
                while (nReader.Read())
                {

                    li = new TN_Item();
                    li.Value = nReader[0];
                    if (n == 2)
                        li.Name = nReader[1].ToString().Trim();
                    else
                    {
                        li.Name = "";
                        for (int i = 1; i < n; i++)
                        {
                            li.Name = li.Name + nReader[i].ToString().Trim();
                            if (i < n - 1)
                                li.Name = li.Name + "  ";
                        }
                    }
                    ListItems.Add(li);
                }
                nReader.Close();
                nCommand.Dispose();
                nConnect.Close();

                CB.DataSource = ListItems;
                if (ListItems.Count > 0)
                {
                    CB.DisplayMember = "Name";
                    CB.ValueMember = "Value";
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
        }
        public static void ComboBoxDataTime(DataGridViewComboBoxColumn CB)
        {

            ArrayList ListItems = new ArrayList();

            TN_Item li;

            li = new TN_Item();
            li.Value = "HOUR";
            li.Name = "GIỜ";
            ListItems.Add(li);

            li = new TN_Item();
            li.Value = "DATE";
            li.Name = "NGÀY";
            ListItems.Add(li);


            CB.DataSource = ListItems;
            if (ListItems.Count > 0)
            {
                CB.DisplayMember = "Name";
                CB.ValueMember = "Value";
            }

        }

        public static void AutoCompleteTextBox(TextBox TB, string SQL)
        {
            string nSQL = SQL;
            AutoCompleteStringCollection Items = new AutoCompleteStringCollection();

            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
            SqlDataReader nReader = nCommand.ExecuteReader();

            //----------------------------------------------------------

            int n = nReader.FieldCount;

            while (nReader.Read())
            {
                Items.Add(nReader[0].ToString().Trim());
            }
            nReader.Close();
            nCommand.Dispose();
            nConnect.Close();

            TB.AutoCompleteCustomSource = Items;
            TB.AutoCompleteMode = AutoCompleteMode.Suggest;
            TB.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }
        public static void AutoCompleteTextBox(DataGridViewTextBoxEditingControl TB, string SQL)
        {
            string nSQL = SQL;
            AutoCompleteStringCollection Items = new AutoCompleteStringCollection();

            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
            SqlDataReader nReader = nCommand.ExecuteReader();

            //----------------------------------------------------------

            int n = nReader.FieldCount;

            while (nReader.Read())
            {
                Items.Add(nReader[0].ToString().Trim());
            }
            nReader.Close();
            nCommand.Dispose();
            nConnect.Close();

            TB.AutoCompleteCustomSource = Items;
            TB.AutoCompleteMode = AutoCompleteMode.Suggest;
            TB.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }
        public static void AutoCompleteTextBox(DataGridViewTextBoxEditingControl TB, string SQL, int NumberColumns)
        {
            string nSQL = SQL;
            AutoCompleteStringCollection Items = new AutoCompleteStringCollection();

            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
            SqlDataReader nReader = nCommand.ExecuteReader();

            while (nReader.Read())
            {
                string strItem = nReader[0].ToString().Trim().PadRight(15, ' ') + " : ";
                for (int i = 1; i < NumberColumns; i++)
                {
                    strItem += nReader[i].ToString().Trim() + " ";

                }
                Items.Add(strItem);
            }
            nReader.Close();
            nCommand.Dispose();
            nConnect.Close();

            TB.AutoCompleteCustomSource = Items;
            TB.AutoCompleteMode = AutoCompleteMode.Suggest;
            TB.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }
        public static void AutoCompleteTextBoxMutiColumn(TextBox TB, string SQL)
        {
            string nSQL = SQL;
            AutoCompleteStringCollection Items = new AutoCompleteStringCollection();

            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
            SqlDataReader nReader = nCommand.ExecuteReader();

            //----------------------------------------------------------

            int n = nReader.FieldCount;

            while (nReader.Read())
            {
                Items.Add(nReader[0].ToString().Trim() + " : " + nReader[1].ToString().Trim());
            }
            nReader.Close();
            nCommand.Dispose();
            nConnect.Close();

            TB.AutoCompleteCustomSource = Items;
            TB.AutoCompleteMode = AutoCompleteMode.Suggest;
            TB.AutoCompleteSource = AutoCompleteSource.CustomSource;


        }
        public static void AutoCompleteComboBox(ComboBox CB, string SQL)
        {
            string nSQL = SQL;
            AutoCompleteStringCollection Items = new AutoCompleteStringCollection();

            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
            SqlDataReader nReader = nCommand.ExecuteReader();

            //----------------------------------------------------------

            int n = nReader.FieldCount;

            while (nReader.Read())
            {
                Items.Add(nReader[0].ToString().Trim());
            }
            nReader.Close();
            nCommand.Dispose();
            nConnect.Close();

            CB.AutoCompleteCustomSource = Items;
            CB.AutoCompleteMode = AutoCompleteMode.Suggest;
            CB.AutoCompleteSource = AutoCompleteSource.ListItems;

        }


    }
}
