namespace Log_Lookup_Tool
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnUser = new System.Windows.Forms.Button();
            this.btnEUD = new System.Windows.Forms.Button();
            this.btnExportHardwareList = new System.Windows.Forms.Button();
            this.btnExportInventoryData = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtIPResult = new System.Windows.Forms.TextBox();
            this.btnIPLookup = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnCustodyReport = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnILog = new System.Windows.Forms.Button();
            this.btnIReturn = new System.Windows.Forms.Button();
            this.btnIIssue = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnElevate = new System.Windows.Forms.Button();
            this.btnBatch = new System.Windows.Forms.Button();
            this.eUDLoggingDataSet = new Log_Lookup_Tool.EUDLoggingDataSet();
            this.vwHardwareListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vwHardwareListTableAdapter = new Log_Lookup_Tool.EUDLoggingDataSetTableAdapters.vwHardwareListTableAdapter();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwHardwareListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUser
            // 
            this.btnUser.Image = global::Log_Lookup_Tool.Properties.Resources.User_16x;
            this.btnUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUser.Location = new System.Drawing.Point(102, 6);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(140, 56);
            this.btnUser.TabIndex = 0;
            this.btnUser.Text = "User Lookup";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnEUD
            // 
            this.btnEUD.Image = global::Log_Lookup_Tool.Properties.Resources.Computer_16x;
            this.btnEUD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEUD.Location = new System.Drawing.Point(102, 145);
            this.btnEUD.Name = "btnEUD";
            this.btnEUD.Size = new System.Drawing.Size(140, 56);
            this.btnEUD.TabIndex = 1;
            this.btnEUD.Text = "EUD Lookup";
            this.btnEUD.UseVisualStyleBackColor = true;
            this.btnEUD.Click += new System.EventHandler(this.btnEUD_Click);
            // 
            // btnExportHardwareList
            // 
            this.btnExportHardwareList.Location = new System.Drawing.Point(96, 145);
            this.btnExportHardwareList.Name = "btnExportHardwareList";
            this.btnExportHardwareList.Size = new System.Drawing.Size(140, 56);
            this.btnExportHardwareList.TabIndex = 2;
            this.btnExportHardwareList.Text = "Export Full Hardware List";
            this.btnExportHardwareList.UseVisualStyleBackColor = true;
            this.btnExportHardwareList.Click += new System.EventHandler(this.btnExportHardwareList_Click);
            // 
            // btnExportInventoryData
            // 
            this.btnExportInventoryData.Location = new System.Drawing.Point(96, 6);
            this.btnExportInventoryData.Name = "btnExportInventoryData";
            this.btnExportInventoryData.Size = new System.Drawing.Size(140, 56);
            this.btnExportInventoryData.TabIndex = 3;
            this.btnExportInventoryData.Text = "Export Inventory Data";
            this.btnExportInventoryData.UseVisualStyleBackColor = true;
            this.btnExportInventoryData.Click += new System.EventHandler(this.btnExportInventoryData_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(349, 234);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnUser);
            this.tabPage1.Controls.Add(this.btnEUD);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(341, 207);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnExportInventoryData);
            this.tabPage2.Controls.Add(this.btnExportHardwareList);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(341, 207);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Exports";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtIPResult);
            this.tabPage3.Controls.Add(this.btnIPLookup);
            this.tabPage3.Controls.Add(this.txtIP);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(341, 207);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tools";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtIPResult
            // 
            this.txtIPResult.Location = new System.Drawing.Point(110, 31);
            this.txtIPResult.Multiline = true;
            this.txtIPResult.Name = "txtIPResult";
            this.txtIPResult.Size = new System.Drawing.Size(195, 58);
            this.txtIPResult.TabIndex = 3;
            // 
            // btnIPLookup
            // 
            this.btnIPLookup.Image = global::Log_Lookup_Tool.Properties.Settings.Default.SearchProperty_16x;
            this.btnIPLookup.Location = new System.Drawing.Point(273, 2);
            this.btnIPLookup.Name = "btnIPLookup";
            this.btnIPLookup.Size = new System.Drawing.Size(32, 23);
            this.btnIPLookup.TabIndex = 2;
            this.btnIPLookup.UseVisualStyleBackColor = true;
            this.btnIPLookup.Click += new System.EventHandler(this.btnIPLookup_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(110, 4);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(157, 20);
            this.txtIP.TabIndex = 1;
            this.txtIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIP_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP to VLAN Lookup";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnCustodyReport);
            this.tabPage4.Controls.Add(this.btnReturn);
            this.tabPage4.Controls.Add(this.btnAccept);
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(341, 207);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "EUD Custody";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnCustodyReport
            // 
            this.btnCustodyReport.Location = new System.Drawing.Point(114, 154);
            this.btnCustodyReport.Name = "btnCustodyReport";
            this.btnCustodyReport.Size = new System.Drawing.Size(99, 47);
            this.btnCustodyReport.TabIndex = 2;
            this.btnCustodyReport.Text = "EUDs In CSC Custody";
            this.btnCustodyReport.UseVisualStyleBackColor = true;
            this.btnCustodyReport.Click += new System.EventHandler(this.btnCustodyReport_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(114, 80);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(99, 47);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "EUD Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(114, 6);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(99, 47);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "EUD Acceptance";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnBatch);
            this.tabPage5.Controls.Add(this.btnILog);
            this.tabPage5.Controls.Add(this.btnIReturn);
            this.tabPage5.Controls.Add(this.btnIIssue);
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(341, 207);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "iAccess";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnILog
            // 
            this.btnILog.Location = new System.Drawing.Point(6, 154);
            this.btnILog.Name = "btnILog";
            this.btnILog.Size = new System.Drawing.Size(99, 47);
            this.btnILog.TabIndex = 2;
            this.btnILog.Text = "View iAccess Log";
            this.btnILog.UseVisualStyleBackColor = true;
            this.btnILog.Visible = false;
            // 
            // btnIReturn
            // 
            this.btnIReturn.Location = new System.Drawing.Point(236, 6);
            this.btnIReturn.Name = "btnIReturn";
            this.btnIReturn.Size = new System.Drawing.Size(99, 47);
            this.btnIReturn.TabIndex = 1;
            this.btnIReturn.Text = "iAccess Return";
            this.btnIReturn.UseVisualStyleBackColor = true;
            this.btnIReturn.Click += new System.EventHandler(this.btnIReturn_Click);
            // 
            // btnIIssue
            // 
            this.btnIIssue.Location = new System.Drawing.Point(6, 6);
            this.btnIIssue.Name = "btnIIssue";
            this.btnIIssue.Size = new System.Drawing.Size(99, 47);
            this.btnIIssue.TabIndex = 0;
            this.btnIIssue.Text = "iAccess Issue";
            this.btnIIssue.UseVisualStyleBackColor = true;
            this.btnIIssue.Click += new System.EventHandler(this.btnIIssue_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "LogProvider_16x.png");
            this.imageList1.Images.SetKeyName(1, "Report_16x.png");
            this.imageList1.Images.SetKeyName(2, "Toolbox_16x.png");
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnElevate
            // 
            this.btnElevate.Image = global::Log_Lookup_Tool.Properties.Settings.Default.AccountGroup_16x;
            this.btnElevate.Location = new System.Drawing.Point(333, 7);
            this.btnElevate.Name = "btnElevate";
            this.btnElevate.Size = new System.Drawing.Size(28, 23);
            this.btnElevate.TabIndex = 5;
            this.btnElevate.UseVisualStyleBackColor = true;
            this.btnElevate.Click += new System.EventHandler(this.btnElevate_Click);
            // 
            // btnBatch
            // 
            this.btnBatch.Location = new System.Drawing.Point(236, 154);
            this.btnBatch.Name = "btnBatch";
            this.btnBatch.Size = new System.Drawing.Size(99, 47);
            this.btnBatch.TabIndex = 3;
            this.btnBatch.Text = "New Batch";
            this.btnBatch.UseVisualStyleBackColor = true;
            this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
            // 
            // eUDLoggingDataSet
            // 
            this.eUDLoggingDataSet.DataSetName = "EUDLoggingDataSet";
            this.eUDLoggingDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // vwHardwareListBindingSource
            // 
            this.vwHardwareListBindingSource.DataMember = "vwHardwareList";
            this.vwHardwareListBindingSource.DataSource = this.eUDLoggingDataSet;
            // 
            // vwHardwareListTableAdapter
            // 
            this.vwHardwareListTableAdapter.ClearBeforeFill = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 258);
            this.Controls.Add(this.btnElevate);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Log Lookup Tool";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eUDLoggingDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwHardwareListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnEUD;
        private System.Windows.Forms.Button btnExportHardwareList;
        private EUDLoggingDataSet eUDLoggingDataSet;
        private System.Windows.Forms.BindingSource vwHardwareListBindingSource;
        private EUDLoggingDataSetTableAdapters.vwHardwareListTableAdapter vwHardwareListTableAdapter;
        private System.Windows.Forms.Button btnExportInventoryData;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button btnIPLookup;
		private System.Windows.Forms.TextBox txtIP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.TextBox txtIPResult;
		private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCustodyReport;
        private System.Windows.Forms.Button btnElevate;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnILog;
        private System.Windows.Forms.Button btnIReturn;
        private System.Windows.Forms.Button btnIIssue;
        private System.Windows.Forms.Button btnBatch;
    }
}

