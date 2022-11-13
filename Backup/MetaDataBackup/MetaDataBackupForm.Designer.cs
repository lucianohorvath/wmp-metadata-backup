namespace Preston.Media
{
    partial class MetaDataBackupForm
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lbResults = new System.Windows.Forms.ListBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.resultLabel = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChooseAttributesToBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToUseMetadataBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMetadataBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkRecursive = new System.Windows.Forms.CheckBox();
            this.lblSourceFolder = new System.Windows.Forms.Label();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDatabaseFileLocation = new System.Windows.Forms.Label();
            this.btnDatabaseBrowse = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.CheckFileExists = false;
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.FileName = "WMPMetadata";
            this.openFileDialog.Filter = "XML files|*.xml|All files|*.*";
            // 
            // btnBackup
            // 
            this.btnBackup.Enabled = false;
            this.btnBackup.Location = new System.Drawing.Point(214, 370);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 11;
            this.btnBackup.Text = "&Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lbResults
            // 
            this.lbResults.FormattingEnabled = true;
            this.lbResults.Location = new System.Drawing.Point(7, 19);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(513, 82);
            this.lbResults.TabIndex = 10;
            // 
            // btnRestore
            // 
            this.btnRestore.Enabled = false;
            this.btnRestore.Location = new System.Drawing.Point(295, 370);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 12;
            this.btnRestore.Text = "&Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(459, 370);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar);
            this.groupBox2.Controls.Add(this.resultLabel);
            this.groupBox2.Controls.Add(this.lbResults);
            this.groupBox2.Location = new System.Drawing.Point(7, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(527, 150);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 128);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(513, 15);
            this.progressBar.TabIndex = 7;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.resultLabel.Location = new System.Drawing.Point(7, 108);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(128, 13);
            this.resultLabel.TabIndex = 14;
            this.resultLabel.Text = "0 media items backed up.";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(376, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(542, 24);
            this.menuStrip.TabIndex = 24;
            this.menuStrip.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChooseAttributesToBackup});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Visible = false;
            // 
            // tsmiChooseAttributesToBackup
            // 
            this.tsmiChooseAttributesToBackup.Name = "tsmiChooseAttributesToBackup";
            this.tsmiChooseAttributesToBackup.Size = new System.Drawing.Size(233, 22);
            this.tsmiChooseAttributesToBackup.Text = "Choose Attributes to Backup";
            this.tsmiChooseAttributesToBackup.Click += new System.EventHandler(this.tsmiChooseAttributesToBackup_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToUseMetadataBackupToolStripMenuItem,
            this.aboutMetadataBackupToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // howToUseMetadataBackupToolStripMenuItem
            // 
            this.howToUseMetadataBackupToolStripMenuItem.Name = "howToUseMetadataBackupToolStripMenuItem";
            this.howToUseMetadataBackupToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.howToUseMetadataBackupToolStripMenuItem.Text = "&How to use Metadata Backup";
            this.howToUseMetadataBackupToolStripMenuItem.Click += new System.EventHandler(this.howToUseMetadataBackupToolStripMenuItem_Click);
            // 
            // aboutMetadataBackupToolStripMenuItem
            // 
            this.aboutMetadataBackupToolStripMenuItem.Name = "aboutMetadataBackupToolStripMenuItem";
            this.aboutMetadataBackupToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.aboutMetadataBackupToolStripMenuItem.Text = "&About Metadata Backup";
            this.aboutMetadataBackupToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkRecursive);
            this.groupBox4.Controls.Add(this.lblSourceFolder);
            this.groupBox4.Controls.Add(this.btnBrowseSource);
            this.groupBox4.Location = new System.Drawing.Point(7, 31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(527, 94);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Choose folder to backup or restore";
            // 
            // chkRecursive
            // 
            this.chkRecursive.AutoSize = true;
            this.chkRecursive.Location = new System.Drawing.Point(24, 69);
            this.chkRecursive.Name = "chkRecursive";
            this.chkRecursive.Size = new System.Drawing.Size(114, 17);
            this.chkRecursive.TabIndex = 25;
            this.chkRecursive.Text = "Include &subfolders";
            this.chkRecursive.UseVisualStyleBackColor = true;
            // 
            // lblSourceFolder
            // 
            this.lblSourceFolder.Location = new System.Drawing.Point(7, 19);
            this.lblSourceFolder.Name = "lblSourceFolder";
            this.lblSourceFolder.Size = new System.Drawing.Size(427, 40);
            this.lblSourceFolder.TabIndex = 21;
            this.lblSourceFolder.Text = "Browse for folder to backup or restore.";
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Location = new System.Drawing.Point(446, 14);
            this.btnBrowseSource.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSource.TabIndex = 18;
            this.btnBrowseSource.Text = "Br&owse...";
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDatabaseFileLocation);
            this.groupBox1.Controls.Add(this.btnDatabaseBrowse);
            this.groupBox1.Location = new System.Drawing.Point(7, 132);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 74);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose metadata backup file to backup to or restore from";
            // 
            // lblDatabaseFileLocation
            // 
            this.lblDatabaseFileLocation.Location = new System.Drawing.Point(7, 19);
            this.lblDatabaseFileLocation.Name = "lblDatabaseFileLocation";
            this.lblDatabaseFileLocation.Size = new System.Drawing.Size(427, 40);
            this.lblDatabaseFileLocation.TabIndex = 26;
            this.lblDatabaseFileLocation.Text = "Browse for metadata backup file location.";
            // 
            // btnDatabaseBrowse
            // 
            this.btnDatabaseBrowse.Location = new System.Drawing.Point(446, 14);
            this.btnDatabaseBrowse.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.btnDatabaseBrowse.Name = "btnDatabaseBrowse";
            this.btnDatabaseBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnDatabaseBrowse.TabIndex = 25;
            this.btnDatabaseBrowse.Text = "Bro&wse...";
            this.btnDatabaseBrowse.Click += new System.EventHandler(this.btnDatabaseBrowse_Click);
            // 
            // MetaDataBackupForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(542, 400);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MetaDataBackupForm";
            this.Text = "Metadata Backup";
            this.Load += new System.EventHandler(this.MetaDataBackupForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.ListBox lbResults;
		private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblSourceFolder;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.ToolStripMenuItem aboutMetadataBackupToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkRecursive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDatabaseFileLocation;
        private System.Windows.Forms.Button btnDatabaseBrowse;
        private System.Windows.Forms.ToolStripMenuItem howToUseMetadataBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiChooseAttributesToBackup;

    }
}