using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace TNLibrary.SYS.Forms
{
    public class ListViewMyGroup
    {
        private ListView mLV;
        private Hashtable[] groupTables;
       

        public ListViewMyGroup(ListView LV)
        {
            mLV = LV;
        }
        public void InitializeGroup()
        {

            groupTables = new Hashtable[mLV.Columns.Count];
            for (int column = 0; column < mLV.Columns.Count; column++)
            {
                // Create a hash table containing all the groups 
                // needed for a single column.
                groupTables[column] = CreateGroupsTable(column);
            }

            // Start with the groups created for the Title column.
            SetGroups(0);

        }
        public void SetGroups(int column)
        {
            // Remove the current groups.
            mLV.Groups.Clear();

            // Retrieve the hash table corresponding to the column.
            Hashtable groups = (Hashtable)groupTables[column];

            // Copy the groups for the column to an array.
            ListViewGroup[] groupsArray = new ListViewGroup[groups.Count];
            groups.Values.CopyTo(groupsArray, 0);

            // Sort the groups and add them to mLV.
            ListViewGroupSorter nSorter;
            nSorter = new ListViewGroupSorter(mLV.Sorting);

            Array.Sort(groupsArray, nSorter);
            
            mLV.Groups.AddRange(groupsArray);
            
           
            // Iterate through the items in mLV, assigning each 
            // one to the appropriate group.
            foreach (ListViewItem item in mLV.Items)
            {
                // Retrieve the subitem text corresponding to the column.
                string subItemText = item.SubItems[column].Text;
                
                // For the Title column, use only the first letter.
                if (column == 0 && subItemText.Length >1)
                {
                    subItemText = subItemText.Substring(0, 1);
                }

                // Assign the item to the matching group.
                item.Group = (ListViewGroup)groups[subItemText];
            }
            
        }
        private Hashtable CreateGroupsTable(int column)
        {
            // Create a Hashtable object.
            Hashtable groups = new Hashtable();

            // Iterate through the items in mLV.
            foreach (ListViewItem item in mLV.Items)
            {
                // Retrieve the text value for the column.
                string subItemText = item.SubItems[column].Text;

                // Use the initial letter instead if it is the first column.
                if (column == 0 && subItemText.Length>1)
                {
                    subItemText = subItemText.Substring(0, 1);
                }

                // If the groups table does not already contain a group
                // for the subItemText value, add a new group using the 
                // subItemText value for the group header and Hashtable key.
                if (!groups.Contains(subItemText))
                {
                    groups.Add(subItemText, new ListViewGroup(subItemText,
                        HorizontalAlignment.Left));
                }
            }

            // Return the Hashtable object.
            return groups;
        }
    }
    
   

}
