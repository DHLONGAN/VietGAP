using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using TNConfig;
namespace TNLibrary.SYS.Forms
{
    public class TreeViewData
    {
        private string m_Table = null;
        private string m_FieldValue = "CategoryKey";
        private string m_FieldDisplay = "CategoryName";

        private string mRootName = "Category";
        public int RootValue = 0;
        public bool ShowRoot = true;
        public bool sort;

        public TreeViewData()
        {

        }

        public string Table
        {
            set
            {
                m_Table = value;
            }
        }
        public string FieldValue
        {
            set
            {
                m_FieldValue = value;
            }
        }
        public string FieldDisplay
        {
            set
            {
                m_FieldDisplay = value;
            }
        }

        public string RootName
        {
            set
            {
                mRootName = value;
            }
        }

        public void Initialize(TreeView TV)
        {
            TreeNode nodeCurrent;
            TV.Nodes.Clear();
            if (ShowRoot)
            {
                nodeCurrent = new TreeNode(mRootName, 0, 0);
                nodeCurrent.Tag = RootValue;
                TV.Nodes.Add(nodeCurrent);
                LoadChildNodeTree(nodeCurrent, sort);
                nodeCurrent.Expand();
            }
            else
            {
                LoadRootNodeTree(TV, 0, sort);
            }
        }
        public void LoadChildNodeTree(TreeNode nodeCurrent, bool Sort)
        {
            TreeNode nodeChild;
            nodeCurrent.Nodes.Clear();
            string nSQL = "";

            if (!Sort)
                nSQL = "SELECT * FROM " + m_Table + " WHERE Parent = @Parent ORDER BY " + m_FieldValue + " ASC";
            else
                nSQL = "SELECT * FROM " + m_Table + " WHERE Parent = @Parent ORDER BY " + m_FieldDisplay + " ASC";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = (int)nodeCurrent.Tag;
                SqlDataReader nReader = nCommand.ExecuteReader();

                while (nReader.Read())
                {
                    // Create the main ListViewItem

                    nodeChild = new TreeNode(nReader[m_FieldDisplay].ToString(), 1, 2);
                    nodeChild.Tag = nReader[m_FieldValue];
                    nodeCurrent.Nodes.Add(nodeChild);
                }
                nReader.Close();
                nCommand.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
            finally
            {
                nConnect.Close();
            }
        }
        public void LoadRootNodeTree(TreeView TreeViewData, int Parent, bool Sort)
        {
            TreeNode nodeChild;
            string nSQL = "";
            if (!Sort)
                nSQL = "SELECT * FROM " + m_Table + " WHERE Parent = @Parent ORDER BY " + m_FieldValue + " ASC";
            else
                nSQL = "SELECT * FROM " + m_Table + " WHERE Parent = @Parent ORDER BY " + m_FieldDisplay + " ASC";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = Parent;
                SqlDataReader nReader = nCommand.ExecuteReader();

                while (nReader.Read())
                {
                    // Create the main ListViewItem

                    nodeChild = new TreeNode(nReader[m_FieldDisplay].ToString(), 1, 1);
                    nodeChild.Tag = nReader[m_FieldValue];
                    TreeViewData.Nodes.Add(nodeChild);
                }
                nReader.Close();
                nCommand.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e);
            }
            finally
            {
                nConnect.Close();
            }
        }

    }
}
