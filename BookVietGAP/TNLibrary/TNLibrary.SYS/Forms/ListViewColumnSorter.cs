using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TNLibrary.SYS.Forms
{
    public class ListViewColumnSorter: IComparer
    {

            private int column;
            private bool numeric = false;
            private SortOrder order;

            public int Column
            {
                get { return column; }
                set { column = value; }
            }

            public bool Numeric
            {
                get { return numeric; }
                set { numeric = value; }
            }

            public SortOrder Order
            {
                get { return order; }
                set { order = value; }
            }

            // Stores the clumn order.
            public ListViewColumnSorter(int columnIndex)
            {
                Column = columnIndex;
            }


            // Compares the groups by header value, using the saved sort
            // order to return the correct value.
            public int Compare(object x, object y)
            {
                int result;
                ListViewItem ListX = (ListViewItem)x;
                ListViewItem ListY = (ListViewItem)y;

                if (numeric)
                {
                    decimal listXVal, listYVal;
                    try
                    {
                        listXVal = decimal.Parse(ListX.SubItems[Column].Text);
                    }
                    catch
                    {
                        listXVal = 0;

                    }

                    try
                    {
                        listYVal = decimal.Parse(ListY.SubItems[Column].Text);
                    }
                    catch
                    {
                        listYVal = 0;
                    }

                    //MessageBox.Show (" X : " + listXVal.ToString () + " Y : " + listYVal.ToString ());
                    result = decimal.Compare(listXVal, listYVal);
                }
                else
                {
                    string listXText = ListX.SubItems[Column].Text;
                    string listYText = ListY.SubItems[Column].Text;

                    result = string.Compare(listXText, listYText);
                }

                if (order == SortOrder.Ascending)
                {
                    return result;
                }
                else
                {
                    return -result;
                }
            }
        }
}
