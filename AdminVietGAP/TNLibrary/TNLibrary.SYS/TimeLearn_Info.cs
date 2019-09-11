using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TNLibrary.SYS
{
    public class TimeLearn_Info
    {
        private int m_TimeLearnKey = 0;
        private string m_TimeLearnName = "";
        private DateTime m_TimeStart;
        private DateTime m_TimeEnd;
        private string m_Color = "Orange";
        public TimeLearn_Info()
        {
        }

        public int TimeLearnKey
        {
            get { return m_TimeLearnKey; }
            set { m_TimeLearnKey = value; }
        }
        public string TimeLearnName
        {
            get { return m_TimeLearnName; }
            set { m_TimeLearnName = value; }
        }
        public DateTime TimeStart
        {
            get { return m_TimeStart; }
            set { m_TimeStart = value; }
        }
        public DateTime TimeEnd
        {
            get { return m_TimeEnd; }
            set { m_TimeEnd = value; }
        }
        public string Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

    }
}
