namespace LPsolveSBMLUI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.controlSetup1 = new LPsolveSBMLUI.ControlSetup();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.Flux = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.copyResultsToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generateLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
      this.toolImportFameConstraints = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sBWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.printDialog1 = new System.Windows.Forms.PrintDialog();
      this.printDocument1 = new System.Drawing.Printing.PrintDocument();
      this.saveDialog = new System.Windows.Forms.SaveFileDialog();
      this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.lblResult = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
      this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolExportL2 = new System.Windows.Forms.ToolStripButton();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.mainMenu.SuspendLayout();
      this.toolStripContainer2.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer2.ContentPanel.SuspendLayout();
      this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer2.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.controlSetup1);
      this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
      this.splitContainer1.Size = new System.Drawing.Size(854, 368);
      this.splitContainer1.SplitterDistance = 289;
      this.splitContainer1.TabIndex = 0;
      // 
      // controlSetup1
      // 
      this.controlSetup1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.controlSetup1.Location = new System.Drawing.Point(5, 5);
      this.controlSetup1.Maximize = true;
      this.controlSetup1.Minimize = false;
      this.controlSetup1.Name = "controlSetup1";
      this.controlSetup1.Size = new System.Drawing.Size(279, 358);
      this.controlSetup1.TabIndex = 0;
      this.controlSetup1.LoadClicked += new System.EventHandler(this.OnLoadClicked);
      this.controlSetup1.LayoutClicked += new System.EventHandler(this.OnGenerateLayoutClicked);
      this.controlSetup1.RunClicked += new System.EventHandler(this.OnRunClicked);
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(5, 5);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
      this.splitContainer2.Size = new System.Drawing.Size(551, 358);
      this.splitContainer2.SplitterDistance = 270;
      this.splitContainer2.TabIndex = 2;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.Color.White;
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(551, 270);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPicturePaint);
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Flux,
            this.Value});
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(0, 0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new System.Drawing.Size(551, 84);
      this.dataGridView1.TabIndex = 1;
      // 
      // Flux
      // 
      this.Flux.HeaderText = "Flux";
      this.Flux.Name = "Flux";
      this.Flux.ReadOnly = true;
      // 
      // Value
      // 
      this.Value.HeaderText = "Value";
      this.Value.Name = "Value";
      this.Value.ReadOnly = true;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.Filter = "SBML files|*.xml;*.sbml|All files|*.*";
      // 
      // mainMenu
      // 
      this.mainMenu.Dock = System.Windows.Forms.DockStyle.None;
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sBWToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(854, 24);
      this.mainMenu.TabIndex = 1;
      this.mainMenu.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyResultsToClipboardToolStripMenuItem,
            this.generateLayoutToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripSeparator1,
            this.mnuImport,
            this.toolImportFameConstraints,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // openToolStripMenuItem
      // 
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.openToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
      this.openToolStripMenuItem.Text = "&Open";
      this.openToolStripMenuItem.Click += new System.EventHandler(this.OnLoadClicked);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
      this.saveToolStripMenuItem.Text = "&Save";
      this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveClicked);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(250, 6);
      // 
      // copyResultsToClipboardToolStripMenuItem
      // 
      this.copyResultsToClipboardToolStripMenuItem.Name = "copyResultsToClipboardToolStripMenuItem";
      this.copyResultsToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.copyResultsToClipboardToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
      this.copyResultsToClipboardToolStripMenuItem.Text = "Copy Results to Clipboard";
      this.copyResultsToClipboardToolStripMenuItem.Click += new System.EventHandler(this.OnCopyToClipboard);
      // 
      // generateLayoutToolStripMenuItem
      // 
      this.generateLayoutToolStripMenuItem.Name = "generateLayoutToolStripMenuItem";
      this.generateLayoutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.generateLayoutToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
      this.generateLayoutToolStripMenuItem.Text = "Generate Layout";
      this.generateLayoutToolStripMenuItem.Click += new System.EventHandler(this.OnGenerateLayoutClicked);
      // 
      // printToolStripMenuItem
      // 
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
      this.printToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
      this.printToolStripMenuItem.Text = "&Print";
      this.printToolStripMenuItem.Click += new System.EventHandler(this.OnPrintClicked);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(250, 6);
      // 
      // mnuImport
      // 
      this.mnuImport.Name = "mnuImport";
      this.mnuImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
      this.mnuImport.Size = new System.Drawing.Size(253, 22);
      this.mnuImport.Text = "Import";
      // 
      // toolImportFameConstraints
      // 
      this.toolImportFameConstraints.Name = "toolImportFameConstraints";
      this.toolImportFameConstraints.Size = new System.Drawing.Size(253, 22);
      this.toolImportFameConstraints.Text = "Import Constraints";
      this.toolImportFameConstraints.ToolTipText = "Replaces the constraints with ones inported from F.A.M.E";
      this.toolImportFameConstraints.Click += new System.EventHandler(this.OnImportConstraintsClicked);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(250, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExitClicked);
      // 
      // sBWToolStripMenuItem
      // 
      this.sBWToolStripMenuItem.Name = "sBWToolStripMenuItem";
      this.sBWToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
      this.sBWToolStripMenuItem.Text = "&SBW";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
      this.aboutToolStripMenuItem.Text = "&About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAboutClicked);
      // 
      // printDialog1
      // 
      this.printDialog1.Document = this.printDocument1;
      this.printDialog1.UseEXDialog = true;
      // 
      // printDocument1
      // 
      this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.OnPrintPage);
      // 
      // saveDialog
      // 
      this.saveDialog.Filter = "SBML files|*.xml;*.sbml|SBML Level 2 with FBA annotation|*.xml|SBML Level 2 with " +
    "COBRA annotation|*.xml|LP files|*.lp|All files|*.*";
      this.saveDialog.Title = "Export SBML / LP file";
      // 
      // toolStripContainer2
      // 
      // 
      // toolStripContainer2.BottomToolStripPanel
      // 
      this.toolStripContainer2.BottomToolStripPanel.Controls.Add(this.statusStrip1);
      // 
      // toolStripContainer2.ContentPanel
      // 
      this.toolStripContainer2.ContentPanel.Controls.Add(this.splitContainer1);
      this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(854, 368);
      this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
      this.toolStripContainer2.Name = "toolStripContainer2";
      this.toolStripContainer2.Size = new System.Drawing.Size(854, 439);
      this.toolStripContainer2.TabIndex = 3;
      this.toolStripContainer2.Text = "toolStripContainer2";
      // 
      // toolStripContainer2.TopToolStripPanel
      // 
      this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.mainMenu);
      this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.toolStrip1);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblResult,
            this.lblStatus});
      this.statusStrip1.Location = new System.Drawing.Point(0, 0);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(854, 22);
      this.statusStrip1.TabIndex = 0;
      // 
      // lblResult
      // 
      this.lblResult.Name = "lblResult";
      this.lblResult.Size = new System.Drawing.Size(839, 17);
      this.lblResult.Spring = true;
      this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblStatus
      // 
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(0, 17);
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator,
            this.copyToolStripButton,
            this.toolStripSeparator2,
            this.toolExportL2});
      this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.toolStrip1.Location = new System.Drawing.Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Padding = new System.Windows.Forms.Padding(12, 0, 1, 0);
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size(854, 25);
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 2;
      // 
      // openToolStripButton
      // 
      this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
      this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.openToolStripButton.Name = "openToolStripButton";
      this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.openToolStripButton.Text = "&Open";
      this.openToolStripButton.Click += new System.EventHandler(this.OnLoadClicked);
      // 
      // saveToolStripButton
      // 
      this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
      this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.saveToolStripButton.Name = "saveToolStripButton";
      this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.saveToolStripButton.Text = "&Save";
      this.saveToolStripButton.Click += new System.EventHandler(this.OnSaveClicked);
      // 
      // printToolStripButton
      // 
      this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
      this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.printToolStripButton.Name = "printToolStripButton";
      this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.printToolStripButton.Text = "&Print";
      this.printToolStripButton.Click += new System.EventHandler(this.OnPrintClicked);
      // 
      // toolStripSeparator
      // 
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
      // 
      // copyToolStripButton
      // 
      this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
      this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.copyToolStripButton.Name = "copyToolStripButton";
      this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
      this.copyToolStripButton.Text = "&Copy";
      this.copyToolStripButton.Click += new System.EventHandler(this.OnCopyToClipboard);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolExportL2
      // 
      this.toolExportL2.Checked = true;
      this.toolExportL2.CheckOnClick = true;
      this.toolExportL2.CheckState = System.Windows.Forms.CheckState.Checked;
      this.toolExportL2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.toolExportL2.Image = ((System.Drawing.Image)(resources.GetObject("toolExportL2.Image")));
      this.toolExportL2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolExportL2.Name = "toolExportL2";
      this.toolExportL2.Size = new System.Drawing.Size(59, 22);
      this.toolExportL2.Text = "Export &L2";
      this.toolExportL2.ToolTipText = "When checked, the SBML exported to SBW will be exported as SBML Level 2 file (as " +
    "compatible with JDesigner), rather than as SBML Level 3 file. ";
      // 
      // MainForm
      // 
      this.AllowDrop = true;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(854, 439);
      this.Controls.Add(this.toolStripContainer2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mainMenu;
      this.Name = "MainForm";
      this.Text = "Flux Balance";
      this.Load += new System.EventHandler(this.OnFormLoad);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.toolStripContainer2.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer2.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer2.ContentPanel.ResumeLayout(false);
      this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer2.TopToolStripPanel.PerformLayout();
      this.toolStripContainer2.ResumeLayout(false);
      this.toolStripContainer2.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ControlSetup controlSetup1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sBWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripMenuItem generateLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flux;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.ToolStripMenuItem copyResultsToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblResult;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolImportFameConstraints;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolExportL2;
    }
}

