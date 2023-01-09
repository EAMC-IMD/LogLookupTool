﻿namespace EAMC_Log_Lookup_Tool {
    partial class frmComputerSearch {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComputerSearch));
            this.lblSearch = new System.Windows.Forms.Label();
            this.tbxSearch = new System.Windows.Forms.TextBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.btnLogs = new System.Windows.Forms.Button();
            this.btnAD = new System.Windows.Forms.Button();
            this.btnAppList = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnHardwareInfo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 9);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(76, 13);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search Criteria";
            // 
            // tbxSearch
            // 
            this.tbxSearch.Location = new System.Drawing.Point(94, 6);
            this.tbxSearch.Name = "tbxSearch";
            this.tbxSearch.Size = new System.Drawing.Size(641, 20);
            this.tbxSearch.TabIndex = 3;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToOrderColumns = true;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(15, 41);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(660, 397);
            this.dgvResults.TabIndex = 4;
            this.dgvResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellClick);
            // 
            // btnLogs
            // 
            this.btnLogs.Enabled = false;
            this.btnLogs.Location = new System.Drawing.Point(693, 153);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(95, 59);
            this.btnLogs.TabIndex = 5;
            this.btnLogs.Text = "Login Activity";
            this.btnLogs.UseVisualStyleBackColor = true;
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // btnAD
            // 
            this.btnAD.Enabled = false;
            this.btnAD.Location = new System.Drawing.Point(693, 265);
            this.btnAD.Name = "btnAD";
            this.btnAD.Size = new System.Drawing.Size(95, 59);
            this.btnAD.TabIndex = 6;
            this.btnAD.Text = "AD Info";
            this.btnAD.UseVisualStyleBackColor = true;
            this.btnAD.Click += new System.EventHandler(this.btnAD_Click);
            // 
            // btnAppList
            // 
            this.btnAppList.Enabled = false;
            this.btnAppList.Location = new System.Drawing.Point(693, 41);
            this.btnAppList.Name = "btnAppList";
            this.btnAppList.Size = new System.Drawing.Size(95, 59);
            this.btnAppList.TabIndex = 7;
            this.btnAppList.Text = "Application List";
            this.btnAppList.UseVisualStyleBackColor = true;
            this.btnAppList.Click += new System.EventHandler(this.btnAppList_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(741, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(24, 19);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnHardwareInfo
            // 
            this.btnHardwareInfo.Enabled = false;
            this.btnHardwareInfo.Location = new System.Drawing.Point(693, 377);
            this.btnHardwareInfo.Name = "btnHardwareInfo";
            this.btnHardwareInfo.Size = new System.Drawing.Size(95, 59);
            this.btnHardwareInfo.TabIndex = 9;
            this.btnHardwareInfo.Text = "EUD Hardware Info";
            this.btnHardwareInfo.UseVisualStyleBackColor = true;
            this.btnHardwareInfo.Click += new System.EventHandler(this.btnHardwareInfo_Click);
            // 
            // frmComputerSearch
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnHardwareInfo);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAppList);
            this.Controls.Add(this.btnAD);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.tbxSearch);
            this.Controls.Add(this.lblSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmComputerSearch";
            this.Text = "EUD Search";
            this.Shown += new System.EventHandler(this.frmComputerSearch_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox tbxSearch;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.Button btnAD;
        private System.Windows.Forms.Button btnAppList;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnHardwareInfo;
    }
}