using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace TNLibrary.SYS.Forms
{
    public partial class TNDateTimePicker : UserControl
    {
        private DateTime m_DateTime = DateTime.MinValue;
        private string m_Format = "dd/MM/yyyy";
        public TNDateTimePicker()
        {
            InitializeComponent();
            
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Text = dateTimePicker1.Value.ToString(m_Format);
            m_DateTime = dateTimePicker1.Value; 
        }

        #region [Methor]
        public DateTime Value
        {
            set
            {
                DateTime nDate = new DateTime();
                m_DateTime = value;
                if (value == DateTime.MinValue || value == nDate )
                    maskedTextBox1.Text = "";
                else
                    maskedTextBox1.Text = m_DateTime.ToString(m_Format);
            }
            get
            {
                return m_DateTime;
            }
        }
        public override string Text
        {
            set
            {
                maskedTextBox1.Text = value;
                if (value.Length == 0)
                    Value = DateTime.MinValue;
                else
                    UpdateValue();
            }
            get
            {
                return maskedTextBox1.Text;
            }
        }
        public new void Focus()
        {
           maskedTextBox1.Focus();
        }
        public string CustomFormat
        {
            set
            {
                m_Format = value;
                string nMask = m_Format;
                nMask = nMask.Replace('d', '0');
                nMask = nMask.Replace('M', '0');
                nMask = nMask.Replace('y', '0');
                maskedTextBox1.Mask = nMask;
            }
            get
            {
                return m_Format;
            }
        }
        #endregion

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            UpdateValue();
        }
        private void UpdateValue()
        {
            string nStrDate = maskedTextBox1.Text;
            if (nStrDate.Trim() == "/  /")
                m_DateTime = DateTime.MinValue;
            else
            {
                CultureInfo nCultureInfo;
                if (m_Format == "MM/dd/yyyy")
                    nCultureInfo = new CultureInfo("en-US");
                else
                    nCultureInfo = new CultureInfo("es-ES");

                DateTime nDateTime;
                if (!DateTime.TryParse(nStrDate, nCultureInfo, DateTimeStyles.None, out nDateTime))
                {
                    MessageBox.Show("Nhập sai định dạng ngày [" + m_Format + "]");
                    maskedTextBox1.Focus();
                }
                else
                    m_DateTime = nDateTime;
            }
        }

        private void TNDateTimePicker_SizeChanged1(object sender, EventArgs e)
        {
            dateTimePicker1.Left = this.Width - dateTimePicker1.Width;
            dateTimePicker1.Width = this.Width;
        }

        private void dateTimePicker1_Resize(object sender, EventArgs e)
        {
            maskedTextBox1.Width = dateTimePicker1.Width - 37;
        }
    }
}
