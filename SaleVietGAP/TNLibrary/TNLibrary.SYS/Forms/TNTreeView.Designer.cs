namespace TNLibrary.SYS.Forms
{
    partial class TNTreeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TNTreeView));
            this.TreeViewData = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdNew = new System.Windows.Forms.ToolStripButton();
            this.cmdDel = new System.Windows.Forms.ToolStripButton();
            this.cmdEdit = new System.Windows.Forms.ToolStripButton();
            this.ImgTreeView = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeViewData
            // 
            this.TreeViewData.AllowDrop = true;
            this.TreeViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeViewData.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeViewData.ForeColor = System.Drawing.Color.Navy;
            this.TreeViewData.FullRowSelect = true;
            this.TreeViewData.ItemHeight = 25;
            this.TreeViewData.LabelEdit = true;
            this.TreeViewData.Location = new System.Drawing.Point(0, 39);
            this.TreeViewData.Name = "TreeViewData";
            this.TreeViewData.Size = new System.Drawing.Size(282, 262);
            this.TreeViewData.TabIndex = 3;
            this.TreeViewData.DragLeave += new System.EventHandler(this.TreeViewData_DragLeave);
            this.TreeViewData.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeViewData_AfterLabelEdit);
            this.TreeViewData.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeViewData_DragDrop);
            this.TreeViewData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewData_AfterSelect);
            this.TreeViewData.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeViewData_DragEnter);
            this.TreeViewData.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeViewData_BeforeLabelEdit);
            this.TreeViewData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeViewData_KeyDown);
            this.TreeViewData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeViewData_ItemDrag);
            this.TreeViewData.DragOver += new System.Windows.Forms.DragEventHandler(this.TreeViewData_DragOver);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdNew,
            this.cmdDel,
            this.cmdEdit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(282, 39);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdNew
            // 
            this.cmdNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdNew.Image = ((System.Drawing.Image)(resources.GetObject("cmdNew.Image")));
            this.cmdNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(36, 36);
            this.cmdNew.Text = "Tạo mới danh mục";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // cmdDel
            // 
            this.cmdDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDel.Image = ((System.Drawing.Image)(resources.GetObject("cmdDel.Image")));
            this.cmdDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.Size = new System.Drawing.Size(36, 36);
            this.cmdDel.Text = "Xóa danh mục";
            this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(36, 36);
            this.cmdEdit.Text = "Thay đổi danh mục";
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // ImgTreeView
            // 
            this.ImgTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgTreeView.ImageStream")));
            this.ImgTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.ImgTreeView.Images.SetKeyName(0, "Icon_TreeViewRoot1.png");
            this.ImgTreeView.Images.SetKeyName(1, "forder20x25.png");
            this.ImgTreeView.Images.SetKeyName(2, "ForderSelect.png");
            // 
            // TNTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TreeViewData);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TNTreeView";
            this.Size = new System.Drawing.Size(282, 301);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView TreeViewData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdNew;
        private System.Windows.Forms.ToolStripButton cmdDel;
        private System.Windows.Forms.ImageList ImgTreeView;
        private System.Windows.Forms.ToolStripButton cmdEdit;
    }
}
