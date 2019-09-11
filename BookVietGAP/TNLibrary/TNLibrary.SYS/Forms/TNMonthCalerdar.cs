using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TNLibrary.SYS.Forms
{
    public partial class TNMonthCalerdar : UserControl
    {
        public event EventHandler CellClick;

        private Color mColorCurrentMonth = Color.Navy;
        private Color mColorOtherMonth = Color.Gray;
        private int mSelectedRowIndex = 0;
        private DateTime mSelectionStart = DateTime.Now;
        private DateTime mSelectionEnd = DateTime.Now;
        private bool mIsSelectedMutilDate;
        public TNMonthCalerdar()
        {
            InitializeComponent();
            SetupLayoutCalendar(TNGridCalendar);

        }
        private void TNMonthCalerdar_Load(object sender, EventArgs e)
        {
            DateTime nBeginDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01);
            FillMonth(nBeginDay);

        }

        #region [Calendar]

        private void SetupLayoutCalendar(DataGridView GV)
        {
            // Setup Column 
            GV.Columns.Clear();
            // Setup Column 
            GV.Columns.Add("Monday", "Mon");
            GV.Columns.Add("Tuesday", "Tue");
            GV.Columns.Add("Wednesday", "Web");
            GV.Columns.Add("Thursday", "Thu");
            GV.Columns.Add("Friday", "Fri");
            GV.Columns.Add("Saturday", "Sat");
            GV.Columns.Add("Sunday", "Sun");

            for (int i = 0; i < 7; i++)
            {
                GV.Columns[i].Width = 30;
                GV.Columns[i].ReadOnly = true;
                GV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            // setup style view
            GV.ScrollBars = ScrollBars.None;
            GV.BackgroundColor = Color.White;
            GV.MultiSelect = true;
            GV.AllowUserToResizeRows = false;

            GV.GridColor = Color.FromArgb(227, 239, 255);
            GV.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            GV.DefaultCellStyle.ForeColor = Color.Navy;
            GV.DefaultCellStyle.Font = new Font("Arial", 9);
            //     GV.DefaultCellStyle.SelectionBackColor = Color.Red;

            GV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            GV.RowHeadersVisible = false;
            GV.AllowUserToAddRows = false;
            GV.AllowUserToDeleteRows = false;


            // setup Height Header
            GV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            GV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            // GV.Dock = DockStyle.Fill;
            //Activate On Gridview
            this.TNGridCalendar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TNGridCalendar_CellDoubleClick);
            this.TNGridCalendar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TNGridCalendar_CellClick);

            this.cmdNextMonth.MouseLeave += new System.EventHandler(this.cmdNextMonth_MouseLeave);
            this.cmdNextMonth.Click += new System.EventHandler(this.cmdNextMonth_Click);
            this.cmdNextMonth.MouseHover += new System.EventHandler(this.cmdNextMonth_MouseHover);

            this.cmdPreMonth.MouseLeave += new System.EventHandler(this.cmdPreMonth_MouseLeave);
            this.cmdPreMonth.Click += new System.EventHandler(this.cmdPreMonth_Click);
            this.cmdPreMonth.MouseHover += new System.EventHandler(this.cmdPreMonth_MouseHover);

            GV.Rows.Add(6);
            txtToday.Text = "Today : " + DateTime.Now.ToString("dd/MM/yyyy");

        }
        public void FillMonth(DateTime nDateStart)
        {

            txtMonthName.Text = nDateStart.ToString("MMMM , yyyy");
            TNGridCalendar.Tag = nDateStart;

            int nDayOfWeek = Day_Of_Week(nDateStart.DayOfWeek.ToString())-1;
            //Previous Month
            nDateStart = nDateStart.AddDays(-nDayOfWeek);
            for (int i = 0; i < nDayOfWeek ; i++)
            {
                TNGridCalendar.Rows[0].Cells[i].Value = nDateStart.Day.ToString();
                TNGridCalendar.Rows[0].Cells[i].Style.ForeColor = mColorOtherMonth;
                TNGridCalendar.Rows[0].Cells[i].Tag = nDateStart;

                nDateStart = nDateStart.AddDays(1);

            }
            if (nDayOfWeek == 0)
                nDayOfWeek = 1;
            for (int i = nDayOfWeek ; i < 7; i++)
            {
                TNGridCalendar.Rows[0].Cells[i].Value = nDateStart.Day.ToString();
                TNGridCalendar.Rows[0].Cells[i].Style.ForeColor = mColorCurrentMonth;
                TNGridCalendar.Rows[0].Cells[i].Tag = nDateStart;

                nDateStart = nDateStart.AddDays(1);
            }
            Color nColor = mColorCurrentMonth;
            for (int i = 1; i < 6; i++)
            {

                for (int j = 0; j < 7; j++)
                {
                    TNGridCalendar.Rows[i].Cells[j].Value = nDateStart.Day.ToString();
                    TNGridCalendar.Rows[i].Cells[j].Tag = nDateStart;

                    if (nDateStart.Day == 1)
                        nColor = mColorOtherMonth;

                    TNGridCalendar.Rows[i].Cells[j].Style.ForeColor = nColor;

                    nDateStart = nDateStart.AddDays(1);
                }

            }
            //Next Month

        }

        // [ Action ]
        private void TNGridCalendar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

        }
        private void TNGridCalendar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mIsSelectedMutilDate)
            {
                for (int i = 0; i < 7; i++)
                {
                    TNGridCalendar.Rows[mSelectedRowIndex].Cells[i].Style.BackColor = Color.White;
                }
                for (int i = 0; i < 7; i++)
                {
                    TNGridCalendar.Rows[e.RowIndex].Cells[i].Style.BackColor = Color.Orange;
                    mSelectedRowIndex = e.RowIndex;
                }
                mSelectionStart = (DateTime)TNGridCalendar.Rows[e.RowIndex].Cells[0].Tag;
                mSelectionEnd = (DateTime)TNGridCalendar.Rows[e.RowIndex].Cells[6].Tag;
            }
            if (this.CellClick != null)
                this.CellClick(this, e);
        }

        private void cmdNextMonth_Click(object sender, EventArgs e)
        {
            DateTime nDateStart = (DateTime)TNGridCalendar.Tag;
            nDateStart = nDateStart.AddMonths(1);
            FillMonth(nDateStart);
        }
        private void cmdNextMonth_MouseHover(object sender, EventArgs e)
        {
            cmdNextMonth.BorderStyle = BorderStyle.FixedSingle;
        }
        private void cmdNextMonth_MouseLeave(object sender, EventArgs e)
        {
            cmdNextMonth.BorderStyle = BorderStyle.None;
        }

        private void cmdPreMonth_Click(object sender, EventArgs e)
        {
            DateTime nDateStart = (DateTime)TNGridCalendar.Tag;
            nDateStart = nDateStart.AddMonths(-1);
            FillMonth(nDateStart);
        }
        private void cmdPreMonth_MouseHover(object sender, EventArgs e)
        {
            cmdPreMonth.BorderStyle = BorderStyle.FixedSingle;
        }
        private void cmdPreMonth_MouseLeave(object sender, EventArgs e)
        {
            cmdPreMonth.BorderStyle = BorderStyle.None;
        }

        // Function
        private int Day_Of_Week(string strDayOfWeek)
        {
            int nDay = 0;
            switch (strDayOfWeek)
            {
                case "Monday":
                    nDay = 1;
                    break;
                case "Tuesday":
                    nDay = 2;
                    break;
                case "Wednesday":
                    nDay = 3;
                    break;
                case "Thursday":
                    nDay = 4;
                    break;
                case "Friday":
                    nDay = 5;
                    break;
                case "Saturday":
                    nDay = 6;
                    break;
                case "Sunday":
                    nDay = 7;
                    break;
            }
            return nDay;
        }
        private void SelectedToday()
        {
            for (int i = 0; i < 5; i++)
            {
                bool nIsToday = false ;
                for (int j = 0; j < 7; j++)
                {
                    DateTime nValueCheck = (DateTime)TNGridCalendar.Rows[i].Cells[j].Tag ;

                    if (nValueCheck.Year == DateTime.Now.Year && nValueCheck.Month == DateTime.Now.Month && nValueCheck.Day == DateTime.Now.Day)
                    {
                        nIsToday = true;
                        break;
                    }
                }
                if (nIsToday)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        TNGridCalendar.Rows[i].Cells[k].Style.BackColor = Color.Orange;
                        mSelectedRowIndex = i;
                    }
                    mSelectionStart = (DateTime)TNGridCalendar.Rows[i].Cells[0].Tag;
                    mSelectionEnd = (DateTime)TNGridCalendar.Rows[i].Cells[6].Tag;
                }
            }
        }
        #endregion

        public Color ColorCurrentMonth
        {
            set { mColorCurrentMonth = value; }
        }
        public Color ColorOtherMonth
        {
            set { mColorOtherMonth = value; }
        }
        public bool IsSelectedMutilDate
        {
            set { mIsSelectedMutilDate = value; SelectedToday(); }
        }
        public DateTime SelectionStart
        {
            get { return mSelectionStart; }
        }
        public DateTime SelectionEnd
        {
            get { return mSelectionEnd; }
        }
    }


}
