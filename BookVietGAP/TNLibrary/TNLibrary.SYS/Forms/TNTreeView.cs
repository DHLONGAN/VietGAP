using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using TNConfig;
namespace TNLibrary.SYS.Forms
{
    public partial class TNTreeView : UserControl
    {
        public string RootName = "Category";
        public int RootValue = 0;
        public string Table = null;
        public string FieldDisplay = "CategoryName";
        public string FieldValue = "CategoryKey";
        public bool sort;
        public ImageList ImageTV = new ImageList();

        public TNTreeView()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            this.Cursor = Cursors.WaitCursor;

            if (ImageTV.Images.Count == 0)
                TreeViewData.ImageList = ImgTreeView;
            else
                TreeViewData.ImageList = ImageTV;

            TreeNode nodeCurrent;
            TreeViewData.Nodes.Clear();

            nodeCurrent = new TreeNode(RootName, 0, 0);
            nodeCurrent.Tag = RootValue;
            TreeViewData.Nodes.Add(nodeCurrent);
            LoadChildNodeTree(nodeCurrent, sort);

            nodeCurrent.ForeColor = Color.Navy;
            nodeCurrent.Expand();

            this.Cursor = Cursors.Default;

        }

        #region [ Action on Tree View ]

        private void TreeViewData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TreeNode nodeCurrent = e.Node;
            //clear all sub-folders
            nodeCurrent.Nodes.Clear();

            LoadChildNodeTree(nodeCurrent, sort);

            nodeCurrent.ForeColor = Color.Navy;

