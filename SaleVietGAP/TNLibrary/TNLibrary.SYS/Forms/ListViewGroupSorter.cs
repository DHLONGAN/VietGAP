using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TNLibrary.SYS.Forms
{
    public class ListViewGroupSorter : IComparer
    {
            private SortOrder order;
            private bool numeric = false;

            public bool Numeric
            {
                get { return numeric; }
                set { numeric = value; }
            }

            // Stores the sort order.
            public ListViewGroupSorter(SortOrder theOrder)
            {
                order = theOrder;
            }

            // Compares the groups by header value, using the saved sort
            // order to return the correct value.
            public int Compare(object x, object y)
            {
                int result;

                if (numeric)
                {
                    decimal listXVal, listYVal;
                    try
                    {
                        listXVal = decimal.Parse(((ListViewGroup)x).Header);
                    }
                    catch
                    {
                        listXVal = 0;

                    }

                    try
                    {
                        listYVal = decimal.Parse(((ListViewGroup)y).Header);
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
                    result = String.Compare(
                        ((ListViewGroup)x).Header,
                        ((ListViewGroup)y).Header
                    );
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
