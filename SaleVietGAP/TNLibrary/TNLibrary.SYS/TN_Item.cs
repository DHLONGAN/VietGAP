using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNLibrary.SYS
{
    public class TN_Item
    {
        private string m_ID = null;
        private string m_Name = null;
        private object m_Value = null;

        public TN_Item()
        {
        }

        #region [ Properties ]

        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public object Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        #endregion
    }
}