            this.Cursor = Cursors.Default;
        }
        private void TreeViewData_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (cmdEdit.Enabled == false)
            {
                e.CancelEdit = true;
            }
        }
        private void TreeViewData_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if ((int)TreeViewData.SelectedNode.Tag != 0)
                {
                    TreeNode nFirstNode = TreeViewData.SelectedNode.Parent.FirstNode;
                    int n = TreeViewData.SelectedNode.Parent.Nodes.Count;
                    if (HaveNodeName(nFirstNode, n, e.Label.ToString(), (int)e.Node.Tag))
                    {
                        MessageBox.Show("Đã có danh mục này rồi");
                        e.CancelEdit = true;
                        TreeViewData.SelectedNode.BeginEdit();
                    }
                    else
                    {
                        UpdateNode(e.Label.ToString(), (int)e.Node.Tag);
                    }
                }
                else
                    e.CancelEdit = true;
            }
        }

        #endregion

        #region [ Function on tree view ]

        public void LoadChildNodeTree(TreeNode nodeCurrent, bool Sort)
        {
            TreeNode nodeChild;
            nodeCurrent.Nodes.Clear();
            string nSQL = "";

            if (!Sort)
                nSQL = "SELECT * FROM " + Table + " WHERE Parent = @Parent ORDER BY " + FieldValue + " ASC";
            else
                nSQL = "SELECT * FROM " + Table + " WHERE Parent = @Parent ORDER BY " + FieldDisplay + " ASC";

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
                    nodeChild = new TreeNode(nReader[FieldDisplay].ToString(), 1, 2);
                    nodeChild.Tag = nReader[FieldValue];
                    nodeCurrent.Nodes.Add(nodeChild);
                }

                nReader.Close();
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                MessageBox.Show("Error: " + Err.ToString());
            }
            finally
            {
                nConnect.Close();
            }


        }
        public void LoadRootNodeTree(int Parent, bool Sort)
        {
            TreeNode nodeChild;
            string nSQL = "";

            if (!Sort)
                nSQL = "SELECT * FROM " + Table + " WHERE Parent = @Parent ORDER BY " + FieldValue + " ASC";
            else
                nSQL = "SELECT * FROM " + Table + " WHERE Parent = @Parent ORDER BY " + FieldDisplay + " ASC";

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

                    nodeChild = new TreeNode(nReader[FieldDisplay].ToString(), 1, 1);
                    nodeChild.Tag = nReader[FieldValue];
                    TreeViewData.Nodes.Add(nodeChild);
                }

                nReader.Close();
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                MessageBox.Show("Error: " + Err.ToString());
            }
            finally
            {
                nConnect.Close();
            }
        }

        private string NewNodeName(TreeNode NodeSelect, int n)
        {
            int[] a = new int[100];
            string NodeName;

            for (int i = 0; i < 100; i++)
                a[i] = 0;
            for (int i = 1; i <= n; i++)
            {
                NodeName = NodeSelect.Text;
                if (String.Compare(NodeName, "New Forder", true) == 0)
                {
                    a[0] = 1;
                }
                else
                {
                    if (NodeName.Length >= 12)
                    {

                        int nIndexArray = 0;
                        string aa = NodeName.Substring(0, 12);
                        if (String.Compare(NodeName.Substring(0, 12), "New Forder (", true) == 0)
                        {
                            int nIndexStr = NodeName.IndexOf(')', 12);
                            if (int.TryParse(NodeName.Substring(12, nIndexStr - 12), out nIndexArray))
                            {
                                a[nIndexArray] = 1;
                            }
                        }

                    }
                }
                NodeSelect = NodeSelect.NextNode;
            }
            int k = 0;
            for (int i = 0; i < 100; i++)
            {
                if (a[i] == 0)
                {
                    k = i;
                    break;
                }
            }

            if (k == 0)
                return "New Forder";
            else
                return "New Forder (" + k.ToString() + ")";
        }
        private bool HaveNodeName(TreeNode NodeSelect, int n, string NodeNameNew, int NodeKey)
        {
            for (int i = 1; i <= n; i++)
            {
                if ((String.Compare(NodeNameNew, NodeSelect.Text, true) == 0) && (int)NodeSelect.Tag != NodeKey)
                    return true;
                NodeSelect = NodeSelect.NextNode;
            }
            return false;
        }

        #endregion

        #region [ DrapDrop On TreeView ]
        private TreeNode hoverNode;
        private TreeNode NodeMove;
        private void TreeViewData_ItemDrag(object sender, ItemDragEventArgs e)
        {
            NodeMove = (TreeNode)e.Item;
            (sender as TreeView).DoDragDrop(NodeMove.Tag.ToString(), DragDropEffects.Move);
        }
        private void TreeViewData_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void TreeViewData_DragOver(object sender, DragEventArgs e)
        {
            Point mouseLocation = TreeViewData.PointToClient(new Point(e.X, e.Y));
            TreeNode node = TreeViewData.GetNodeAt(mouseLocation);

            if (node != null)
            {
                if (hoverNode == null)
                {
                    node.BackColor = Color.LightBlue;
                    node.ForeColor = Color.White;
                    hoverNode = node;
                }
                else if (hoverNode != node)
                {
                    hoverNode.BackColor = Color.White;
                    hoverNode.ForeColor = Color.Navy;
                    node.BackColor = Color.LightBlue;
                    node.ForeColor = Color.White;
                    hoverNode = node;
                }
            }
        }
        private void TreeViewData_DragDrop(object sender, DragEventArgs e)
        {
            Point dropLocation = (sender as TreeView).PointToClient(new Point(e.X, e.Y));
            TreeNode dropNode = (sender as TreeView).GetNodeAt(dropLocation);

            int KeyTo = (int)dropNode.Tag;

            string KeyMove = (string)e.Data.GetData(typeof(string));
            if (int.Parse(KeyMove) != KeyTo)
            {
                UpdateParent(int.Parse(KeyMove), KeyTo);
                NodeMove.Remove();
                LoadChildNodeTree(dropNode, sort);
            }
            ResetNodeColor(hoverNode);


        }
        private void TreeViewData_DragLeave(object sender, EventArgs e)
        {
            ResetNodeColor(hoverNode);
        }
        private void ResetNodeColor(TreeNode Node)
        {
            if (Node != null)
            {
                Node.BackColor = Color.White;
                Node.ForeColor = Color.Navy;
            }
        }
        #endregion

        #region [ Access Data ]

        public int AddNewNode(string NodeName, int ParentKey)
        {

            int nResult = 0;
            string nSQL = "INSERT INTO " + Table + " (" + FieldDisplay + ",Parent )Values(@NodeName,@Parent)"
                        + " SELECT " + FieldValue + " FROM " + Table + " WHERE " + FieldValue + " = SCOPE_IDENTITY()";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@NodeName", SqlDbType.NVarChar).Value = NodeName;
                nCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = ParentKey;
                nResult = int.Parse(nCommand.ExecuteScalar().ToString());
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                MessageBox.Show("Error: " + Err.ToString());
            }
            finally
            {
                nConnect.Close();
            }

            return nResult;
        }
        public string UpdateNode(string NodeName, int NodeKey)
        {
            string nResult = "";
            string nSQL = "UPDATE " + Table + " SET " + FieldDisplay + " = @NodeName WHERE " + FieldValue + " = @NodeKey";
            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@NodeKey", SqlDbType.Int).Value = NodeKey;
                nCommand.Parameters.Add("@NodeName", SqlDbType.NVarChar).Value = NodeName;
                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                MessageBox.Show("Error: " + Err.ToString());
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        public string DeleteNode(int NodeKey)
        {

            string nResult = "";
            string nSQL = "DELETE FROM " + Table + " WHERE " + FieldValue + "= @NodeKey";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@NodeKey", SqlDbType.Int).Value = NodeKey;
                nResult = nCommand.ExecuteNonQuery().ToString();
                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                MessageBox.Show("Error: " + Err.ToString());
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }
        public string UpdateParent(int KeyMove, int KeyTo)
        {
            string nResult = "";

            //---------- String SQL Access Database ---------------
            string nSQL = "UPDATE " + Table + " SET Parent = @Parent WHERE " + FieldValue + "= @NodeKey";

            string nConnectionString = ConnectDataBase.ConnectionString;
            SqlConnection nConnect = new SqlConnection(nConnectionString);
            nConnect.Open();
            try
            {
                SqlCommand nCommand = new SqlCommand(nSQL, nConnect);
                nCommand.CommandType = CommandType.Text;

                nCommand.Parameters.Add("@NodeKey", SqlDbType.Int).Value = KeyMove;
                nCommand.Parameters.Add("@Parent", SqlDbType.Int).Value = KeyTo;

                nResult = nCommand.ExecuteNonQuery().ToString();

                nCommand.Dispose();

            }
            catch (Exception Err)
            {
                MessageBox.Show("Error: " + Err.ToString());
            }
            finally
            {
                nConnect.Close();
            }
            return nResult;
        }

        #endregion

        #region [ Action On ToolTrip ]
        private void cmdDel_Click(object sender, EventArgs e)
        {
            DelNote();
        }
        private void cmdNew_Click(object sender, EventArgs e)
        {
            int n = TreeViewData.SelectedNode.Nodes.Count;
            TreeNode nNode = TreeViewData.SelectedNode.FirstNode;
            string nNodeName = NewNodeName(nNode, n);
            int NodeKey = AddNewNode(nNodeName, (int)TreeViewData.SelectedNode.Tag);
            nNode = new TreeNode();
            nNode.Text = nNodeName;
            nNode.Tag = NodeKey;
            nNode.ImageIndex = 1;
            nNode.SelectedImageIndex = 2;

            TreeViewData.SelectedNode.Nodes.Add(nNode);
            nNode.BeginEdit();

        }
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            TreeViewData.SelectedNode.BeginEdit();
        }
        private void TreeViewData_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode.ToString() == "Delete")
            {
                DelNote();
            }
        }
        private void DelNote()
        {
            if (cmdDel.Enabled == false) return;
            if (TreeViewData.SelectedNode != null && (int)TreeViewData.SelectedNode.Tag != 0)
            {
                if (MessageBox.Show("[" + TreeViewData.SelectedNode.Text + "] Sẽ bị xóa ? ", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DeleteNode((int)TreeViewData.SelectedNode.Tag);
                    TreeViewData.SelectedNode.Remove();
                }
            }
        }
        #endregion

        #region [ Properties]
        public bool SetCmdNew
        {
            set { cmdNew.Enabled = value; }
        }
        public bool SetCmdEdit
        {
            set{cmdEdit.Enabled = value;}
        }
        public bool SetCmdDel
        {
            set {cmdDel.Enabled = value; }
        }
        #endregion
    }
}
