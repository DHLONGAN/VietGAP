namespace TNLibrary.SYS.Forms
{
    partial class TNMonthCalerdar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtToday = new System.Windows.Forms.Label();
            this.TNGridCalendar = new System.Windows.Forms.DataGridView();
            this.cmdNextMonth = new System.Windows.Forms.Label();
            this.cmdPreMonth = new System.Windows.Forms.Label();
            this.txtMonthName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TNGridCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtToday
            // 
            this.txtToday.AutoSize = true;
            this.txtToday.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToday.ForeColor = System.Drawing.Color.White;
            this.txtToday.Location = new System.Drawing.Point(31, 181);
            this.txtToday.Name = "txtToday";
            this.txtToday.Size = new System.Drawing.Size(48, 15);
            this.txtToday.TabIndex = 5;
            this.txtToday.Text = "Today : ";
            // 
            // TNGridCalendar
            // 
            this.TNGridCalendar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TNGridCalendar.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TNGridCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TNGridCalendar.Location = new System.Drawing.Point(3, 20);
            this.TNGridCalendar.Name = "TNGridCalendar";
            this.TNGridCalendar.Size = new System.Drawing.Size(212, 155);
            this.TNGridCalendar.TabIndex = 0;
            // 
            // cmdNextMonth
            // 
            this.cmdNextMonth.BackColor = System.Drawing.Color.White;
            this.cmdNextMonth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNextMonth.ForeColor = System.Drawing.Color.Navy;
            this.cmdNextMonth.Location = new System.Drawing.Point(196, 3);
            this.cmdNextMonth.Name = "cmdNextMonth";
            this.cmdNextMonth.Size = new System.Drawing.Size(19, 16);
            this.cmdNextMonth.TabIndex = 3;
            this.cmdNextMonth.Text = ">";
            this.cmdNextMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdPreMonth
            // 
            this.cmdPreMonth.BackColor = System.Drawing.Color.White;
            this.cmdPreMonth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPreMonth.ForeColor = System.Drawing.Color.Navy;
            this.cmdPreMonth.Location = new System.Drawing.Point(3, 3);
            this.cmdPreMonth.Name = "cmdPreMonth";
            this.cmdPreMonth.Size = new System.Drawing.Size(19, 16);
            this.cmdPreMonth.TabIndex = 4;
            this.cmdPreMonth.Text = "<";
            this.cmdPreMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMonthName
            // 
            this.txtMonthName.BackColor = System.Drawing.Color.White;
            this.txtMonthName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonthName.ForeColor = System.Drawing.Color.Black;
            this.txtMonthName.Location = new System.Drawing.Point(22, 3);
            this.txtMonthName.Name = "txtMonthName";
            this.txtMonthName.Size = new System.Drawing.Size(177, 16);
            this.txtMonthName.TabIndex = 2;
            this.txtMonthName.Text = "JUNE";
            this.txtMonthName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TNMonthCalerdar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.Controls.Add(this.txtToday);
            this.Controls.Add(this.TNGridCalendar);
            this.Controls.Add(this.cmdNextMonth);
            this.Controls.Add(this.txtMonthName);
            this.Controls.Add(this.cmdPreMonth);
            this.Name = "TNMonthCalerdar";
            this.Size = new System.Drawing.Size(218, 206);
            this.Load += new System.EventHandler(this.TNMonthCalerdar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TNGridCalendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtToday;
        private System.Windows.Forms.DataGridView TNGridCalendar;
        private System.Windows.Forms.Label cmdNextMonth;
        private System.Windows.Forms.Label cmdPreMonth;
        private System.Windows.Forms.Label txtMonthName;
    }
}
